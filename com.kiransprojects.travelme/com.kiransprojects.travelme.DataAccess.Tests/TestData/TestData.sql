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
	'password'
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
	'209F9526-3611-4F30-A79C-55557FFBECF5'
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