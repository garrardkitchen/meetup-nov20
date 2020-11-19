namespace dotnet_demo.Controllers
{
    public interface IConsumer<T>
    {
        string Id { get; set; }
        string Type { get; set; }
        string Source { get; set; }
        T Data { get; set; }
        string Topic { get; set; }
        string PubSubName { get; set; }
    }
}