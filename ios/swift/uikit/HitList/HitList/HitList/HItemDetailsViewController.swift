//
//  HItemDetailsViewController.swift
//  HitList
//
//  Created by Krzysztof Maraszkiewicz on 05/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class HItemDetailsViewController: UIViewController {

    var name = ""
    
    @IBOutlet weak var NameTextField: UITextField!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        NameTextField.text = name
    }
    

    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Get the new view controller using segue.destination.
        // Pass the selected object to the new view controller.
    }
    */

    @IBAction func cancelButton(_ sender: Any) {
        navigationController?.popViewController(animated: true)
    }
}
