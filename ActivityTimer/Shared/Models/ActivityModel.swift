//
//  Activity.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 24/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import WatchKit

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
    
    var operationType: String?
    
    required convenience init(coder deCoder: NSCoder) {
        
        let id = deCoder.decodeObject(forKey: Keys.idKeyName) as? URL
        let name = deCoder.decodeObject(forKey: Keys.nameKeyName) as! String
        let operationType = deCoder.decodeObject(forKey: Keys.operationTypeKeyName) as? String
        
        self.init(id: id, name: name, operationType: operationType)
    }
    
    init(id: URL?, name: String) {
        self.id = id
        self.name = name
    }
    
    convenience init(id: URL?, name: String, operationType: String?) {
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
        aCoder.encode(self.operationType, forKey: Keys.operationTypeKeyName)
    }
}
