//
//  City.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 30/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

class City: Codable {
    var name: String
    
    init(name: String) {
        self.name = name
    }
    
    static var NewYork: City = {
        return City(name: "New York")
    }()
}
