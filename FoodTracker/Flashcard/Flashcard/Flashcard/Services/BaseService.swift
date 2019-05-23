//
//  BaseService.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 15/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import CoreData
import os.log
import UIKit

class BaseService {
    
    ///The entity name in CoreData storage
    let entityName: String
    
    ///Type of logged data
    let osLogType: OSLog
    
    /** Controller name of web api url */
    var controllerName: String?
    
    ///The app delegate instance
    var appDelegate: AppDelegate?
    
    ///Generate new instance of object
    ///- parameter applicationDelegate: instance of UIApplicationDelegate
    init (entityName: String, osLogType: OSLog,
          applicationDelegate: UIApplicationDelegate?) throws {
        
        guard let appDelegate = applicationDelegate as? AppDelegate else {
            throw AppDelegateError.NullReferenceException
        }
        
        self.entityName = entityName
        self.appDelegate = appDelegate
        self.osLogType = osLogType
    }
    
    ///Get absolute path to web api url page
    ///- parameter actionName: name of web api action name
    ///
    final func absoluteUrlPath(actionName: String = "") -> URL {
        return URL(string: "http://flashcard.izabelamaraszkiewiczit.hostingasp.pl/api/" + controllerName! + "/" + actionName)!
    }
    
    /**
        Fetch data request helper
        - parameters:
            - httpMethod: The request method
            - onBegin action fire before request send
            -
    */
    final func requestAndFetchData<T: Decodable> (httpMethod: String, onBegin: @escaping () -> Void, onSuccess: @escaping (T) -> Void, onFinish: @escaping () -> Void, onError: @escaping (DataRequestError) -> Void) {
        
        
        var request = URLRequest(url: absoluteUrlPath())
        request.httpMethod = httpMethod
        
        onBegin()
        
        URLSession.shared.dataTask(with: request, completionHandler: {
            data, response, error -> Void in
            
            defer {
                onFinish()
            }
            
            if let err = error {
                os_log("Error %{PUBLIC}@ while fetching data", log: OSLog.categoryService, type: .error, "\(err)")
                
                onError(DataRequestError.responseError)
            } else {
                do {
                    let jsonDecoder = JSONDecoder()
                    
                    if let str = String(bytes: data!, encoding: .utf8) {
                        os_log("Data from response %{PUBLIC}@", log: self.osLogType, type: .info, str)
                    }
                    
                    let responseModel = try jsonDecoder.decode(T.self, from: data!)
                    
                    onSuccess(responseModel)
                } catch let exception as NSError {
                    os_log("Error %{PUBLIC}@ while converting data. User info: %{PUBLIC}@", log: self.osLogType, type: .error, "\(exception)", exception.userInfo)
                    
                    onError(DataRequestError.convertToModelError)
                }
            }
        }).resume()
    }
    
    ///get NSManagedObjectContext context instance
    final func getManagedContext () throws -> NSManagedObjectContext {
        if let managedContext = appDelegate?.persistentContainer.viewContext {
            return managedContext
        } else {
            throw ManagedObjectContextError.NullReferenceError
        }
    }
    
    final func wherePredicate(_ findQueryPredicate: (NSManagedObject) -> Bool) throws {
        
        do {
            
            let managedObjects = try getCommon() as [NSManagedObject]
            var searchedItems = [NSManagedObject]()
            
            if !managedObjects.isEmpty {
                
                for managedObject in managedObjects {
                    if findQueryPredicate(managedObject) {
                        searchedItems.append(managedObject)
                    }
                }
            }
            
        } catch ManagedObjectContextError.NullReferenceError {
            os_log("NSManagedObjectContext is null", log: osLogType, type: .error)
        } catch let error as NSError {
            os_log("Error occours while tring to getItemsToRemove data from CoreData %{PUBLIC}@. User info", log: osLogType, type: .error, error, error.userInfo)
            
            throw GeneralError.UnknownError
        }
    }
    
