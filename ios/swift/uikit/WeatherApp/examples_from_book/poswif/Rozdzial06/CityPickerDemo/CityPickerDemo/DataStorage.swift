//
//  DataStorage.swift
//  CityPickerDemo
//
//  Created by Emil Atanasov on 7/31/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import Foundation

class Country {
    var name = "brak"
    var cities:[City] = []
    
    init(name:String) {
        self.name = name
    }
    
    init(name:String, cities:[City]) {
        self.name = name
        self.cities = cities
    }
}

class City {
    var name: String
    var population: Int
    
    init(name:String, population:Int) {
        self.name = name
        self.population = population
    }
}

class Capital : City {
    var isActive = true
}


extension Country {
    static public func getHardcodedData() -> [Country] {
        
        var countries:[Country] = []
        
        // Dodanie wybranych krajów europejskich.
        let germany = Country(name:"Niemcy")
        
        germany.cities += [Capital(name: "Berlin", population: 3_426_354)]
        germany.cities += [City(name: "Hamburg", population: 1_739_117)]
        germany.cities += [City(name: "Monachium", population: 1_260_391)]
        germany.cities += [City(name: "Kolonia", population: 963_395)]
        
        countries.append(germany)
        
        let italy = Country(name: "Włochy")
        
        italy.cities += [Capital(name:"Rzym", population:2_648_843)]
        italy.cities += [City(name:"Mediolan", population:1_305_591)]
        italy.cities += [City(name:"Neapol", population:1_046_987)]
        italy.cities += [City(name:"Wenecja", population:297_743)]
        
        
        countries.append(italy)
        
        let france = Country(name:"Francja")
        
        france.cities += [Capital(name:"Paryż", population: 2_152_000)]
        france.cities += [City(name:"Marsylia", population: 808_000)]
        france.cities += [City(name:"Lyon", population: 422_000)]
        
        countries.append(france)
        
        let uk = Country(name:"Wielka Brytania")
        
        uk.cities += [Capital(name:"Londyn", population: 7_074_265)]
        uk.cities += [City(name:"Birmingham", population: 1_020_589)]
        uk.cities += [City(name:"Leeds", population: 726_939)]
        uk.cities += [City(name:"Glasgow", population: 616_430)]
        
        countries.append(uk)
        
        let spain = Country(name:"Hiszpania")
        
        spain.cities += [Capital(name:"Madryt", population: 2_824_000)]
        spain.cities += [City(name:"Barcelona", population: 1_454_000)]
        spain.cities += [City(name:"Walencja", population: 736_000)]
        
        countries.append(spain)
        
        return countries
    }
}
