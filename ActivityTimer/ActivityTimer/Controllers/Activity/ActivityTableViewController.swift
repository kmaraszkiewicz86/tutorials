//
//  ActivityTableViewController.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 24/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import os.log

class ActivityTableViewController: UITableViewController {

    var activities = [Activity]()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        self.navigationItem.leftBarButtonItem = self.editButtonItem
    }

    // MARK: - Table view data source

    override func numberOfSections(in tableView: UITableView) -> Int {
        // #warning Incomplete implementation, return the number of sections
        
        print(tableView.isEditing)
        
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        // #warning Incomplete implementation, return the number of rows
        return activities.count
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        guard let cell = tableView.dequeueReusableCell(withIdentifier: "ActivityTableViewCell", for: indexPath) as? ActivityTableViewCell else {
            
            os_log("Could not found valid ActivityTableViewCell type to convert data to table cell", log: OSLog.activityTableViewController, type: .fault)
            fatalError("Occours error while tring to find proper table cell type")
        }

        cell.nameLabel.text = activities[indexPath.row].name

        return cell
    }
 
    // Override to support conditional editing of the table view.
    override func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        // Return false if you do not want the specified item to be editable.
        return true
    }

    
    // Override to support editing the table view.
    override func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCell.EditingStyle, forRowAt indexPath: IndexPath) {
        if editingStyle == .delete {
            // Delete the row from the data source
            
            print(indexPath.row)
            
            activities.remove(at: indexPath.row)
            tableView.deleteRows(at: [indexPath], with: .fade)
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
                   log: OSLog.activityTableViewController,
                   type: .info,
            segue.destination)
            
        case "EditActivity":
            
            guard let activityFormViewController = segue.destination as? ActivityFormViewController else {
                os_log("Invalid convertion from segue.destination to ActivityFormViewController", log: OSLog.activityTableViewController, type: .error)
                
                fatalError("Errour occours while tring to navigate to edit form")
                
            }
            
            guard let selectedCell = sender as? ActivityTableViewCell else {
                os_log("Invalid convertion from sender to ActivityTableViewCell", log: OSLog.activityTableViewController, type: .error)
                
                fatalError("Errour occours while tring to navigate to edit form")
            }
            
            guard let index = tableView.indexPath(for: selectedCell) else {
                os_log("Invalid convertion from sender to ActivityTableViewCell", log: OSLog.activityTableViewController, type: .error)
                
                fatalError("Errour occours while tring to navigate to edit form")
            }
            
            activityFormViewController.activity = activities[index.row]
            
        default:
            os_log("Destination %s not implemented",
                   log: OSLog.activityTableViewController,
                   type: .error,
                   segue.destination)
            fatalError("Segue destination not found")
        }
        
    }
    
    @IBAction func redeirectFromForm(sender: UIStoryboardSegue) {
        
        if let activityFormViewController = sender.source as? ActivityFormViewController,
            let activity = activityFormViewController.activity {
            
            if let index = tableView.indexPathForSelectedRow {
                activities[index.row] = activity
                tableView.reloadRows(at: [index], with: .fade)
                
            } else {
                activities.append(activity)
                self.tableView.reloadData()
            }
        }
        
    }
 
}
