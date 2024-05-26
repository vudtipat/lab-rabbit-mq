using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "dev", Password = "P@ssw0rd" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

var message = "Hello World";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(
    exchange: string.Empty,
    routingKey: "hello",
    basicProperties: null,
    body: body
);

Console.WriteLine($" [x] Sent {message}");
