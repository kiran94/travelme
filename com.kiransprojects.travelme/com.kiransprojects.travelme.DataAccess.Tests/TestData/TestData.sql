DELETE FROM Media 
GO
DELETE FROM Post
GO
DELETE FROM Trip
GO
DELETE FROM UserEntity
GO
INSERT INTO UserEntity 
VALUES
(
	'9fc0b724-d55f-441d-a1ae-ec726d7737f7',
	'Kiran',
	'Patel',
	'19940805 10:00:00 AM',
	'Kiran@test.com',
	'password',
	null
)
GO
INSERT INTO Trip
VALUES
(
	'209F9526-3611-4F30-A79C-55557FFBECF5',
	'Australia',
	'Aussies',
	'Backpacking',
	'9fc0b724-d55f-441d-a1ae-ec726d7737f7'
)
GO
INSERT INTO Trip
VALUES
(
	'6D8BCE5C-5681-472E-A1DC-97C5EA0C23FA',
	'Thailand',
	'Thai!',
	'Backpacking',
	'9fc0b724-d55f-441d-a1ae-ec726d7737f7'
)
GO
INSERT INTO Post
VALUES
(
	'832B97D6-F958-497A-952D-0224F27C4E1A',
	'PostName',
	'Lat',
	'Long',
	'209F9526-3611-4F30-A79C-55557FFBECF5',
	'20120809 10:00:00 AM'
)
GO
INSERT INTO UserEntity
VALUES
(
	'B42B1A1E-9DD5-4904-B7CE-9D55FD9A547A',
	'SecondEntity',
	'SecondEntity',
	'19940805 10:00:00 AM',
	'SecondEntity',
	'SecondEntity',
	null
)
GO
INSERT INTO UserEntity VALUES
(
	'4CB1882A-4285-4D05-972C-0E2A9B97FACB',
	'TripTests',
	'Tests',
	'19940805 10:00:00 AM',
	'email',
	'password',
	null
)

INSERT INTO Trip VALUES
(
	'AD495462-5CB7-4861-8A37-5A0836AA1344',
	'TripTests',
	'UpdatingTests',
	'Tests',
	'4CB1882A-4285-4D05-972C-0E2A9B97FACB'
)
GO
INSERT INTO UserEntity(ID, FirstName, Email, UserPassword) VALUES
(
	'5CAC3D38-E27F-4BF4-9231-2E6E8DC98E8B',
	'IsolatedTrip',
	'Email',
	'Password'
)
GO
INSERT INTO Trip VALUES
(
	'E6339A89-4705-4986-A799-45EB42914FB6',
	'IsolatedTrip',
	'Trip',
	'Isolated',
	'5CAC3D38-E27F-4BF4-9231-2E6E8DC98E8B'
)
GO
INSERT INTO UserEntity(ID, FirstName, Email, UserPassword) VALUES
(
	'2CAF93D9-8548-41E0-916E-098B54308C82',
	'IsolatedTrip',
	'Email',
	'Password'
)
GO
INSERT INTO Trip VALUES
(
	'5D753572-3BC0-46DD-A8B1-B70EBD2FBD6C',
	'IsolatedTrip',
	'Trip',
	'Isolated',
	'2CAF93D9-8548-41E0-916E-098B54308C82'
)