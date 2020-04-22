READ COMMITTED

 

Rozpoczynamy od wykonania w bieżącej sesji prostego zapytania.  W domyślnym poziomie izolacji transakcji wykonujemy zwykły SELECT i sprawdzamy poprawność wyświetlonych danych.
-- krok 1 
-- ustawiamy poziom izolacji na READ COMMITED i sprawdzamy odczyt przykladowej tabelki
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

select * from dbo.TabelkaTransakcje;
-- execute

W kroku 2 w drugiej sesji rozpoczynamy transakcję i wykonujemy taki sam update jak poprzednio we wcześniejszym poziomie izolacji transakcji. Transakcja się nie kończy, ale samo polecenie update tak.
-- krok 2
-- uruchamiamy transakcje, ale nie wykonujemy jej zakonczenia
-- domyslny poziomo izolacji – READ COMMITTED
use Transakcje
go

BEGIN TRANSACTION
update dbo.TabelkaTransakcje
set kolumna2_var20 = 'xxxxxxx'
-- execute

W kroku 3 ponawiamy wykonanie komendy z kroku 1 – w tym oddzielnym oknie (tam gdzie był select) i sprawdzamy jaki będzie wynik polecenia. Pytanie – jaki będzie efekt końcowy? Co dostaniemy ?
-- krok 3
-- w pierwszej sesji wykonujemy ponownie komende z kroku 1
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

select * from dbo.TabelkaTransakcje;
-- execute

 Jak można zaobserwować, w oknie z poleceniem SELECT (osobna transakcja) nie dostaliśmy żadnego wyniku. Inaczej było w poprzednim przykładzie, wynik był, dostaliśmy dane, które były zmienione w trakcie trwania transakcji, teraz już nie mamy takiej możliwości. Polecenie SELECT jest zablokowane do czasu, kiedy transakcja wykonująca UPDATE się albo nie zakończy, albo zostanie cofnięta. Nie mamy już DIRTY READS.
Wykonamy w takim razie w kroku 4 w aktualnej transakcji update jej cofnięcie. Pytanie – co dostaniemy w drugim oknie sesji ?
-- krok 4
-- w sesji z trwającą transakcją wykonujemy cofniecie wykonanej transakcji
ROLLBACK TRANSACTION;
-- execute

Po cofnięciu transakcji w oknie z poleceniem SELECT mogliśmy zauważyć wynik działania tej komendy. Widzimy, że SELECT trwa tak długo, aż wszystkie dane w stanie spójnym będą mogły być odczytane i przedstawione końcowemu użytkownikowi. Długie UPDATEy spowalniają możliwość wykonywania SELECTów, co nie?
A co z Non-repeatable read? Ten fenomen na tym poziomie izolacji transakcji powinien występować, prawda ? To opowieść o kwiatkach. Sprawdźmy.
W aktualnym oknie (tam gdzie była wykonywana sesja z UPDATE) wykonujemy prostą operacje sumowania za pomocą polecenia SUM, otwieramy transakcje w domyślnym poziomie izolacji, ale jej nie kończymy.
-- krok 5
-- uruchamiamy w bieżącym oknie transakcje, ale nie wykonujemy jej zakonczenia
-- domyslny poziomo izolacji – READ COMMITTED
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN TRANSACTION
select sum(kolumna1_PK)
from dbo.TabelkaTransakcje
-- execute

Wynik prezentowany na ekranie to – 55. Suma danych w kolumnie id – 1, 2, 4 itd. Tak powinno być. W drugim oknie korzystając z tego samego poziomu izolacji transakcji wykonujemy update jednego wiersza, zmieniamy wartość id (działamy jako inna transakcja na tych danych).
-- krok 6
-- w drugim oknie wykonajmy w transakcji update wierszy zwiekszajac 10 do 100
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
update dbo.TabelkaTransakcje
set kolumna1_PK = 100 where kolumna1_PK = 10;
go
–execute

 

Operacja update się udaje, ponieważ w otwartej transakcji mamy polecenie SELECT. Wcześniej mieliśmy sytuacje odwrotną, wykonywaliśmy UPDATE i do rozpoczętego update nie mogliśmy wykonać SELECTA. Teraz możemy. Więc w otwartej transakcji wykonajmy ponownie polecenie SELECT SUM. Co teraz otrzymamy?
-- krok 7
-- w oknie aktualnym (trwająca sesja) kontynuujemy transakcje sprawdzajac sumę jak w kroku 5
select sum(kolumna1_PK)
from dbo.TabelkaTransakcje
–execute

Jako wynik dostaliśmy sumę równą nie 55 ale 145. W tej samej transakcji !!! Kończymy transakcję i przywracamy stan początkowy danych.
-- krok 8
-- konczymy transakcje i wracamy z danymi do stanu poczatkowego
update dbo.TabelkaTransakcje
set kolumna1_PK = 10 where kolumna1_PK = 100;
select sum(kolumna1_PK)
from dbo.TabelkaTransakcje;
go

COMMIT TRANSACTION
go

 

Jak widzimy, pojawiło się zjawisko Non-repeatable read. Czyli nie-powtarzalny odczyt. Jest on możliwy w tym poziomie izolacji transakcji. Tak jak wspomniane dziewczynki z kwiatkami, różna liczba kwiatków w czasie wypadku na łąkę tego samego dnia. Tak więc należy uważać na selecty w długo trwających transakcjach, które mogą dać inne dane jeśli w międzyczasie będą działały updatey na danych. Aby nie było nie-powtarzalnych odczytów – podnieśmy poziom izolacji transakcji 🙂

 