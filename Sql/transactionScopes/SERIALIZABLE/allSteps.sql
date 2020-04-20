Jak poprzednio – otwieramy sesję i wykonujemy w poziomie izolacji transakcji polecenie SELECT SUM zawężając zakres danych poleceniem BETWEEN.
— krok 1
— ustawiamy poziom izolacji na SERIALIZABLE i sprawdzamy odczyt przykladowej tabelki
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

— sprawdzenie poziomu izolacji transakcji
SELECT CASE transaction_isolation_level
WHEN 0 THEN ‚Unspecified’
WHEN 1 THEN ‚ReadUncommitted’
WHEN 2 THEN ‚ReadCommitted’
WHEN 3 THEN ‚Repeatable’
WHEN 4 THEN ‚Serializable’
WHEN 5 THEN ‚Snapshot’ END AS TRANSACTION_ISOLATION_LEVEL
FROM sys.dm_exec_sessions
where session_id = @@SPID

BEGIN TRAN
select *
FROM dbo.TabelkaTransakcje
where kolumna1_PK between 20 and 40;
–execute

W drugiej sesji wykonajmy próbę dodania wiersza w podanym trzymanym zakresie. Ostatnio się udało, teraz pewnie się nie uda. Wiadomo 🙂

— krok 2
— w aktualnej sesji w READ COMMITTED wykonujemy insert new row,
— poprzednio sie udalo
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (26, ‚nowynowy’);
–execute

No i mamy HOLD, operacja nie powiodła się. Polecenie INSERT zatrzymało się. Takiego efektu się spodziewaliśmy. W końcu zablokowaliśmy wszystkie trzy fenomeny. A insert spoza trzymanego zakresu uda się? Sprawdźmy.

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (11, ‚nowynowy’);
–execute

Mi się nie udał. A taki insert?
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

insert into dbo.TabelkaTransakcje (kolumna1_pk, kolumna2_var20) values (111, ‚nowynowy’);
–execute