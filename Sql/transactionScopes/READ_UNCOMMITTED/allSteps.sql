READ UNCOMMITTED

W aktualnym oknie sesji wykonujemy krok pierwszy przykładu, rozpoczynamy transakcję oraz wykonujemy update wszystkich 10 rekordów w tabeli. Transakcji nie kończymy.
— krok 1
— uruchamiamy transakcje, ale nie wykonujemy jej zakonczenia
— domyslny poziomo izolacji – READ COMMITTED
use Transakcje
go

BEGIN TRANSACTION
update dbo.TabelkaTransakcje
set kolumna2_var20 = ‚xxxxxxx’
— execute

 

Następnie w kroku 2 w drugiej sesji na utworzonej bazie TRANSAKCJE zmieniamy poziom izolacji transakcji na READ UNCOMMITTED i wykonujemy prosty SELECT danych. Jak będzie wynik ?
— krok 2
— w drugiej sesji wykonujemy poniższe komendy i spawdzamy jak wygladaja dane
— zmieniamy izolacje transakcji na READ UNCOMITTED
use Transakcje
go

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

select * from dbo.TabelkaTransakcje;
— execute

W wyniku otrzymamy dane (10 zmienionych wierszy), które pokazują faktyczny stan tabelki na tą chwilę. Należy zwrócić uwagę, że transakcja zmieniająca dane się nie zakończyła. Może ona zakończyć się pozytywnie, ale wcale nie musi. Tak więc polecenie SELECT zwróciło brudne dane, jeszcze nie zatwierdzone dane. Jak w opowieści o biletach.
Wykonajmy w pierwszym oknie ROLLBACK TRANSACTION
— krok 3
— w aktualnej sesji wykonujemy cofniecie wykonanej transakcji
ROLLBACK TRANSACTION;
— execute

Jaki więc będzie wynik, kiedy znowu w drugiej sesji wykonamy zapytanie SELECT?
— krok 4
— w drugiej sesji wykonujemy ponownie komende odczytu danych i sprawdzamy jak to teraz wyglada
— nie zmieniamy poziomu izolacji
select * from dbo.TabelkaTransakcje;
— execute

Aby nie dopuścić do takiego stanu rzeczy i nie otrzymywać nie zatwierdzonych danych podnosimy poziom izolacji na READ COMMITED. Jak pamiętamy, jest do domyślny poziom izolacji danych w produkcie Microsoft SQL Server.