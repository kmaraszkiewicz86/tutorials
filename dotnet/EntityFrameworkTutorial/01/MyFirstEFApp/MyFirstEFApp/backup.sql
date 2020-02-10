create database entityTutorial01 collate SQL_Latin1_General_CP1_CI_AS
go

drop table Books
go

drop table Author
go

create table Authors
(
    AuthorId int identity
        constraint Author_pk
            primary key nonclustered,
    Name     nvarchar(255) not null,
    WebURL   nvarchar(255)
)
go

create table Books
(
    BookId      int identity
        constraint Books_pk
            primary key nonclustered,
    Title       nvarchar(255) not null,
    Description text          not null,
    PublishedOn datetime      not null,
    AuthorId    int           not null
        constraint Books_Author_AuthorId_fk
            references Author
            on delete cascade
)
go

INSERT INTO entityTutorial01.dbo.Author (AuthorId, Name, WebURL) VALUES (1, 'Izolda', 'http://izolda.com');
INSERT INTO entityTutorial01.dbo.Author (AuthorId, Name, WebURL) VALUES (2, 'Lilo', 'http://lilo.com');
INSERT INTO entityTutorial01.dbo.Author (AuthorId, Name, WebURL) VALUES (3, 'Mailo', 'http://mailo.com');
INSERT INTO entityTutorial01.dbo.Author (AuthorId, Name, WebURL) VALUES (4, 'Szarlota', 'http://szarlota.com');

INSERT INTO entityTutorial01.dbo.Books (BookId, Title, Description, PublishedOn, AuthorId) VALUES (1, 'O tym jednym co sie zesral', 'opis', '2001-01-01 00:00:00.000', 1);
INSERT INTO entityTutorial01.dbo.Books (BookId, Title, Description, PublishedOn, AuthorId) VALUES (2, 'O tym co grzebal w smieciach', 'opis', '2010-02-02 00:00:00.000', 1);
INSERT INTO entityTutorial01.dbo.Books (BookId, Title, Description, PublishedOn, AuthorId) VALUES (3, 'O tym ja nazrec sie i chudnac', 'opis', '2013-02-10 00:00:00.000', 2);
INSERT INTO entityTutorial01.dbo.Books (BookId, Title, Description, PublishedOn, AuthorId) VALUES (4, 'O zasadach gry w pike', 'opis', '2014-03-11 00:00:00.000', 3);

go

