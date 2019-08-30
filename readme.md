# Examples.MediaApi

## Task

Create a Web API that when called:
- Calls, combines and returns the results of:
  - http://jsonplaceholder.typicode.com/photos
  - http://jsonplaceholder.typicode.com/albums
- Allows an integrator to filter on the user id – so just returns the albums and photos relevant
to a single user.

## Requirements

- .NET Core 2.2
- Tested on Visual Studio 16.2.3

## Endpoints

### Albums

- **All:** GET: /api/albums
- **Filtered by userId:** GET: /api/albums?userid=7
