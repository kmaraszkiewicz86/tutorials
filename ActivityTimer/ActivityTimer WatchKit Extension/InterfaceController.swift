//
//  InterfaceController.swift
//  ActivityTimer WatchKit Extension
//
//  Created by Krzysztof Maraszkiewicz on 23/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation
import os.log
import WatchKit
import WatchConnectivity

class ActivityInterfaceController: WKInterfaceController {
    
    @IBOutlet weak var table: WKInterfaceTable!
    
    private static let osLogName = OSLog.activityInterfaceController
    
    private var activities = [ActivityModel]()
    
    private let session: WCSession? = WCSession.isSupported() ? WCSession.default : nil
    
    private var validReachableSession: WCSession? {
        if let session = self.session, session.isReachable {
            return session
        }
        
        return nil
    }
    
    override func awake(withContext context: Any?) {
        super.awake(withContext: context)
    }
    
    override func willActivate() {
        // This method is called when watch view controller is about to be visible to user
        super.willActivate()
        
        sendMessageAnmdGetResponseFromIPhone()
        
        session?.delegate = self
        session?.activate()
    }
    
    override func didDeactivate() {
        // This method is called when watch view controller is no longer visible
        super.didDeactivate()
    }
    
    private func refreshTable () {
        
        self.table.setNumberOfRows(activities.count, withRowType: "ActivityRowController")
        
        let rowCount = self.table.numberOfRows
        
        for i in 0..<rowCount {
            let row = self.table.rowController(at: i) as! ActivityRowController
            
            row.nameLbl.setText(activities[i].name)
        }
    }

}

extension ActivityInterfaceController : WCSessionDelegate {
 
    func session(_ session: WCSession, activationDidCompleteWith activationState: WCSessionActivationState, error: Error?) {
        print("activationDidCompleteWith activationState:\(activationState) error:\(String(describing: error))")
    }
    
    func session(_ session: WCSession, didReceiveMessage message: [String : Any]) {
        DispatchQueue.main.async {
            self.activities.append(ActivityModel(name: message["response"] as! String))
            
            self.refreshTable()
        }
    }
    
    private func sendMessageAnmdGetResponseFromIPhone() {
        initSession { (sess) in
            sess.sendMessageData(Data(), replyHandler: { (data) in
                
                do {
                    let unarchiver = try NSKeyedUnarchiver(forReadingFrom: data)
                    
                    let activitiesFromResponse = unarchiver.decodeObject(of: [NSArray.self, ActivityModel.self, NSURL.self], forKey: "activities") as? [ActivityModel]
                    
                    if let error = unarchiver.error {
                        os_log("Occours error while tring to decode data. With error: %{PUBLIC}@", log: ActivityInterfaceController.osLogName, type: .error, "\(error)")
                        
                        WKAlertHelper.showInfoAlert(title: "Error occours", message: "Error occours while tring to get data from IPhone", usingController: self)
                        
                        return
                    }
                    
                    self.activities = activitiesFromResponse!
                    DispatchQueue.main.async {
                        self.refreshTable()
                    }
                    
                } catch let error as NSError {
                    os_log("Error occours while tring to get data from IPhone. %{PUBLIC}@. %{PUBLIC}@", log: OSLog.activityInterfaceController, type: .error, error, error.userInfo)
                    
                    WKAlertHelper.showInfoAlert(title: "Error occours", message: "Error occours while tring to get data from IPhone", usingController: self)
                }
            }, errorHandler: { (err) in
                DispatchQueue.main.async {
                    WKAlertHelper.showInfoAlert(title: "Error occours", message: "Error occours while tring to fetch data from IPhone", usingController: self)
                    
                    os_log("Error occours while watch tring to fetch data from IPhone app. %{PUBLIC}@", log: OSLog.activityInterfaceController, type: .error, "\(err)")
                }
            })
        }
    }
    
    private func initSession (sessionAction: (WCSession) -> Void) {
        
        if let sess = validReachableSession {
            sessionAction(sess)
        } else {
            
            let isSupported = WCSession.isSupported()
            let isReachable = session?.isReachable ?? false
            var errorType = ""
            
            if !isSupported {
                errorType = " supported"
            } else if !isReachable {
                errorType = " reachable"
            }
            
            if !isSupported || !isReachable {
            
                os_log("Watch session is not %{PUBLIC}@", log: ActivityInterfaceController.osLogName, type: .error,
                       errorType)
                
                WKAlertHelper.showInfoAlert(title: "Session is on error state", message: "Watch session is not \(errorType)", usingController: self)
                
                return
            }
            
            sessionAction(validReachableSession!)
        }
    }
}
