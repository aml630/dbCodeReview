##DB Usage

##Author
Alex Larson

##Project
This project uses the Nancy framework to display data which has been created, saved, updated, and destroyed.  

##Directions

Setup/Installation Requirements

Clone this repository. Use the scripts.sql in the root directory to make the databases. Or follow these commands in SQLCMD/SQL Server to create hair_stylists and hair_stylists_test: CREATE DATABASE hair_stylists; GO CREATE TABLE clients name VARCHAR(255)), id (id INT IDENTITY(1,1), stylist_id int; CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255));  GO Install Nancy the web viewer Build the project using "dnu restore". Run the project by calling "dnx kestrel"

##Description
A user can input any number of hair stylists who can each have any number of clients.  all stylists and clients can have their names changed or removed form the list.    

##Tools Used
C#
Nancy  
Kestrel
DNX451  
Razor
MSSQL

##License
This project is released under the [MIT License]
