//
//  LocationForecast.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 29/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation
import UIKit

public class LocationForecast {
    
    var location: Location?
    
    var weather: String?
    
    var forecastForToday: [Forecast]?
    
    var forecastForNextDays: [DailyForecast]?
    
    static func getTestData() -> LocationForecast {
        
        let aMinute = 60
        let location = Location(name: "Suchy Las")
        let forecast = LocationForecast()
        forecast.location = location
        forecast.weather = "słonecznie"
        
        let calendar = Calendar.current
        
        let now = Date()
        let dateComponents = DateComponents(calendar: calendar,
                                            year: calendar.component(.year, from: now),
                                            month: calendar.component(.month, from: now),
                                            day: calendar.component(.day, from: now))
        
        let today = calendar.date(from: dateComponents)!
        let tomorrow = calendar.date(byAdding: .day, value: 1, to: today)!
        let dayAfterTomorrow = calendar.date(byAdding: .day, value: 1, to: tomorrow)!
        
        var detailedForecast: [Forecast] = []
        for i in 0...23 {
            detailedForecast.append(Forecast(
                date: today.addingTimeInterval(TimeInterval(60 * aMinute * i)),
                weather: "słonecznie",
                temperature: 25))
        }
        
        forecast.forecastForToday = detailedForecast
        let tomorrowForecast = DailyForecast(date: tomorrow,
                                     weather: "słonecznie",
                                     temperature: 25)
        
        tomorrowForecast.isWholeDay = true
        tomorrowForecast.minTemp = 23
        tomorrowForecast.maxTemp = 27
        
        let dayAfterTomorrowForecast = DailyForecast(date: dayAfterTomorrow,
                                                     weather: "Częściowe zachmurzenie",
                                                     temperature: 25)
        
        dayAfterTomorrowForecast.isWholeDay = true
        dayAfterTomorrowForecast.minTemp = 24
        dayAfterTomorrowForecast.maxTemp = 28
        
        forecast.forecastForNextDays = [tomorrowForecast, dayAfterTomorrowForecast]
        
        return forecast
        
    }
    
    static func getImageFor(weather:String) -> UIImage {
        switch weather.lowercased() {
        case "słonecznie":
            return #imageLiteral(resourceName: "sunny")
        case "deszcz":
            fallthrough
        case "lekki deszcz":
            return #imageLiteral(resourceName: "rain")
        case "śnieg":
            return #imageLiteral(resourceName: "snow")
        case "zachmurzenie":
            return #imageLiteral(resourceName: "cloudy")
        case "częściowe zachmurzenie":
            return #imageLiteral(resourceName: "partly_cloudy")
        default:
            return #imageLiteral(resourceName: "sunny")
        }
    }
    
}
