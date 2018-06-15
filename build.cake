var target = Argument("target", "CreateInstall");
var config = "Release";
var createCommunityPackages = "./Build/BuildScripts/CreateCommunityPackages.build";
var buildNumber = "9.2.2";

Task("CompileSource")
	.Does(() =>
{
	MSBuild(createCommunityPackages, c =>
	{
		c.Configuration = config;
		c.WithProperty("BUILD_NUMBER", buildNumber);
		c.Targets.Add("CompileSource");
	});
});

Task("CreateInstall")
	.IsDependentOn("CompileSource")
	.Does(() =>
{
	MSBuild(createCommunityPackages, c =>
	{
		c.Configuration = config;
		c.WithProperty("BUILD_NUMBER", buildNumber);
		c.Targets.Add("CreateInstall");
	});
});

RunTarget(target);