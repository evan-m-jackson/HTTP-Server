# HTTP-Server

This HTTP Server was created using .NET Core 6.0.

To start the server, download the code and save it locally. Next navigate to the directory and then to the project file using the following command:

    cd HTTPServerProject

Then the following command can be entered to start the server:

    dotnet run

## For Developers

In order to run the tests, make sure the terminal is in the main directory and enter the following:

    dotnet test HTTPServerProject.Tests

Alternatively, navigate to the Tests sub-directory and type in the 'dotnet test' command like this:

    cd HTTPServerProject.Tests
    dotnet test

In order to run the acceptance tests, go into the 'http-server-spec' directory [here](https://github.com/evan-m-jackson/HTTP-Server/blob/6a0efb7af2c333f282801496616eb8063dc5b0d9/http-server-spec) and follow the README.