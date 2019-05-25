//
//  Activity.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 24/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

///Activity model
class Activity: NSObject {
    
    ///The name of activity
    var name: String
    
    ///initializes Activity object
    /// - parameter name: The activity name
    init(name: String) {
        self.name = name
    }
}
