//
//  WeatherInfoVO.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 20/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

struct WeatherInfoVO: Codable {
    var id: Int
    
    var main: String
    
    var description: String
    
    var icon: String
}
