needed a few tutorials during the setting-up phase (creating the database container, configuring startup.cs)


URL: https://localhost:5001/profiles

uses a docker container with a postgre database.


setup instructions:

-> create database container
(taken from https://medium.com/@arjungibson/create-a-postgresql-database-using-docker-9ea95dfd31b8, slightly modified)
install docker
run command (in cmd) `docker run --name SocialMedia -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root -e POSTGRES_DB=SocialMedia -p 5432:5432 -d postgres`
container must be running for the api to work.

-> install packages
i'm not sure whether listing packages in SocialMedia.csproj is enough or they'll need to be installed on a new machine
dependencies are:
Microsoft.AspNetCore.Mvc.NewtonsoftJson version 5.0.11
Microsoft.EntityFrameworkCore version 5.0.11
Microsoft.EntityFrameworkCore.Design version 5.0.11
Microsoft.EntityFrameworkCore.SqlServer version 5.0.11  // i'm not sure whether this is actually needed
Microsoft.Extensions.DependencyInjection version 5.0.2
Newtonsoft.Json version 13.0.1
Npgsql.EntityFrameworkCore.PostgreSQL version 5.0.10
Swashbuckle.AspNetCore version 5.6.3
install a dependency with dotnet add package [package]
might have to cd to SocialMedia first

-> create tables in database
must add a migration with `dotnet ef migrations add Initialize` and then apply it with `dotnet ef database update`
make sure api isn't running when creating or applying a migration

-> (optional) add data source in ide
in database tab, create new data source
select postgres
change host to `localhost`, port to `5432`, username to `root`, password to `root`, database to `SocialMedia`
click ok
can now view data in database, as well as manage migrations


POST
Controller listens to HTTP request and gets a RequestModel
Controller sends RequestModel to Service
Service creates database Entity (class Profile) from RequestModel
Service sends Entity to Repository
Repository stores Entity in Database
Service creates TO from Entity
Service returns TO to Controller
Controller sends TO back as a 200 HTTP response

GET
Controller listens to HTTP request
Controller calls Service get method
Service calls Repository get method
Repository gets Entities from Database
Repository returns Entities to Service
Service creates TOs from Entities
Service returns TOs to Controller
Controller sends TOs back as a 200 HTTP response