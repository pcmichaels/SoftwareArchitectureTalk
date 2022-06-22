
using EventSourcing.Common;


var session = new Session<OrderEvent>();
session.Initialise(@"c:\tmp\tmp.data");
session.StartSession();


decimal total = 0;

var events = session.Read();

foreach (var eachEvent in events)
{
    Console.WriteLine($"{eachEvent.Action}: {eachEvent.Order.OrderDate} / {eachEvent.Order.CustomerId}");
    total += eachEvent.Order.OrderValue;
}

Console.WriteLine($"Order total value: {total}");
