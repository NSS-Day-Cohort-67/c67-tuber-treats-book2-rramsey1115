using TuberTreats.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.----------------------------------------------------------------------------------
List<Customer> customers = new List<Customer>
{
    new Customer
    {
        Id = 1,
        Name = "John Doe",
        Address = "123 Main St",
        TuberOrders = new List<TuberOrder>()
    },
    new Customer { Id = 2, Name = "Jane Smith", Address = "456 Oak St", TuberOrders = new List<TuberOrder>()},
    new Customer { Id = 3, Name = "Bob Johnson", Address = "789 Pine St", TuberOrders = new List<TuberOrder>() },
    new Customer { Id = 4, Name = "Alice Williams", Address = "101 Cedar St", TuberOrders = new List<TuberOrder>() },
    new Customer { Id = 5, Name = "Charlie Brown", Address = "202 Birch St", TuberOrders = new List<TuberOrder>() },
    new Customer { Id = 6, Name = "Eva Davis", Address = "303 Elm St", TuberOrders = new List<TuberOrder>() },
    new Customer { Id = 7, Name = "Frank Miller", Address = "404 Maple St", TuberOrders = new List<TuberOrder>() },
    new Customer { Id = 8, Name = "Grace Wilson", Address = "505 Walnut St", TuberOrders = new List<TuberOrder>() },
    new Customer { Id = 9, Name = "Henry Jones", Address = "606 Pine St", TuberOrders = new List<TuberOrder>() },
    new Customer { Id = 10, Name = "Ivy Taylor", Address = "707 Oak St", TuberOrders = new List<TuberOrder>() }
};

List<Topping> bakedPotatoToppings = new List<Topping>
{
    new Topping
    {
        Id = 1,
        Name = "Sour Cream"
    },
    new Topping { Id = 2, Name = "Butter" },
    new Topping { Id = 3, Name = "Cheddar Cheese" },
    new Topping { Id = 4, Name = "Bacon Bits" },
    new Topping { Id = 5, Name = "Chives" },
    new Topping { Id = 6, Name = "Broccoli" },
    new Topping { Id = 7, Name = "Guacamole" },
    new Topping { Id = 8, Name = "Salsa" },
    new Topping { Id = 9, Name = "Caramelized Onions" },
    new Topping { Id = 10, Name = "Jalapenos" }
};

List<TuberDriver> tuberDrivers = new List<TuberDriver>
{
    new TuberDriver
    {
        Id = 1,
        Name = "Driver Steve",
        TuberDeliveries = new List<TuberOrder>
            {
                new TuberOrder
                {
                    Id = 1001,
                    OrderPlacedOnDate = new DateTime(2023, 11, 28, 17, 22, 11),
                    CustomerId = 1,
                    TuberDriverId = 1,
                    DeliveredOnDate = new DateTime(2023, 11, 28, 17, 23, 15),
                    Toppings = new List<Topping>()
                    {
                        new()
                        {
                            Id = 2,
                            Name = "Butter",
                        },
                        new()
                        {
                            Id = 3,
                            Name = "Cheddar Cheese",
                        },
                        new()
                        {
                            Id = 9,
                            Name = "Caramelized Onions"
                        }
                    }
                },
            }
        },
    new TuberDriver
    {
        Id = 2,
        Name = "Driver Luke",
        TuberDeliveries = new List<TuberOrder>
        {
            new TuberOrder
            {
                Id = 1002,
                OrderPlacedOnDate = new DateTime(2023, 12, 1, 11,45, 33),
                CustomerId = 3,
                TuberDriverId = 2,
                DeliveredOnDate = new DateTime(2023, 12, 1, 12, 10, 44),
                Toppings = new List<Topping>()
                {
                    new()
                    {
                        Id = 7,
                        Name = "Guacamole"
                    },
                    new()
                    {
                        Id = 8,
                        Name = "Salsa",
                    },
                    new()
                    {
                        Id = 10,
                        Name = "Jalepenos",
                    }
                }
            },
        }
    },
    new TuberDriver
    {
        Id = 3,
        Name = "Driver Tammy",
        TuberDeliveries = new List<TuberOrder>
        {
            new TuberOrder
            {
                Id = 1003,
                OrderPlacedOnDate = new DateTime(2023, 12, 3, 3, 33, 33),
                CustomerId = 4,
                TuberDriverId = 3,
                DeliveredOnDate = new DateTime(2023, 12, 3, 3, 55, 10),
                Toppings = new List<Topping>()
                {
                    new()
                    {
                        Id = 2,
                        Name = "Butter",
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Cheddar Cheese",
                    },
                    new()
                    {
                        Id = 9,
                        Name = "Caramelized Onions"
                    }
                }
            },
        }
    },
};

List<TuberOrder> tuberOrders = new List<TuberOrder>
{
    new()
    {

    Id = 1001,
    OrderPlacedOnDate = new DateTime(2023, 11, 28, 17, 22, 11),
    CustomerId = 1,
    TuberDriverId = 1,
    DeliveredOnDate = new DateTime(2023, 11, 28, 17, 23, 15),
    Toppings = new List<Topping>()
        {
        new() { Id = 2, Name = "Butter" },
        new() { Id = 3, Name = "Cheddar Cheese"},
        new() { Id = 9, Name = "Caramelized Onions"}
        }
    },
    new()
    {
        Id = 1002,
        OrderPlacedOnDate = new DateTime(2023, 12, 1, 11,45, 33),
        CustomerId = 3,
        TuberDriverId = 2,
        DeliveredOnDate = new DateTime(2023, 12, 1, 12, 10, 44),
        Toppings = new List<Topping>()
        {
            new() { Id = 7, Name = "Guacamole" },
            new() { Id = 8, Name = "Salsa" },
            new() { Id = 10, Name = "Jalepenos" }
        }
    },
    new()
    {
        Id = 1003,
        OrderPlacedOnDate = new DateTime(2023, 12, 3, 3, 33, 33),
        CustomerId = 4,
        TuberDriverId = 3,
        DeliveredOnDate = new DateTime(2023, 12, 3, 3, 55, 10),
        Toppings = new List<Topping>()
        {
            new() { Id = 2, Name = "Butter", },
            new() { Id = 3, Name = "Cheddar Cheese" },
            new() { Id = 9, Name = "Caramelized Onions" }
        }
    }
};

List<TuberTopping> tuberToppings = new List<TuberTopping>
{
    new TuberTopping { Id = 1, TuberOrderId = 1001, ToppingId = 2 }, // Butter
    new TuberTopping { Id = 2, TuberOrderId = 1001, ToppingId = 3 }, // Cheddar Cheese
    new TuberTopping { Id = 3, TuberOrderId = 1001, ToppingId = 9 }, // Caramelized Onions
    new TuberTopping { Id = 4, TuberOrderId = 1002, ToppingId = 7 }, // Guacamole
    new TuberTopping { Id = 5, TuberOrderId = 1002, ToppingId = 8 }, // Salsa
    new TuberTopping { Id = 6, TuberOrderId = 1002, ToppingId = 10 }, // Jalapenos
    new TuberTopping { Id = 7, TuberOrderId = 1003, ToppingId = 2 }, // Butter
    new TuberTopping { Id = 8, TuberOrderId = 1003, ToppingId = 3 }, // Cheddar Cheese
    new TuberTopping { Id = 9, TuberOrderId = 1003, ToppingId = 9 }, // Caramelized Onions
};

// ----------------------------------------------------------------------------------------------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//add endpoints here---------------------------------------------------------------------------------------------------






// ----------------------------------------------------------------------------------------------------------------------
app.Run();
//don't touch or move this!
public partial class Program { }