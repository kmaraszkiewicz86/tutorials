//
//  InterfaceController.swift
//  ActivityTimer WatchKit Extension
//
//  Created by Krzysztof Maraszkiewicz on 23/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import WatchKit
import WatchConnectivity
import Foundation


class InterfaceController: WKInterfaceController {
    
    @IBOutlet weak var table: WKInterfaceTable!
    
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
        
        self.activities += [
            ActivityModel(name: "tesst1"),
            ActivityModel(name: "tesst2")
        ]
        
        refreshTable()
        
        session?.delegate = self
        session?.activate()
    }
    
    override func didDeactivate() {
        // This method is called when watch view controller is no longer visible
        super.didDeactivate()
    }
    
    @IBAction func sendAction() {
        
        validReachableSession?.sendMessage(["request": "getAll"], replyHandler: { (res) in
            DispatchQueue.main.async {
                self.activities.append(ActivityModel(name: res["response"] as! String))
                
                self.refreshTable()
            }
        }, errorHandler: { (err) in
            DispatchQueue.main.async {
                self.activities.append(ActivityModel(name: "err"))
                
                self.refreshTable()
            }
        })
        
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

extension InterfaceController : WCSessionDelegate {
 
    func session(_ session: WCSession, activationDidCompleteWith activationState: WCSessionActivationState, error: Error?) {
        print("activationDidCompleteWith activationState:\(activationState) error:\(String(describing: error))")
    }
    
    func session(_ session: WCSession, didReceiveMessage message: [String : Any]) {
        DispatchQueue.main.async {
            self.activities.append(ActivityModel(name: message["response"] as! String))
            
            self.refreshTable()
        }
    }
}
