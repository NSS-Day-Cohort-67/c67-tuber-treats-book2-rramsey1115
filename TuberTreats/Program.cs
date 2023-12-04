using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Localization;
using TuberTreats.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.----------------------------------------------------------------------------------
List<Customer> customers = new List<Customer>
{
    new Customer
    { Id = 1, Name = "John Doe", Address = "123 Main St" },
    new Customer { Id = 2, Name = "Jane Smith", Address = "456 Oak St"},
    new Customer { Id = 3, Name = "Bob Johnson", Address = "789 Pine St" },
    new Customer { Id = 4, Name = "Alice Williams", Address = "101 Cedar St" },
    new Customer { Id = 5, Name = "Charlie Brown", Address = "202 Birch St" },
};

List<Topping> bakedPotatoToppings = new List<Topping>
{
    new Topping { Id = 1, Name = "Sour Cream" },
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
    },
    new TuberDriver
    {
        Id = 2,
        Name = "Driver Luke",
    },
    new TuberDriver
    {
        Id = 3,
        Name = "Driver Tammy",
    },
};

List<TuberOrder> tuberOrders = new List<TuberOrder>
{
    new()
    {
        Id = 1,
        OrderPlacedOnDate = new DateTime(2023, 11, 28, 17, 22, 11),
        CustomerId = 1,
        TuberDriverId = 1,
        DeliveredOnDate = new DateTime(2023, 11, 28, 17, 23, 15),
    },
    new()
    {
        Id = 2,
        OrderPlacedOnDate = new DateTime(2023, 12, 1, 11,45, 33),
        CustomerId = 3,
        TuberDriverId = 2,
        DeliveredOnDate = new DateTime(2023, 12, 1, 12, 10, 44),
    },
    new()
    {
        Id = 3,
        OrderPlacedOnDate = new DateTime(2023, 12, 3, 3, 33, 33),
        CustomerId = 4,
        TuberDriverId = 3,
        DeliveredOnDate = new DateTime(2023, 12, 3, 3, 55, 10),
    },
    new()
    {
        Id = 4,
        OrderPlacedOnDate = new DateTime(2023, 12, 3, 9, 45, 34),
        CustomerId = 1,
    },
};

