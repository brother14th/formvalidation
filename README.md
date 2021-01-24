# User Form Validation API

This is a user form validation API. It's written in C#. 

FluentValidation library is used to build the validation rules.

User form validation  

| Fields     | Constraints                                                  |
| :--------- | ------------------------------------------------------------ |
| firstName  | 5 - 10 alphabetical characters                               |
| lastName   | 5 - 10 alphabetical characters                               |
| address    | 10 - 100 alpha numeric characters                            |
| age        | Positive numeric number. 0<age<150                           |
| parentName | 5 - 10 alphabetical characters, only required if age is less than 18 |
| email      | Valid email address. Max. length is 150 chars.               |
| webpage    | Valid URL. Max. length is 2048 chars                         |

Note: All the fields are mandatory. 



## Steps to run the API

1. Download and rebuild the solution in Visual Studio 2019
2. Start the UserApp.Api without debugging 

# REST API/Azure Function

The REST API  is described below.

## Validate User Form

- To validate user's form data

### Request (Valid user form data)

`POST /api/validate/userform`

    curl -i -X POST -H "Content-Type: application/json"  -d "{\"firstName\":\"abcde\", \"lastName\":\"uvwxyz\", \"address\":\"123Clementi\", \"age\":\"16\", \"parentName\":\"parentabc\",\"email\":\"test@test.com\","\"website\":\"http://www.google.com\""}" http://localhost:7071/api/validate/userform

### Response

    HTTP/1.1 200 OK
    Date: Sun, 24 Jan 2021 15:49:32 GMT
    Content-Type: application/json; charset=utf-8
    Server: Kestrel
    Content-Length: 32
    
    {"type":"Success","messages":[]}

### Request (Invalid user form data)

`POST /api/validate/userform`

    curl -i -X POST -H "Content-Type: application/json"  -d "{\"firstName\":\"abc\", \"lastName\":\"xyz\", \"address\":\"add1\", \"age\":\"16\", \"parentName\":\"\",\"email\":\"testAttest.com\","\"website\":\"mywebsite\""}" http://localhost:7071/api/validate/userform

### Response

    HTTP/1.1 400 Bad Request
    Date: Sun, 24 Jan 2021 15:54:04 GMT
    Content-Type: application/json; charset=utf-8
    Server: Kestrel
    Content-Length: 602
    
    {"type":"Error","messages":[{"field":"FirstName","error":"'First Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!"},{"field":"LastName","error":"'Last Name' should only contain alphabetical characters! Min. length is 5 and max. length is 10!"},{"field":"Address","error":"'Address' should only contain alphanumeric characters! Min. length is 10 and max. length is 100!"},{"field":"ParentName","error":"'Parent Name' must not be empty."},{"field":"Email","error":"'Email' is not a valid email address."},{"field":"Website","error":"'Website' URL is invalid!"}]}






