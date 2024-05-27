using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "dev", Password = "P@ssw0rd" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

/*
ExchangeType.Fanout = broadcast message it means without declare routekey
ExchangeType.Direct = exact match with routekey
ExchangeType.Topic = pattern match with routekey
ExchangeType.Fanout = match with exact header
*/

channel.ExchangeDeclare("demo-exchange",ExchangeType.Fanout);

var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(
    queue: queueName, 
    exchange: "demo-exchange",
    routingKey: string.Empty
);

Console.WriteLine(" [*] Waiting for message.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] {message}");
};
channel.BasicConsume(queue: queueName,
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();