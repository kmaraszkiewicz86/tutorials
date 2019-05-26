//
//  ViewController.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 23/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ActivityFormViewController: UIViewController {

    //MARK: View controll
    @IBOutlet weak var saveButton: UIBarButtonItem!
    
    @IBOutlet weak var nameTextField: UITextField!
    
    //MARK: Properties
    var activity: Activity?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        nameTextField.delegate = self
        
        if let activity = self.activity {
            nameTextField.text = activity.name
        }
        
        validateForm()
    }
    
    @IBAction func cancelAction(_ sender: UIBarButtonItem) {
        
        let isPresentingViewController = presentingViewController is UINavigationController
        
        if isPresentingViewController {
            dismiss(animated: true, completion: nil)
        } else if let navController = navigationController {
            navController.popViewController(animated: true)
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        super.prepare(for: segue, sender: sender)
        
        if let btn = sender as? UIBarButtonItem, btn === saveButton {
            activity = Activity(name: nameTextField.text ?? "")
        }
    }
}

///Extension for UITextFieldDelegate
extension ActivityFormViewController: UITextFieldDelegate {
    
    fileprivate func validateForm () {
        let text = self.nameTextField.text ?? ""
        print(text)
        
        self.saveButton.isEnabled = !text.isEmpty
    }
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        textField.resignFirstResponder()
        return true
    }
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        self.saveButton.isEnabled = false
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        validateForm()
        
        if let name = self.nameTextField.text {
            self.activity = Activity(name: name)
        }
    }
}

