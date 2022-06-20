
using IDocumentStore store = new DocumentStore()
{
    Url = "http://localhost",
    DefaultDatabase = "Orders"
};

var newOrder = new OrderEvent()
{
    Action = "New Order",
    Order = new Order()
    {
        OrderDate = DateTime.Now(),
        CustomerId = "1234",
        OrderValue = 41.8m
    }
};

store.Initialize();
using IDocumentSession session = store.OpenSession();
session.Store(newOrder);
session.SaveChanges();

