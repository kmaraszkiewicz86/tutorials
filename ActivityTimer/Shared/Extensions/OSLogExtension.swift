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
    
}
