//
//  ViewController.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 23/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ActivityFormViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
    }
    
    @IBAction func cancelAction(_ sender: UIBarButtonItem) {
        
        let isPresentingViewController = presentingViewController is UINavigationController
        
        if isPresentingViewController {
            dismiss(animated: true, completion: nil)
        } else if let navController = navigationController {
            navController.popViewController(animated: true)
        }
    }
}

