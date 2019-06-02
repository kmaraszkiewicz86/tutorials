//
//  WKAlertHelper.swift
//  ActivityTimer WatchKit Extension
//
//  Created by Krzysztof Maraszkiewicz on 30/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import WatchKit

class WKAlertHelper {
 
    static func showInfoAlert<T: WKInterfaceController>(title: String, message: String, usingController controller: T) {
        let okAction = WKAlertAction(title: "OK", style: .cancel, handler: {
            
        })
        
        controller.presentAlert(withTitle: title, message: message, preferredStyle: .alert, actions: [okAction])
    }
}
