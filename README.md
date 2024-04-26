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

![image](https://github.com/travis-mann/cs509_midterm_exam/assets/96485031/f6e13af5-7dcb-4377-8117-206e443695cd)

2. Improve your Class Diagrams (object design)
- [ ] `TODO`

3. What software architecture are you using? e.g. Layered/How many layers? Repository? Something else?

> This design is a 4 layered architechture because there are the following 4 layers which each depend on the next one down and cannot talk to any layer except the one below:
> 1. Program Loop: The simpliest layer, responsible for starting the program and transitioning between elements from the 2nd layer.
> 2. User Interface Layer: Reponsible for managing interactions between the user and persistant data. All CRUD operations are intitiated here and sent to the next layer down via C# calls. This layer is operates independantly from the chosen data storage solution (AKA there is no SQL or Entity frameworks here)
> 3. Data Access Layer: This layer communicates directly with the MySQL database using an entity framework. It is responsible for executing CRUD operations from C# calls at the User Interface Layer. Since every layer is separated by dependancy injection, a different storage solution could be swapped in without modifying any upper layers, as long as a new DAL was included that implemented the same interface.
> 4. Database: Persistant data storage for this program, implemented with MySQL.

5. Refactor your code based on the system and object design.  
> Please see the most recent commits to this repository.

4. Setup and implement unit tests. Note, only test public/internal stuff. Also, setup codecovLinks to an external site. or equivalent. I expect to see 90% code coverage.
> - See the unit test project "ATM.Test.csproj" within this solution for unit test implementations using FluentAssertions, AutoFixture & Moq.
> - Code coverage is pushed during GitHub Actions CI/CD here https://app.codecov.io/gh/travis-mann/cs509_midterm_exam/blob/main/atm%2FUserMenu%2FCreateNewAccountMenuOption.cs
> - Note that the current coverage percentage on Codecov exceeds 90%.

6. Please answer the following: discuss the pros/cons of the environments: VMs, docker, plain old computer. Things to consider: development vs production, working in teams, the OS you need, cost, licensing etc...
> VMs
  > - PROS:
  > - CONS: 

> Docker
  > - PROS: Modern Approach, Lightweight/ Lowest Overhead
  > - CONS: Only runs natively on Linux

> Plain old computer
  > - PROS: 
  > - CONS: 

7. Setup the following (or equivalent):
- [X] EditorConfig
    > https://github.com/RehanSaeed/EditorConfig
- [X] A linter for formatting your code such as: StyleCopAnalyzers, dotnet format,...
    > Run dotnet format --check
- [X] I expect C# documentation of public stuff. Generate class documentation via doxygen, sandcastle, ... 
  - [X] You can generate it as a pdf or setup "read the docs"
        > Documentation generated as an XML with "GenerateDocumentationFile" csproj option, converted to markdown with DefaultDocumentation nuget package and pushed to https://github.com/travis-mann/cs509_midterm_exam/wiki during GitHub actions CI/CD
- [X] Setup a build system such as CAKE, ...
  - [X] At the very least you should just use scripts (PowerShell or bash)
- [X] Setup a CI/CD server such as appveyor, travis-ci,...
- [X] Everytime someone pushes code to github the CI/CD should
  - [X] Run your build script which includes the following (and will fail if things go wrong by notifying the user) 
  - [X] Run StyleCopAnalyzers (or equivalent)
      > Run dotnet format --check w/ the editorconfig used applies stylecop settings  
  - [X] Build the code (release and debug mode)
  - [X] Build the documentation (and publish them somewhere such as "read the docs" or a folder on google drive etc.)
      > Documentation generated as an XML with "GenerateDocumentationFile" csproj option, converted to markdown with DefaultDocumentation nuget package and pushed to https://github.com/travis-mann/cs509_midterm_exam/wiki during GitHub actions CI/CD  
  - [X] Run the unit test(s)
      > dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  --no-build  
      > Coverage report is uploaded to https://app.codecov.io/gh/travis-mann/cs509_midterm_exam
