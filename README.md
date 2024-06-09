# Reuters Market Data API

This project is a simple API for managing asset information, built with ASP.NET Core and Entity Framework Core.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Getting Started

### 1. Setup SQL Server Express

1. Download and install SQL Server Express from the link above.
2. Open SQL Server Management Studio (SSMS) and connect to your local SQL Server Express instance.
3. Create a new database named `ReutersMarketData`.

### 2. Clone the Repository

```
git clone https://github.com/flpbraga/ReutersMarketDataApi.git
cd reuters-market-data-api
```
### 3. Configure the Connection String

Update the connection string in appsettings.json:

```json
"ConnectionStrings": {
  "DefaultConnection": "{ConnectionString}"
}
```

### 4.Add Migrations and Update Database

Run the following commands in the Package Manager Console or a terminal:

# Install the EF Core tools if you haven't already
dotnet tool install --global dotnet-ef

# Add a new migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update

### 5. Run the Application
Start the application:

bash
```
dotnet run
```

### 6. Call the API
Use a tool like Postman or curl to call the API.

Sample JSON to Insert an Asset
```json
{
  "name": "Microsoft Corporation",
  "symbol": "MSFT",
  "isin": "US5949181045",
  "prices": [
    {
      "value": 250.00,
      "date": "2023-01-01T00:00:00Z",
      "source": "Reuters"
    },
    {
      "value": 255.00,
      "date": "2023-01-02T00:00:00Z",
      "source": "Reuters"
    }
  ]
}

```

Insert Asset Example
Use Postman or curl to send a POST request to https://localhost:5001/api/assets with the JSON body:

Postman Example:

Open Postman.
Create a new POST request to https://localhost:5001/api/assets.
Set the request body to raw JSON and paste the sample JSON above.
Send the request.

### 7. Get Assets
You can retrieve all assets or a specific asset using the following endpoints:

Get all assets: GET https://localhost:5001/api/assets
Get an asset by ID: GET https://localhost:5001/api/assets/{id}

### 8. Get Assets by Date

To get assets with prices on a specific date:

Get Asset by Date Example: 
```
GET https://localhost:5001/api/assets/GetAssetByDate?startDate=2023-01-01
```
or
```
GET https://localhost:5001/api/assets/GetAssetByDate?startDate=2023-01-01&source=Bloomberg
```
