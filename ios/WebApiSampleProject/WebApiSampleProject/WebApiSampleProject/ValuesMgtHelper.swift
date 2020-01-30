//
//  ValuesMgtHelper.swift
//  WebApiSampleProject
//
//  Created by Krzysztof Maraszkiewicz on 10/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ValuesMgtHelper {
    
    private let url = "http://localhost:5001/api/values"
    
    func GetAll (onBegin: @escaping () -> Void, onSucess: @escaping ([String]) -> Void, onRequestFailure: @escaping (Error) -> Void, onConvertFailure: @escaping (NSError) -> Void) {
        var request = URLRequest(url: URL(string: self.url)!)
        request.httpMethod = "GET"
        
        onBegin()
        
        URLSession.shared.dataTask(with: request, completionHandler: {
            data, response, error -> Void in
            do {
                if let err = error {
                    print("\(err)")
                    onRequestFailure(err)
                } else {
                    let jsonDecoder = JSONDecoder()
                    let responseModel = try jsonDecoder.decode([String].self, from: data!)
                    onSucess(responseModel)
                }
            } catch let exception as NSError {
                print("\(exception)")
                onConvertFailure(exception)
            }
        }).resume()
    }
    
}
