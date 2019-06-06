//
//  WCSessionExtension.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 02/06/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import WatchConnectivity

extension WCSession {
    
    static func initIOSSession(session: WCSession?, sessionAction: (WCSession) -> Void, onError: (String) -> Void) {
        
        let isSupported = WCSession.isSupported()
        let isReachable = session?.isReachable ?? false
        let isWatchAppInstalled = session?.isWatchAppInstalled ?? false
        let isPaired = session?.isPaired ?? false
        var errorType = ""
        
        
        
        if !isSupported {
            errorType = " supported"
        } else if !isReachable {
            errorType = " reachable"
        } else if !isWatchAppInstalled {
            errorType = " watch app not istalled"
        } else if !isPaired {
            errorType = " nor paired"
        }
        
        if !isSupported || !isReachable || !isWatchAppInstalled || !isPaired {
            
            onError(errorType)
            return
        }
        
        sessionAction(session!)
    }
}
