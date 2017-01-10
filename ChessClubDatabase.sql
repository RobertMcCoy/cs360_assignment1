CREATE TABLE Students (
	StudentId int NOT NULL IDENTITY PRIMARY KEY,
	StudentFname nvarchar(50),
	StudentLname nvarchar(50),
	StudentDivision char(1),
)
GO

CREATE TABLE Matches (
	MatchId int NOT NULL IDENTITY,
	Student1Id int NOT NULL FOREIGN KEY REFERENCES Students(StudentId),
	Student2Id int NOT NULL FOREIGN KEY REFERENCES Students(StudentId),
	MatchDate DateTime,
	MatchWinner int
)
GO

INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Stuart','Papadopoulos','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Mittie','Pomerantz','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Drew','Henry','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Marget','Faller','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Harrison','Gillen','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Kiana','Samms','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Davis','Yelle','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Hassan','Rankins','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Sharonda','Zehr','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Valda','Waddy','A')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Donna','Redel','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Charles','Landin','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Joann','Patton','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Krysta','Malbon','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Merlyn','Hanlin','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Vikki','Pavlik','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Stephenie','Yeh','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Domingo','Brittan','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Cleta','Goering','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Melodi','Troche','B')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Man','Weick','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Cara','Lei','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Leland','Hollenbeck','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Andres','Dinsmore','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Marcellus','Gerner','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Miyoko','Steven','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Robbyn','Stetz','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Marica','Rain','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Jesse','Bradeen','C')
INSERT INTO Students(StudentFname,StudentLname,StudentDivision) VALUES ('Berta','Zimmer','C')
GO
