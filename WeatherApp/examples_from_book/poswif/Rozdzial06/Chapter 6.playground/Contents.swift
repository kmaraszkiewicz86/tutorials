//: Swift 4

//: Rozdział 6.


//: Typ generyczny



struct Item<T> {
    var raw: T
    var description: String
    
    init(raw: T, description:String = "brak opisu") {
        self.raw = raw
        self.description = description
    }
}

var itemInt:Item<Int> = Item(raw: 55, description: "pięćdziesiąt pięć")
print("To jest liczba całkowita \(itemInt.raw) wraz z opisem - \(itemInt.description)")

var itemDouble:Item<Double> = Item(raw: 3.14, description: "Pi")
print("To jest liczba całkowita \(itemInt.raw) wraz z opisem - \(itemDouble.description)")

//: Tablica

var words = Array<String>(arrayLiteral: "jeden", "dwa", "trzy")
//var words = ["jeden", "dwa", "trzy"]

for word in words {
    print(word)
}

var emptyArrayOfInts = [Int]()
var emptyArrayOfInts2 = Array<Int>()
var emptyArray:[Int] = []

var tenZeros = Array(repeating: 0, count: 10)
print("Liczba elementów wynosi \(tenZeros.count).")


var even = [2, 4, 6]
var odd = [1, 3 ,5]
var concatenated = even + odd
print(concatenated)

var part = concatenated[2...4]
print(part)


//: Iteracja przez wszystkie elementy.

for value in concatenated {
    print("Element: \(value)")
}

for (index, value) in concatenated.enumerated() {
    print("Element #\(index + 1): \(value)")
}


//: Zbiór

let a = 5
let b = 5

if a.hashValue == b.hashValue {
    print("a == b")
} else {
    print("a != b")
}

var phrases = Set<String>()
phrases.insert("witaj")
phrases.insert("świecie")
phrases.insert("wit" + "aj") // "witaj".

for item in phrases {
    print(item)
}

var cars:Set = ["Tesla", "Ferari", "Audi"]

for item in cars {
    print(item)
}

var electricCars:Set = ["Tesla", "Volkswagen"]

var intersection = electricCars.intersection(cars)
print("Część wspólna: \(intersection)")
// Część wspólna: ["Tesla"]

var union = electricCars.union(cars)
print("Unia: \(union)")
//Unia: ["Ferari", "Volkswagen", "Audi", "Tesla"]

var substract = electricCars.subtracting(cars)
print("Różnica: \(substract)")
// Różnica: ["Volkswagen"]

var symetricDifference = electricCars.symmetricDifference(cars)
print("Różnica symetryczna: \(symetricDifference)")
// Różnica symetryczna: ["Ferari", "Volkswagen", "Audi"]

if electricCars.isSubset(of: union) {
    print("Każdy zbiór jest podzbiorem unii wszystkich zbiorów.")
}

if union.isSuperset(of: cars) {
    print("Unia to nadzbiór wszystkich zbiorów.")
}

if electricCars.isDisjoint(with: cars) {
    print("Te zbiory nie mają części wspólnej.")
} else {
    print("Te zbiory mają przynajmniej jeden wspólny element.")
}

electricCars.isStrictSuperset(of: )

//: Słownik

var animalsDictionary = Dictionary<String, String>(dictionaryLiteral: ("pies", "🐶"), ("kot", "🐱"))
var animalsDictionaryLiteral = ["pies": "🐶","kot": "🐱"]
// Dodanie nowego powiązania.
animalsDictionary["ptak"] = "🐦"

for association in animalsDictionary {
    print("\(association.key) -> \(association.value)")
}

// Wszystkie pary klucz-wartość.
for (animalName, animalEmoji) in animalsDictionary {
    print("\(animalName) -> \(animalEmoji)")
}

// Wszystkie klucze.
for animalName in animalsDictionary.keys {
    print("\(animalName)")
}
// Wszystkie wartości.
for animalEmoji in animalsDictionary.values {
    print("\(animalEmoji)")
}

var allEmojis = [String](animalsDictionary.values)


