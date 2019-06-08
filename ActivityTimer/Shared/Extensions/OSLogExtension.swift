//
//  OSLogExtension.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 25/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import os.log

///The OSLog extension
extension OSLog {
 
    ///The bundle indetifier
    private static var bundle = Bundle.main.bundleIdentifier!
    
    ///The ActivityTableViewController name for oslog class
    static let activityTableViewController = OSLog(subsystem: bundle, category: "ActivityTableViewController")
    
    ///The ActivityService name for oslog class
    static let activityService = OSLog(subsystem: bundle, category: "ActivityService")
    
    ///The ActivityInterfaceController name for oslog class
    static let activityInterfaceController = OSLog(subsystem: bundle, category: "ActivityInterfaceController")
    
    ///The NSKeyedArchiverExtension name for oslog class
    static let nsKeyedArchiverExtension = OSLog(subsystem: bundle, category: "NSKeyedArchiverExtension")
    
    ///The NSKeyedUnarchiverExtension name for oslog class
    static let nsKeyedUnarchiverExtension = OSLog(subsystem: bundle, category: "NSKeyedUnarchiverExtension")
    
    ///The ActivityCollection name for oslog class
    static let activityCollection = OSLog(subsystem: bundle, category: "ActivityCollection")
    
}
