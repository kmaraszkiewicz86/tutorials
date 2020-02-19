using System;
using System.ComponentModel;
using System.Resources;

namespace ProgrammingPatterns.Core
{
    /// <summary>
    /// Umozliwia tworzenie rodzin zaleznych lub spokrewnionych obiektow
    /// w sposob abstrakcyjny, bez opisywania konkretnych klas
    ///
    /// Implementacja:
    /// 1. Przy implementacji wykorzystywana jest kompozycja.
    /// 2. Fabryka zwraca cala rodzine powiazanych ze soba obiektow.
    ///     Zazwyczaj do tworzenia obiektow wykorzystywana jest metoda wytworcza
    ///
    /// Przyklady zastosowania:
    /// 1. implementacja wyswietalania tekstu np doc, txt, html
    /// 2. obsluga roznych baz danych
    /// </summary>
    interface IFabrykaCzesciRowerowych
    {
        IKierownica WyprodukujKierownice();
        IKola WyprodukujKola();
        IRama WyprodukujRame();
        IKoszyk WyprodukujKoszyk();
    }

    interface IKierownica
    {
        IKierownica DodajKierownice();
    }

    interface IKola
    {
        IKola DodajKola();
    }

    interface IRama
    {
        IRama DodajRame();
    }

    interface IKoszyk
    {
        IKoszyk ZamontujKoszyk();
    }
    
    enum TypRoweru
    {
        Gorski = 0,
        Miejski = 1
    }

    abstract class Rower
    {
        protected IKierownica Kierownica;
        protected IKola Kola;
        protected IRama Rama;
        protected IKoszyk Koszyk;

        public abstract void Skladanie();
        
        public static void MontazSiodelka()
        {
            Console.WriteLine("Siodełko zamontowsane.");
        }

        public void Sprzedaz()
        {
            Console.WriteLine("Sprzedane!");
        }
    }

    class RowerMiejski : Rower
    {
        private readonly FabrykaCzesciDoMiejskiego _fabrykaCzesciDoMiejskiego;

        public RowerMiejski(FabrykaCzesciDoMiejskiego fabrykaCzesciDoMiejskiego)
        {
            _fabrykaCzesciDoMiejskiego = fabrykaCzesciDoMiejskiego;
            Skladanie();
        }
        
        public sealed override void Skladanie()
        {
            Kierownica = _fabrykaCzesciDoMiejskiego.WyprodukujKierownice().DodajKierownice();
            Kola = _fabrykaCzesciDoMiejskiego.WyprodukujKola().DodajKola();
            Rama = _fabrykaCzesciDoMiejskiego.WyprodukujRame().DodajRame();
            Koszyk = _fabrykaCzesciDoMiejskiego.WyprodukujKoszyk().ZamontujKoszyk();
        }
    }
    
    class RowerGorski : Rower
    {
        private readonly FabrykaCzesciDoGorskiego _fabrykaCzesciDoGorskiego;

        public RowerGorski(FabrykaCzesciDoGorskiego fabrykaCzesciDoGorskiego)
        {
            _fabrykaCzesciDoGorskiego = fabrykaCzesciDoGorskiego;
            Skladanie();
        }
        
        public sealed override void Skladanie()
        {
            Kierownica = _fabrykaCzesciDoGorskiego.WyprodukujKierownice().DodajKierownice();
            Kola = _fabrykaCzesciDoGorskiego.WyprodukujKola().DodajKola();
            Rama = _fabrykaCzesciDoGorskiego.WyprodukujRame().DodajRame();
        }
    }
    
    class FabrykaRowerow
    {
        public Rower Zamowienie(TypRoweru model)
        {
            Rower rower = ZlozRower(model);
            Rower.MontazSiodelka();
            rower.Sprzedaz();
            return rower;
        }

        private static Rower ZlozRower(TypRoweru model)
        {
            switch (model)
            {
                case TypRoweru.Gorski:
                    return new RowerGorski(new FabrykaCzesciDoGorskiego());
                
                case TypRoweru.Miejski:
                    return new RowerMiejski(new FabrykaCzesciDoMiejskiego());
            }

            throw new Exception($"Not supported model type: {model.ToString()}");
        }
    }

    class KierownicaMiejski : IKierownica
    {
        private const string Name = "Kierownica do roweru miejskiego";
        
        public IKierownica DodajKierownice()
        {
            Console.WriteLine($"Dodano: {Name}");
            return new KierownicaMiejski();
        }
    }

    class KierownicaGorski : IKierownica
    {
        private const string Name = "Kierownica do roweru górskiego";
        public IKierownica DodajKierownice()
        {
            Console.WriteLine($"Dodano: {Name}");
            return  new KierownicaGorski();
        }
    }

    class KolaDoMiejskiego : IKola
    {
        private const string Name = "Koła do roweru miejskiego";
        public IKola DodajKola()
        {
            Console.WriteLine($"Dodano: {Name}");
            return new KolaDoMiejskiego();
        }
    }

    class KolaDoGorskiego: IKola
    {
        private const string Name = "Koła do roweru górskiego";
        
        public IKola DodajKola()
        {
            Console.WriteLine($"Dodano: {Name}");
            return new KolaDoGorskiego();
        }
    }

    class RamaDoMiejskiego : IRama
    {
        private const string Name = "Rama do roweru miejskiego";
        
        public IRama DodajRame()
        {
            Console.WriteLine($"Dodano: {Name}");
            return new RamaDoMiejskiego();
        }
    }
    
    class RamaDoGorskiego : IRama
    {
        private const string Name = "Rama do roweru górskiego";
        
        public IRama DodajRame()
        {
            Console.WriteLine($"Dodano: {Name}");
            return new RamaDoGorskiego();
        }
    }

    class KoszykDoMiejskiego : IKoszyk
    {
        private const string Name = "Koszy do roweru miejskiego";
        public IKoszyk ZamontujKoszyk()
        {
            Console.WriteLine($"Dodano: {Name}");
            return new KoszykDoMiejskiego();
        }
    }

    class FabrykaCzesciDoMiejskiego : IFabrykaCzesciRowerowych
    {
        public IKierownica WyprodukujKierownice()
        {
            return new KierownicaMiejski();
        }

        public IKola WyprodukujKola()
        {
            return new KolaDoMiejskiego();
        }

        public IRama WyprodukujRame()
        {
            return new RamaDoMiejskiego();
        }

        public IKoszyk WyprodukujKoszyk()
        {
            return new KoszykDoMiejskiego();
        }
    }
    
    class FabrykaCzesciDoGorskiego : IFabrykaCzesciRowerowych
    {
        public IKierownica WyprodukujKierownice()
        {
            return new KierownicaGorski();
        }

        public IKola WyprodukujKola()
        {
            return new KolaDoGorskiego();
        }

        public IRama WyprodukujRame()
        {
            return new RamaDoGorskiego();
        }

        public IKoszyk WyprodukujKoszyk()
        {
            return null;
        }
    }

    static class AbstractFactoryExample
    {
        public static void DoWork()
        {
            var fabrykaRowerow = new FabrykaRowerow();
            Console.WriteLine("___Miejski___");
            fabrykaRowerow.Zamowienie(TypRoweru.Miejski);
            Console.WriteLine("__Gorski__");
            fabrykaRowerow.Zamowienie(TypRoweru.Gorski);
        }
    }
}