//
//  WindVO.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 20/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

struct WindVO: Codable {
    var speed: Double
    
    var degree: Double
    enum CodingKeys: String, CodingKey {
        case speed
        case degree = "deg"
    }
}
