[![codecov](https://codecov.io/gh/travis-mann/cs509_midterm_exam/graph/badge.svg?token=192SCO4JL4)](https://codecov.io/gh/travis-mann/cs509_midterm_exam)

# CS509 Midterm/ Final Exam
Purpose: Midterm/ Final exam for CS509 S24 "Design of Software Systems"

## Setup Instructions:
1. Create a new visual studio project cloned from this repository
2. Update connectionString in app.config with your database connection string using this format "server=\<DATABASE IP OR LOCALHOST\>;uid=\<CONNECTION USERNAME\>;pwd=\<CONNECTION PASSWORD\>;database=atm"
3. Run database_seed.sql in MySQL workbench on the connection configured in step 2.
4. Run atm.
5. Use the credentials devadmin/12345 for an admin user or devcust/12345 for a customer user.

## Final
1. Make a System Design drawing

![image](https://github.com/travis-mann/cs509_midterm_exam/assets/96485031/9e7407e1-9255-4f02-b309-90c29686b313)

2. Improve your Class Diagrams (object design)

![image](https://github.com/travis-mann/cs509_midterm_exam/assets/96485031/33331820-2435-471a-9a8c-1a5a10a5e2cd)

3. What software architecture are you using? e.g. Layered/How many layers? Repository? Something else?

> This design is a 4 layered architechture because there are the following 4 layers which each depend on the next one down and cannot talk to any layer except the one below:
> 1. Program Layer: The simpliest layer, responsible for starting the program and transitioning between elements from the 2nd layer.
> 2. User Interface Layer: Reponsible for managing interactions between the user and persistant data. All CRUD operations are intitiated here and sent to the next layer down via C# calls. This layer is operates independantly from the chosen data storage solution (AKA there is no SQL or Entity frameworks here)
> 3. Data Access Layer: This layer communicates directly with the MySQL database using an entity framework. It is responsible for executing CRUD operations from C# calls at the User Interface Layer. Since this layer is separated by dependancy injection, a different storage solution could be swapped in without modifying any upper layers, as long as a new DAL was included that implemented the same IAccountDAL interface.
> 4. Persistant Data Storage Layer: Persistant data storage for this program, implemented with MySQL.

5. Refactor your code based on the system and object design.  
> Please see the most recent commits to this repository.

4. Setup and implement unit tests. Note, only test public/internal stuff. Also, setup codecovLinks to an external site. or equivalent. I expect to see 90% code coverage.
> - See the unit test project "ATM.Test.csproj" within this solution for unit test implementations using FluentAssertions, AutoFixture & Moq.
> - Code coverage is pushed during GitHub Actions CI/CD here https://app.codecov.io/gh/travis-mann/cs509_midterm_exam
> - Note that the current coverage percentage on Codecov exceeds 90%.

![image](https://github.com/travis-mann/cs509_midterm_exam/assets/96485031/0191c6d6-d394-4777-95b6-2df7ddec7cdf)

6. Please answer the following: discuss the pros/cons of the environments: VMs, docker, plain old computer. Things to consider: development vs production, working in teams, the OS you need, cost, licensing etc...
> VMs
  > - PROS: Isolated and easily reproducable environments. VM images can be created and shared across a team to ensure the same development environment with no additional configurations. Similarly for deployment, you can utilize the same base VM image used in development to minimze the amount of custom configuration. VMs can run with any OS / on any host OS and there are various VM offerings, some of which have licening fees and others which are completely free.
  > - CONS: A virtual environment does not use host OS resources as efficiently as docker. Additionally using a VM over plain old computers introduces additional complexity to the system with the VM software, any required configurations and consideration for where VMs are hosted.

> Docker
  > - PROS: Docker is the most modern deployment approach which creates isolated environments in the form of containers that efficiently utilizize the resources from their host os. This is the easiest option for deployment and working in teams because the container you develop in is the exact same container that is shared and deployed into production. The base Docker technology is completely free for command line use.
  > - CONS: While there are Windows and Linux containers available, Windows containers are "second-class citizens" and therefore most projects should be modified to run in a linux container. Additionally, the docker desktop gui application for Docker does require a licencing fee for enterprise applications.

> Plain old computer
  > - PROS: This is the easiest deployment option from a configuration standpoint and does not require any specific OS, costs or licencing beyond what is required for the project and machine itself.
  > - CONS: This option is the hardest to utilize across teams and for smooth deployments since it lacks an isolated/ easily reproducable environment. Each environment created on a plain old computer is essentially custom and must rely on build scripts or similar approaches to ensure the same environment is created across different machines.

7. Setup the following (or equivalent):
- [X] EditorConfig
    > https://github.com/RehanSaeed/EditorConfig
- [X] A linter for formatting your code such as: StyleCopAnalyzers, dotnet format,...
    > dotnet format --verify-no-changes
- [X] I expect C# documentation of public stuff. Generate class documentation via doxygen, sandcastle, ... 
  - [X] You can generate it as a pdf or setup "read the docs"
        > Documentation generated as an XML with "GenerateDocumentationFile" csproj option, converted to markdown with DefaultDocumentation nuget package and pushed to https://github.com/travis-mann/cs509_midterm_exam/wiki during GitHub actions CI/CD
- [X] Setup a build system such as CAKE, at the very least you should just use scripts (PowerShell or bash)
    > Cake Build Script: https://github.com/travis-mann/cs509_midterm_exam/blob/main/build.cake
- [X] Setup a CI/CD server such as appveyor, travis-ci,...
    > GitHub Actions: https://github.com/travis-mann/cs509_midterm_exam/blob/main/.github/workflows/github_actions.yml
- [X] Everytime someone pushes code to github the CI/CD should
  - [X] Run your build script which includes the following (and will fail if things go wrong by notifying the user)
      > CI/CD emulates the same actions from the build script which was a cleaner approach because it allows the CI/CD pipeline to pass or fail each individual step.
  - [X] Run StyleCopAnalyzers (or equivalent)
      > Run dotnet format --check w/ the editorconfig used applies stylecop settings  
  - [X] Build the code (release and debug mode)
      > dotnet build /p:RunCodeAnalysis=true --configuration Debug  
      > dotnet build /p:RunCodeAnalysis=true --configuration Release
  - [X] Build the documentation (and publish them somewhere such as "read the docs" or a folder on google drive etc.)
      > Documentation generated as an XML with "GenerateDocumentationFile" csproj option, converted to markdown with DefaultDocumentation nuget package and pushed to https://github.com/travis-mann/cs509_midterm_exam/wiki during GitHub actions CI/CD  
  - [X] Run the unit test(s)
      > dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  --no-build  
      > Coverage report is uploaded to https://app.codecov.io/gh/travis-mann/cs509_midterm_exam
