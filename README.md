# Digital First Careers - Find a Course Client

This project provides a find a course client which allows courses to be searched and returned for a give set of keywords.
Its uses the WFC find a course service as its data source.


## Getting Started

This is a self-contained Visual Studio 2019 solution containing a number of projects (web application, service and repository layers, with associated unit test and integration test projects).

### Installing

Clone the project and open the solution in Visual Studio 2019.

## List of dependencies

|Item	|Purpose|
|-------|-------|
|Azure Cosmos DB | Document storage |
|Find a Course|Service to search and extract courses|

## Local Config Files

Once you have cloned the public repo you need to rename the appsettings files by removing the -template part from the configuration file names listed below.

| Location | Repo Filename | Rename to |
|-------|-------|-------|
| DFC.App.CurrentOppotunities.IntegrationTests | appsettings-template.json | appsettings.json |
| DFC.App.CurrentOppotunities | appsettings-template.json | appsettings.json |

## Configuring to run locally

The project contains a number of "appsettings-template.json" files which contain sample appsettings for the web app and the integration test projects. To use these files, rename them to "appsettings.json" and edit and replace the configuration item values with values suitable for your environment.

By default, the appsettings include a local Azure Cosmos Emulator configuration using the well known configuration values. These may be changed to suit your environment if you are not using the Azure Cosmos Emulator. 
In addition -


### Course search client settings will need to be edited.

|File                                       |Setting                        |Example value                      |
|------------------------------------------|------------------------------|----------------------------------|
| appsettings.json     | CourseSearchSvc.ServiceEndpoint      | https://localhost:8090 |
| appsettings.json     | CourseSearchSvc.APIKey      | 55e116d6-2f64-47ae-b753-468ed36d7827 |
| appsettings.json     | Configuration.CourseSearchClient.CosmosAuditConnection.EndpointUrl      | https://localhost:8081 |

The Find a Course API source can be found here https://github.com/SkillsFundingAgency/dfc-providerportal-findacourse
## Deployments

This package can be used as part of a larger solution that need to search for courses.
Using this package will simplify the interactions with the backend API that is provided by the find a course service.

To use this package you will need to supply configuration settings from the hosting app.
For a example of how to do this please take a look at the Intergration Test that is part of this solution.

If a logger is not injected in then the package will not do any logging.
If settings for the Cosmos auditing container are not supplied then there will be no auditing.

        public void GetCoursesAsync()
        {
            var configuration = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .Build();

            var courseSearchClientSettings = new CourseSearchClientSettings
            {
                CourseSearchSvcSettings = configuration.GetSection("Configuration:CourseSearchClient:CourseSearchSvc").Get<CourseSearchSvcSettings>() ?? new CourseSearchSvcSettings(),
                CourseSearchAuditCosmosDbSettings = configuration.GetSection("Configuration:CourseSearchClient:CosmosAuditConnection").Get<CourseSearchAuditCosmosDbSettings>() ?? new CourseSearchAuditCosmosDbSettings(),
            };

            var serviceProvider = new ServiceCollection()
                .AddSingleton(courseSearchClientSettings)
                .AddSingleton<ICourseSearchClient, CourseSearchClient>()
                .AddFindACourseServices(courseSearchClientSettings)
                .BuildServiceProvider();

            var courseSearchClient = serviceProvider.GetService<ICourseSearchClient>();
            var results = courseSearchClient.GetCoursesAsync(configuration.GetSection("CourseSearch:KeyWordsForTest").Get<string>());

            results.Result.Count().Should().BeGreaterThan(0);
        }

## Using this package

In your application startup, you must instead manually add your Automapper profiles **as well as the FindACourseProfile()**:

			services.AddSingleton(serviceProvider =>
                 return new MapperConfiguration(cfg =>
                 {
                     cfg.AddProfiles(
                         new List<Profile>
                         {
                            new SomeProfile1(),
                            new SomeProfile2(),
                            new SomeProfile3(),
							new FindACourseProfile(),
                         });
                 }).CreateMapper();
             });

## Built With

* Microsoft Visual Studio 2019
* .Net Core 2.2