    final func anyPredicate(_ anyPredicate: NSPredicate) throws -> Bool {
        
        do {
            
            let managedObjects = try getCommon(anyPredicate) as [NSManagedObject]
            
            return !managedObjects.isEmpty && managedObjects.count > 0
            
        } catch ManagedObjectContextError.NullReferenceError {
            os_log("NSManagedObjectContext is null", log: osLogType, type: .error)
            
            throw ManagedObjectContextError.NullReferenceError
        } catch let error as NSError {
            os_log("Error occours while tring to getItemsToRemove data from CoreData %{PUBLIC}@. User info", log: osLogType, type: .error, error, error.userInfo)
            
            throw GeneralError.UnknownError
        }
        
    }
    
    final func allPredicate(_ allQueryPredicate: (NSManagedObject) -> Bool) throws -> Bool {
        
        do {
            
            let managedObjects = try getCommon() as [NSManagedObject]
            
            if !managedObjects.isEmpty {
                
                for managedObject in managedObjects {
                    
                    if !allQueryPredicate(managedObject) {
                        return false
                    }
                }
            }
            
            return true
            
        } catch ManagedObjectContextError.NullReferenceError {
            os_log("NSManagedObjectContext is null", log: osLogType, type: .error)
            
            throw ManagedObjectContextError.NullReferenceError
        } catch let error as NSError {
            os_log("Error occours while tring to getItemsToRemove data from CoreData %{PUBLIC}@. User info", log: osLogType, type: .error, error, error.userInfo)
            
            throw GeneralError.UnknownError
        }
        
    }
    
    final func getCommon(_ predicate: NSPredicate? = nil) throws -> [NSManagedObject] {
        do {
            
            let managedContext = try getManagedContext()
            
            let fetchRequest = NSFetchRequest<NSManagedObject>(entityName: entityName)
            if let pre = predicate {
                fetchRequest.predicate = pre
            }
            
            return try managedContext.fetch(fetchRequest)
        }  catch ManagedObjectContextError.NullReferenceError {
            throw ManagedObjectContextError.NullReferenceError
        }  catch let error as NSError {
            os_log("Error occours while tring to gete data from CoreData %{PUBLIC}@. User info", log: osLogType, type: .error, error, error.userInfo)
            
            throw GeneralError.UnknownError
        }
    }
    
    final func save(_ addItemAcion: (NSManagedObject) -> Void) throws {
        do {
            
            let managedContext = try getManagedContext()
            
            let entity = NSEntityDescription.entity(forEntityName: entityName, in: managedContext)!
            
            let managedObject = NSManagedObject(entity: entity, insertInto: managedContext)
            
            addItemAcion(managedObject)
            
            try managedContext.save()
            
        } catch ManagedObjectContextError.NullReferenceError {
            throw ManagedObjectContextError.NullReferenceError
        } catch let error as NSError {
            os_log("Error ocured while tring to saved category data to CoreData with details %{PUBLIC}@. User info: %{PUBLIC}@", log: osLogType, type: .error, error, error.userInfo)
            
            throw GeneralError.UnknownError
        }
    }
    
    final func remove(predicate: NSPredicate) throws {
        do {
            
            let managedContext = try getManagedContext()
            
            let fetchRequest = NSFetchRequest<NSManagedObject>(entityName: entityName)

            fetchRequest.predicate = predicate
            
            let categoriesToDelete =  try managedContext.fetch(fetchRequest)
            
            if !categoriesToDelete.isEmpty {
                for categoryToDelete in categoriesToDelete {
                    managedContext.delete(categoryToDelete)
                }
            }
            
            try managedContext.save()
        }  catch ManagedObjectContextError.NullReferenceError {
            throw ManagedObjectContextError.NullReferenceError
        }  catch let error as NSError {
            os_log("Error occours while tring to delete data from CoreData %{PUBLIC}@. User info", log: osLogType, type: .error, error, error.userInfo)
            
            throw GeneralError.UnknownError
        }
    }
}
