//
//  OSLog.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 14/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import os.log

///Extension OSLog type
extension OSLog {
    
    ///The bundle id
    private static var subsystem = Bundle.main.bundleIdentifier!
    
    //MARK: logger types
    ///The category service type for logger
    static let categoryService = OSLog(subsystem: subsystem, category: "categoryService")
    
}
