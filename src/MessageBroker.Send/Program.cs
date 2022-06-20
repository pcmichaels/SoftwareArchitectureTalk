using System.Text;
using RabbitMQ.Client;

Console.WriteLine("Send message:");
string? msg = Console.ReadLine();
SendNewMessage(msg!);


static void SendNewMessage(string message)
{
    var factory = new ConnectionFactory() { HostName = "localhost" };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();
    
    var result = channel.QueueDeclare("NewQueue", true, false, false, null);
    Console.WriteLine(result);
  
    channel.BasicPublish("", "NewQueue", null, Encoding.UTF8.GetBytes(message));                
}