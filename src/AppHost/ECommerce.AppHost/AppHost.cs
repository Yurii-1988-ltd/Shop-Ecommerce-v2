var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithDataVolume();

var database = sql.AddDatabase("Ecommerce");

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

var catalog = mongo.AddDatabase("Catalog");
var cart = mongo.AddDatabase("Cart");

var order = mongo.AddDatabase("Order");

var api = builder.AddProject<Projects.Ecommerce_API>("ecommerce-api")
    .WithReference(database)
    .WithReference(catalog)
    .WithReference(order);

builder.AddProject<Projects.Ecommerce_Admin>("ecommerce-admin")
    .WithReference(api);

builder.Build().Run();