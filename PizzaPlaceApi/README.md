# PizzaPlaceApi
PizzaPlaceApi is a mini project for Erlich 24hr code challenge.

Run this on PizzaPlaceApi.Api CLI to create migration for DB (if does not exist)
dotnet ef migrations add InitialCreate --project ../PizzaPlaceApi.Infrastructure --startup-project ./

run this to create a local copy of DB
dotnet ef database update --project ../PizzaPlaceApi.Infrastructure --startup-project ./

