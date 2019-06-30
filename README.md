# Rezultate Vot 

[![GitHub contributors](https://img.shields.io/github/contributors/code4romania/rezultate-vot.svg?style=for-the-badge)](https://github.com/code4romania/rezultate-vot/graphs/contributors) [![GitHub last commit](https://img.shields.io/github/last-commit/code4romania/rezultate-vot.svg?style=for-the-badge)](https://github.com/code4romania/rezultate-vot/commits/master) [![License: MPL 2.0](https://img.shields.io/badge/license-MPL%202.0-brightgreen.svg?style=for-the-badge)](https://opensource.org/licenses/MPL-2.0)

This project parses results published by BEC (Biroul Electoral Central) and provides real time partial results for elections in Romania.

[See the project live](insert_link_here)

The partial results published by BEC (Biroul Electoral Central) are often raw results that are not easily interpreted and give no meaningful information to regular users. The aim of the project is to aggregate the raw data and provide timely updates on the progression of voting results in Romanian elections.

[Contributing](#contributing) | [Built with](#built-with) | [Repos and projects](#repos-and-projects) | [Deployment](#deployment) | [Feedback](#feedback) | [License](#license) | [About Code4Ro](#about-code4ro)

## Contributing

This project is built by amazing volunteers and you can be one of them! Here's a list of ways in [which you can contribute to this project](.github/CONTRIBUTING.MD).

You can also list any pending features and planned improvements for the project here.

## Built With

### Programming languages

C# 7

### Platforms

.NET Core 2.1

### Package managers

NuGet

### Database technology & provider

Azure Blob Storage

Azure Table Storage

## Repos and projects

TBD

## Deployment

#### Requirements

##### For Visual Studio
- Install the [Azure Functions workload](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs)
  - It also includes the Azure Storage Emulator
- Install [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)
- Select ElectionResults.DataProcessing as startup project and run it

##### For Visual Studio Code
- Install the [Azure Functions CLI](https://github.com/Azure/azure-functions-core-tools)
- Install [Azure Storage Emulator](https://go.microsoft.com/fwlink/?linkid=717179&clcid=0x409) (for Linux use [Azurite](https://github.com/azure/azurite))
- Install [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/) 
- cd src\ElectionResults.DataProcessing
- func start

By default, the functions are configured to use local storage, this can be changed in local.settings.json by modifying the AzureWebJobsStorage key with a valid Azure Storage Key.
To see the blob container where the CSV files are downloaded, open **Azure Storage Explorer** then navigate to **Local & Attached** -> **Storage Accounts** -> **Emulator** -> **Blob Containers**

To see the table where the processed results are stored open **Azure Storage Explorer** then navigate to **Local & Attached** -> **Storage Accounts** -> **Emulator** -> **Tables**

#### Settings
Most of the settings are stored in local.settings.json and the file is not ignored because it doesn't contain sensitive data.
In this file you'll find the following settings:
- **AzureWebJobsStorage**: "UseDevelopmentStorage=true"
  - uses the local storage emulator
- **ScheduleTriggerTime**: "0 */5 * * * *"
  - runs the CSV downloader function every 5 minutes
- **BlobContainerName**: "election-results"
  - the name of the blob container where CSV files are downloaded
- **AzureTableName**: "election-statistics"
  - the name of the Azure Table where the JSON statistics are stored
  
If you want to use your own Azure Storage account for development, you should follow the following steps:
- Create a file named ["secrets.settings.json"](https://www.tomfaltesek.com/azure-functions-local-settings-json-and-source-control/) in the folder ElectionResults.DataProcessing
- This file is ignored so you can include credentials in it. If you open FunctionSettings.cs you will see that this file overrides local.settings.json and the environment variables
- Add a "AzureWebJobsStorage" key and set the connection string as the value
Example:

`
{
  "AzureWebJobsStorage": "<your connection string>"
}
`

#### Run unit tests
- cd tests\ElectionResults.Tests
- dotnet test


## Feedback

* Request a new feature on GitHub.
* Vote for popular feature requests.
* File a bug in GitHub Issues.
* Email us with other feedback contact@code4.ro

## License 

This project is licensed under the MPL 2.0 License - see the [LICENSE](LICENSE) file for details

## About Code4Ro

Started in 2016, Code for Romania is a civic tech NGO, official member of the Code for All network. We have a community of over 500 volunteers (developers, ux/ui, communications, data scientists, graphic designers, devops, it security and more) who work pro-bono for developing digital solutions to solve social problems. #techforsocialgood. If you want to learn more details about our projects [visit our site](https://www.code4.ro/en/) or if you want to talk to one of our staff members, please e-mail us at contact@code4.ro.

Last, but not least, we rely on donations to ensure the infrastructure, logistics and management of our community that is widely spread across 11 timezones, coding for social change to make Romania and the world a better place. If you want to support us, [you can do it here](https://code4.ro/en/donate/).
