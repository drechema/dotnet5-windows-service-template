# Windows Service Template (dotnet)

This is a template for dotnet windows services. This template project use appsettings for configuration (production and development) and serilog for logging (production and development).

The starting point was the dotnet worker template:

```bash
dotnet new worker
```

This project is using dotnet framework version 5, but should works also using version 3. A nice thing of using dotnet framework is that is multplatform, at least for development you can use Mac or linux. The service is going to work starting it from command line (from project directory):

```bash
dotnet run
```

The project will use the `appsettings.json` or `appsettings.Development.json` file depending of the **Environment Mode**. You can set this mode in two different ways:

1. Creating a `Properties\launchSettings.json` file where set `"DOTNET_ENVIRONMENT": "Development"`
2. Defining an setting environment variable `DOTNET_ENVIRONMENT` to `Development`

For easyness this project use the `Properties\launchSettings.json` file

## Deploy in windows

### Publishing

In order to deploy in windows you should compile and publish from windows:

```bash
dotnet publish --configuration Release --output c:\tmp\DemoService
```

This command should create the needed files and the `Workerservice.exe` file (only when publish from windows). For distribution problably is better to use self-contained configuration in order to include all the needed libraries:

```bash
dotnet publish --runtime win-x64 --configuration Release --output c:\tmp\DemoService --self-contained
```

### Define windows service

Form the target machine suppose you have all the files in the directory `c:\tmp\DemoService`. Then open Power Shell as **Administrator** and execute the command from `C:\WINDOWS\system32`:

```bash
 sc.exe create DemoService binpath=  C:\tmp\DemoService\WorkerService.exe start= auto
```

This command will define a new Windows Service `DemoService` visible in the Windows Services Management tool. You can start and stop this service from this tool. Remenber that you have to define the `appsettings.json` file in the servce directory. Also in this file any path that is defined must be a complete path and not relative. This is because the working directory for a Windows service is `C:\WINDOWS\system32`.
