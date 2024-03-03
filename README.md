# CS509 Midterm Exam
Purpose: Midterm exam for CS509 S24 "Design of Software Systems"

## Setup Instructions:
1. Create a new visual studio project cloned from this repository
2. Update connectionString in app.config with your database connection string using this format "server=\<DATABASE IP OR LOCALHOST\>;uid=\<CONNECTION USERNAME\>;pwd=\<CONNECTION PASSWORD\>;database=atm"
3. Run database_seed.sql in MySQL workbench on the connection configured in step 2.
4. Run atm.
5. Use the credentials devadmin/12345 for an admin user or devcust/12345 for a customer user.
