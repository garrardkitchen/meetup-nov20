using System;

namespace dotnet_demo.Controllers
{
    [Serializable]
    public class Consumer<T> : IConsumer<T>
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public T Data { get; set; }
        public string Topic { get; set; }
        public string PubSubName { get; set; }

        public Consumer()
        {
        }
    }
}