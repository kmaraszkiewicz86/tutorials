var text = "Witaj, świecie!"
text = "Cześć, to jest Swift!"

var five = 5
var four = 4
var sum = four + five

// Ponowna deklaracja zmiennej.
//var five = 2 + 3

let helloWorld = "Witaj, świecie!"
//helloWorld = "Cześć, to jest Swift!" // Kompilator wygeneruje komunikat błędu.

// Ponowna deklaracja zmiennej.
//var text:String = "Witaj, świecie!"

// Ponowna deklaracja zmiennej.
//var a, b, sum: Double

var greeting: String, age:Int, money:Double

var x:Double = 3.0, b:Bool = true

// Ponowna deklaracja zmiennej.
// Inferencja typu.
//var x = 3.0, b = true

// Typ opcjonalny.

var fiveOrNothing: Int? = 5
// Konstrukcja if zostanie omówiona dalej w tym rozdziale.
if let five = fiveOrNothing {
    print(five);
} else {
    print("Brak wartości!");
}

fiveOrNothing = nil

// Konstrukcja if zostanie omówiona dalej w tym rozdziale.

if let five = fiveOrNothing {
    print(five);
} else {
    print("Brak wartości!");
}


let number = 5
let divisor = 3
let remainder = number % divisor // Stała remainder ma wartość w postaci liczby całkowitej.
let quotient = number / divisor // Stała quotient ma wartość w postaci liczby całkowitej.

let hey = "Cześć"
let greetingSwift = hey + " Swift 4!" // Operator + przeprowadza konkatenację ciągów tekstowych.


// Typ wyliczeniowy.
enum AnEnumeration {
    // Miejsce na definicje wartości.
}

enum GameInputDevice {
    case keyboard, joystick, mouse
}

var input = GameInputDevice.mouse
//...
// W dalszej części kodu.
input = .joystick

let num = 5
if num % 2 == 0 {
    print("Liczba \(num) jest parzysta.")
} else {
    print("Liczba \(num) jest nieparzysta.")
}

var logicalCheck = 7 > 5
if (logicalCheck) {
    // Kod przeznaczony do wykonania, gdy wartością sprawdzanego warunku jest true.
} else {
    // Kod przeznaczony do wykonania, gdy wartością sprawdzanego warunku jest false.
}

// Pętla.
let collection = [1, 2, 3]
for variable in collection {
    // Dowolna operacja.
}

// Ponowne użycie zmiennej sum.
sum = 0
for index in 1...10 {
    sum += index
    print("(index)")
}
print("Suma: \(sum)")
// Wartość zmiennej sum wynosi 55.

let threeTimes = 3
for _ in 1...threeTimes {
    print("Wyświetl ten komunikat.")
}

var i = 1
let max = 10
sum = 0
while i <= max {
    sum += i
    i += 1
}
print("Suma: \(sum)")

i = 1
sum = 0
repeat {
    sum += i
    i += 1
} while i <= max
print("Suma: \(sum)")


let point = (1, 1)
switch point {
case let (x, y) where x == y:
    print("X wynosi \(x). Y wynosi \(y). Mają tę samą wartość.");
case (1, let y):
    print("X wynosi 1. Y wynosi \(y). Mogą mieć różne wartości.");
case (let x, 1):
    print("X wynosi \(x). Y wynosi 1. Mogą mieć różne wartości.");
case let (x, y) where x > y:
    print("X wynosi \(x). Y wynosi \(y). X ma większą wartość niż Y.");
default:
    print("Czy jesteś pewien?")
}


func printSum() {
    let a = 3
    let b = 4
    print("Suma \(a) + \(b) = \(a + b)")
}

printSum()

func functionName(argumentLabel variableName:String) -> String {
    let returnedValue = "przekazano wartość " + variableName
    return returnedValue
}
// Tutaj mamy wywołanie funkcji i zwrot wygenerowanej przez nią wartości.
let resultOfFunctionCall = functionName(argumentLabel: "nic")


func concatenateStrings(_ s1:String, _ s2:String) -> String {
    return s1 + s2
}
let helloSwift = concatenateStrings("Witaj ", "Swift!")
// Lub:
concatenateStrings("Witaj ", "Swift!")


// Zdefiniowanie funkcji odszukującej największy element i jego indeks w tablicy liczb całkowitych.
func maxItemIndex(numbers:[Int]) -> (item:Int, index:Int) {
    var index = -1
    var max = Int.min
    // Użycie tego zapisu do połączenia indeksu z poszczególnymi elementami.
    for (i, val) in numbers.enumerated() {
        if max < val {
            max = val
            index = i
        }
    }
    
    return (max, index)
}

let maxItemTuple = maxItemIndex(numbers: [12, 2, 6, 3, 4, 5, 2, 10])
if maxItemTuple.index >= 0 {
    print("Największy element to \(maxItemTuple.item).")
}
// Wyświetlenie komunikatu "Największy element to 12.".


func generateGreeting(greet:String, thing:String = "świecie") -> String {
    return greet + thing + "!"
}

print(generateGreeting(greet: "Witaj, "))
print(generateGreeting(greet: "Witaj, ", thing: " Swift 4"))

func maxValue(_ numbers:Int...) -> Int {
    var max = Int.min
    for v in numbers {
        if max < v {
            max = v
        }
    }
    
    return max
}

print(maxValue(1, 2, 3, 4, 5))
// Wyświetlona będzie wartość 5.


func updateVar(_ x: inout Int, newValue: Int = 5) {
    x = newValue
}

var ten = 10
print(ten)
updateVar(&ten, newValue: 15)
print(ten)

func generateGreeting(_ greeting: String?) -> String {
    guard let greeting = greeting else {
        // Nie ma powitania, więc należy przeprowadzić pewną operację i zakończyć działanie.
        return "Brak powitania:("
    }
    // Jest powitanie, więc należy wygenerować odpowiedni komunikat.
    return greeting + " Swift 4!"
}

print(generateGreeting(nil))
print(generateGreeting("Cześć"))
