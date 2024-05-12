
# Basic Redis .NetCore API

SimpleWebAPI is a basic web API built using .NET Core, Entity Framework's in-memory database, and Redis Stack Exchange.API for caching. It provides a foundation for building RESTful APIs with CRUD operations for managing products.

### Features:

- CRUD Operations: Supports Create, Read, Update, and Delete operations for managing products

- In-Memory Database: Utilizes Entity Framework's in-memory database for data persistence during development.

- Redis Caching: Integrates Redis for caching to enhance API performance

### Prerequisites :

- [.NET Core SDK](https://dotnet.microsoft.com/en-us/download)

- [Redis (Docker Image)](https://hub.docker.com/_/redis)

- [Another Redis Desktop Manager](https://github.com/qishibo/AnotherRedisDesktopManager/releases)


## Getting Started :

###  Install .NET Core SDK :

- To run the project, you need to have the .NET Core SDK installed on your computer. If it's not installed, you can download and install it from [here](https://dotnet.microsoft.com/download).

###  Clone the Repository :

- To run the project, you need to clone the codes from GitHub. You can clone the repository by running the following command in a command prompt:

```bash
git clone https://github.com/fatihserhatturan/Redis_NetCore
```


### Run the Project :

- Once you're in the project directory, you can run the project using the .NET Core CLI (Command Line Interface). Use the following command to start the project:

```bash
dotnet run
```





## Configuration

- AppDbContext: Modify the database context configuration in Models/AppDbContext.cs.

- Redis Configuration: Adjust Redis connection settings in Program.cs and AppSettings.json


  
## Used Technologies




  
## API Using

#### Get All Products

```http
  GET /Product/GetAll
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `null` | `JSON` | Retrieve all products. |

#### Get Product By ID

```http
  GET /Product/GetById/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | Retrieve a product by ID  |

#### Create New Product


  ```http
  POST /Product/Add
  ```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `product` | `Object` | Create a new product |
