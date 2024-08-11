# PizzaPlaceApi
PizzaPlaceApi is a mini project for Erlich 24hr code challenge.

Run this on PizzaPlaceApi.Api CLI to create migration for DB (if does not exist)
dotnet ef migrations add InitialCreate --project ../PizzaPlaceApi.Infrastructure --startup-project ./

run this to create a local copy of DB
dotnet ef database update --project ../PizzaPlaceApi.Infrastructure --startup-project ./



Import:
    Import data/records in sequence to avoid getting error, for the tables has required foreign keys and constraints.
        Sequence:
            1. pizza_types.csv
            2. pizzas.csv
            3. orders.cvs
            4. order_details.csv
