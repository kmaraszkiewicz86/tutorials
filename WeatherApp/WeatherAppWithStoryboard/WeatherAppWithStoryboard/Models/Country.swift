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
