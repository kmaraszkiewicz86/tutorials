//
//  OSLogExtension.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 25/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import os.log

extension OSLog {
 
    private static var bundle = Bundle.main.bundleIdentifier!
    
    static let activityTableViewController = OSLog(subsystem: bundle, category: "ActivityTableViewController")
    
    static let activityService = OSLog(subsystem: bundle, category: "ActivityService")
    
    static let activityInterfaceController = OSLog(subsystem: bundle, category: "ActivityInterfaceController")
    
    static let nsKeyedArchiverExtension = OSLog(subsystem: bundle, category: "NSKeyedArchiverExtension")
    
    static let activityCollection = OSLog(subsystem: bundle, category: "ActivityCollection")
    
}
