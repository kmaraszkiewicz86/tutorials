//
//  Activity.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 24/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

///Activity model
@objc(ActivityModel)
class ActivityModel: NSObject, NSCoding, NSSecureCoding {
    
    static var supportsSecureCoding = true
    
    struct Keys {
        static let idKeyName = "id"
        static let nameKeyName = "name"
        static let operationTypeKeyName = "operationType"
    }
    
    //the activity identifier
    var id: URL?
    
    ///The name of activity
    var name: String
    
    var operationType: ActivityOperationType?
    
    required convenience init(coder deCoder: NSCoder) {
        
        let id = deCoder.decodeObject(forKey: Keys.idKeyName) as? URL
        let name = deCoder.decodeObject(forKey: Keys.nameKeyName) as! String
        let operationTypeInt = deCoder.decodeObject(forKey: Keys.operationTypeKeyName) as? Int
        
        self.init(id: id, name: name, operationTypeInt: operationTypeInt)
    }
    
    init(id: URL?, name: String) {
        self.id = id
        self.name = name
    }
    
    convenience init(id: URL?, name: String, operationTypeInt: Int?) {
        self.init(id: id, name: name)
        
        self.operationType = toActivityOperationType(type: operationTypeInt)
    }
    
    convenience init(id: URL?, name: String, operationType: ActivityOperationType?) {
        self.init(id: id, name: name)
        
        self.operationType = operationType
    }
    
    ///initializes Activity object
    /// - parameter name: The activity name
    init(name: String) {
        self.name = name
    }
    
    func encode(with aCoder: NSCoder) {
        aCoder.encode(self.id, forKey: Keys.idKeyName)
        aCoder.encode(self.name, forKey: Keys.nameKeyName)
        aCoder.encode(toInt(type: self.operationType), forKey: Keys.operationTypeKeyName)
    }
    
    private func toInt (type: ActivityOperationType?) -> Int? {
        
        if let t = type {
        
            switch t {
                case .added:
                    return 1
                
                case .deleted:
                    return 2
                
                case .updated:
                    return 3
                
                default:
                    return nil
                
                }
        }
        
        return nil
    }
    
    private func toActivityOperationType (type: Int?) -> ActivityOperationType? {
        
        if let t = type {
        
            switch t {
            case 1:
                return .added
                
            case 2:
                return .deleted
                
            case 3:
                return .updated
                
            default:
                return nil
                
            }
        }
        
        return nil
    }
}
