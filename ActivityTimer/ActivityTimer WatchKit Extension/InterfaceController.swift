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
    
    //MARK: Outlets
    @IBOutlet weak var table: WKInterfaceTable!
    @IBOutlet weak var loadDataButton: WKInterfaceButton!
    
    //MARK: private properties
    private static let osLogName = OSLog.activityInterfaceController
    private var activities = [ActivityModel]()
    private let session: WCSession? = WCSession.isSupported() ? WCSession.default : nil
    
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
    
    @IBAction func loadDataAction() {
        sendMessageAnmdGetResponseFromIPhone()
    }
    
    private func refreshTable () {
        
        if self.activities.isEmpty || self.activities.count == 0 {
            toggleLoadDataBtnVisible(true)
        } else {
            toggleLoadDataBtnVisible(false)
        }
        
        self.table.setNumberOfRows(activities.count, withRowType: "ActivityRowController")
        
        let rowCount = self.table.numberOfRows
        
        for i in 0..<rowCount {
            let row = self.table.rowController(at: i) as! ActivityRowController
            
            row.nameLbl.setText(activities[i].name)
        }
    }
    
    private func toggleLoadDataBtnVisible(_ isHidden: Bool) {
        loadDataButton.setEnabled(isHidden)
        loadDataButton.setHidden(!isHidden)
    }
}

extension ActivityInterfaceController : WCSessionDelegate {
 
    func session(_ session: WCSession, activationDidCompleteWith activationState: WCSessionActivationState, error: Error?) {
        print("activationDidCompleteWith activationState:\(activationState) error:\(String(describing: error))")
    }
    
    func session(_ session: WCSession, didReceiveMessageData messageData: Data) {
        do {
            
            try NSKeyedUnarchiver.decodeActivity(messageData, forKey: "activity") { (activity: ActivityModel) in
                    self.activities.append(activity)
                    self.refreshTable() }
            
            
        } catch let error as NSError {
            os_log("Error occours while tring to get data from IPhone. %{PUBLIC}@. %{PUBLIC}@", log: OSLog.activityInterfaceController, type: .error, error, error.userInfo)
            
            WKAlertHelper.showInfoAlert(title: "Error occours", message: "Error occours while tring to get data from IPhone", usingController: self)
        }
    }
    
    private func sendMessageAnmdGetResponseFromIPhone() {
        initSession { (validReachableSession) in
            validReachableSession.sendMessageData(Data(), replyHandler: { (data) in
                
                do {
                    
                    try NSKeyedUnarchiver.decodeActivity(data, forKey: "activities", afterDecodeAction: { (activitiesFromResponse: [ActivityModel]) in
                            self.activities = activitiesFromResponse
                            self.refreshTable()})
                    
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
        
            WCSession.initSession(session: self.session, sessionAction: sessionAction) { (errorType) in
            
            os_log("Watch session is not %{PUBLIC}@", log: ActivityInterfaceController.osLogName, type: .error,
                   errorType)
            
            WKAlertHelper.showInfoAlert(title: "Session is on error state", message: "Watch session is not \(errorType)", usingController: self)
            
        }
    }
}
