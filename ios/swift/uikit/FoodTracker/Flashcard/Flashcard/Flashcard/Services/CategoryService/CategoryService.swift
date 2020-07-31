//
//  CategoryService.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 13/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import CoreData
import UIKit
import os.log

class CategoryService: BaseService {
    
    init (applicationDelegate: UIApplicationDelegate?) throws {
        
        do {
            try super.init(entityName: "Category",
                           osLogType: OSLog.categoryService,
                           applicationDelegate: applicationDelegate)
        } catch AppDelegateError.NullReferenceException {
            throw AppDelegateError.NullReferenceException
        }
        
        self.controllerName = "Category"
    }
    
    func getAll(onBegin: () -> Void, onFinish: () -> Void) throws -> [CategoryModel] {
        do {
            
            onBegin()
            
            defer {
                onFinish()
            }
            
            return try getCommon().toCategoryModelArray()
        } catch {
            throw GeneralError.UnknownError
        }
    }
    
    func fetchDataFromServer(onBegin: @escaping () -> Void,
                onSuccess: @escaping ([CategoryModel]) -> Void,
                onFinish: @escaping () -> Void,
                onError: @escaping (DataRequestError) -> Void) {
        
        requestAndFetchData(httpMethod: "GET", onBegin: {
            () -> Void in
                onBegin()
        }, onSuccess: {
            (categories: [CategoryModel]) -> Void in
            do {
                try self.mergeDataFromServerAndCoreData(categories: categories)
                
                onSuccess(categories)
            } catch {
                onError(DataRequestError.responseError)
            }
            
        }, onFinish: {
            () -> Void in
                onFinish()
        }, onError: {
            (dataRequestError) -> Void in
                onError(dataRequestError)
        })
    }
    
    func mergeDataFromServerAndCoreData (categories: [CategoryModel]) throws {
        
        if !categories.isEmpty {
            do {
                //add only items that doesnt exists in CoreData table
                for category in categories {
                    if try anyPredicate(NSPredicate(format: "id=%@", String(category.id))) {
                        continue
                    }
                
                    try save({
                        (categoryManagedObject) -> Void in
                        categoryManagedObject.setValue(category.id, forKey: "id")
                        categoryManagedObject.setValue(category.name, forKey: "name")
                        
                    })
                }
                
                //todo: remove items from db in mergeDataFromServerAndCoreData
//                //remove all items from CoreData that doesnt exists in server
//                for category in categories {
//                    if try allPredicate({ (managedObject) -> Bool in
//                        return (managedObject.value(forKey: "id") as! Int64) == category.id
//                    }) {
//                        continue
//                    }
//
//                    try save({
//                        (categoryManagedObject) -> Void in
//                        categoryManagedObject.setValue(category.id, forKey: "id")
//                        categoryManagedObject.setValue(category.name, forKey: "name")
//
//                    })
//                }
            } catch ManagedObjectContextError.NullReferenceError {
                throw ManagedObjectContextError.NullReferenceError
            } catch {
                throw GeneralError.UnknownError
            }
        }
    }
}
