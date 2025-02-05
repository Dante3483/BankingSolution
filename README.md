## **1. Project Overview**

**BankingSolution API** is a RESTful web service built with ASP.NET Core that allows to **manage bank accounts**. It also allows to create **deposits, withdrawals, and transfers transactions**. It follows **clean architecture**, ensuring scalability and maintainability.

## **2. Technology Stack**

| Technology                | Purpose                                        |
| ------------------------- | ---------------------------------------------- |
| **ASP.NET Core**          | Backend framework                              |
| **Entity Framework Core** | ORM for database management                    |
| **SQLite**                | Database                                       |
| **xUnit**                 | Unit testing                                   |
| **Moq**                   | Mock dependencies                              |
| **Fluent Assertions**     | Make more readable and maintainable assertions |
| **Swagger (Swashbuckle)** | API documentation                              |

## **3. Architecture Overview**

The project follows **Clean Architecture**, separating concerns into different layers:

- **Domain Layer (`BankingSolution.Domain`)** – Core entities, enums and interfaces.
- **Infrastructure Layer (`BankingSolution.Infrastructure`)** – Database access, repositories.
- **Customer API (`BankingSolution.WebApi.CustomerApi`)** – Provides endpoints for managing customer data, including retrieving, adding, and deleting customers.
- **Account API (`BankingSolution.WebApi.AccountApi`)** – Provides endpoints for managing account data, including retrieving, adding, and deleting accounts.
- **Transaction API (`BankingSolution.WebApi.TransactionApi`)** – Provides endpoints for managing transactions, including retrieving, adding deposit, withdrawal, and transfer transactions, and deleting transactions.

## **4. API Endpoints**

### **4.1 Customer API**

| Method     | Endpoint             | Description           |
| ---------- | -------------------- | --------------------- |
| **GET**    | `/api/customer`      | Get all customers     |
| **GET**    | `/api/customer/{id}` | Get a customer by id  |
| **POST**   | `/api/customer`      | Create a new customer |
| **DELETE** | `/api/customer/{id}` | Delete a customer     |

### **4.2 Account API**

| Method     | Endpoint            | Description                 |
| ---------- | ------------------- | --------------------------- |
| **GET**    | `/api/account`      | Get all accounts            |
| **GET**    | `/api/account/{id}` | Get an account detail by id |
| **POST**   | `/api/account`      | Create a new account        |
| **DELETE** | `/api/account/{id}` | Delete an account           |

### **4.3 Transaction API**

| Method     | Endpoint                    | Description                     |
| ---------- | --------------------------- | ------------------------------- |
| **GET**    | `/api/transaction`          | Get all transactions            |
| **GET**    | `/api/transaction/{id}`     | Get a transaction by id         |
| **POST**   | `/api/transaction/deposit`  | Create a deposit transaction    |
| **POST**   | `/api/transaction/withdraw` | Create a withdrawal transaction |
| **POST**   | `/api/transaction/transfer` | Create a transfer transaction   |
| **DELETE** | `/api/transaction/{id}`     | Delete a transfer               |

## **5. How to Run the Project**

**Clone the repository:**

```
git clone https://github.com/Dante3483/BankingSolution.git
cd BankingSolution
```

**Run projects:**

```
dotnet run --project BankingSolution.WebApi.CustomerApi
```

```
dotnet run --project BankingSolution.WebApi.AccountApi
```

```
dotnet run --project BankingSolution.WebApi.TransactionApi
```

**Open Swagger UI:**

```
http://localhost:{port}/swagger/index.html
```

## **6. Testing**

Tests cover almost all the necessary methods from all WebApi and Repositories.

**Run all tests:**

```
dotnet test
```

**Technologies used:**

- **xUnit** for unit tests
- **Moq** for mocking dependencies
- **Fluent Assertions** for more readable and maintainable assertions
