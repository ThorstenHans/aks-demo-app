# Database Create

```
set DbConnectionString=<YOUR_CONNECTIONSTRING>
dotnet ef migrations add InitialMigration
dotnet ef database update
set DbConnectionString=
```