var builder = DistributedApplication.CreateBuilder(args);

// 1. SQL Server
var sql = builder.AddSqlServer("sql")
    .WithDataVolume();

var database = sql.AddDatabase("Ecommerce");

// 2. RabbitMQ
var rabbitmqUsername = builder.AddParameter(
    "rabbitmq-username",
    secret: true);

var rabbitmqPassword = builder.AddParameter(
    "rabbitmq-password",
    secret: true);

var messaging = builder.AddRabbitMQ(
        "messaging",
        rabbitmqUsername,
        rabbitmqPassword)
    .WithManagementPlugin()
    .WithDataVolume()
    .WithEnvironment(
        "RABBITMQ_SERVER_ADDITIONAL_ERL_ARGS",
        "-rabbit loopback_users []");

// 3. Mailpit
var mailpit = builder.AddContainer(
        "mailpit",
        "axllent/mailpit")
    .WithHttpEndpoint(
        targetPort: 8025,
        name: "dashboard")
    .WithEndpoint(
        targetPort: 1025,
        name: "smtp");

// 4. PostgreSQL
var postgres = builder.AddPostgres("postgres")
    .WithEnvironment(
        "POSTGRES_PASSWORD",
        "postgres")
    .WithDataVolume()
    .WithEndpoint(
        port: 5432,
        targetPort: 5432,
        name: "tcp");

var inventory = postgres.AddDatabase("inventories");
var notification = postgres.AddDatabase("notifications");

// 5. MongoDB
var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume();

var catalog = mongo.AddDatabase("Catalog");
var cart = mongo.AddDatabase("Cart");
var order = mongo.AddDatabase("Order");
var coupon = mongo.AddDatabase("Coupon");

// 6. SMTP endpoint
var smtpEndpoint = mailpit.GetEndpoint(
    "smtp",
    KnownNetworkIdentifiers.LocalhostNetwork);

var api = builder.AddProject<Projects.Ecommerce_API>(
        "ecommerce-api")
    .WithReference(database)
    .WithReference(catalog)
    .WithReference(cart)
    .WithReference(order)
    .WithReference(inventory)
    .WithReference(notification)
    .WithReference(coupon)
    .WithReference(messaging)
    .WithEnvironment(
        "SmtpOptions__Host",
        smtpEndpoint.Property(EndpointProperty.Host))
    .WithEnvironment(
        "SmtpOptions__Port",
        smtpEndpoint.Property(EndpointProperty.Port))
    .WaitFor(messaging);

// 8. Admin
builder.AddProject<Projects.Ecommerce_Admin>(
        "ecommerce-admin")
    .WithReference(api)
    .WaitFor(api);

// 9. Storefront
builder.AddProject<Projects.Ecommerce_Storefront>(
        "ecommerce-storefront")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();