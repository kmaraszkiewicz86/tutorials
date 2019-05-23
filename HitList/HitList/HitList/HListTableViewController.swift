//
//  ViewController.swift
//  HitList
//
//  Created by Krzysztof Maraszkiewicz on 30/04/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import CoreData

class HListTableViewController: UITableViewController{

    var people = [NSManagedObject]()

    override func viewDidLoad() {
        super.viewDidLoad()
        
        navigationItem.leftBarButtonItem = editButtonItem
        
        title = "The list"
        tableView.delegate = self
        tableView.dataSource = self
        tableView.register(HListTableViewCell.self, forCellReuseIdentifier: "Cell")
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        guard let appDelegate = UIApplication.shared.delegate as? AppDelegate else {
            return
        }
        
        let managedObject = appDelegate.persistentContainer.viewContext
        
        let fetchRequest = NSFetchRequest<NSManagedObject>(entityName: "Person")
        
        do {
            people = try managedObject.fetch(fetchRequest)
        } catch let error as NSError {
            print("Could not fetch \(error). \(error.userInfo)")
        }
    }

    @IBAction func addName(_ sender: UIBarButtonItem) {
        let alert = UIAlertController(title: "New Name", message: "Add a new name", preferredStyle: .alert)
        
        let saveAction = UIAlertAction(title: "Save", style: .default) {
            
            [unowned self] action in
            
            print(action.title!)
            
            guard let textField = alert.textFields?.first,
                let nameToSave = textField.text else {
                return
            }
            
            self.save(name: nameToSave)
            self.tableView.reloadData()
            
        }
        
        let cancelAction = UIAlertAction(title: "Cancel", style: .cancel)
        
        alert.addTextField()
        
        alert.addAction(saveAction)
        alert.addAction(cancelAction)
        
        present(alert, animated: true)
    }
    
    func save(name: String) {
        guard let appDelegate = UIApplication.shared.delegate as? AppDelegate else {
            return
        }
        
        let managedContext = appDelegate.persistentContainer.viewContext
        
        let entity = NSEntityDescription.entity(forEntityName: "Person", in: managedContext)!
        
        let person = NSManagedObject(entity: entity, insertInto: managedContext)
        
        person.setValue(name, forKey: "name")
        
        do {
            try managedContext.save()
            people.append(person)
        } catch let error as NSError {
            print("Could not save \(error). \(error.userInfo)")
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        guard let controllerView = segue.destination as? HItemDetailsViewController else {
            print("Wrong destination \(segue.destination)")
            return
        }
        
        guard let hListCell = sender as? HListTableViewCell else {
            print("Wrong sender type")
            return
        }
        
        controllerView.name = hListCell.nameLabel.text ?? ""
    }
    
    @IBAction func returnMethod (sender: UIStoryboardSegue) {
        if let controllerView = sender.source as? HItemDetailsViewController {
            
            if let name = controllerView.NameTextField.text,
                !name.isEmpty {
                let indexPath = tableView.indexPathForSelectedRow!
                let person = people[indexPath.row]
                
                print(indexPath.row)
                print(controllerView.name)
                
                let oldName = person.value(forKey: "name") as! String
                
                guard let appDelegate = UIApplication.shared.delegate as? AppDelegate else {
                    return
                }
                
                let managedObject = appDelegate.persistentContainer.viewContext
                
                let fetchRequest = NSFetchRequest<NSManagedObject>(entityName: "Person")
                fetchRequest.predicate = NSPredicate(format: "name=%@", oldName)
                
                do {
                    let items = try managedObject.fetch(fetchRequest)
                    items[0].setValue(name, forKey: "name")
                    
                    try managedObject.save()
                    
                    person.setValue(name, forKey: "name")
                    tableView.reloadData()
                    
                } catch let error as NSError {
                    fatalError("Could not update record wit error \(error). \(error.userInfo)")
                }
            }
        }
    }
    
    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return people.count
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let person = people[indexPath.row]
        
        guard let cell = tableView.dequeueReusableCell(withIdentifier: "HListTableViewCell", for: indexPath) as? HListTableViewCell else {
            fatalError("Conversion failed for HListTableViewCell")
        }
        
        cell.nameLabel?.text = person.value(forKey: "name") as? String
        return cell
    }
    
    override func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }
    
    override func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCell.EditingStyle, forRowAt indexPath: IndexPath) {
        
        if editingStyle == .delete {
            
            guard let appDelegate = UIApplication.shared.delegate as? AppDelegate else {
                return
            }
            
            let managedObject = appDelegate.persistentContainer.viewContext
            
            let personFromArray = people[indexPath.row] as NSManagedObject
            
            let fetchRequest = NSFetchRequest<NSManagedObject>(entityName: "Person")
            
            fetchRequest.predicate = NSPredicate(format: "name = %@", personFromArray.value(forKey: "name") as! String)
            
            
            do
            {
                let personsFromDB = try managedObject.fetch(fetchRequest)
                
                for p in personsFromDB {
                    managedObject.delete(p)
                }
                
                try managedObject.save()
                
                people.remove(at: indexPath.row)
                tableView.reloadData()
                
            } catch let error as NSError {
                fatalError("Error while deleting item \(personFromArray) with error => \(error). \(error.userInfo)")
            }
        }
        
    }
    
}
