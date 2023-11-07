namespace ProductAPI.RabbitMQ
{
    // http://localhost:15672  login Dashboard RabbitMQ
    public interface IRabbitMQProducer
    {
        //For Send List
        public void SendProductMessage<T>(T message);

        //For Send 1 varriable
        public void SendProductMessageIsCorrect(string message);
    }
}
