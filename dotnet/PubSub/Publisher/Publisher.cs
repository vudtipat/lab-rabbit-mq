using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "dev", Password = "P@ssw0rd" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

/*
ExchangeType.Fanout = broadcast message it means without declare routekey
ExchangeType.Direct = exact match with routekey
ExchangeType.Topic = pattern match with routekey
ExchangeType.Fanout = match with exact header
*/

string input = "";

while(input != "exit")
{
    Console.WriteLine("Input the message and input exit to terminate: ");
    input = Console.ReadLine();
    var body = Encoding.UTF8.GetBytes(input);

    channel.BasicPublish(
        exchange: "demo-exchange",
        routingKey: string.Empty,
        basicProperties: null,
        body: body
    );

    Console.WriteLine($"Sent {input}");    
}

