//
//  Activity.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 24/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

///Activity model
class ActivityModel: NSObject {
    
    //the activity identifier
    var id: URL?
    
    ///The name of activity
    var name: String
    
    ///initializes Activity object
    /// - parameter name: The activity name
    init(name: String) {
        self.name = name
    }
    
    init (id: URL, name: String) {
        self.id = id
        self.name = name
    }
}
