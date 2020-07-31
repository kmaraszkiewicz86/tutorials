//
//  CategoryTableViewController.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 12/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class CategoryTableViewController: UITableViewController {

    //MARK: UI controls
    ///The progress view handler
    @IBOutlet weak var progressView: UIProgressView!
    
    ///The add button handler
    @IBOutlet weak var addBtn: UIBarButtonItem!
    
    ///The load from server button handler
    @IBOutlet weak var loadFromServerBtn: UIBarButtonItem!
    
    //MARK: properties
    ///The categories array
    var categories = [CategoryModel]()
    
    ///The categories service
    var categoryService = try! CategoryService(applicationDelegate: UIApplication.shared.delegate)
    
    ///The progress timer handler
    var progressViewTimer: Timer!
    
    ///Array of colors that are used for enabled or disabled table view style
    let colors: [String: UIColor] = ["Enabled": #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0),
                                     "Disabled": #colorLiteral(red: 0.8039215803, green: 0.8039215803, blue: 0.8039215803, alpha: 1)]
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        //progressView.isHidden = true
        
        getDataFromCoreData()
        
        refreshControl = UIRefreshControl()
        refreshControl?.attributedTitle = NSAttributedString(string: "Pull to refresh")
        refreshControl?.addTarget(self, action: #selector(refreshDataFromTableView(sender:)), for: UIControl.Event.valueChanged)
        
        tableView.addSubview(refreshControl!)
        
        
        progressView.setProgress(0, animated: false)

        self.navigationItem.leftBarButtonItem = self.editButtonItem
    }

    //MARK: Actions
    ///Action run after pull down table view list
    @objc func refreshDataFromTableView(sender: Any?) {
        fetchDataFromServer()
        
    }
    
    ///Acion triggers while progress view are working
    @objc func updateProgressView() {
        
        progressView.progress += 0.1
        progressView.setProgress(progressView.progress, animated: true)
        
        if progressView.progress >= 1 {
            progressView.progress = 0
        }
        
    }
    
    ///Acion triggers after Load From Server Btn clicked
    @IBAction func ReloadDataFromDb(_ sender: UIBarButtonItem) {
        
        fetchDataFromServer()
        
    }
    
    // MARK: - Table view data source
    override func numberOfSections(in tableView: UITableView) -> Int {
        // #warning Incomplete implementation, return the number of sections
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        // #warning Incomplete implementation, return the number of rows
        return categories.count
    }

    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        guard let cell = tableView.dequeueReusableCell(withIdentifier: "CategoryTableViewCell", for: indexPath) as? CategoryTableViewCell else {
            fatalError("Could not load CategoryTableViewCell object")
        }

        let category = categories[indexPath.row]
        
        cell.categoryNameLabel.text = category.name

        return cell
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
    
    //MARK: private methods
    ///Get data from CoreData category table
    private func getDataFromCoreData () {
        do {
            categories = try categoryService.getAll(onBegin: {
                () -> Void in
                DispatchQueue.main.async {
                    self.toogleProgressView(state: true)
                }
            }, onFinish: {
                () -> Void in
                DispatchQueue.main.async {
                    self.toogleProgressView(state: false)
                }})
            } catch {
            self.printError(errorMessage: "Error occured while tring to get data from local storage")
        }
    }
    
    ///Fetch data from server action
    private func fetchDataFromServer() {
        
            categoryService.fetchDataFromServer(onBegin: {
                () -> Void in
                DispatchQueue.main.async {
                    self.toogleProgressView(state: true)
                }
            },onSuccess: {
                categoriesModel -> Void in
                self.categories.removeAll()
                self.categories += categoriesModel
                
                DispatchQueue.main.async {
                    self.tableView.reloadData()
                }
            }, onFinish: {
                () -> Void in
                DispatchQueue.main.async {
                    self.toogleProgressView(state: false)
                    self.refreshControl?.endRefreshing()
                }
            }, onError: {
                dataRequestError -> Void in
                switch dataRequestError {
                case .responseError:
                        self.printError(errorMessage: "Error occured while tring to get response from server")
                        break
                    
                case .convertToModelError:
                    self.printError(errorMessage: "Error occoured while tring to convert data from server")
                    break
                }
            })
    }
    
    ///Change visibility of loading bar
    private func toogleProgressView (state: Bool) {
        
        if state {
            
            if progressViewTimer == nil {
                
                progressViewTimer = Timer.scheduledTimer(timeInterval: 1, target: self, selector: #selector(updateProgressView), userInfo: nil, repeats: true)
                
                navigationItem.title = "Loading data. Please wait..."
                
            }
            
        } else {
            
            progressViewTimer.invalidate()
            progressViewTimer = nil
            progressView.setProgress(0, animated: true)
            
            navigationItem.title = "List of categories"
        }
        
        tableView.isUserInteractionEnabled = !state
        progressView.isHidden = !state
        addBtn.isEnabled = !state
        loadFromServerBtn.isEnabled = !state
        
        toogleTableViewBackgroundColor(state: state)
        
    }
    
    ///Sets color for controls depend of table view state
    private func toogleTableViewBackgroundColor(state: Bool) {
        
        let color = state ? colors["Disabled"] : colors["Enabled"]
        
        tableView.backgroundColor = color
        
        if !tableView.visibleCells.isEmpty {
            for cell in tableView.visibleCells {
                cell.backgroundColor = color
            }
        }
    }
    
    ///Create alert with error on view
    private func printError (errorMessage: String) {
        DispatchQueue.main.async {
            let okBtn = UIAlertAction(title: "OK", style: .cancel, handler: nil)
            
            let alert = UIAlertController(title: "Error occured", message: errorMessage, preferredStyle: .alert)
            
            alert.addAction(okBtn)
            
            self.present(alert, animated: true)
        }
    }
}
