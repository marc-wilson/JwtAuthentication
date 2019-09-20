### Jwt API Authentication

#### Setup
1. Add SQL Server Connection String to the appsettings.json file
2. Open a terminal and navigate to the JwtAuthentication Project Directory
3. Run initial migrations `dotnet ef migrations add InitialCreate` ('InitialCreate' can be anything. Just a descriptive name for the migration script)
4. Apply migrations `dotnet ef database update`
5. Run the project

#### Using the API
1. Register a new user by sending an empty `POST` request to https://localhost:5001/api/auth/register
2. Login to the application by sending an empty `POST` request to https://localhost:5001/api/auth/signin
3. Make a `GET` request to https://localhost:5001/api/values with the header value `Authorization Bearer <token>` returned in step 2

* Example based off of https://github.com/medhatelmasry/JwtAuthentication