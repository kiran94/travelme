CREATE DATABASE travelme;
GO
CREATE TABLE UserEntity
(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	FirstName nvarchar(100) NOT NULL,
	LastName nvarchar(100),
	DateOfBirth datetime,
	Email nvarchar(500) NOT NULL,
	UserPassword nvarchar(128) NOT NULL,
	ProfilePicture varbinary
)
GO
CREATE TABLE Trip
(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	TripName nvarchar(20) NOT NULL,
	TripDescription nvarchar(50),
	TripLocation nvarchar(75),
	UserID uniqueidentifier NOT NULL
)
GO
CREATE TABLE Post
(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Post nvarchar(256) NOT NULL,
	PostLat nvarchar(11),
	PostLong nvarchar(11),
	TripID uniqueidentifier NOT NULL
)
GO
CREATE TABLE Media
(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	Media varbinary,
	PostID uniqueidentifier NOT NULL
)