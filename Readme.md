# DogiHub Indexer API

## With Docker
Go to the root of the repository and run following commands

    docker build -t dogihub-indexer-api .
    docker run -d -p 80:8080 -e EnableSwagger=true -e Indexer__RedisConnectionString="host:port,password=yourpass,abortConnect=false" dogihub-indexer-api

omit the -e EnableSwagger=true if you doesn't want to enable the swagger
go to http://localhost to se the app running
if EnableSwagger is true, go to http://localhost/swagger/index.html to see the full swagger

## Without Docker

### Prerequisites
Before to start, ensure you have installed the following:

.NET 8 SDK (check with dotnet --version)

### Installation
Clone the Git repository
Restore the NuGet packages

    dotnet restore

### Configuration

Open the appsettings.json file.
Modify the configuration values for your needs

### Launch the application via the command line:

    dotnet run
	
Once the application is running, you can access the API via the link given by dotnet run command

