# ProjectLibrary

A RESTful service to let users manage their libraries with the following operations:
- create a book
- delete a book
- update a book
- get list of books or one
- get list of books filtered by genre

How to launch:
1) clone the repository
2) install packages written in ProjectLibrary.csproj via NuGet, if VisualStudio didn't it on its own
3) install MongoDb server and start it on localhost with the following command: 
```
> mongod --dbpath <your_path> --noauth --port 27017
```
4) run the project with IIS Express option
5) now RESTful Web API is ready

API:
Authentication:
1) send JSON as a POST request to /api/auth/register 
2) send JSON as a POST request to /api/auth/login
Example of JSON:
```json
{
  "login" : "admin",
  "password" : "qwerty"
}
```
Response is a JWT-token
