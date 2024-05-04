# Environment setup

1. Install visual studio professional, follow instruction: https://statics.teams.cdn.office.net/evergreen-assets/safelinks/1/atp-safelinks.html 
2. Install git https://git-scm.com/download/win 
3. Install net core 3.1 https://aka.ms/dotnet-core-applaunch?framework=Microsoft.NETCore.App&framework_version=3.1.0&arch=x64&rid=win10-x64 
4. Clone repository: https://eysbp.visualstudio.com/eyfabric-business-enablement/_git/microapp-platform-qa-automation
5. Open repository in visual studio
6. Go to Extensions -> Manage Extensions -> Install "SpecFlow for Visual Studio 2022" extension
7. Go to Tools -> Options -> Text editor -> Code Cleanup -> Select "Run Code Cleanup profile on Save" checkbox and click on ok button

# Webdriver versions
Each browser version usually has a corresponding WebDriver version that is compatible with it.
To match the browser version with the WebDriver version, you need to first check the installed version of the browser on your system.

How to verify the browser version?
Chrome - click on three dots on the right top corner and select help -> about google chrome
Edge - click on three dots on the right top corner and select Help and feedback -> about microsoft edge

After checking the browser version, you should download the appropriate version of WebDriver from the official website of the provider.
Make sure you download a WebDriver that is compatible with the browser version installed on your system.

The correct version of webdriver can be checked at the following sites:
- Chrome
https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/
- Edge
https://chromedriver.storage.googleapis.com/


Our configuration supports three methods of WebDriver handling. The "type" field is used for configuring the handling and has three values:
-  auto: This method automatically detects the browser and system versions and downloads the appropriate WebDriver version from the internet. 
Note that this option will not work in environments without internet access.
- manual: This method allows specifying a specific WebDriver version (DriverVersion parameter) for a given system (OS parameter). 
It enables integration with an internal artifact repository and allows downloading the WebDriver binary files from the internet or intranet. 
The URL and binary file retrieval mechanism are implemented in the ChromeDriverConfig and EdgeDriverConfig classes. To use an internal artifact repository, 
you need to modify these classes and provide the correct path to the WebDriver binary files.
- ci: In cases where the previous options for downloading WebDriver binary files are not suitable, the "ci" option can be used. 
This method does not rely on internet or intranet access for downloading the WebDriver. If the "manual" option with an internal repository 
does not work correctly (e.g., due to lack of network access in the test environment), the "ci" option should be used. 
To do this, copy the correct versions of the WebDriver binary files to the "Binary" folder. The process of verifying the browser 
and WebDriver versions is described above.

# Configuration
The configuration is located in Resources/configuration/environment.yaml. The configuration parameters can be overridden from the command line.

The configuration has been divided into three parts:

- Webdriver - contains information about the webdriver configuration
- Environment - contains information about the environment
- Timeouts - contains information about the used timeouts

Webdriver:
- Browser - the name of the browser used for testing. Available values: chrome, edge
- IsHeadless - whether the tests should be run in headless mode, without generating a visible browser window. Tests run faster, but we don't see what's happening in the browser. Values: true/false
If the tests are run in a CI environment, this option should be set to true. Values: true/false
- IsIncognito - whether the browser should be run in incognito mode. Values: true/false
- IsElementHighlightActive - whether the framework should highlight elements on the page when performing actions.
- Type - the method of handling the download of webdriver binary files. More details
- DriverVersion - the version of the webdriver. Example: 113.0.5672.63 More details
- OS - the version of the operating system. Available values: linux, windows More details

Environment:
- name - the name of the environment. This variable is not used anywhere. It can be used to log information about the environment.
- url - the homepage of the tested application

Timeouts:
- ImplicitWaitTimeoutInSeconds - a variable that should be used in ExplicitWait and WebDriverWait.
- PageTimeoutInSeconds - the general timeout for page loading.
- AsynchronousJavaScriptTimeoutInSeconds - the timeout for executing JavaScript scripts.

# Executing tests
- Executing tests in Visual Studio
Visual Studio offers various methods of running tests, such as running a single test, running a test suite, or running tests for a specific project.
We can use the Test Explorer window in Visual Studio to view the list of available tests and select them for execution. Open test explorer by selecting Test -> Test explorer.
Run tests by selecting a test and clicking the green "play" icon.

- Executing tests in command line
Running tests from the command line provides a flexible and efficient way to execute tests.
This approach is particularly useful for integration with automation scripts, build pipelines, and continuous integration/continuous deployment (CI/CD) processes.

Executing all tests:
dotnet test MicroappPlatformQaAutomation.csproj

Executing tests for specific tag:
dotnet test MicroappPlatformQaAutomation.csproj --filter "Category=leftMenu"

Executing multiple tags:
dotnet test MicroappPlatformQaAutomation.csproj --filter "Category=leftMenu & Category=hamburgerMenu"

Executing specific feature:
dotnet test MicroappPlatformQaAutomation.csproj --filter "Feature=featureName"

More details:
https://docs.specflow.org/projects/specflow/en/latest/Execution/Executing-Specific-Scenarios.html 

# Running tests from commandline by using nunit
By using nunit3-console.exe, you can efficiently run NUnit tests from the command line and easily customize your test execution. With options for selecting specific tests, controlling output, and passing custom parameters

Before using nunit3-console.exe, ensure that you have installed NUnit Console Runner on your system.
Check if nunit3-console.exe exist in C:\Users\youruser\.nuget\packages\nunit.consolerunner\3.16.3\tools
Binary file should exist because it is added as dependency to test automation project. If not then try to build project and
nuget will automatically install it.

To run tests using nunit3-console.exe, open a command prompt or terminal and navigate to the directory where nunit3-console.exe is located. Then, execute the following command: nunit3-console.exe <path_to_test_assembly>. Replace <path_to_test_assembly> with the path to your test assembly (DLL file). This command will execute all the tests in the specified assembly.

Selecting Tests:
You can further refine the selection of tests to be executed by using the --where option. This option allows you to specify a filter expression based on test names, categories, or other attributes. For example: nunit3-console.exe <path_to_test_assembly> --where "cat == MyCategory" This command will only run tests with the tag "MyCategory". You can customize the filter expression according to your specific requirements.
More about selecting tests: https://docs.nunit.org/articles/nunit/running-tests/Test-Selection-Language.html 

Custom Parameters:
NUnit Console Runner allows you to pass custom parameters to your tests. You can define and use these parameters within your test code to control the behavior of your tests. To pass custom parameters, use the --params option followed by key-value pairs. For example:
nunit3-console.exe <path_to_test_assembly> --params:Key1=Value1;Key2=Value2
In test code, you can access these parameters using the TestContext.Parameters property.

Overriding configuration by passign parameters to nunit console:
Running tests in edge browser:
"C:\Users\yourusername\.nuget\packages\nunit.consolerunner\3.16.3\tools\nunit3-console.exe" "C:\Users\yourusername \workspaces\microapp-platform-qa-automation\microapp-platform-qa-automation\bin\Debug\netcoreapp3.1 MicroappPlatformQaAutomation.dll" **--params:browser=edge**
Parameters that can be overriden: browser, headless, incognito, highlightElement, pageTimeout, implicitTimeout, scriptTimeout, url, type, driverVersion, os