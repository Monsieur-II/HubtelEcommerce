# Hubtel Ecommerce API 🛒🛍️

This project is a RESTful API for the Hubtel Ecommerce application to unify cart experience for users . It is built with C# using .NET 6

## Features

- User Authentication and Registration
- Authorization
- JWT Token Generation
- Swagger Documentation
- Relational Database


### Prerequisites

To run and test the API, you need the following tools installed:

- .NET 6
- Visual Studio, Rider or VS Code
- Postman, Swagger, [REST Client Extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) for VS Code or any API testing tool

## API Endpoints

### /api/auth/register
* `POST /login`: Authenticate a user and return a JWT token.
* `POST /register`: Register a new user.

### /api/v1/carts
* `POST`: Add a new cart.
* `GET /{id}`: Get a cart by ID.
* `GET`: Get all carts.
* `DELETE /{id}`: Delete a cart by ID.


## Technologies Used

- C#
- .NET 6
- Entity Framework Core
- MySQL
- Swagger
