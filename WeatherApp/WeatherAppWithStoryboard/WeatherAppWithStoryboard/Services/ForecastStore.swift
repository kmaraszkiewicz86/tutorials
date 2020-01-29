//
//  ForecastStore.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 21/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import Alamofire

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
        
        let urlString = ForecastStore.weatherApi + ForecastStore.weatherApiQuery + "&id="
            + String(describing: cityId)
        
        
        Alamofire.request(urlString).responseJSON { (response) in
            
            guard let data = response.data else {
                callback(nil, LoadingError.wrongResponse)
                return
            }
            
            do {
                let rawData = String(data: data, encoding: String.Encoding.utf8)
                
                print(rawData ?? "request: n/a")
                
                let decoder = JSONDecoder()
                let responseModel = try decoder.decode(WeatherResponse.self, from: data)
                callback(responseModel, nil)
            } catch let err {
                
                print(err)
                
                callback(nil, LoadingError.wrongResponse)
            }
            
        }
    }
}
