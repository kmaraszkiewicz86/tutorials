//
//  ForecastStore.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 21/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ForecastStore {
    
    public static let shared = ForecastStore()
    
    static let weatherApi = "https://api.openweathermap.org/data/2.5/weather"
    static let weatherApiQuery = "appId=3c3945aff71cbc05e8fb632fdde15e21&units=metric"
    
    private init() {
        
    }
    
    public func loadForecast(for city: City, callback: @escaping (_ response: WeatherResponse?, _ error: LoadingError?) -> Void) {
        guard let cityId = city.id else {
            callback(nil, LoadingError.invalidCity)
            return
        }
        
        let configuration = URLSessionConfiguration.default
        let session = URLSession(configuration: configuration)
        let urlString = ForecastStore.weatherApi + ForecastStore.weatherApiQuery + "&id="
            + String(describing: city.id)
        
        if let url = URL(string: urlString) {
            let task = session.dataTask(with: url) {
                (data, response, error) in
                if let _ = error {
                    callback(nil, LoadingError.wrongResponse)
                    return
                } else {
                    guard let data = data else {
                        callback(nil, LoadingError.wrongResponse)
                        return
                    }
                    
                    do {
                        let rawData = String(data: data, encoding: String.Encoding.utf8)
                        let decoder = JSONDecoder()
                        let responseModel = try decoder.decode(WeatherResponse.self, from: data)
                        callback(responseModel, nil)
                    } catch let err {
                        callback(nil, LoadingError.wrongResponse)
                    }
                }
            }
            
            task.resume()
        }
        
    }
}
