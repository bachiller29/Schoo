create database ShoolDb;
go

Use ShoolDb;
Go

Create Table Students(
	Id int identity (1,1) primary key,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) null,
	BirthDate Date not null,
	Email nvarchar(150) unique not null,
	CreatedAt datetime default getdate(),
	UpdatedAt datetime null
);

Create Table Teachers(
	Id int identity (1,1) primary key,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) null,	
	Email nvarchar(150) unique not null,
	Specialty nvarchar(100) null,
	CreatedAt datetime default getdate(),
	UpdatedAt datetime null
);

Create Table Courses(
	Id int identity (1,1) primary key,
	CourseName nvarchar(100) not null,
	Description nvarchar(500) null,
	TeacherId int not null,
	CreatedAt datetime default getdate(),
	UpdatedAt datetime null,
	constraint fk_Courses_Teachers foreign key (TeacherId) references Teachers(Id)
);

Create Table Qualifications(
	Id int identity (1,1) primary key,
	StudentId int not null,
	CourseId int not null,
	Score decimal(5,2) check (Score between 0 and 100),
	QualificationDate date not null default getdate(),
	CreatedAt datetime default getdate(),
	UpdatedAt datetime null,
	constraint fk_Qualifications_Students foreign key (StudentId) references Students(Id),
	constraint fk_Qualifications_Courses foreign key (CourseId) references Courses(Id)
);