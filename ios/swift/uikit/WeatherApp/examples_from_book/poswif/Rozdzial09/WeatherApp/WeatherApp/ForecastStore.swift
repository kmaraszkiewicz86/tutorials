//
//  ForecastStore.swift
//  WeatherApp
//
//  Created by Emil Atanasov on 24.01.18.
//  Copyright © 2018 Appose Studio Inc. All rights reserved.
//

import Foundation
import UIKit
import Alamofire

class ForecastStore {
    public static let instance:ForecastStore = ForecastStore()
    enum LoadingError {
        case invalidCity
        case noConnection
        case invalidURL
        case wrongResponse
    }
    
    static let WEATHER_API = "https://api.openweathermap.org/data/2.5/weather"
    static let WEATHER_API_QUERY = "?appid=b9c42822d232ff6d3fe31938d37090cb&units=metric"
    
    private init() {
        print("Miejsce na kod inicjalizacji.")
    }
    
    public func loadForecast(for city: City, callback: @escaping (_ response:WeatherResponse?, _ error:LoadingError?)->()) {
        guard let cityId = city.id else {
            callback(nil, LoadingError.invalidCity)
            return
        }
        
        let configuration = URLSessionConfiguration.default
        let session = URLSession(configuration: configuration)
        
        let urlString = ForecastStore.WEATHER_API
            + ForecastStore.WEATHER_API_QUERY + "&id=" + String(describing: cityId)
        print(urlString)
        if let url = URL(string: urlString) {
            let task = session.dataTask(with: url) { (data, response, error) in
                if let _ = error {
                    callback(nil, LoadingError.wrongResponse)
                } else {
                    guard let data = data else {
                        callback(nil, LoadingError.wrongResponse)
                        return
                    }
                    do {
                        // Debugowanie.
                        let rawData = String(data: data, encoding: String.Encoding.utf8)
                        print(rawData)
                        
                        let decoder = JSONDecoder()
                        let responseModel = try decoder.decode(WeatherResponse.self, from: data)
                        print(responseModel.name)
                        callback(responseModel, nil)
                    } catch let err {
                        print("Błąd", err)
                        callback(nil, LoadingError.wrongResponse)
                    }
                }
            }
            task.resume()
        }
    }
    
    func loadForecastAlamofire(for city:City, callback: @escaping (WeatherResponse?, LoadingError?) -> ()) {
        guard let cityId = city.id else {
            callback(nil, LoadingError.invalidCity)
            return
        }
        
        let urlString = ForecastStore.WEATHER_API
            + ForecastStore.WEATHER_API_QUERY + "&id=" + String(describing: cityId)
        
        Alamofire.request(urlString).responseJSON { response in
            // Debugowanie.
            print("Żądanie: \(String(describing: response.request))")
            print("Odpowiedź: \(String(describing: response.response))")
            print("Wynik: \(response.result)")
            if let json = response.result.value {
                print("JSON: \(json)") // Serializowana odpowiedź JSON.
            }
            
            guard let data = response.data else {
                callback(nil, LoadingError.wrongResponse)
                return
            }
            do {
                // Debugowanie.
                let rawData = String(data: data, encoding: String.Encoding.utf8)
                print(rawData)
                
                let decoder = JSONDecoder()
                let responseModel = try decoder.decode(WeatherResponse.self, from: data)
                print(responseModel.name)
                callback(responseModel, nil)
            } catch let err {
                print("Błąd", err)
                callback(nil, LoadingError.wrongResponse)
            }
            
        }
    }
}
