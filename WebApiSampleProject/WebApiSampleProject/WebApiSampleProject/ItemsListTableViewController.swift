//
//  ItemsListTableViewController.swift
//  WebApiSampleProject
//
//  Created by Krzysztof Maraszkiewicz on 06/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ItemsListTableViewController: UITableViewController {

    var titleNames = [String]()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        refreshControl = UIRefreshControl()
        
        loadData(onEndFetchingData: nil)
        
        refreshControl?.attributedTitle = NSAttributedString(string: "Pull to refresh")
        
        refreshControl?.addTarget(self, action: #selector(refresh(sender:)), for: UIControl.Event.valueChanged)
        
        tableView.addSubview(refreshControl!)
        
        // Uncomment the following line to preserve selection between presentations
        // self.clearsSelectionOnViewWillAppear = false

        // Uncomment the following line to display an Edit button in the navigation bar for this view controller.
        // self.navigationItem.rightBarButtonItem = self.editButtonItem
    }
    
    @objc func refresh (sender: AnyObject) {
        loadData(onEndFetchingData: {
            () -> Void in
            DispatchQueue.main.async {
                self.refreshControl?.endRefreshing()
            }
        })
    }

    // MARK: - Table view data source

    override func numberOfSections(in tableView: UITableView) -> Int {
        // #warning Incomplete implementation, return the number of sections
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return titleNames.count
    }

    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {   
        guard let cell = tableView.dequeueReusableCell(withIdentifier: "ItemTableViewCell", for: indexPath) as? ItemTableViewCell else {
            fatalError("Could not convert cell to proper type")
        }

        cell.titleLabel.text = titleNames[indexPath.row]

        return cell
    }
 
    private func loadData(onEndFetchingData: (() -> Void)?) {
        
        let valuesHelper = ValuesMgtHelper()
        
        valuesHelper.GetAll(onBegin: {
            DispatchQueue.main.async {
                self.titleNames.removeAll()
                self.tableView.reloadData()
            }
        },onSucess: {
            (val) -> Void in
            for v in val {
                self.titleNames.append(v)
            }
            
            DispatchQueue.main.async {
                self.tableView.reloadData()
                
                if let action = onEndFetchingData {
                    action()
                }
            }
        }, onRequestFailure: {
            (error) -> Void in
            DispatchQueue.main.async {
                self.showAlert(message: "\(error)")
                if let action = onEndFetchingData {
                    action()
                }            }
        }, onConvertFailure: {
            (error) -> Void in
            DispatchQueue.main.async {
                self.showAlert(message: "\(error)")
                if let action = onEndFetchingData {
                    action()
                }
            }
        })
    }
    
    private func showAlert(message: String) {
        let okBtn = UIAlertAction(title: "OK", style: .cancel, handler: nil)
        
        let alert = UIAlertController(title: "Error occured", message: message, preferredStyle: .alert)
        
        alert.addAction(okBtn)
        
        self.present(alert, animated: true)
    }

    /*
    // Override to support conditional editing of the table view.
    override func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        // Return false if you do not want the specified item to be editable.
        return true
    }
    */

    /*
    // Override to support editing the table view.
    override func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCell.EditingStyle, forRowAt indexPath: IndexPath) {
        if editingStyle == .delete {
            // Delete the row from the data source
            tableView.deleteRows(at: [indexPath], with: .fade)
        } else if editingStyle == .insert {
            // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view
        }    
    }
    */

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

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

}
