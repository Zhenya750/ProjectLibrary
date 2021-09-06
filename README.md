# ProjectLibrary

A RESTful service to let users manage their libraries with the following operations:
- create a book
- delete a book
- update a book
- get list of books or one
- get list of books filtered by genre

Stack: 
- ASP.NET Core 5.0 Web API
- MongoDB 4.4.9

How to launch:
-----------------------------
1) clone the repository
2) install packages written in `ProjectLibrary.csproj` via NuGet, if VisualStudio didn't it on its own
3) install MongoDb server and start it with the following command: 
```
> mongod --noauth --port 27017
```
4) edit MongoDB connection settings in `appsettings.json` file 
5) run the project with IIS Express option
6) now RESTful Web API is ready

API:
-----------------------------
### Authentication:
- **to register** send JSON with POST request to `/api/auth/register`
- **to login** send JSON with POST request to `/api/auth/login`

Example of JSON:
```json
{
  "login" : "admin",
  "password" : "qwerty"
}
```
Response is a JWT-token similar to this:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54...
```

### Working with books:
#### Reading
- **to get list of books** send GET request to `/api/books`
- **to get list of books filtered by genre** send GET request to `/api/books/genre/{your genre}`

JSON response can look like this:
```json
[{
  "name" : "book name 1",
  "author" : "author name 1",
  "genres" : ["genre1", "genre2", "genre3"],
  "id" : "some id 1"
},
{
  "name" : "book name 2",
  "author" : "author name 2",
  "genres" : ["genre2"],
  "id" : "some id 2"
}]
```

- **to get info about one book** send GET request to `/api/books/{book id}`

#### Creating
- **to create a book** send JSON with POST requst to `/api/books/`

JSON in request should contain all book's fields except of id:
```json
{
  "name" : "new book name",
  "author" : "new author",
  "genres" : ["new genre"]
}
```

#### Updating
- **to update a book** send JSON with PUT request to `/api/books/`

JSON in request should contain all book's fields:
```json
{
  "name" : "new book name",
  "author" : "new author",
  "genres" : ["new genre"],
  "id" : "but the same id"
}
```

#### Deleting
- **to delete a book** send DELETE request to `/api/books/{book id}`
