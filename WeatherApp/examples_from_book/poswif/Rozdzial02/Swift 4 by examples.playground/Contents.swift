//: Playground - noun: a place where people can play
// Dowolny komentarz.
import UIKit


var str = "Witaj, playground!"

switch str {
case "swift":
    print("Witaj, Swift 4!")
default:
    print("Kim jesteś?")
}

var sum = 0
for i in 1...5 {
    print("\(i)")
    
    sum += i
}

test("Cześć")

let img = UIImage(named: "open_xcode.png")


// Kod znaczników.
//: # Nagłówek duży
//: ## Nagłówek zwykły
//: ### Nagłówek mały
 
 
//: Lista
//: * opcja 1
//: * opcja 2
//: * opcja 3
//:   - opcja 3.1
 
//: Lista numerowana
//: 1. opcja 1
//: 1. opcja 2
//: 1. opcja 3
 
 
//: To jest tekst _pochylony_.
 
//: To jest tekst __pogrubiony__.
 
//: To jest tekst ___pogrubiony i pochylony___.
 
//: To jest tekst **pogrubiony**.
 
//: Kod ```let x = 5``` ma zupełnie inny styl.
 
//: Oto definicja przykładu:
//: - Example:
//: `var a = 13`\
//: `var b = a + 7`
 
 
//: Oto definicja uwagi:
//: - Note:
//:"Możesz zacząć poznawać język Swift 4."
//: \
//: \
//: Projekt typu playground to naprawdę wygodny sposób na naukę Swift 4.
 
//: ![Image from resources](open_xcode.png)

//: [Swift](https://swift.org)

