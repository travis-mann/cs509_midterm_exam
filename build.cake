var defaultName = "Default";
var restoreName = "Restore";
var styleAnalyzerName = "StyleAnalyzer";
var buildName = "Build";
var staticAnalyzerName = "StaticAnalyzer";
var testName = "Test";
var cleanName = "Clean";

var target = Argument("target", defaultName);
var configuration = Argument("configuration", "Release");
var solutionPath = "./atm.sln";
var outputPath = "./output";
var projectPath = "./atm/atm.csproj";
var testProjectPath = "./atm.Test/atm.Test.csproj";


Task(restoreName).Does(() => { DotNetRestore(solutionPath); });

Task(buildName).Does(() =>
{
	DotNetBuild(solutionPath, new DotNetBuildSettings
	{
		NoRestore = true,
		Configuration = configuration,
		OutputDirectory = outputPath
	});
});


Task(staticAnalyzerName).IsDependentOn(buildName)
.Does(() =>
{
	DotNetFormatAnalyzers(solutionPath);
});

Task(testName).IsDependentOn(staticAnalyzerName).Does(() =>
{
	DotNetTest(testProjectPath);
});

Task(cleanName).IsDependentOn(testName).Does(() =>
{
	CleanDirectory(outputPath);
});

Task(defaultName).IsDependentOn(cleanName);

RunTarget(target);