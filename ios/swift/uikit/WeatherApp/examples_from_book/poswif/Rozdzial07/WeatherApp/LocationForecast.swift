//
//  LocationForecast.swift
//  WeatherApp
//
//  Created by Emil Atanasov on 9/14/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import Foundation
import UIKit

extension Date {
    
    var yesterday: Date {
        return Calendar.current.date(byAdding: .day, value: -1, to: self)!
    }
    
    var tomorrow: Date {
        return Calendar.current.date(byAdding: .day, value: 1, to: self)!
    }
    
    var noon: Date {
        return Calendar.current.date(bySettingHour: 12, minute: 0, second: 0, of: self)!
    }
    
    var midnight: Date {
        let cal = Calendar(identifier: .gregorian)
        return cal.startOfDay(for: self)
    }
    
    var month: Int {
        return Calendar.current.component(.month,  from: self)
    }
    
    var isLastDayOfMonth: Bool {
        return tomorrow.month != month
    }
}

// Model kraju i miasta.
class Country {
    var name = "brak"
    var cities:[City] = []
    
    init(name:String) {
        self.name = name
    }
    
    init(name:String, cities:[City]) {
        self.name = name
        self.cities = cities
    }
}

public class City: Codable {
    var name: String
    
    init(name:String) {
        self.name = name
    }
    
    static var NewYork: City {
        get {
            return City(name: "Nowy Jork")
        }
    }
}

public struct Location: Codable {
    var city:City
    
    init(city: City) {
        self.city = city
    }
    
    var name: String {
        get {
            return self.city.name
        }
    }
    
    var timeZone:Int = 0
    var temperature:String = "-"
}

// Pobranie listy wszystkich dostępnych lokalizacji.
extension Country {
    static public func getHardcodedData() -> [Country] {
        
        var countries:[Country] = []
        
        // Dodanie wybranych krajów europejskich.
        let germany = Country(name:"Niemcy")
        
        germany.cities += [City(name: "Berlin")]
        germany.cities += [City(name: "Hamburg")]
        germany.cities += [City(name: "Monachium")]
        germany.cities += [City(name: "Kolonia")]
        
        countries.append(germany)
        
        let italy = Country(name: "Włochy")
        
        italy.cities += [City(name:"Rzym")]
        italy.cities += [City(name:"Mediolan")]
        italy.cities += [City(name:"Neapol")]
        italy.cities += [City(name:"Wenecja")]
        
        countries.append(italy)
        
        let france = Country(name:"Francja")
        
        france.cities += [City(name:"Paryż")]
        france.cities += [City(name:"Marsylia")]
        france.cities += [City(name:"Lyon")]
        
        countries.append(france)
        
        let uk = Country(name:"Wielka Brytania")
        
        uk.cities += [City(name:"Londyn")]
        uk.cities += [City(name:"Birmingham")]
        uk.cities += [City(name:"Leeds")]
        uk.cities += [City(name:"Glasgow")]
        
        countries.append(uk)
        
        let spain = Country(name:"Hiszpania")
        
        spain.cities += [City(name:"Madryt")]
        spain.cities += [City(name:"Barcelona")]
        spain.cities += [City(name:"Walencja")]
        
        countries.append(spain)
        
        return countries
    }
}

public class Forecast {
    var date:Date
    var weather:String = "brak"
    var temperature = 100
    
    public init(date:Date, weather: String, temperature: Int) {
        self.date = date
        self.weather = weather
        self.temperature = temperature
    }
}

public class DailyForecast : Forecast {
    var isWholeDay = false
    var minTemp = -100
    var maxTemp = 100
}


public class LocationForecast {
    
    public static let degreeSymbol = "°"
    
    var location:Location?
    var weather:String?
    
    var forecastForToday:[Forecast]?
    var forecastForNextDays:[DailyForecast]?
    
    // Utworzenie przykładowych danych i ich wyświetlenie w interfejsie użytkownika.
    static func getTestData() -> LocationForecast {
        
        let aMinute = 60
        
        let location = Location(city: City.NewYork)
        let forecast = LocationForecast()
        
        forecast.location = location
        forecast.weather = "słonecznie"
        
        // Dzisiaj.
        let today = Date().midnight
      
        var detailedForecast:[Forecast] = []

        for i in 0...23 {
            detailedForecast.append(Forecast(date: today.addingTimeInterval(TimeInterval(60 * aMinute * i)), weather: "słonecznie",temperature: 25))
        }
        
        forecast.forecastForToday = detailedForecast
        
        
        let tomorrow = DailyForecast(date: today.tomorrow, weather: "słonecznie",temperature: 25)
        tomorrow.isWholeDay = true
        tomorrow.minTemp = 23
        tomorrow.maxTemp = 27
        
        let afterTomorrow = DailyForecast(date: tomorrow.date.tomorrow, weather: "częściowe zachmurzenie",temperature: 25)
        afterTomorrow.isWholeDay = true
        afterTomorrow.minTemp = 24
        afterTomorrow.maxTemp = 28
        
        forecast.forecastForNextDays = [tomorrow, afterTomorrow]

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



