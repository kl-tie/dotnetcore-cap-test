using Confluent.Kafka;
using System.Text;
using System.Text.Json;

var config = new ProducerConfig { BootstrapServers = "localhost:19092" };

using var p = new ProducerBuilder<string, string>(config).Build();

Console.WriteLine("Enter q to exit. Press any key to produce a message");
var input = Console.ReadLine();

while (input != "q")
{
    try
    {
        var id = Guid.NewGuid();
        var data = new Transaction
        {
            Id = id
        };
        var json = JsonSerializer.Serialize(data);

        var headers = new Headers
        {
            //{ "cap-msg-group", Encoding.UTF8.GetBytes("test.app") },
            { "cap-msg-id", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()) },
            { "cap-msg-name", Encoding.UTF8.GetBytes("transaction") }
        };

        await p.ProduceAsync("transaction", new Message<string, string>
        {
            Key = id.ToString(),
            Headers = headers,
            Value = json,
        });

        Console.WriteLine("Data was sent.");
    }
    catch (ProduceException<Null, string> e)
    {
        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
    }

    input = Console.ReadLine();
}

record Transaction
{
    public Guid Id { get; set; }
}