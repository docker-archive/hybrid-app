# Hybrid Sample App - .NET Core API

This is the Sign Up API implemented in .NET Core and packaged to run in Docker containers on Linux.

## Usage

The `users` endpoint has services to fetch and create users.

### All Users

Request: 

```
curl http://localhost:57989/api/users \
  -H 'Content-Type: application/json'
```

Response:

```
[
    {
        "id": 1,
        "userName": "u1",
        "firstName": "User",
        "lastName": "One",
        "password": "abc",
        "emailAddress": "u1@abc.com",
        "dateOfBirth": "1981-12-31T00:00:00"
    },
    {
        "id": 2,
        "userName": "u2",
        "firstName": "User",
        "lastName": "Two",
        "password": "abc",
        "emailAddress": "u2@abc.com",
        "dateOfBirth": "1982-12-31T00:00:00"
    }
]
```

### User by Username

Request: 

```
curl http://localhost:57989/api/users/u1 \
  -H 'Content-Type: application/json'
```

Response:

```
{
    "id": 1,
    "userName": "u1",
    "firstName": "User",
    "lastName": "One",
    "password": "abc",
    "emailAddress": "u1@abc.com",
    "dateOfBirth": "1981-12-31T00:00:00"
}
```

> Returns `200` or `404` if user not found

### User by Username

Request: 

```
curl 'http://localhost:57989/api/users?userName=u1&password=abc' \
  -H 'Content-Type: application/json' \
```

Response:

```
{
    "id": 1,
    "userName": "u1",
    "firstName": "User",
    "lastName": "One",
    "password": "abc",
    "emailAddress": "u1@abc.com",
    "dateOfBirth": "1981-12-31T00:00:00"
}
```

> Returns `200` or `404` if user not found or password does not match

### Create User

Request: 

```
curl http://localhost:57989/api/users \
  -H 'Content-Type: application/json' \
  -d '{
  "userName" : "u3",
  "firstName" : "User",
  "lastName" : "One",
  "password" : "abc",
  "emailAddress" : "u1@abc.com",
  "dateOfBirth" : "1981-12-31"
}'
```

Response:

```
{
    "id": 3,
    "userName": "u3",
    "firstName": "User",
    "lastName": "One",
    "password": "abc",
    "emailAddress": "u1@abc.com",
    "dateOfBirth": "1981-12-31T00:00:00"
}
```

> Returns `201` if successfully created, `409` if username already exists.