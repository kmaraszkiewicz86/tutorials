//
//  NSKeyedUnarchivedExtension.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 06/06/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import os.log
import WatchKit

extension NSKeyedUnarchiver {
    
    static func decodeActivity<TActivity>(_ data: Data, forKey: String, afterDecodeAction: (TActivity)  -> Void) throws {

            let unarchiver = try NSKeyedUnarchiver(forReadingFrom: data)
            let items = unarchiver.decodeObject(of: [NSURL.self, NSArray.self, ActivityModel.self], forKey: "activity") as? TActivity
        
            if let error = unarchiver.error {
                os_log("Occours error while tring to decode data. With error: %{PUBLIC}@", log: OSLog.nsKeyedUnarchiverExtension, type: .error, "\(error)")
                
                throw NSKeyedUnarchiverError.error(errorMessage: "\(error)")
            }
        
        if let i = items {
            afterDecodeAction(i)
        }
    }
}
