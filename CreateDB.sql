create extension if not exists "UUID-OSSP";
select uuid_generate_v4();


create table "User"
(
	"Id" uuid primary key not null,
	"FirstName" varchar(50),
	"LastName" varchar(50),
	"Email" varchar(50) not null,
	"DateOfBirth" TIMESTAMP,
	"Password" varchar(50) not null,
	"IsActive" bool null,
	"UpdatedByUserId" uuid null,	
	"DateCreated" timestamp null,
	"DateUpdated" timestamp null,
	"RoleId" uuid,
	constraint "FK_User_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id")
)

INSERT INTO "User" ("Id", "FirstName", "LastName", "Email", "DateOfBirth", "Password", "IsActive", "UpdatedByUserId", "DateCreated", "DateUpdated", "RoleId")
VALUES
	(uuid_generate_v4(), 'John', 'Doe', 'johndoe@example.com', '1990-01-01', 'password123', true, NULL, '2023-06-01 09:00:00', '2023-06-02 14:30:00', 'a12a817d-d714-4e6c-8e1b-654f1f2e6c3f'), -- Administrator
    (uuid_generate_v4(), 'Ava', 'Wilson', 'avawilson@example.com', '1993-02-18', 'guestpassword6', true, NULL, '2023-06-11 10:00:00', '2023-06-12 11:30:00', 'c56d2734-8d6f-49b9-83e1-5d1f2f717f16'), -- Guest
    (uuid_generate_v4(), 'Mia', 'Martinez', 'miamartinez@example.com', '1991-07-29', 'guestpassword7', true, NULL, '2023-06-13 13:45:00', '2023-06-14 09:15:00', 'c56d2734-8d6f-49b9-83e1-5d1f2f717f16'), -- Guest
    (uuid_generate_v4(), 'Lucas', 'Garcia', 'lucasgarcia@example.com', '1996-04-12', 'guestpassword8', true, NULL, '2023-06-15 15:30:00', '2023-06-16 10:45:00', 'c56d2734-8d6f-49b9-83e1-5d1f2f717f16'), -- Guest
    (uuid_generate_v4(), 'Liam', 'Rodriguez', 'liamrodriguez@example.com', '1998-11-05', 'guestpassword9', true, NULL, '2023-06-17 12:15:00', '2023-06-18 14:00:00', 'c56d2734-8d6f-49b9-83e1-5d1f2f717f16'), -- Guest
    (uuid_generate_v4(), 'Emma', 'Johnson', 'emmajohnson@example.com', '1990-12-15', 'userpassword1', false, NULL, '2023-06-10 09:00:00', '2023-06-10 09:00:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'Oliver', 'Williams', 'oliverwilliams@example.com', '1992-06-27', 'userpassword2', false, NULL, '2023-06-11 10:30:00', '2023-06-11 10:30:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'Sophia', 'Brown', 'sophiabrown@example.com', '1995-02-03', 'userpassword3', false, NULL, '2023-06-12 13:15:00', '2023-06-12 13:15:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'William', 'Taylor', 'williamtaylor@example.com', '1993-09-19', 'userpassword4', false, NULL, '2023-06-13 15:45:00', '2023-06-13 15:45:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'Isabella', 'Jones', 'isabellajones@example.com', '1991-03-07', 'userpassword5', false, NULL, '2023-06-14 17:30:00', '2023-06-14 17:30:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'James', 'Davis', 'jamesdavis@example.com', '1989-07-22', 'userpassword6', false, NULL, '2023-06-15 11:45:00', '2023-06-15 11:45:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'Emily', 'Wilson', 'emilywilson@example.com', '1994-05-11', 'userpassword7', false, NULL, '2023-06-16 14:30:00', '2023-06-16 14:30:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'Benjamin', 'Miller', 'benjaminmiller@example.com', '1996-11-29', 'userpassword8', false, NULL, '2023-06-17 16:15:00', '2023-06-17 16:15:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'), -- User
    (uuid_generate_v4(), 'Abigail', 'Anderson', 'abigailanderson@example.com', '1993-04-14', 'userpassword9', false, NULL, '2023-06-18 19:00:00', '2023-06-18 19:00:00', 'd56e2734-8d6f-49b9-83e1-5d1f2f717f16'); -- User


create table "Role"
(
	"Id" uuid primary key not null,
	"Title" varchar(50),
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_Role_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_Role_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id")
)

INSERT INTO "Role" ("Id", "Title", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
VALUES
    ('a12a817d-d714-4e6c-8e1b-654f1f2e6c3f', 'Administrator', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    ('d56e2734-8d6f-49b9-83e1-5d1f2f717f16', 'User', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    ('c56d2734-8d6f-49b9-83e1-5d1f2f717f16', 'Guest', true, NULL, NULL, '2023-06-01 09:00:00', NULL);

alter table "User"
add	constraint "FK_User_Role_Id" foreign key("RoleId") references "Role"("Id")

ALTER TABLE "User" ALTER COLUMN "Password" TYPE varchar(255);
ALTER TABLE "User" add constraint email_unique unique ("Email");

create table "Genre"
(
	"Id" uuid primary key not null,
	"Title" varchar(50),
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_Genre_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_Genre_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id")
)

INSERT INTO "Genre" ("Id", "Title", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
VALUES
    (uuid_generate_v4(), 'Action', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Comedy', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Drama', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Fantasy', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Horror', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Romance', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Sci-Fi', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Thriller', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Mystery', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Adventure', true, NULL, NULL, '2023-06-01 09:00:00', NULL);

create table "Actor"
(
	"Id" uuid primary key not null,
	"FirstName" varchar(50),
	"LastName" varchar(50),
	"Bio" varchar(255),
	"Image" varchar(255),
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_Actor_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_Actor_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id")
)
   
INSERT INTO "Actor" ("Id", "FirstName", "LastName", "Bio", "Image", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
VALUES
    (uuid_generate_v4(), 'Tom', 'Hanks', 'Tom Hanks is an American actor and filmmaker.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Leonardo', 'DiCaprio', 'Leonardo DiCaprio is an American actor, film producer, and environmentalist.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Brad', 'Pitt', 'Brad Pitt is an American actor and film producer.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    -- Add more actors here
    (uuid_generate_v4(), 'Johnny', 'Depp', 'Johnny Depp is an American actor, producer, and musician.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Robert', 'Downey Jr.', 'Robert Downey Jr. is an American actor and producer.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Meryl', 'Streep', 'Meryl Streep is an American actress and singer.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    -- Add more actors here
    (uuid_generate_v4(), 'Jennifer', 'Lawrence', 'Jennifer Lawrence is an American actress.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Denzel', 'Washington', 'Denzel Washington is an American actor, director, and producer.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Emma', 'Stone', 'Emma Stone is an American actress.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    -- Add more actors here
    (uuid_generate_v4(), 'Al Pacino', 'Pacino', 'Al Pacino is an American actor and filmmaker.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Natalie', 'Portman', 'Natalie Portman is an Israeli-born American actress and filmmaker.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Harrison', 'Ford', 'Harrison Ford is an American actor.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    -- Add more actors here
    (uuid_generate_v4(), 'Charlize', 'Theron', 'Charlize Theron is a South African and American actress and producer.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Anthony', 'Hopkins', 'Anthony Hopkins is a Welsh actor, director, and producer.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Scarlett', 'Johansson', 'Scarlett Johansson is an American actress and singer.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    -- Add more actors here
    (uuid_generate_v4(), 'Matt', 'Damon', 'Matt Damon is an American actor, producer, and screenwriter.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Angelina', 'Jolie', 'Angelina Jolie is an American actress, filmmaker, and humanitarian.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Daniel', 'Craig', 'Daniel Craig is an English actor.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    -- Add more actors here
    (uuid_generate_v4(), 'Chris', 'Evans', 'Chris Evans is an American actor.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Anne', 'Hathaway', 'Anne Hathaway is an American actress.', NULL, true, NULL, NULL, '2023-06-01 09:00:00', NULL);
    -- Add more actors here




create table "Movie"
(
	"Id" uuid primary key not null,
	"Title" varchar(50),
	"Runtime" int,
	"YearOfRelease" timestamp,
	"Image" varchar(255),
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_Movie_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_Movie_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id")
)

INSERT INTO "Movie" ("Id", "Title", "Runtime", "YearOfRelease", "Image", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
VALUES
    (uuid_generate_v4(), 'The Shawshank Redemption', 142, '1994-09-23 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Godfather', 175, '1972-03-24 00:00:00', 'https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Pulp Fiction', 154, '1994-10-14 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNGNhMDIzZTUtNTBlZi00MTRlLWFjM2ItYzViMjE3YzI5MjljXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Dark Knight', 152, '2008-07-18 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Fight Club', 139, '1999-10-15 00:00:00', 'https://m.media-amazon.com/images/I/81JWVTlPQ2L._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Forrest Gump', 142, '1994-07-06 00:00:00', 'https://m.media-amazon.com/images/I/81xTx-LxAPL._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Matrix', 136, '1999-03-31 00:00:00', 'https://m.media-amazon.com/images/I/51EG732BV3L._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Goodfellas', 146, '1990-09-12 00:00:00', 'https://m.media-amazon.com/images/M/MV5BY2NkZjEzMDgtN2RjYy00YzM1LWI4ZmQtMjIwYjFjNmI3ZGEwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Silence of the Lambs', 118, '1991-02-14 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMWE3NTE2YzgtNjUyYy00Y2NmLTljNWMtNjY4MjZhYmMzM2M4XkEyXkFqcGdeQXVyMTEyMDcwNw@@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Inception', 148, '2010-07-16 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMjExMjkwNTQ0Nl5BMl5BanBnXkFtZTcwNTY0OTk1Mw@@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Lord of the Rings: The Fellowship of the Ring', 178, '2001-12-19 00:00:00', 'https://upload.wikimedia.org/wikipedia/en/8/8a/The_Lord_of_the_Rings%2C_TFOTR_%282001%29.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Lion King', 88, '1994-06-15 00:00:00', 'https://lumiere-a.akamaihd.net/v1/images/image_fc5cb742.jpeg?region=0,0,540,810', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Star Wars: Episode V - The Empire Strikes Back', 124, '1980-05-17 00:00:00', 'https://s3.amazonaws.com/nightjarprod/content/uploads/sites/192/2022/04/21120853/k2J0GbxnuWJARxLHa2vAyO77qRX.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Avengers', 143, '2012-05-04 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Gladiator', 155, '2000-05-01 00:00:00', 'https://throughthesilverscreenuk.files.wordpress.com/2016/08/gladiator-movie-poster.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Departed', 151, '2006-10-06 00:00:00', 'https://m.media-amazon.com/images/I/81ZOilPKzYL._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Dark Knight Rises', 164, '2012-07-20 00:00:00', 'https://musicart.xboxlive.com/7/5ab02f00-0000-0000-0000-000000000002/504/image.jpg?w=1920&h=1080', true, NULL, NULL, '2023-06-01 09:00:00', NULL),        
	(uuid_generate_v4(), 'Star Wars: Episode IV - A New Hope', 121, '1977-05-25 00:00:00', 'https://m.media-amazon.com/images/I/81aA7hEEykL._AC_UF1000,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
	(uuid_generate_v4(), 'Jurassic Park', 127, '1993-06-11 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMjM2MDgxMDg0Nl5BMl5BanBnXkFtZTgwNTM2OTM5NDE@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
	(uuid_generate_v4(), 'Titanic', 194, '1997-12-19 00:00:00', 'https://assets.gadgets360cdn.com/pricee/assets/product/202301/Titanic_1674401841.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
	(uuid_generate_v4(), 'Inglourious Basterds', 153, '2009-08-20 00:00:00', 'https://m.media-amazon.com/images/M/MV5BOTJiNDEzOWYtMTVjOC00ZjlmLWE0NGMtZmE1OWVmZDQ2OWJhXkEyXkFqcGdeQXVyNTIzOTk5ODM@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Prestige', 130, '2006-10-20 00:00:00', 'https://m.media-amazon.com/images/I/51wILNNX2VL._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Grand Budapest Hotel', 99, '2014-02-26 00:00:00', 'https://i.ytimg.com/vi/uC4n3_EK_kA/movieposter.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Blade Runner', 117, '1982-06-25 00:00:00', 'https://static.wikia.nocookie.net/bladerunner/images/e/e0/Blade-runner-directors-cut-poster--large-msg-119325148375.jpg/revision/latest?cb=20110425200646', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Interstellar', 169, '2014-11-05 00:00:00', 'https://m.media-amazon.com/images/I/61pyUElLh7L._AC_UF1000,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Social Network', 120, '2010-09-24 00:00:00', 'https://i.pinimg.com/474x/8b/41/e2/8b41e2f169d86d87aa4c543af61d51b7.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Big Lebowski', 117, '1998-03-06 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNDQwMTAzOTkxNV5BMl5BanBnXkFtZTgwMjc0MTAwMjE@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Memento', 113, '2000-09-05 00:00:00', 'https://m.media-amazon.com/images/I/91OJsdscU9L._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Usual Suspects', 106, '1995-07-19 00:00:00', 'https://m.media-amazon.com/images/I/81+cm1GUITL._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Reservoir Dogs', 99, '1992-01-21 00:00:00', 'https://m.media-amazon.com/images/I/51d1KIaTeHS._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Taxi Driver', 114, '1976-02-08 00:00:00', 'https://m.media-amazon.com/images/M/MV5BM2M1MmVhNDgtNmI0YS00ZDNmLTkyNjctNTJiYTQ2N2NmYzc2XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'No Country for Old Men', 122, '2007-05-19 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMjA5Njk3MjM4OV5BMl5BanBnXkFtZTcwMTc5MTE1MQ@@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Amélie', 122, '2001-04-25 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNDg4NjM1YjMtYmNhZC00MjM0LWFiZmYtNGY1YjA3MzZmODc5XkEyXkFqcGdeQXVyNDk3NzU2MTQ@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Seven', 127, '1995-09-22 00:00:00', 'https://m.media-amazon.com/images/M/MV5BOTUwODM5MTctZjczMi00OTk4LTg3NWUtNmVhMTAzNTNjYjcyXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Eternal Sunshine of the Spotless Mind', 108, '2004-03-19 00:00:00', 'https://musicart.xboxlive.com/7/869c1100-0000-0000-0000-000000000002/504/image.jpg?w=1920&h=1080', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Sixth Sense', 107, '1999-08-06 00:00:00', 'https://m.media-amazon.com/images/I/711uZBBjIeL._RI_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Oldboy', 120, '2003-11-21 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMTI3NTQyMzU5M15BMl5BanBnXkFtZTcwMTM2MjgyMQ@@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Green Mile', 189, '1999-12-10 00:00:00', 'https://m.media-amazon.com/images/I/51GLlocYQ9L._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Truman Show', 103, '1998-06-05 00:00:00', 'https://images-na.ssl-images-amazon.com/images/S/pv-target-images/12b4afd1d605b2f5bdb5be3c8d31f39fdbdcd4ae92b62c236dad2a23d7e8e925._RI_TTW_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'American Beauty', 122, '1999-09-15 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNTBmZWJkNjctNDhiNC00MGE2LWEwOTctZTk5OGVhMWMyNmVhXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'City of God', 130, '2002-08-30 00:00:00', 'https://m.media-amazon.com/images/M/MV5BOTMwYjc5ZmItYTFjZC00ZGQ3LTlkNTMtMjZiNTZlMWQzNzI5XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Scarface', 170, '1983-12-09 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNjdjNGQ4NDEtNTEwYS00MTgxLTliYzQtYzE2ZDRiZjFhZmNlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Whiplash', 106, '2014-10-10 00:00:00', 'https://play-lh.googleusercontent.com/fCn_UmRfVvj-YSU0KRUQ3pztA3UDcZ3ClMwxifI7UJ-bFo7ToB5n_OxoGKmgMzuGNjBoHixs0b6yGA-jBvzz', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Great Gatsby', 143, '2013-05-10 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMTkxNTk1ODcxNl5BMl5BanBnXkFtZTcwMDI1OTMzOQ@@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'A Clockwork Orange', 136, '1971-12-19 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMTY3MjM1Mzc4N15BMl5BanBnXkFtZTgwODM0NzAxMDE@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Apocalypse Now', 147, '1979-08-15 00:00:00', 'https://upload.wikimedia.org/wikipedia/sh/a/ac/Apocnow.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Django Unchained', 165, '2012-12-25 00:00:00', 'https://m.media-amazon.com/images/I/81Z+lBcAYWL._AC_UF894,1000_QL80_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Requiem for a Dream', 102, '2000-10-06 00:00:00', 'https://m.media-amazon.com/images/M/MV5BOTdiNzJlOWUtNWMwNS00NmFlLWI0YTEtZmI3YjIzZWUyY2Y3XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Her', 126, '2013-12-18 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMjA1Nzk0OTM2OF5BMl5BanBnXkFtZTgwNjU2NjEwMDE@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Gone Girl', 149, '2014-10-03 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMTk0MDQ3MzAzOV5BMl5BanBnXkFtZTgwNzU1NzE3MjE@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'La La Land', 128, '2016-08-31 00:00:00', 'https://images.savoysystems.co.uk/GCL/466818.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Gravity', 91, '2013-08-28 00:00:00', 'https://m.media-amazon.com/images/M/MV5BNjE5MzYwMzYxMF5BMl5BanBnXkFtZTcwOTk4MTk0OQ@@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Birdman', 119, '2014-08-27 00:00:00', 'https://m.media-amazon.com/images/M/MV5BODAzNDMxMzAxOV5BMl5BanBnXkFtZTgwMDMxMjA4MjE@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Revenant', 156, '2015-12-16 00:00:00', 'https://lumiere-a.akamaihd.net/v1/images/revenant_584x800_6d98d1b6.jpeg?region=0%2C0%2C584%2C800', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Mad Max: Fury Road', 120, '2015-05-13 00:00:00', 'https://m.media-amazon.com/images/M/MV5BN2EwM2I5OWMtMGQyMi00Zjg1LWJkNTctZTdjYTA4OGUwZjMyXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Arrival', 116, '2016-09-01 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMTExMzU0ODcxNDheQTJeQWpwZ15BbWU4MDE1OTI4MzAy._V1_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Hereditary', 127, '2018-01-21 00:00:00', 'https://m.media-amazon.com/images/M/MV5BOTU5MDg3OGItZWQ1Ny00ZGVmLTg2YTUtMzBkYzQ1YWIwZjlhXkEyXkFqcGdeQXVyNTAzMTY4MDA@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Witch', 92, '2015-01-27 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMTUyNzkwMzAxOF5BMl5BanBnXkFtZTgwMzc1OTk1NjE@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'Moonlight', 111, '2016-10-21 00:00:00', 'https://pics.filmaffinity.com/Moonlight-912634365-large.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Hobbit: An Unexpected Journey', 169, '2012-11-28 00:00:00', 'https://i.ytimg.com/vi/TCqcHJZ9aJY/movieposter_en.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Hobbit: The Desolation of Smaug', 161, '2013-12-02 00:00:00', 'https://m.media-amazon.com/images/M/MV5BMzU0NDY0NDEzNV5BMl5BanBnXkFtZTgwOTIxNDU1MDE@._V1_FMjpg_UX1000_.jpg', true, NULL, NULL, '2023-06-01 09:00:00', NULL),
    (uuid_generate_v4(), 'The Hobbit: The Battle of the Five Armies', 144, '2014-12-01 00:00:00', 'https://play-lh.googleusercontent.com/qdPd6bAQnFEcDyYm6vm1pYPd8m2Knp1GPJ6454P8wS9xZjLuJ1RjJ5yLR7Vi7Qc-3eJ49g', true, NULL, NULL, '2023-06-01 09:00:00', NULL);

create table "ActorMovie"
(
	"Id" uuid primary key not null,
	"MovieId" uuid,
	"ActorId" uuid,
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_ActorMovie_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_ActorMovie_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id"),
	constraint "FK_ActorMovie_Actor_Id" foreign key("ActorId") references "Actor"("Id"),
	constraint "FK_ActorMovie_Movie_Id" foreign key("MovieId") references "Movie"("Id")
)

INSERT INTO "ActorMovie" ("Id", "MovieId", "ActorId", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
SELECT uuid_generate_v4(), m."Id", a."Id", true, NULL, NULL, now(), NULL
FROM "Movie" m
CROSS JOIN LATERAL (
    SELECT "Id"
    FROM "Actor"
    ORDER BY random()
    LIMIT 3
) a;



create table "MovieGenre"
(
	"Id" uuid primary key not null,
	"MovieId" uuid,
	"GenreId" uuid,
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_MovieGenre_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_MovieGenre_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id"),
	constraint "FK_MovieGenre_Genre_Id" foreign key("GenreId") references "Genre"("Id"),
	constraint "FK_MovieGenre_Movie_Id" foreign key("MovieId") references "Movie"("Id")
)

INSERT INTO "MovieGenre" ("Id", "MovieId", "GenreId", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
SELECT uuid_generate_v4(), m."Id", g."Id", true, NULL, NULL, now(), NULL
FROM "Movie" m
CROSS JOIN LATERAL (
    SELECT "Id"
    FROM "Genre"
    ORDER BY random()
    LIMIT floor(random() * 2 + 2) -- Assign 2 or 3 genres
) g;

delete from "Review" 
create table "Review"
(
	"Id" uuid primary key not null,
	"Title" varchar(50),
	"Content" varchar(255),
	"Score" int,
	"MovieId" uuid,
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_Review_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_Review_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id"),
	constraint "FK_Review_Movie_Id" foreign key("MovieId") references "Movie"("Id")
)

INSERT INTO "Review" ("Id", "Title", "Content", "Score", "MovieId", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
SELECT
    uuid_generate_v4(),
    'Review Title',
    'Review Content',
    floor(random() * 5) + 1,
    m."Id",
    true,
    u."Id",
    u."Id",
    now(),
    now()
FROM
    "Movie" m
JOIN "User" u ON true
WHERE (SELECT COUNT(*) FROM "Review" WHERE "MovieId" = m."Id") < 4; -- Limit to 3 or 4 reviews per movie





create table "WatchList"
(
	"Id" uuid primary key not null,
	"IsWatched" bool,
	"UserId" uuid,
	"MovieId" uuid,
	"IsActive" bool,
	"CreatedByUserId" uuid,
	"UpdatedByUserId" uuid,	
	"DateCreated" timestamp,
	"DateUpdated" timestamp,
	constraint "FK_WatchList_Created_User_Id" foreign key("CreatedByUserId") references "User"("Id"),
	constraint "FK_WatchList_Updated_User_Id" foreign key("UpdatedByUserId") references "User"("Id"),
	constraint "FK_WatchList_User_Id" foreign key("UserId") references "User"("Id"),
	constraint "FK_WatchList_Movie_Id" foreign key("MovieId") references "Movie"("Id")
)

INSERT INTO "WatchList" ("Id", "UserId", "MovieId", "IsActive", "CreatedByUserId", "UpdatedByUserId", "DateCreated", "DateUpdated")
SELECT
    uuid_generate_v4(),
    u."Id",
    m."Id",
    true,
    u."Id",
    u."Id",
    now(),
    now()
FROM
    (SELECT "Id" FROM "User" ORDER BY random() LIMIT 5) u
CROSS JOIN LATERAL
    (SELECT "Id" FROM "Movie" ORDER BY random() LIMIT 5) m;

alter table "Movie"
add column "AverageScore" decimal(3,2)