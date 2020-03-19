# Pet Store
A console application written in C# .Net Core to display a list of pets.

## Problem Statement
1. Use the swagger documentation [https://petstore.swagger.io/](https://petstore.swagger.io/)
2. Develop a console application to execute the required sample API(s) to return a list of available Pets from the Pet Store  
3. Print a list of available Pets to the console sorted in Categories and displayed in reverse order by Name

## Prequisites
- [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
- [.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Instructions
Open the solution in Visual Studio and run the **PetStore.StoreConsole** project to view the final output.

## Notes
- The Visual Studio Extension [REST API Client Code Generator](https://marketplace.visualstudio.com/items?itemName=ChristianResmaHelle.APIClientCodeGenerator) was used to generate
PetStore.Services/PetstoreClient.cs with the *NSwagCodeGenerator* tool
- Categories and pet names were both sorted in descending order
- Pets with a null Category are placed in a default "Other" category
