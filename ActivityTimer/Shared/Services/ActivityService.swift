//
//  ActivityService.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 27/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import CoreData
import UIKit
import os.log

///The activity service
class ActivityService {
    
    ///The singlethon of ActivityService class
    static let shared = ActivityService()
    
    ///The os_log name
    private static let osLogName = OSLog.activityService
    
    ///The NSManagedObjectContext instance
    private let managedObject: NSManagedObjectContext

    ///The initializer of ActiveService instance
    init() {
    
        guard let appDelegate = UIApplication.shared.delegate as? AppDelegate else {
            os_log("Error occours while tring to convert UIApplication.shared.delegate to AppDelegate type", log: ActivityService.osLogName,
                   type: .error)
            fatalError("Invalid type of UIApplication.shared.delegate")
        }
        
        self.managedObject = appDelegate.persistentContainer.viewContext
    }
    
    ///Get all activities
    ///- returns: The ActivityModel array
    func getAll() throws -> [ActivityModel] {
        
        let fetchPredicate = NSFetchRequest<NSManagedObject>(entityName: "Activity")
        
        do {
            let activities = try managedObject.fetch(fetchPredicate)
            
            return activities.toActivityModel()
            
        } catch let error as NSError {
            os_log("Error while saving data to database. %{PUBLIC}@. %{PUBLIC}@", log: ActivityService.osLogName, type: .error, "\(error)", "\(error.userInfo)")
            
            throw ServiceError.databaseError
            
        }
    }
    
    ///Saves activity model to db
    /// - parameter activityModel: The activity model data
    /// - throws: `ServiceError.databaseError` if error occours while saving data
    /// - returns: The ActivityModel array
    func save(activityModel: ActivityModel) throws -> ActivityModel {
        
        let entity = NSEntityDescription.entity(forEntityName: "Activity", in: managedObject)!
        let activity = NSManagedObject(entity: entity, insertInto: managedObject)
        
        activity.setValue(activityModel.name, forKey: "name")
        
        do {
            try managedObject.save()
            
            return activity.toActivityModel()
        } catch let error as NSError {
            
            os_log("Error while saving data to database. %{PUBLIC}@. %{PUBLIC}@", log: ActivityService.osLogName, type: .error, "\(error)", "\(error.userInfo)")
            
            throw ServiceError.databaseError
        }
    }
    
    ///Updates activity model data
    /// - parameter id: The activity identifier
    /// - parameter activityModel: The activity model data
    /// - Throws: `ServiceError.databaseError` if error occours while updating data
    func update (id: URL?, activityModel: ActivityModel) throws {
        
        do {
            let activity = try findByUrlId(id)
            
            activity.setValue(activityModel.name, forKey: "name")
            
            try managedObject.save()
            
        } catch let error as NSError {
            os_log("Error occours while tring to deleting data from CoreData. %{PUBLIC}@. %{PUBLIC}@",
                   log: ActivityService.osLogName,
                   type: .error,
                   "\(error)", "\(error.userInfo)")
            
            throw ServiceError.databaseError
        }
        
    }
    
    ///Deletes activity
    /// - parameter activityModel: The activity model data
    /// - Throws: `ServiceError.databaseError` if error occours while deleting data
    func delete(activityModel: ActivityModel) throws {
        
        do {
            managedObject.delete(try findByUrlId(activityModel.id))
            
            try managedObject.save()
            
        } catch let error as NSError {
            os_log("Error occours while tring to deleting data from CoreData. %{PUBLIC}@. %{PUBLIC}@",
                   log: ActivityService.osLogName,
                   type: .error,
                   "\(error)", "\(error.userInfo)")
            
            throw ServiceError.databaseError
        }
    }
    
    ///Finds activity by url id
    /// - parameter id: The id of activity model data
    /// - Throws: `ServiceError.databaseError` if error occours while updating data
    /// - returns: The NSManagedObject
    private func findByUrlId (_ id: URL?) throws -> NSManagedObject {
        do {
            guard let id = id,
                let objectID = managedObject.persistentStoreCoordinator?.managedObjectID(forURIRepresentation: id) else {
                    
                    os_log("Error occours while tring to get object id from ActivityModel",
                           log: ActivityService.osLogName,
                           type: .error)
                    
                    throw ServiceError.databaseError
                    
            }
            
            return try managedObject.existingObject(with: objectID)
            
        } catch let error as NSError {
            os_log("Error occours while tring to fetch data from CoreData by object id. %{PUBLIC}@. %{PUBLIC}@",
                   log: ActivityService.osLogName,
                   type: .error,
                   "\(error)", "\(error.userInfo)")
            
            throw ServiceError.databaseError
        }
    }
}
