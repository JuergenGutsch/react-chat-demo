#addin nuget:?package=Cake.Kudu.Client

/*
$env:KUDU_CLIENT_BASEURI_APP = "https://cake-demo.scm.azurewebsites.net:443"
$env:KUDU_CLIENT_USERNAME_APP = "`$cake-demo"
$env:KUDU_CLIENT_PASSWORD_APP = ""
*/ 

string  baseUriApp     = EnvironmentVariable("KUDU_CLIENT_BASEURI_APP"),
        userNameApp    = EnvironmentVariable("KUDU_CLIENT_USERNAME_APP"),
		    passwordApp    = EnvironmentVariable("KUDU_CLIENT_PASSWORD_APP"),
        baseUriIdent   = EnvironmentVariable("KUDU_CLIENT_BASEURI_IDENT"),
        userNameIdent  = EnvironmentVariable("KUDU_CLIENT_USERNAME_IDENT"),
		    passwordIdent  = EnvironmentVariable("KUDU_CLIENT_PASSWORD_IDENT");;

var target = Argument("target", "Default");

Task("Clean")
  .Does(() =>
  {	
    DotNetCoreClean("./react-chat-demo.sln");
    CleanDirectory("./publish/");
  });

Task("Restore")
	.IsDependentOn("Clean")
	.Does(() => {
		DotNetCoreRestore("./react-chat-demo.sln");
	});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() => 
	{
		var settings = new DotNetCoreBuildSettings
		{
			NoRestore = true,
			Configuration = "Release"
		};
		DotNetCoreBuild("./react-chat-demo.sln", settings);
	});

Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
	{
		var settings = new DotNetCoreTestSettings
		{
			NoBuild = true,
			Configuration = "Release",
			NoRestore = true
		};
		var testProjects = GetFiles("./**/*.Tests.csproj");
		foreach(var project in testProjects)
		{
				DotNetCoreTest(project.FullPath, settings);
		}
	});

Task("Publish")
	.IsDependentOn("Test")
	.Does(() => 
	{
		var settings = new DotNetCorePublishSettings
		{
			Configuration = "Release",
			OutputDirectory = "./publish/ReactChatDemo/",
			NoRestore = true
		};
		DotNetCorePublish("./ReactChatDemo/ReactChatDemo.csproj", settings);
		settings.OutputDirectory = "./publish/ReactChatDemoIdentities/";
		DotNetCorePublish("./ReactChatDemoIdentities/ReactChatDemoIdentities.csproj", settings);
	});

Task("Deploy")
	.IsDependentOn("Publish")
	.Does(() => 
	{
		var kuduClient = KuduClient(
			 baseUriApp,
			 userNameApp,
			 passwordApp);
		var sourceDirectoryPath = "./publish/ReactChatDemo/";
		var remoteDirectoryPath = "/site/wwwroot/";

		kuduClient.ZipUploadDirectory(
			sourceDirectoryPath,
			remoteDirectoryPath);

		kuduClient = KuduClient(
			 baseUriIdent,
			 userNameIdent,
			 passwordIdent);
		sourceDirectoryPath = "./publish/ReactChatDemoIdentities/";
		remoteDirectoryPath = "/site/wwwroot/";

		kuduClient.ZipUploadDirectory(
			sourceDirectoryPath,
			remoteDirectoryPath);
	});

Task("Default")
	.IsDependentOn("Publish")
  .Does(() =>
  {
    Information("Your build is done :-)");
  });

RunTarget(target);