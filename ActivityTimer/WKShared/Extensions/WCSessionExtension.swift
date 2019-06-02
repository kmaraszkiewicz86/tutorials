//
//  WCSessionExtension.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 02/06/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import WatchConnectivity

extension WCSession {
    
    static func initSession(session: WCSession?, sessionAction: (WCSession) -> Void, onError: (String) -> Void) {
        
        let isSupported = WCSession.isSupported()
        let isReachable = session?.isReachable ?? false
        var errorType = ""
        
        if !isSupported {
            errorType = " supported"
        } else if !isReachable {
            errorType = " reachable"
        }
        
        if !isSupported || !isReachable {
            
            onError(errorType)
            return
        }
        
        sessionAction(session!)
    }
}
