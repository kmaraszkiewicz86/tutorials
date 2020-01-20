//
//  WeatherResponse.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 20/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

struct WeatherResponse: Codable {
    var weather: [WeatherInfoVO]
    
    var visibility: Int
    
    var wind: WindVO
    
    var time: Int
    
    var name: String
    
    var id: Int
    
    var responseCode: Int
    
    var forecast: WeatherVO
    
    enum CodingKeys: String, CodingKey {
        case weather
        
        case visibility
        
        case wind
        
        case time = "dt"
        
        case name
        
        case id
        
        case responseCode = "cod"
        
        case forecast = "main"
    }
}
