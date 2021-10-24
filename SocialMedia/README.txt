URL: https://localhost:5001/profiles

uses a docker container with a postgre database.


setup instructions:

-> create database container
(taken from https://medium.com/@arjungibson/create-a-postgresql-database-using-docker-9ea95dfd31b8, slightly modified)
install docker
run command (in cmd) `docker run --name SocialMedia -e POSTGRES_USER=root -e POSTGRES_PASSWORD=root -e POSTGRES_DB=SocialMedia -p 5432:5432 -d postgres`
container must be running for the api to work.

-> create tables in database
must add a migration with `dotnet ef migrations add Initialize` and then apply it with `dotnet ef database update`

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