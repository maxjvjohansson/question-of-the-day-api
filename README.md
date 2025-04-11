# Question of the Day API

The **Question of the Day API** is a RESTful service built with C# and ASP.NET Core.  
It allows users to:

- **Submit questions** via a `POST` endpoint
- **Retrieve the daily question** via a `GET` endpoint  
The API serves **one random question per day**, ensuring consistency throughout a 24-hour period.

---

## Features

- RESTful API architecture
- Data persistence with **PostgreSQL** (via Entity Framework Core)
- Auto-generated documentation with **Swagger**
- Daily question logic with random selection & date tracking
- SQLite support in development

---

## Technologies Used

- [.NET 8 (ASP.NET Core Web API)](https://dotnet.microsoft.com/en-us/)
- [Entity Framework Core (EF Core)](https://learn.microsoft.com/en-us/ef/core/)
- [Npgsql (PostgreSQL EF Provider)](https://www.npgsql.org/efcore/)
- [Swagger / Swashbuckle](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-8.0)
- [Railway](https://railway.app/) for hosting and deployment

---

## Installation & Usage

1. **Clone the repo**  
   ```bash
   git clone https://github.com/yourusername/question-of-the-day-api.git
   cd question-of-the-day-api
```

 
2. **Set up your database connection** 
 
  - In `appsettings.json` or environment variables, add your PostgreSQL connection string:


```ini
POSTGRES_CONNECTION_STRING=Host=...;Database=...;Username=...;Password=...
```
 
4. **Run migrations** 


```bash
dotnet ef database update
```
 
6. **Start the API** 


```bash
dotnet run --project src/QuestionsApi
```



---



## Endpoints 

| Method | Endpoint | Description | 
| --- | --- | --- | 
| GET | /api/questions/today | Returns today's question | 
| POST | /api/questions | Adds a new question (JSON) | 

**Example JSON for POST:** 


```json
{
  "text": "What’s your favorite programming language?"
}
```



---



## Tutorials & Inspiration 


This project was built independently, but inspired by the following resources:

 
- [Build your first Web API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio)
 
- [Entity Framework Core + PostgreSQL (Npgsql)](https://www.npgsql.org/efcore/index.html?tabs=onconfiguring)
 
- [Documenting APIs with Swagger in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-8.0)



---



## 🧑‍💻 Author 

Created by [Max Johansson]


---



## License 

This project is open-source and available under the [MIT License]
