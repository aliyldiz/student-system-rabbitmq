namespace StudentSystem.RabbitMQ;

public interface IRabbitMQProducer
{
    public void SendMessage<T>(T message);
}