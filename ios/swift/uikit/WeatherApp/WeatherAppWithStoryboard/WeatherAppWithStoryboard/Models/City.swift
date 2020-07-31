//
//  City.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 30/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

class City: Codable {
    
    var id: Int?
    
    var name: String
    
    convenience init (id: Int, name: String) {
        self.init(name: name)
        
        self.id = id
    }
    
    init(name: String) {
        self.name = name
    }
    
    static var NewYork: City = {
        return City(id: 5128638, name: "New York")
    }()
}
