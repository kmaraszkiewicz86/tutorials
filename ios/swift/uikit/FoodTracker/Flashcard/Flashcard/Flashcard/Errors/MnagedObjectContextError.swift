//
//  MnagedObjectContextError.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 16/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

///Error types of `NSManagedObjectContext` type
enum ManagedObjectContextError: Error {
    
    ///occurs when NSManagedObjectContext instance is nil
    case NullReferenceError
    
    ///Error while tring to saved data to CoreData
    case CoreDataError
}
