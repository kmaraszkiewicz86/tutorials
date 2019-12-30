//
//  Location.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 29/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

public struct Location {
    
    var city: City
    
    var name: String {
        return city.name
    }
    
    init(city: City) {
        self.city = city
    }
}
