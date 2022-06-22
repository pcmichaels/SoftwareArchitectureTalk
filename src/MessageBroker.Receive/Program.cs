using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare("SalesOrder", ExchangeType.Fanout);

var result = channel.QueueDeclare("OrderRaised", false, false, false, null);
string queueName = result.QueueName;
channel.QueueBind(queueName, "SalesOrder", "");

Console.WriteLine(result);
  
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
  
channel.BasicConsume(queueName, true, consumer);


Console.WriteLine("Receiving...");
Console.ReadLine();

static void Consumer_Received(object sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine(message);
}

