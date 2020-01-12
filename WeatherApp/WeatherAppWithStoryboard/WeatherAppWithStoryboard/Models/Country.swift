//
//  Country.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 30/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

class Country {
    var name = "brak"
    
    var cities:[City] = []
    
    init(name: String) {
        self.name = name
    }
    
    convenience init(name: String, cities: [City]) {
        self.init(name: name)

        self.cities = cities
    }
}

extension Country {
    
    static public func getHardcodedData() -> [Country] {
        
        var countries:[Country] = []
        
        // Dodanie wybranych krajów europejskich.
        let germany = Country(name:"Niemcy")
        
        germany.cities += [City(name: "Berlin")]
        germany.cities += [City(name: "Hamburg")]
        germany.cities += [City(name: "Monachium")]
        germany.cities += [City(name: "Kolonia")]
        
        countries.append(germany)
        
        let italy = Country(name: "Włochy")
        
        italy.cities += [City(name:"Rzym")]
        italy.cities += [City(name:"Mediolan")]
        italy.cities += [City(name:"Neapol")]
        italy.cities += [City(name:"Wenecja")]
        
        countries.append(italy)
        
        let france = Country(name:"Francja")
        
        france.cities += [City(name:"Paryż")]
        france.cities += [City(name:"Marsylia")]
        france.cities += [City(name:"Lyon")]
        
        countries.append(france)
        
        let uk = Country(name:"Wielka Brytania")
        
        uk.cities += [City(name:"Londyn")]
        uk.cities += [City(name:"Birmingham")]
        uk.cities += [City(name:"Leeds")]
        uk.cities += [City(name:"Glasgow")]
        
        countries.append(uk)
        
        let spain = Country(name:"Hiszpania")
        
        spain.cities += [City(name:"Madryt")]
        spain.cities += [City(name:"Barcelona")]
        spain.cities += [City(name:"Walencja")]
        
        countries.append(spain)
        
        return countries
    }
    
}
