//
//  DataRequestError.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 14/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.

/**
 Error using while fetching response from server
 */
enum DataRequestError : Error {
    
    ///occurs when error is in response data
    case responseError
    
    ///occurs when error while tring convert repsone data to object
    case convertToModelError
}
