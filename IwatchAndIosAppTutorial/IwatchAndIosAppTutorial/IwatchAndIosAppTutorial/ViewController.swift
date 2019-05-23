//
//  ViewController.swift
//  IwatchAndIosAppTutorial
//
//  Created by Krzysztof Maraszkiewicz on 21/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import WatchConnectivity

class ViewController: UIViewController {

    var connectivityHandler = WatchSessionManager.shared
    
    var counter = 0
    
    @IBOutlet weak var btnSendMsg: UIButton!
    
    @IBOutlet weak var picker: UIPickerView!
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        connectivityHandler.iOSDelegate = self
        picker.delegate = self
        picker.dataSource = self
    }


    @IBAction func sendMessage(_ sender: UIButton) {
        counter += 1
        connectivityHandler.sendMessage(message: ["msg" : "Message \(counter)" as AnyObject]) { (error) in
            print("Error sending message: \(error)")
        }
    }
}

extension ViewController: iOSDelegate {
    
    func messageReceived(tuple: MessageReceived) {
        // Handle receiving message
        
        guard let reply = tuple.replyHandler else {
            return
        }
        
        // Need reply to counterpart
        switch tuple.message["request"] as! RequestType.RawValue {
        case RequestType.date.rawValue:
            reply(["date" : "\(Date())"])
        case RequestType.version.rawValue:
            let version = ["version" : "\(Bundle.main.object(forInfoDictionaryKey: "CFBundleShortVersionString") ?? "No version")"]
            reply(["version" : version])
        default:
            break
        }
    }
    
    func applicationContextReceived(tuple: ApplicationContextReceived) {
        DispatchQueue.main.async {
            if let row = tuple.applicationContext["row"] as? Int {
                self.btnSendMsg.backgroundColor = Constants.itemList[row].2
            }
        }
    }
}

// MARK: UIPickerViewDataSource - UIPickerViewDelegate
extension ViewController: UIPickerViewDataSource, UIPickerViewDelegate {
    
    func numberOfComponents(in pickerView: UIPickerView) -> Int {
        return 1
    }
    
    func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return Constants.itemList.count;
    }
    
    func pickerView(_ pickerView: UIPickerView, titleForRow row: Int, forComponent component: Int) -> String? {
        return Constants.itemList[row].1
    }
    
    // 4: Call updateApplicationContext to update watch theme
    func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        do {
            try connectivityHandler.updateApplicationContext(applicationContext: ["row" : row])
        } catch {
            print("Error: \(error)")
        }
        self.btnSendMsg.backgroundColor = Constants.itemList[row].2
    }
}
