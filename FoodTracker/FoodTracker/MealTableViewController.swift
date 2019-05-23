//
//  MealTableViewController.swift
//  FoodTracker
//
//  Created by Krzysztof Maraszkiewicz on 23/04/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import os.log

class MealTableViewController: UITableViewController {
    
    //MARK: properties
    
    var meals = [Meal]()

    override func viewDidLoad() {
        super.viewDidLoad()

        navigationItem.leftBarButtonItem = editButtonItem
        
        if let savedMeals = loadMeals() {
            meals += savedMeals
        } else {
            loadSampleMeals()
        }
    }

    // MARK: - Table view data source

    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return meals.count
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let indentifier = "MealTableViewCell"
        
        guard let cell = tableView.dequeueReusableCell(withIdentifier: indentifier, for: indexPath) as? MealTableViewCell else {
            fatalError("The dequeued cell is not an instance of MealTableViewCell.")
        }

        let meal = meals[indexPath.row]
        
        cell.mealLabel.text = meal.name
        cell.imageView?.image = meal.photo
        cell.ratingControl.rating = meal.rating

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
            meals.remove(at: indexPath.row)
            saveMeals()
            // Delete the row from the data source
            tableView.deleteRows(at: [indexPath], with: .fade)
        } else if editingStyle == .insert {
            // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view
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
        
        switch (segue.identifier ?? "") {
            case "AddItem":
                os_log("Adding a new meal.", log: OSLog.default, type: .debug)
            case "ShowDetail":
                guard let mealDetailsViewController = segue.destination as? MealViewController else {
                    fatalError("Unexpected segue \(segue.destination)")
                }
                    
                    guard let selectedMealCell = sender as? MealTableViewCell else {
                        fatalError("Unexpected sender")
                    }
                    
                    guard let index = tableView.indexPath(for: selectedMealCell) else {
                        fatalError("The selected cell is not being displayeg on the table")
                    }
                    
                    let selectedMeal = meals[index.row]
                    mealDetailsViewController.meal = selectedMeal
            
            default:
                fatalError("Unkbown \(segue.identifier ?? "")")
            }
        }
 
    
    private func loadSampleMeals() {
        
        var photos = [UIImage]()
        
        for index in 1...3 {
            photos.append(UIImage(named: "meal\(index)")!)
        }
        
        meals.append(GetMealObj(named: "Caprese Salad", using: photos[0], ratedBy: 4))
        meals.append(GetMealObj(named: "Chicken Potatoes", using: photos[1], ratedBy: 5))
        meals.append(GetMealObj(named: "Pasta with Meatballs", using: photos[2], ratedBy: 3))
    }
    
    private func GetMealObj (named name: String, using photo: UIImage?, ratedBy rating: Int) -> Meal {
        guard let meal = Meal(name: name, photo: photo, rating: rating) else {
            fatalError("Unable to create instance of object")
        }
        
        return meal
    }
    
    @IBAction func unwindToMealList(sender: UIStoryboardSegue) {
        
        if let sourceViewController = sender.source as? MealViewController, let meal = sourceViewController.meal {
            
            if let selectedIndexPath = tableView.indexPathForSelectedRow {
                meals[selectedIndexPath.row] = meal
                tableView.reloadRows(at: [selectedIndexPath], with: .none)
            } else {
                let newIndexPath = IndexPath(row: meals.count, section: 0)
                meals.append(meal)
                tableView.insertRows(at: [newIndexPath], with: .automatic)
                
            }
            
            saveMeals()
        }
    }
    
    private func saveMeals() {
        let isSucessfulSave = NSKeyedArchiver.archiveRootObject(meals, toFile: Meal.ArchivingUrl.path)
        
        if isSucessfulSave {
            os_log("Meals successfully saved.", log: OSLog.default, type: .debug)
        } else {
            os_log("Failed to save meals...", log: OSLog.default, type: .error)
        }
    }
    
    private func loadMeals() -> [Meal]? {
        return NSKeyedUnarchiver.unarchiveObject(withFile: Meal.ArchivingUrl.path) as? [Meal]
    }

}
