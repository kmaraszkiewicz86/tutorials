//
//  InterfaceController.swift
//  IwatchAndIosAppTutorial WatchKit Extension
//
//  Created by Krzysztof Maraszkiewicz on 21/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import WatchKit
import Foundation
import WatchConnectivity

class InterfaceController: WKInterfaceController {
    
    private var connectivityHandler = WatchSessionManager.shared
    
    @IBOutlet weak var messageTable: WKInterfaceTable!
    
    @IBOutlet weak var picker: WKInterfacePicker!
    
    @IBOutlet weak var btnDate: WKInterfaceButton!
    
    @IBOutlet weak var btnVersion: WKInterfaceButton!
    
    var session: WCSession?
    
    var counter = 0
    
    //MARK: - ITems Table
    
    private var messages = [String]()
    {
        didSet {
            DispatchQueue.main.async {
                self.updateMessagesTable()
            }
        }
    }
    
    override func awake(withContext context: Any?) {
        super.awake(withContext: context)
        
        messages.append("Ready :)")
        
        // 2: Set picker data
        let pickerItems: [WKPickerItem] = Constants.itemList.map {
            let pickerItem = WKPickerItem()
            pickerItem.caption = $0.0
            pickerItem.title = $0.1
            return pickerItem
        }
        picker.setItems(pickerItems)
    }
    
    override func willActivate() {
        // This method is called when watch view controller is about to be visible to user
        super.willActivate()
        
        connectivityHandler.startSession()
        connectivityHandler.watchOSDelegate = self
    }
    
    override func didDeactivate() {
        // This method is called when watch view controller is no longer visible
        super.didDeactivate()
    }
    
    @IBAction func requestData() {
        let data = ["request": RequestType.date.rawValue as AnyObject]
        connectivityHandler.sendMessage(message: data, replyHandler: {
            (res) in
            self.messages.append("\(res)")
        }, errorHandler: {
            (error) in
            print("\(error)")
        })
    }
    
    
    @IBAction func requestVersion() {
        let data = ["request" : RequestType.version.rawValue as AnyObject]
        connectivityHandler.sendMessage(message: data, replyHandler: { (response) in
            self.messages.append("Reply: \(response)")
        }) { (error) in
            print("Error sending message: \(error)")
        }
    }
    
    // 3: IBAction for WKInterfacePicker
    @IBAction func changeTheme(_ value: Int) {
        // 4: Change current theme
        selectedTheme(row: value)
        // 5: Call to update application context
        do {
            try connectivityHandler.updateApplicationContext(applicationContext: ["row" : value] as [String : AnyObject])
        } catch {
            print("Error: \(error)")
        }
    }
    
    // 6: Change theme of buttons
    func selectedTheme(row: Int) {
        self.btnDate.setBackgroundColor(Constants.itemList[row].2)
        self.btnVersion.setBackgroundColor(Constants.itemList[row].2)
    }
    
    func updateMessagesTable() {
        messageTable.setNumberOfRows(messages.count, withRowType: "Row")
        for (i, msg) in messages.enumerated() {
            let row = messageTable.rowController(at: i) as? Row
            row?.lbl.setText(msg)
        }
    }
}

extension InterfaceController: WatchOSDelegate {
    
    func messageReceived(tuple: MessageReceived) {
        DispatchQueue.main.async() {
            WKInterfaceDevice.current().play(.notification)
            if let msg = tuple.message["msg"] {
                self.messages.append("\(msg)")
            }
        }
    }
    
    // 7: Manage recieved data
    func applicationContextReceived(tuple: ApplicationContextReceived) {
        DispatchQueue.main.async() {
            if let row = tuple.applicationContext["row"] as? Int {
                self.changeTheme(row)
            }
        }
    }
}
