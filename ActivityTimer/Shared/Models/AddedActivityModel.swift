//
//  ActivityModelWrapper.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 02/06/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

class AddedActivityModel: NSObject, NSCoding, NSSecureCoding {
    
    static var supportsSecureCoding = true
    
    struct Keys {
        static let activityModelKeyName = "activityModel"
    }
    
    let operationType = "Add"
    
    let activityModel: ActivityModel
    
    required convenience init(coder deCode: NSCoder) {
        
        let activityModel = deCode.decodeObject(forKey: Keys.activityModelKeyName) as! ActivityModel
        
        self.init(activityModel: activityModel)
    }
    
    init (activityModel: ActivityModel) {
        
        self.activityModel = activityModel
        
    }
    
    func encode(with aCoder: NSCoder) {
        aCoder.encode(self.activityModel, forKey: Keys.activityModelKeyName)
    }
}
