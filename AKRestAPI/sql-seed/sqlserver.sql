CREATE DATABASE AKTestDataBase;
GO
USE AKTestDataBase;
GO
CREATE TABLE dbo.people(id INT NOT NULL PRIMARY KEY, name [VARCHAR](64), enabled [VARCHAR](16));
GO
INSERT INTO people([id],[name],[enabled]) VALUES (1,N'Waldo',N'True'), (2,N'Paul',N'True'), (3,N'Devin',N'True'), (4,N'Thor',N'False');
GO
SELECT * FROM people;
GO