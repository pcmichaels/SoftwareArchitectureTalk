using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ReceiveNextMessage();

static void ReceiveNextMessage()
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();
    
    var result = channel.QueueDeclare("NewQueue", true, false, false, null);
    Console.WriteLine(result);
  
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    consumer.Received += Consumer_Received;
  
    channel.BasicConsume("NewQueue", true, consumer);  
}

static void Consumer_Received(object sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine(message);
}

