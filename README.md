# weather-api
cd ./WeatherAPI - Navigate to web app source.
dotnet run - Runs source code without any explicit compile or launch commands.
https://localhost:5001/swagger - Open project in browser.
Press "Authorize" button to login. 
Available logins: 
username: reader, password: reader (has role to read endpoints)
username: writer, password: writer (has role to write endpoints)
username: test, password: test (has roles to read|write endpoints)

#GraphQl
url: https://localhost:5001/graphql
example:
{
  cities(cityFilterModel: { 
    name: "Kaunas", 
    pageNumber: 0,
    pageSize: 5
  }){
    id,
    name,
    temperature,
    updated
  }
}