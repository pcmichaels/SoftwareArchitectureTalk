using EventSourcing.Common;


var session = new Session<OrderEvent>();
session.Initialise(@"c:\tmp\tmp.data");
session.StartSession();

for (int i = 0; i < 10; i++)
{
    var newOrder = new OrderEvent()
    {
        Action = "New Order",
        Order = new Order()
        {
            OrderDate = DateTime.Now,
            CustomerId = Random.Shared.Next(1000).ToString(),
            OrderValue = Random.Shared.Next(100)
        }
    };

    session.Store(newOrder);    
}

session.Commit();




