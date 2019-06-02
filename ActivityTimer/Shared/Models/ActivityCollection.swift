//
//  ActivityCollection.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 31/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import WatchKit
import os.log

class ActivityCollection: NSObject, NSCoding {

    private struct Keys {
        static let activitiesKeyName = "activities"
    }
    
    let activities: [ActivityModel]
    
    required convenience init?(coder aDecoder: NSCoder) {
        
        guard let items = aDecoder.decodeObject(forKey: Keys.activitiesKeyName) as? [ActivityModel] else {
            
            os_log("Cant convert  items", log: OSLog.activityCollection, type: .error)
            
            return nil
        }
        
        self.init(activities: items)
    }
    
    init(activities: [ActivityModel]) {
        self.activities = activities
    }
    
    func encode(with aCoder: NSCoder) {
        aCoder.encode(self.activities, forKey: Keys.activitiesKeyName)
    }
}