var emptyDict:Dictionary<Int, String> = [:]
var emptyMap = [Int: String]()

//: UICollectionView

import UIKit
import PlaygroundSupport

class CollectionViewController : UICollectionViewController {
    
    // Procedura obsługi selektora.
    
    override public func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        
        let animal = self.data[indexPath.row]
        print(animal)
    }
    
    
    var data:[String]
    
    init(data:[String], collectionViewLayout layout: UICollectionViewLayout) {
        self.data = data
        super.init(collectionViewLayout: layout)
        
    }
    // To jest wymagane i po prostu stosujemy delegata.
    required init?(coder aDecoder: NSCoder) {
        self.data = []
        super.init(coder: aDecoder)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.collectionView?.backgroundColor = .white
        self.collectionView?.register(AnimalCollectionViewCell.self, forCellWithReuseIdentifier: "Cell")
    }
    
    // Liczba elementów w poszczególnych sekcjach.
    override func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return self.data.count
    }
    
    override func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell:AnimalCollectionViewCell = collectionView.dequeueReusableCell(withReuseIdentifier: "Cell", for: indexPath) as! AnimalCollectionViewCell
        cell.backgroundColor = .green
        
        let animal = self.data[indexPath.row]
        cell.emoji = animal
        
        if self.dataMap != nil {
            cell.emoji = self.dataMap?[animal]
        } else {
            cell.emoji = animal
        }
 
        return cell
    }
    
    var dataMap:[String:String]?
}

/**
 * Niestandardowa klasa UICollectionViewCell wraz z etykietą na górze.
 */
class AnimalCollectionViewCell :UICollectionViewCell {
    
    private var _label: UILabel
    
    override init(frame: CGRect) {
        let fr = CGRect.init(x: 0, y: 0, width: frame.size.width, height: frame.size.height)
        self._label = UILabel(frame:fr)
        super.init(frame: frame)
        
        _label.text = "?"
        _label.textAlignment = NSTextAlignment.center
        addSubview(_label)
    }
    // Metoda używana podczas inicjalizacji interfejsu użytkownika z pliku Storyboard.
    required init?(coder aDecoder: NSCoder) {
        self._label = UILabel()
        super.init(coder:aDecoder)
        
        _label.text = "?"
        
        addSubview(_label)
    }
    
    public var emoji:String? {
        set {
           _label.text = newValue
        }
        
        get {
            return _label.text
        }
    }
}

//: PuzzleViewLayout
class PuzzleViewLayout : UICollectionViewLayout {
    // Liczba kolumn w układzie.
    var columns: Int = 2
    var padding: CGFloat = 6.0
    // Kolekcja wszystkich atrybutów.
    var layoutAttributes = [UICollectionViewLayoutAttributes]()
    // Wielkość zawartości.
    var contentHeight: CGFloat  = 0.0
    var contentWidth: CGFloat  = 0.0
    
    override func prepare() {
        layoutAttributes.removeAll()
        
        let insets = collectionView!.contentInset
        self.contentWidth = collectionView!.bounds.width - (insets.left + insets.right)
        
        let columnWidth = self.contentWidth / CGFloat(columns)
        
        var column = 0
        // Przesunięcie w pionie.
        var topOffset = [CGFloat](repeating: 0, count: columns)
        // Przesunięcie w poziomie.
        var offset = [CGFloat]()
        
        for column in 0 ..< columns {
            offset += [CGFloat(column) * columnWidth]
        }
        
        // Uwzględniona jest tylko pierwsza sekcja.
        let section = 0
        
        for item in 0 ..< collectionView!.numberOfItems(inSection: section) {
            
            let indexPath = IndexPath(row: item, section: section)
            
            // Losowe określenie wysokości poszczególnych komórek.
            let height:CGFloat = 70 + CGFloat(arc4random_uniform(25) * 10)
            // Użycie obliczonych wartości dla poprzednich elementów.
            let frame = CGRect(x: offset[column], y: topOffset[column], width: columnWidth, height: height)
            
            let insetFrame = frame.insetBy(dx: padding, dy: padding)
            
            let attributes = UICollectionViewLayoutAttributes(forCellWith: indexPath)
            attributes.frame = insetFrame
            self.layoutAttributes.append(attributes)
            
            // Rozciągnięcie granic widoku zawartości.
            self.contentHeight = max(frame.maxY, contentHeight)
            
            // Przejście do następnego położenia Y.
            topOffset[column] = topOffset[column] + height
            
            // Przejście do następnej kolumny i pozostanie w prawidłowym indeksie [0 .. columns - 1].
            column = (column + 1) % columns
        }
        
    }
    // Zwrot wielkości całego widoku kolekcji.
    override var collectionViewContentSize : CGSize {
        return CGSize(width: contentWidth, height: contentHeight)
    }
    
