using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace StudentSystem.RabbitMQ;

public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly IConfiguration _configuration;
        
    public RabbitMQProducer(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void SendMessage<T>(T message)
    {
        var connectionHost = _configuration.GetSection("RabbitMQConfiguration:HostName").Value;
        
        var factory = new ConnectionFactory
        {
            HostName = connectionHost
        };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "student",
            exclusive: false,
            autoDelete: false);
        
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "",
            routingKey: "student",
            body: body);
    }
}