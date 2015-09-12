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
	PostData nvarchar(256) NOT NULL,
	PostLat nvarchar(11),
	PostLong nvarchar(11),
	TripID uniqueidentifier NOT NULL
)
GO
CREATE TABLE Media
(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	MediaData varbinary,
	PostID uniqueidentifier NOT NULL
)
GO
ALTER TABLE Trip 
ADD CONSTRAINT FK_UserEntity FOREIGN KEY(UserID)
REFERENCES UserEntity(ID)
GO
ALTER TABLE Post
ADD CONSTRAINT FK_Trip FOREIGN KEY(TripID)
REFERENCES Trip(ID)
GO
ALTER TABLE Media
ADD CONSTRAINT FK_Post FOREIGN KEY(PostID)
REFERENCES Post(ID)
GO
ALTER TABLE POST
ADD PostDate DATETIME
GO
ALTER TABLE Media
ALTER COLUMN MediaData nvarchar(1000) NOT NULL
GO
ALTER TABLE UserEntity
ALTER COLUMN ProfilePicture nvarchar(100)
GO
CREATE TABLE Logs
(
	ID uniqueidentifier PRIMARY KEY NOT NULL,
	LogMessage nvarchar(255),
	LogDateTime datetime,
	Error bit 
);
GO
ALTER TABLE UserEntity
ADD Role nvarchar(20)