//
//  Forecast.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 29/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

public class Forecast {
    var date: Date
    
    var weather = "brak"
    
    var temperature = 100
    
    init(date: Date, weather: String, temperature: Int) {
        self.date = date
        self.weather = weather
        self.temperature = temperature
    }
}
