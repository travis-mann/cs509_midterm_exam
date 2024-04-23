# CS509 Midterm Exam
Purpose: Midterm exam for CS509 S24 "Design of Software Systems"

## Setup Instructions:
1. Create a new visual studio project cloned from this repository
2. Update connectionString in app.config with your database connection string using this format "server=\<DATABASE IP OR LOCALHOST\>;uid=\<CONNECTION USERNAME\>;pwd=\<CONNECTION PASSWORD\>;database=atm"
3. Run database_seed.sql in MySQL workbench on the connection configured in step 2.
4. Run atm.
5. Use the credentials devadmin/12345 for an admin user or devcust/12345 for a customer user.

## Final
1. Make a System Design drawing
- [ ] `TODO`

2. Improve your Class Diagrams (object design)
- [ ] `TODO`

3. What software architecture are you using? e.g. Layered/How many layers? Repository? Something else?
- [ ] `TODO`

3. Refactor your code based on the system and object design.
- [ ] `TODO`

4. Setup and implement unit tests. Note, only test public/internal stuff. Also, setup codecovLinks to an external site. or equivalent. I expect to see 90% code coverage.
- [ ] `TODO`

5. Please answer the following: discuss the pros/cons of the environments: VMs, docker, plain old computer. Things to consider: development vs production, working in teams, the OS you need, cost, licensing etc...
- VMs
  - PROS:
  - CONS: 

- Docker
  - PROS:
  - CONS:

- Plain old computer
  - PROS:
  - CONS: 

7. Setup the following (or equivalent):
- [ ] EditorConfig 
- [ ] A linter for formatting your code such as: StyleCopAnalyzers, dotnet format,...
- [ ] I expect C# documentation of public stuff. Generate class documentation via doxygen, sandcastle, ... 
- [ ] You can generate it as a pdf or setup "read the docs"
- [ ] Setup a build system such as CAKE, ...
- [ ] At the very least you should just use scripts (PowerShell or bash)
- [ ] Setup a CI/CD server such as appveyor, travis-ci,...
- [ ] Everytime someone pushes code to github the CI/CD should
- [ ] Run your build script which includes the following (and will fail if things go wrong by notifying the user) 
- [ ] Run StyleCopAnalyzers (or equivalent)
- [ ] Build the code (release and debug mode)
- [ ] Build the documentation (and publish them somewhere such as "read the docs" or a folder on google drive etc.)
- [ ] Run the unit test(s)
