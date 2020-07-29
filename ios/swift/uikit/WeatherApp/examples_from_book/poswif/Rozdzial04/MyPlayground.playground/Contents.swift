//: Playground - noun: a place where people can play

import UIKit

//: struct Car
struct Car {
    var name:String = "brak nazwy"
    var speed = 0
    var maxSpeed = 200
    
    // Metoda.
    func getDescription() -> String {
        return "\(self.name) ma szybkość maksymalną \(self.maxSpeed)"
    }
    // Właściwość - getter (to też metoda) - choć
    // wywoływana nieco inaczej.
    var description:String {
        get {
            return self.getDescription()
        }
    }
}

var ferrari = Car(name:"Ferrari F40", speed: 280, maxSpeed:320)

print(ferrari.getDescription())
print(ferrari.description)

class Permit {
    var validUntil = 2017
    
    init(validUntil: Int) {
        self.validUntil = validUntil
        
        print("Obiekt Permit został utworzony.")
    }
    
    deinit {
        print("Egzemplarz został usunięty.")
    }
}

class Ship {
    var speed = 0
    var isFlying = false
    
    var description:String {
        get {
            return "Szybkość statku wynosi \(self.speed) i \(self.isFlying ? "" : "nie") może on latać."
        }
    }
    
    // Metoda inicjalizacyjna.
    init(speed:Int, isFlying:Bool) {
        self.speed = speed
        self.isFlying = isFlying
    }
    
    // Wygodna wersja metody inicjalizacyjnej.
    convenience init(speed:Int) {
        self.init(speed: speed, isFlying: false);
    }
    
    internal var _id:String = "no-id"
    var serialNumber: String {
        get {
            return self._id
        }
        set {
            _id = newValue
        }
    }
    // Tylko do odczytu.
    var version: String {
        return "1.0.0"
    }
    
    lazy var permit:Permit = Permit(validUntil: 2100)
    
    static let madeIn = "UK"
    
    //
    func calculateDistance(time:Int) -> Int {
        return self.speed * time
    }
}


//let ship = Ship()
//ship.speed = 10
//ship.isFlying = false
//print(ship.description)

let ship = Ship(speed: 10)
print(ship.description)

ship.serialNumber = "mój-pierwszy-statek"
print(ship.serialNumber)

print("Wersja statku: \(ship.version)")

// Właściwość version jest tylko do odczytu, więc kolejne polecenie spowoduje wygenerowanie błędu przez kompilator.
//ship.version = "4.0.0"

print("Upoważnienie jest ważne do roku \(ship.permit.validUntil)")

//ship.permit = Permit(validUntil:3000)

// Rozszerzenie klasy.
extension Ship {
    convenience init(type:String) {
        if(type == "super-sonic") {
            self.init(speed:2000, isFlying: true);
        } else {
            self.init(speed: 10, isFlying: false);
        }
    }
}

// Rozszerzenie struktury.
extension Car {
    // Nie ma potrzeby umieszczania na początku polecenia słowa kluczowego convenience.
    init(name:String) {
        self.name = name
    }
    
    init(name:String, maxSpeed: Int) {
        self.name = name
        self.maxSpeed = maxSpeed
    }
}

var cars = [Car(), Car(name:"Ferrari"), Car(name: "Tesla", maxSpeed: 320), Car(name:"Porshe", speed:50, maxSpeed: 260)]

var ships = [Ship(speed:20), Ship(type:"super-sonic")]


// Dodawanie koloru.
extension Car {
    enum Color {
        case red, blue, silver, green, pink, undefined
    }
    
    func getTypicalColor() -> Color {
        if self.name == "Ferrari" {
            return .red
        }
        
        if self.name == "Tesla" {
            return .silver
        }
        
        if self.name == "Tesla Blue" {
            return .blue
        }
        
        return .undefined
    }
}


print(Car(name: "Tesla Blue").getTypicalColor() == Car.Color.blue ? "Samochód jest niebieski." : "Jaki to kolor?")

//extension Ship {
//    static let madeIn = "UK"
//}

extension Car {
    // Obliczana właściwość tylko do odczytu.
    static var bestCarBrand:String {
        get { return "Tesla" }
    }
    // Przechowywana właściwość typu.
    static var totalNumberOfCars = 0
}

print("Wyprodukowano w \(Ship.madeIn)")

// Uzyskanie dostępu do metadanych typu.
var typeOfShipConstant = type(of: ship)
var typeShip = Ship.self
print("Typ to \(typeOfShipConstant)")
print("Typ to \(typeShip)")
print("Typ Ship.self to \(type(of: typeShip))")




//: Dziedziczenie.


//: Funkcje statyczne kontra funkcje klasy.
class SuperShip:Ship {
    class func getTypeName() -> String {
        return "SuperShip"
    }
}

class MegaShip:SuperShip {
    override class func getTypeName() -> String {
        return "Mega-SuperShip"
    }
}

print(Ship.madeIn)
MegaShip.getTypeName()

print(SuperShip.getTypeName())
print(MegaShip.getTypeName())


// Struktura Weather w akcji.
var emptyForecast = ForecastData()
var weather = Weather()
weather = Weather(hours:[emptyForecast], location:"Gliwice", date:Date())
print(weather.location)

// Klasa SpaceShip dziedziczy po klasie Ship.
class SpaceShip:Ship {
    var numberOfLazerGuns:Int
    init() {
        // Inicjalizacja właściwości lokalnych.
        self.numberOfLazerGuns = 4
        // Inicjalizacja właściwości odziedziczonych.
        // Wywołanie desygnowanej metody inicjalizacyjnej.
        super.init(speed: 50000, isFlying: true)
        
    }
    
//    override var description:String {
//        get {
//            return "Szybkość statku kosmicznego (🚀) wynosi \(self.speed) km/s."
//        }
//    }
    
    override var description:String {
        get {
            return super.description
        }
    }
    
    override func calculateDistance(time: Int) -> Int {
        return super.calculateDistance(time: time) * 2
    }
}

extension SpaceShip {
    convenience init(lazerGuns:Int) {
        // Wywołanie desygnowanego konstruktora.
        self.init()
        self.numberOfLazerGuns = lazerGuns
    }
    
    convenience init(speed:Int, lazerGuns:Int) {
        // Wywołanie desygnowanego konstruktora.
        self.init()
        self.speed = speed
        self.numberOfLazerGuns = lazerGuns
    }
}


var spaceShip = SpaceShip(lazerGuns: 5)
print("Szybkość statku SpaceShip wynosi \(spaceShip.speed)")

print(spaceShip.description)


print(spaceShip.calculateDistance(time: 60 * 60))



// Przykład architektury MVC.
var dc = DateComponents()
dc.year = 2017
dc.month = 7
dc.day = 7
// Utworzenie daty.
let coolDate = Calendar.current.date(from: dc)!

var newYourWeather = Weather(hours:[emptyForecast], location: "Nowy Jork", date:  coolDate)
var sanFranciscoWeather = Weather(hours:[emptyForecast], location: "San Francisco", date: coolDate )

var model = WeatherModel(weather: newYourWeather)
var controller = WeatherController()
// Kontroler potrzebuje modelu.
controller.model = model
model.modelObserver = controller

var view = WeatherView(location: controller.location, date: controller.date, listener: controller)

// Kontroler potrzebuje widoku.
controller.view = view

// Wygenerowanie widoku początkowego.
view.draw()

// Symulacja uaktualnienia modelu i widoku, jeśli trzeba.
model.setNewWeater(weather: sanFranciscoWeather)

// Symulacja akcji użytkownika i uaktualnienia modelu, jeśli trzeba.
view.simulateUserAction()


