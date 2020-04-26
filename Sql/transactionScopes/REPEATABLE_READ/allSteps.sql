----------------REPEATABLE READ--------------------

--Ponownie zaczniemy od wykonania prostego skryptu. Ustawiamy poziom izolacji transakcji na wyższy niż domyślny. Następnie sprawdzamy za pomocą polecenia SELECT i CASE jaki jest ten poziom izolacji transakcji. Rozpoczynamy transakcję i wykonujemy prosty SELECT sumy – jak poprzednio. Znamy odpowiedź na pytanie – jaki będzie wynik.
-- krok 1
-- ustawiamy poziom izolacji na REPEATABLE READ i sprawdzamy odczyt przykladowej tabelki
-- MUSIMY ZAMKNAC WSZYSTKIE POLACZENIA
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;

-- sprawdzenie poziomu izolacji transakcji
SELECT CASE transaction_isolation_level
WHEN 0 THEN 'Unspecified'
WHEN 1 THEN 'ReadUncommitted'
WHEN 2 THEN 'ReadCommitted'
WHEN 3 THEN 'Repeatable'
WHEN 4 THEN 'Serializable'
WHEN 5 THEN 'Snapshot' END AS TRANSACTION_ISOLATION_LEVEL
FROM sys.dm_exec_sessions
where session_id = @@SPID

-- sprawdzenie wlasciwosci bazy danych
EXEC('DBCC USEROPTIONS WITH NO_INFOMSGS')

select * from dbo.TabelkaTransakcje;

BEGIN TRAN
select sum(kolumna1_PK)
FROM dbo.TabelkaTransakcje;
-- execute

--W drugiej sesji spróbujmy wykonać operację UPDATE. Jak poprzednio. 
-- Zmieńmy jedno z ID wierszy – wykonamy to w domyślnym poziomie izolacji 
-- transakcji – READ COMMITED. A tą aktualnie działającą traktujmy jako ewenement w środowisku. 
-- Została uruchomiona po prostu z wyższym poziomem izolacji transakcji – jako jedna z nielicznych. 
-- Czy domyślamy się jaki będzie wynik końcowy polecenia update?
-- krok 2
-- uruchamiamy transakcje w drugim oknie , sprawdzamy czy jest READ COMMITED
-- sprawdzamy bieżący poziom izolacji
use Transakcje
go

-- sprawdzenie poziomu izolacji transakcji
SELECT CASE transaction_isolation_level
WHEN 0 THEN 'Unspecified'
WHEN 1 THEN 'ReadUncommitted'
WHEN 2 THEN 'ReadCommitted'
WHEN 3 THEN 'Repeatable'
WHEN 4 THEN 'Serializable'
WHEN 5 THEN 'Snapshot' END AS TRANSACTION_ISOLATION_LEVEL
FROM sys.dm_exec_sessions
where session_id = @@SPID

update dbo.TabelkaTransakcje
set kolumna1_PK = 100 where kolumna1_PK = 10;

-- Jaki możemy zaobserwować wynik? Żaden. Nic nie możemy zaobserwować. 
-- Polecenie UPDATE nie działa na trwającym SELECT w poziomie izolacji REPEATABLE READ. 
-- Nie ma możliwości zmiany. Nie ma niepowtarzalnych odczytów. Hurra. 
-- Wykonajmy w oknie z otwartą transakcją ponownie polecenie SELECT SUM. 
-- Będą jakieś widoczne zmiany w zwracanych wynikach?
-- krok 3
-- w sesji z otwartą transakcja wykonujemy ponownie komende z kroku 1
-- chyba nie powinno być zmian

select sum(kolumna1_PK)
FROM dbo.TabelkaTransakcje;
-- execute

-- I nie ma zmian. Zwrócona jest wartości 55. Mamy dwie możliwości, albo przerwiemy wykonanie 
-- polecenia update za pomocą przycisku STOP albo wykonamy polecenie COMMIT aktualnie wykonywanej 
-- transakcji. Wykonajmy COMMIT. Spowoduje to, że polecenie UPDATE wykona się i zmieni nam dane. 
-- Sprawdźmy to po operacji COMMIT, a następnie wróćmy z danymi do stanu wyjściowego.
-- krok 4
-- Wykonajmy commit
COMMIT TRANSACTION

select * from dbo.TabelkaTransakcje;

update dbo.TabelkaTransakcje
set kolumna1_PK = 10 where kolumna1_PK = 100;

select * from dbo.TabelkaTransakcje;

