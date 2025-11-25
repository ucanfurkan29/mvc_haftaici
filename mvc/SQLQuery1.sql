Create database SchoolDb
go
use SchoolDb

create Table Students
(
Id int Primary key identity (1,1),
FirstName varchar(50) not null,
LastName varchar(50) not null,
Age int not null
)