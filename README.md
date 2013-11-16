ResolutionActionSystem
======================

This project allows a user to capture information regarding meetings that have
taken place in a business or department.

This project primarily demonstrates the use of Entity Framework and WPF.

Requires SQL Server Express

Some functionality has been omitted due to the nature of the exercise.
For this reason it is necessary to run some database scripts to populate the 
the database with some starting data.

..\ResolutionActionSystemDatabaseLayer\Scripts\Pre-Deployment
  - CreateDatabase.sql
  - CreateTables.sql
  - DataScript.sql
  
First script will generate a database on the local SQL Server Express instance.
Second script will generate the tables.
Third script will populate the database with data for the application to work with.


