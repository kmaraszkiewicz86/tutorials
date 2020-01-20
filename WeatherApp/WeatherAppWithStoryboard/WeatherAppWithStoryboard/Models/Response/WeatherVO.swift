//
//  WeatherVO.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 20/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

struct WeatherVO: Codable {
    var temperature: Double
    
    var pressure: Int
    
    var humidity: Int
    
    var minTemperature: Double
    
    var maxTemperature: Double
    
    enum CodingKeys: String, CodingKey {
        case temperature = "temp"
        case pressure
        case humidity
        case minTemperature = "temp_min"
        case maxTemperature = "temp_max"
    }
}
