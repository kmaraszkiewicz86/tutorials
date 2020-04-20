use master
go

-- jesli istnieje to usun
if exists (select 1 from sys.databases where name = 'Transakcje')
drop database Transakcje;
go

-- tworzenie nowej bazy danych
create database Transakcje
go

-- uzycie bazy danych
use Transakcje
go

-- tworzenie testowej tabelki danych
create table dbo.TabelkaTransakcje
(
kolumna1_pk int primary key,
kolumna2_var20 nvarchar(100)
);
go

-- insert do tabelki przykladowych danych
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (1, 'aaaaaaa');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (2, 'bbbbbbb');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (3, 'ccccccc');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (4, 'ddddddd');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (5, 'eeeeeee');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (6, 'fffffff');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (7, 'ggggggg');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (8, 'hhhhhhh');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (9, 'iiiiiii');
insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (10, 'jjjjjjj');
go

-- sprawdzenie czy dane sa
select * from dbo.TabelkaTransakcje;
go

--READ UNCOMMITTED
SELECT @@SPID