List<TuberTopping> tuberToppings = new List<TuberTopping>
{
    new TuberTopping { Id = 1, TuberOrderId = 1, ToppingId = 2 }, // Butter
    new TuberTopping { Id = 2, TuberOrderId = 1, ToppingId = 3 }, // Cheddar Cheese
    new TuberTopping { Id = 3, TuberOrderId = 1, ToppingId = 9 }, // Caramelized Onions
    new TuberTopping { Id = 4, TuberOrderId = 2, ToppingId = 7 }, // Guacamole
    new TuberTopping { Id = 5, TuberOrderId = 2, ToppingId = 8 }, // Salsa
    new TuberTopping { Id = 6, TuberOrderId = 2, ToppingId = 10 }, // Jalapenos
    new TuberTopping { Id = 7, TuberOrderId = 3, ToppingId = 2 }, // Butter
    new TuberTopping { Id = 8, TuberOrderId = 3, ToppingId = 3 }, // Cheddar Cheese
    new TuberTopping { Id = 9, TuberOrderId = 3, ToppingId = 9 }, // Caramelized Onions
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

// TuberOrders endpoints-----------------------------------
// get all orders
app.MapGet("/tuberorders", () =>
{
    return tuberOrders.Select(to =>
    {
        TuberOrderDTO tuberOrder = new TuberOrderDTO
        {
            Id = to.Id,
            OrderPlacedOnDate = to.OrderPlacedOnDate,
            CustomerId = to.CustomerId,
            TuberDriverId = to.TuberDriverId,
            DeliveredOnDate = to.DeliveredOnDate,
        };
        return tuberOrder;
    }
    ).ToList();
});

// get order by id (includes customer, driver, and toppings data)
app.MapGet("/tuberorders/{id}", (int id) =>
{

    // find matching order by provided id
    TuberOrder order = tuberOrders.FirstOrDefault(to => to.Id == id);
    if (order == null) { return Results.NotFound(); }

    // find customer data
    Customer orderCustomer = customers.FirstOrDefault(c => c.Id == order.CustomerId);
    if (orderCustomer == null) { return Results.NotFound(); }

    // find driver data
    TuberDriver orderDriver = tuberDrivers.FirstOrDefault(td => td.Id == order.TuberDriverId);

    // find toppings data
    List<TuberTopping> toppingsRes = tuberToppings.Where(tt => tt.TuberOrderId == order.Id).ToList();
    List<Topping> orderToppings = new List<Topping>();

    foreach (TuberTopping tRes in toppingsRes)
    {
        foreach (Topping topping in bakedPotatoToppings)
        {
            if (tRes.ToppingId == topping.Id)
            {
                orderToppings.Add(topping);
            }
        }
    }

    // construct DTO object to return to client
    var orderObject = new TuberOrderDTO()
    {
        Id = order.Id,
        OrderPlacedOnDate = order.OrderPlacedOnDate,
        CustomerId = orderCustomer.Id,
        Customer = orderCustomer == null ? null : new CustomerDTO
        {
            Id = orderCustomer.Id,
            Name = orderCustomer.Name,
            Address = orderCustomer.Address
        },
        TuberDriverId = order.TuberDriverId,
        TuberDriver = orderDriver == null ? null : new TuberDriverDTO
        {
            Id = orderDriver.Id,
            Name = orderDriver.Name,
        },
        DeliveredOnDate = order.DeliveredOnDate,
        Toppings = orderToppings.Select(ot => new ToppingDTO
        {
            Id = ot.Id,
            Name = ot.Name
        }).ToList()
    };

    // return result to client
    return Results.Ok(orderObject);

});

// submit new order
app.MapPost("/tuberorders", (TuberOrder newOrderObj) =>
{

    // with placedOnDate 
    newOrderObj.OrderPlacedOnDate = DateTime.Now;
    newOrderObj.Id = tuberOrders.Max(o => o.Id) + 1;

    // with toppings list
    List<ToppingDTO> orderToppings = new List<ToppingDTO>();
    foreach (Topping t in newOrderObj.Toppings)
    {
        ToppingDTO tdto = new ToppingDTO
        {
            Id = t.Id,
            Name = t.Name
        };
        orderToppings.Add(tdto);
    }

    // add customer object
    Customer orderCustomer = customers.FirstOrDefault(c => c.Id == newOrderObj.CustomerId);

    tuberOrders.Add(newOrderObj);

    // return new order so client can see new Id
    return Results.Created($"/tuberorders/{newOrderObj.Id}", new TuberOrderDTO
    {
        Id = newOrderObj.Id,
        OrderPlacedOnDate = newOrderObj.OrderPlacedOnDate,
        CustomerId = newOrderObj.CustomerId,
        Customer = orderCustomer == null ? null : new CustomerDTO
        {
            Id = orderCustomer.Id,
            Name = orderCustomer.Name,
            Address = orderCustomer.Address
        },
        TuberDriverId = newOrderObj.TuberDriverId,
        DeliveredOnDate = newOrderObj.DeliveredOnDate,
        Toppings = orderToppings
    });

});

// Assign a driver to an order
// PUT to /tuberorders/{id}
app.MapPut("/tuberorders/{id}", (int id, TuberDriver newDriver) =>
{
    // find order to update based in orderId
    TuberOrder orderToUpdate = tuberOrders.FirstOrDefault(to => to.Id == id);
    if (orderToUpdate == null)
    {
        return Results.NotFound();
    }

    // find driver object based on passed in newDriverid
    TuberDriver orderDriver = tuberDrivers.FirstOrDefault(td => td.Id == newDriver.Id);
    if (orderDriver == null)
    {
        return Results.BadRequest();
    }

    // add tuberDriverId from updatedOrder object passed in
    orderToUpdate.TuberDriverId = orderDriver.Id;

    // return updated order object for client
    return Results.Ok(new TuberOrderDTO
    {
        Id = orderToUpdate.Id,
        TuberDriverId = orderToUpdate.TuberDriverId
    });
});

// Complete an order
// Post to /tuberorders/{id}/complete
app.MapPost("tuberorders/{id}/complete", (int id) =>
{
    TuberOrder orderToComplete = tuberOrders.FirstOrDefault(to => to.Id == id);
    orderToComplete.DeliveredOnDate = DateTime.Now;
});

// Toppings endpoints------------------------------------
// get all toppings
app.MapGet("/toppings", () =>
{
    return bakedPotatoToppings.Select(topping =>
    new ToppingDTO
    {
        Id = topping.Id,
        Name = topping.Name
    }
    ).ToList();
});

// get toppings by id
app.MapGet("/toppings/{id}", (int id) =>
{
    Topping foundTopping = bakedPotatoToppings.FirstOrDefault(bpt => bpt.Id == id);
    if (foundTopping == null) 
    {
        return Results.NotFound();
    }

    return Results.Ok(new ToppingDTO{
        Id = foundTopping.Id,
        Name = foundTopping.Name
    });
});

// TuberToppings endpoints-------------------------------
// get all tuberToppings
app.MapGet("/tubertoppings", () =>
{
    return tuberToppings.Select(tt =>
    new TuberToppingDTO
    {
        Id = tt.Id,
        ToppingId = tt.ToppingId,
        TuberOrderId = tt.TuberOrderId
    }).ToList();
});

// add a topping to a TuberOrder
app.MapPost("/tubertoppings", (TuberTopping newTuberTopping) => {
    newTuberTopping.Id = tuberToppings.Max(tt => tt.Id) + 1;

    tuberToppings.Add(newTuberTopping);

    // return new TuberTopping object to client
    return Results.Created($"/tubertoppings/{newTuberTopping.Id}", new TuberToppingDTO 
    {
        Id = newTuberTopping.Id,
        ToppingId = newTuberTopping.ToppingId,
        TuberOrderId = newTuberTopping.TuberOrderId
    });
});

// remove a topping from a TuberOrder
app.MapDelete("/tubertoppings/{id}", (int id) => {
    TuberTopping toDelete = tuberToppings.FirstOrDefault(tt => tt.Id == id);
    tuberToppings.Remove(toDelete);
});

// Customers endpoints------------------------------------
// get all customers
app.MapGet("/customers", () =>
{
    return customers.Select(c =>
    {
        // finds tuber orders matching customer
        List<TuberOrder> customerTuberOrders = tuberOrders.Where(to => to.CustomerId == c.Id).ToList();

        CustomerDTO customer = new CustomerDTO
        {
            Id = c.Id,
            Address = c.Address,
            Name = c.Name,
        };
        return customer;
    }).ToList();
});

// get customer by id, WITH their orders
app.MapGet("/customers/{id}", (int id) => {
    // find matching customer in DB based on passed in id
    Customer customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer == null) {
        return Results.NotFound();
    }

    // find orders related to that customer
    List<TuberOrder> customerOrders = tuberOrders.Where(to => to.CustomerId == customer.Id).ToList();

    return Results.Ok(new CustomerDTO 
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            TuberOrders = customerOrders.Select(co => {
                // find driver
                TuberDriver foundDriver = tuberDrivers.FirstOrDefault(td => td.Id == co.TuberDriverId);

                // find toppings
                List<TuberTopping> toppingsRes = tuberToppings.Where(tt => tt.TuberOrderId == co.Id).ToList();
                List<Topping> orderToppings = new List<Topping>();

                foreach (TuberTopping tRes in toppingsRes)
                {
                    foreach (Topping topping in bakedPotatoToppings)
                    {
                        if (tRes.ToppingId == topping.Id)
                        {
                            orderToppings.Add(topping);
                        }
                    }
                }

                return new TuberOrderDTO
                {
                    Id = co.Id,
                    OrderPlacedOnDate = co.OrderPlacedOnDate,
                    CustomerId = co.CustomerId,
                    Customer = new CustomerDTO {
                        Id = customer.Id,
                        Name = customer.Name,
                        Address = customer.Address,
                    },
                    TuberDriverId = co.TuberDriverId,
                    TuberDriver = new TuberDriverDTO {
                        Id = foundDriver.Id,
                        Name = foundDriver.Name
                    },
                    DeliveredOnDate = co.DeliveredOnDate,
                    Toppings = orderToppings.Select(ot => new ToppingDTO{
                        Id = ot.Id,
                        Name = ot.Name
                    }).ToList()
                };
                }
                ).ToList()
        });

});

// add a Customer (return the new customer obj)
app.MapPost("/customers", (Customer newCustomerObj) => {
    newCustomerObj.Id = customers.Max(c => c.Id) + 1;
    customers.Add(newCustomerObj);

    return Results.Created($"/customers/{newCustomerObj.Id}" ,new CustomerDTO {
        Id = newCustomerObj.Id,
        Name = newCustomerObj.Name,
        Address = newCustomerObj.Address,
    });
});

// delete a Customer
app.MapDelete("/customers/{id}", (int id) => {
    Customer foundCustomer = customers.FirstOrDefault(c => c.Id == id);
    customers.Remove(foundCustomer);
});

// TuberDrivers endpoints--------------------------------
// get all employees(TuberDrivers)
app.MapGet("/tuberdrivers", () =>
{
    return tuberDrivers.Select(td =>
    new TuberDriverDTO
    {
        Id = td.Id,
        Name = td.Name,
    }).ToList();
});

// get an employee by id, WITH their deliveries





// ----------------------------------------------------------------------------------------------------------------------
app.Run();
//don't touch or move this!
public partial class Program { }