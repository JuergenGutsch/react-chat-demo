#addin nuget:?package=Cake.Kudu.Client

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

Task("Build")
	.IsDependentOn("Clean")
	.Does(() => 
	{
		DotNetCoreBuild("./react-chat-demo.sln");
	});

Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
	{
    Information("No tests yet.");
	});

Task("Publish")
	.IsDependentOn("Test")
	.Does(() => 
	{
		DotNetCorePublish("./ReactChatDemo/ReactChatDemo.csproj", 
      new DotNetCorePublishSettings
      {
        Configuration = "Release",
        OutputDirectory = "./publish/ReactChatDemo/"
      });
		DotNetCorePublish("./ReactChatDemoIdentities/ReactChatDemoIdentities.csproj", 
      new DotNetCorePublishSettings
      {
        Configuration = "Release",
        OutputDirectory = "./publish/ReactChatDemoIdentities/"
      });
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