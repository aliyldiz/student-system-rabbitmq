using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Reciver;

channel.BasicConsume(queue: "student",
    autoAck: true,
    consumer: consumer);

Console.ReadLine();

void Reciver(object model, BasicDeliverEventArgs eventArgs)
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received message: {message}");
}