    override func layoutAttributesForElements(in rect: CGRect) -> [UICollectionViewLayoutAttributes]? {
        
        var attrs = [UICollectionViewLayoutAttributes]()
        
        // Przekazanie wszystkich elementów, które są widoczne w bieżącym prostokącie.
        for itemAttributes in self.layoutAttributes {
            if itemAttributes.frame.intersects(rect) {
                attrs.append(itemAttributes)
            }
        }
        
        return attrs
    }
    
}


var animals = ["kot", "pies", "ptak", "mysz", "słoń"]
animals.append("niedźwiedź")

var animalsToEmoji = ["kot": "🐱", "pies": "🐶", "ptak": "🐦" , "mysz" : "🐭", "słoń" :" 🐘","niedźwiedź":"🐻"]

var flowLayout:UICollectionViewLayout = UICollectionViewFlowLayout()

//flowLayout.itemSize = CGSize.init(width: 200, height: 200)
//flowLayout.minimumLineSpacing = 50.0
//flowLayout.scrollDirection = UICollectionViewScrollDirection.horizontal

flowLayout = PuzzleViewLayout()

var controller = CollectionViewController(data:animals, collectionViewLayout: flowLayout)
controller.dataMap = animalsToEmoji

PlaygroundPage.current.liveView = controller
PlaygroundPage.current.needsIndefiniteExecution = true



//: Protokół

protocol CustomContractProtocol {
    // Lista wszystkich wymagań (metody i właściwości).
}

struct MyStruct : CustomContractProtocol {
    // Wszystkie właściwości.
    
    // Wszystkie wymagania protokołu.
}

class BaseClass {
    // Pusta klasa bazowa.
}

class MyClass : BaseClass, CustomContractProtocol {
    // Wszystkie właściwości.
    
    // Wszystkie wymagania protokołu.
}


protocol GeoLocationProtocol {
    var long: Double { get set }
    var lat: Double { get set }
    var name: String { get }
    // Funkcja obliczająca odległość od pewnych współrzędnych geograficznych.
    func calculateDistance(to: GeoLocationProtocol) -> Double
}

protocol InitProtocol {
    init(from: Int)
}

class MyInt : InitProtocol {
    var value:Int
    
    required init(from: Int) {
        // Miejsce na dowolny kod.
        self.value = from
    }
    // Metoda inicjalizacyjna, której działanie może zakończyć się niepowodzeniem.
    init?(from: Double) {
        self.value = Int(floor(from))
        
        if(Double(self.value) != from) {
            return nil
        }
    }
}

var myInt = MyInt(from: 3)
var myDouble = MyInt(from: 3.2)

if myDouble != nil {
    print("Wartość wynosi \(myDouble?.value)")
} else {
    print("Wartość to nil.")
}


protocol A {
    var a:Int {get}
}

protocol B {
    var b: Int {get}
}
// Dziedziczenie protokołu.
protocol C: A, B {
    var c: Int {get}
}

class MyTuple : C {
    var a: Int = 0
    var b: Int = 0
    var c: Int {
        get {
            return 7
        }
    }
}

func isSpecial(object: A & B) -> Bool {
    return object.a % object.b == 7
}

extension Collection where Iterator.Element : A {
    func toPrettyString() -> String {
        var s = ""
        for a in self {
            s += "\(a.a):)"
        }
        
        return s
    }
}

var arrayTuples = [MyTuple()]
print(arrayTuples.toPrettyString())
