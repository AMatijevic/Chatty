# Chatty

Chatty is helping with browsing on a large amount of history chat records/events.

## How to run Chatty

- ### Visual Studio (Windows)

  - Clone or download repository
  - Open Chatty.sln file located in the root
    directory of the repository using Visual Studio
  - Run WebApi project
    - The API will automatically open the Swagger specification page [Swagger spec.](https://localhost:5001/swagger)
    - On Swagger spec. Page, you will find all Chatty endpoints that you can try directly from swagger UI.
    - Chatty is using InMemoryDB so data structure and data can be found at [Chatty data](https://github.com/AMatijevic/Chatty/blob/master/src/Infrastructure/Persistence/ApplicationDbContextSeed.cs)

- ### Console (Windows, macOS, Linux)

  - If you don't have DotNet core on your machine [Install latest DotNetCore SDK](https://dotnet.microsoft.com/download)
  - Clone or download repository
  - Open any console window and position yourself to the folder containing Chatty.sln file
  - run `dotnet build` command
  - position yourself to src/WebApi folder and run `dotnet run` command 
  - Open page: [Swagger spec.](https://localhost:5001/swagger)
    - On Swagger spec. Page, you will find all Chatty endpoints that you can try directly from swagger UI.
    - Chatty is using InMemoryDB so data structure and data can be found at [Chatty data](https://github.com/AMatijevic/Chatty/blob/master/src/Infrastructure/Persistence/ApplicationDbContextSeed.cs)

## Structure and Architecture

- Chatty is using **Onion Architecture**
- With current technologies:
  - .NET Core 3.1
  - ASP .NET Core 3.1
  - Entity Framework Core 3.1
  - MediatR
  - AutoMapper
  - FluentValidation
  - xUnit
- The main handlers of application can be found in Application layer, and that would be great place to start further investigation of code.
  - [GetAllChats](https://github.com/AMatijevic/Chatty/blob/master/src/Application/Features/Chat/GetAllChats.cs)
  - [GetAllEvents](https://github.com/AMatijevic/Chatty/blob/master/src/Application/Features/Event/GetAllEvents.cs)
  - [GetAllEventsByChatId](https://github.com/AMatijevic/Chatty/blob/master/src/Application/Features/Event/GetEventsByChatId.cs)

## API

Every event endpoint can use aggregation parameters. Rule build in Chatty is if aggregation value is larger then 1 Hour, you will get grouped events information for that period.

- [api/Chats](https://localhost:5001/api/Chats) return all chats from DB
- [api/Events](https://localhost:5001/api/Events?Aggregation%20value=1) return all events from DB aggregated by 1 minute
- [api/Events/1](https://localhost:5001/api/Events/1?Aggregation%20value=1) return events from chat with id=1 aggregated by 1 minute
