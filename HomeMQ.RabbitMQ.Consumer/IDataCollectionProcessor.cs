namespace HomeMQ.RabbitMQ.Consumers
{
    public interface IDataCollectionProcessor : IRabbitProcessor
    {
        void Process(DataSaveMessage data);
    }
}