using System;
using System.Runtime.ConstrainedExecution;

namespace ProgrammingPatterns.Core
{
    /// <summary>
    /// Wzorzec ten umozliwia tworzenie wielu takich samych obiektow o roznej kofiguracji - oddziela tworzenie
    /// obiektow od ich reprezentacji. Za konfiguracje poszczegolnych obiektow sa odpowiedzialne wyspecializowane klasy,
    /// ktore implementuja interferjs podstawowego obiektu Builder.
    ///
    /// Implementacja:
    /// 1. Tworzymy instancje klasy KonkretnyBudowniczy, ktora stworzy oczekiwany przez nas rodzaj obiektu
    /// 2. Tworzymy instancje klasy Dyrektor i przekazujemy do niej referencje budowniczego
    /// 3. Wywolujemy metode konstrukcji budynku w powyzszym obiekcie (dyrektor zaleca wykonanie budowniczemuwedlug okreslonych zasad
    /// 4. Po stworzeniu budynku odbieramy (pobieramy) go od naszego budowniczego
    ///
    /// </summary>
    public class Budynek
    {
        private string _okno;
        private string _drzwi;
        private string _kolorElewacji;

        public void WstawOkna(string okno)
        {
            _drzwi = _drzwi;
        }

        public void ZamontujDrzwi(string drzwi)
        {
            _drzwi = drzwi;
        }

        public void PomalujElewacje(string elewacja)
        {
            _kolorElewacji = elewacja;
        }

        public void OpiszBudynek()
        {
            if (!string.IsNullOrEmpty(_okno))
            {
                Console.WriteLine($"Okna: {_okno}");
            }

            if (!string.IsNullOrEmpty(_drzwi))
            {
                Console.WriteLine($"Drzwi; {_drzwi}");
            }

            if (!string.IsNullOrEmpty(_kolorElewacji))
            {
                Console.WriteLine($"Elewacja: {_kolorElewacji}");
            }
        }
    }

    public abstract class Budowniczy
    {
        protected Budynek Budowla;

        public void NowyBudynek()
        {
            Budowla = new Budynek();
        }

        public Budynek PobierzBudynek()
        {
            return Budowla;
        }
        
        public abstract void WstawOkna();

        public abstract void ZamontujDrzwi();

        public abstract void PomalujElewacje();
    }

    public class DomJednorodzinny: Budowniczy
    {
        public override void WstawOkna()
        {
            Budowla.WstawOkna("Drewniane = złoty dąb");
        }

        public override void ZamontujDrzwi()
        {
            Budowla.ZamontujDrzwi("Antywlamaniowe z wizjerem");
        }

        public override void PomalujElewacje()
        {
            Budowla.PomalujElewacje("Zolta");
        }
    }

    public class Biurowiec : Budowniczy
    {
        public override void WstawOkna()
        {
            Budowla.WstawOkna("Antracytowe nieotwieralne");
        }

        public override void ZamontujDrzwi()
        {
            Budowla.ZamontujDrzwi("Obrotowe lewoskretne");
        }

        public override void PomalujElewacje()
        {
            //elewacja bedzie szklana
        }
    }

    public class Dyrektor
    {
        private Budowniczy _budowniczy;

        public void WybierzBudowniczego(Budowniczy budowniczy)
        {
            _budowniczy = budowniczy;
        }

        public Budynek PobierzBudynek()
        {
            return _budowniczy.PobierzBudynek();
        }

        public void Buduj()
        {
            _budowniczy.NowyBudynek();
            _budowniczy.WstawOkna();
            _budowniczy.ZamontujDrzwi();
            _budowniczy.PomalujElewacje();
        }
    }
    
    public static class BuilderExample
    {
        public static void DoWork()
        {
            var szef = new Dyrektor();
            Budowniczy budowniczy1 = new DomJednorodzinny();
            Budowniczy budowniczy2 = new Biurowiec();
            
            Console.WriteLine("Pierwsza budowla");
            
            szef.WybierzBudowniczego(budowniczy1);
            szef.Buduj();

            Budynek budynek1 = szef.PobierzBudynek();
            budynek1.OpiszBudynek();
            
            Console.WriteLine("Druga budowla");
            
            szef.WybierzBudowniczego(budowniczy2);
            szef.Buduj();
            Budynek budynek2 = szef.PobierzBudynek();
            budynek2.OpiszBudynek();
        }
    }
}