-- execute

--I jak widzimy, zmiana (update) nie jest możliwa. A co z poleceniem INSERT, czy jest możliwe? 
-- Aby to sprawdzić najpierw przygotujmy sobie tabelkę. W tym celu zmienimy sobie kilka id w wierszach.  
-- W poziomie izolacji READ COMMITED wykonajmy update wierszy.
-- krok 5
-- ustawiamy aktualnie aby było READ COMMITED w biezacym oknie
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

--wykonujemy zmiane danych pod wynik
update dbo.TabelkaTransakcje
set kolumna1_PK = 20 where kolumna1_PK = 2;
update dbo.TabelkaTransakcje
set kolumna1_PK = 30 where kolumna1_PK = 3;
update dbo.TabelkaTransakcje
set kolumna1_PK = 40 where kolumna1_PK = 4;

select * from dbo.TabelkaTransakcje

--W drugiej sesji w poziomie podwyższonym poziomie izolacji transakcji zapytajmy się o sumę wierszy, 
--ale nie wszystkich tylko tych zmienionych. Jaki wynik dostaniemy?
 

-- krok 6
-- tym razem rozpoczynamy od drugiej sesji
-- ustawiamy poziom izolacji na REPEATABLE READ i sprawdzamy odczyt przykladowej tabelki
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL REPEATABLE READ;

-- sprawdzenie poziomu izolacji transakcji
SELECT CASE transaction_isolation_level
WHEN 0 THEN 'Unspecified'
WHEN 1 THEN 'ReadUncommitted'
WHEN 2 THEN 'ReadCommitted'
WHEN 3 THEN 'Repeatable'
WHEN 4 THEN 'Serializable'
WHEN 5 THEN 'Snapshot' END AS TRANSACTION_ISOLATION_LEVEL
FROM sys.dm_exec_sessions
where session_id = @@SPID

select * from dbo.TabelkaTransakcje;

BEGIN TRAN
select *
FROM dbo.TabelkaTransakcje
where kolumna1_PK between 20 and 40;
-- execute

--W tej sesji zostawiamy tak otwartą transakcję. Wiemy już, że nie możemy zrobić update rekordów 
-- odczytywanych przez tą transakcję. Wracamy do okna z update (z kroku 5) gdzie mamy poziom domyślny. 
-- Wykonajmy kolejny prosty update na danych.
-- krok 7
-- kolejny update

update dbo.TabelkaTransakcje
set kolumna1_PK = 100 where kolumna1_PK = 10;
-- execute

--Ejj, dlaczego się udało ? A kolejny update się uda ?
update dbo.TabelkaTransakcje
set kolumna1_PK = 100 where kolumna1_PK = 30;

-- To drugie update się nie uda. Wiadomo dlaczego ?? W krokach wcześniejszych wykonywaliśmy w pierwszej 
--kolejności SELECT a później UPDATE, ale SELECT obejmował wszystkie rekordy tabeli. 
--Tym sposobem blokada była zakładana na wszystkie wiersze i update nie mógł zachodzić. 
--Teraz wykonaliśmy SELECT z klauzulą BETWEEN gdzie wybraliśmy rekordy. 
--I te, które zostały wybrane są blokowane, ale pozostałe nie są. Dlatego drugi update był możliwy. 
--Przerwijmy ten update.
-- A co z insert, czy insert możemy zrobić, na przykład taki? Zadziała to to?

insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (25, 'nowynowy');

--execute

--Zadziała. poziom izolacji transakcji chroni nas przed zmianami, ale nie chroni 
-- przed rekordami duszkami (przed tym trzecim fenomenem). Tak jak w opowieści z filmami. 
-- Koledzy wrócili z obiadu. To ten insert. W związku z tym ponowny select w otwartej transakcji 
-- powinien dać inny wynik niż poprzednio, co nie? Sprawdźmy.
-- execute
-- krok 8
-- chyba nie powinno byc zmian – select w otwartej transakcji;

select *
FROM dbo.TabelkaTransakcje
where kolumna1_PK between 20 and 40;
-- execute

-- No i mamy nowy rekord. Wykonajmy COMMIT albo ROLLBACK otwartej transakcji. 
-- Jak widzimy, nie mamy brudnych odczytów, nie mamy nie-powtarzalnych odczytów ale mamy fantomy. 
-- Co zrobimy aby się tych fantomów pozbyć? No wiadomo, podniesiemy poziom izolacji transakcji  
-- na jeszcze wyższy (to można tak??? ).