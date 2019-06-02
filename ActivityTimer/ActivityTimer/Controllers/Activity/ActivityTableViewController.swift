//
//  ActivityTableViewController.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 24/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import os.log
import WatchConnectivity

class ActivityTableViewController: UITableViewController {
    
    var activities = [ActivityModel]()
    
    private static let osLogName = OSLog.activityTableViewController
    
    private let activityService = ActivityService.shared
    
    private let sesssion: WCSession? = WCSession.isSupported() ? WCSession.default : nil
    
    private var validateReachableSession: WCSession?
    {
        if let sess = self.sesssion, sess.isPaired && sess.isWatchAppInstalled {
            return self.sesssion
        }
        
        return nil
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

        self.navigationItem.leftBarButtonItem = self.editButtonItem
        
        sesssion?.delegate = self
        sesssion?.activate()
        
        fetchData()
    }

    // MARK: - Table view data source

    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return activities.count
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        guard let cell = tableView.dequeueReusableCell(withIdentifier: "ActivityTableViewCell", for: indexPath) as? ActivityTableViewCell else {
            
            os_log("Could not found valid ActivityTableViewCell type to convert data to table cell", log: ActivityTableViewController.osLogName, type: .fault)
            fatalError("Occours error while tring to find proper table cell type")
        }

        let activity = activities[indexPath.row]
        
        cell.nameLabel.text = activity.name

        return cell
    }
 
    // Override to support conditional editing of the table view.
    override func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }

    
    // Override to support editing the table view.
    override func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCell.EditingStyle, forRowAt indexPath: IndexPath) {
        if editingStyle == .delete {
            
            do {
                try activityService.delete(activityModel: activities[indexPath.row])
                activities.remove(at: indexPath.row)
                tableView.deleteRows(at: [indexPath], with: .fade)
            } catch ServiceError.databaseError {
                showAlert(title: "Error", withMessage: "Error with saving data occours")
            } catch {
                showAlert(title: "Error", withMessage: "Unknow exception occours")
            }
        }
    }

    /*
    // Override to support rearranging the table view.
    override func tableView(_ tableView: UITableView, moveRowAt fromIndexPath: IndexPath, to: IndexPath) {

    }
    */

    /*
    // Override to support conditional rearranging of the table view.
    override func tableView(_ tableView: UITableView, canMoveRowAt indexPath: IndexPath) -> Bool {
        // Return false if you do not want the item to be re-orderable.
        return true
    }
    */

    
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
        switch (segue.identifier ?? "") {
        case "AddActivity":
            os_log("Adding activity with %{PUBLIC}@ destination",
                   log: ActivityTableViewController.osLogName,
                   type: .info,
            segue.destination)
            
        case "EditActivity":
            
            guard let activityFormViewController = segue.destination as? ActivityFormViewController else {
                os_log("Invalid convertion from segue.destination to ActivityFormViewController", log: ActivityTableViewController.osLogName, type: .error)
                
                fatalError("Errour occours while tring to navigate to edit form")
                
            }
            
            guard let selectedCell = sender as? ActivityTableViewCell else {
                os_log("Invalid convertion from sender to ActivityTableViewCell", log: ActivityTableViewController.osLogName, type: .error)
                
                fatalError("Errour occours while tring to navigate to edit form")
            }
            
            guard let index = tableView.indexPath(for: selectedCell) else {
                os_log("Invalid convertion from sender to ActivityTableViewCell", log: ActivityTableViewController.osLogName, type: .error)
                
                fatalError("Errour occours while tring to navigate to edit form")
            }
            
            activityFormViewController.activity = activities[index.row]
            
        default:
            os_log("Destination %s not implemented",
                   log: ActivityTableViewController.osLogName,
                   type: .error,
                   segue.destination)
            fatalError("Segue destination not found")
        }
        
    }
    
    @IBAction func redeirectFromForm(sender: UIStoryboardSegue) {
        
        if let activityFormViewController = sender.source as? ActivityFormViewController,
            let activity = activityFormViewController.activity {
            
                var workType = ""
            
                do {
                    if let index = tableView.indexPathForSelectedRow {
                        
                        workType = "updating"
                        
                        try activityService.update(id: activities[index.row].id, activityModel: activity)
                        activities[index.row].name = activity.name
                        tableView.reloadRows(at: [index], with: .fade)
                        
                    } else {
                        
                        workType = "saving"
                        
                        validateReachableSession?.sendMessage(["response": activity.name], replyHandler: nil, errorHandler: nil)
                        
                        activities.append(try activityService.save(activityModel: activity))
                        self.tableView.reloadData()
                    }
                } catch ServiceError.databaseError {
                    showAlert(title: "Error", withMessage: "Error with \(workType) data occours")
                } catch {
                    showAlert(title: "Error", withMessage: "Unknow exception occours")
                }
        }
    }
    
    //MARK: helper methods
    private func showAlert (title: String, withMessage: String) {
        
        let action = UIAlertController(title: title, message: withMessage, preferredStyle: .alert)
        
        let okAction = UIAlertAction(title: "OK", style: .cancel, handler: nil)
        
        action.addAction(okAction)
        
        present(action, animated: true)
        
    }
    
    private func fetchData () {
        do {
            activities = try activityService.getAll()
            tableView.reloadData()
        } catch ServiceError.databaseError {
            showAlert(title: "Error", withMessage: "Error with saving data occours")
        } catch {
            showAlert(title: "Error", withMessage: "Unknow exception occours")
        }
    }
}


extension ActivityTableViewController: WCSessionDelegate {
    
    func session(_ session: WCSession, didReceiveMessageData messageData: Data, replyHandler: @escaping (Data) -> Void) {
        
        let archiver = NSKeyedArchiver(requiringSecureCoding: false)
        archiver.encode(self.activities, forKey: "activities")
        
        if archiver.encodedData.isEmpty {
            os_log("Archiver encoded data is empty", log: ActivityTableViewController.osLogName, type: .error)
        }
        
        if let error = archiver.error {
            os_log("Occours error while tring tp encode data. With error: %{PUBLIC}@", log: ActivityTableViewController.osLogName, type: .error, "\(error)")
        }
        
        replyHandler(archiver.encodedData)
    }
    
    func session(_ session: WCSession, activationDidCompleteWith activationState: WCSessionActivationState, error: Error?) {
        print("activationDidCompleteWith activationState:\(activationState) error:\(String(describing: error))")
    }
    
    func sessionDidBecomeInactive(_ session: WCSession) {
        print("sessionDidBecomeInactive: \(session)")
    }
    
    func sessionDidDeactivate(_ session: WCSession) {
        print("sessionDidDeactivate: \(session)")
        
        self.sesssion?.activate()
    }
    
}
