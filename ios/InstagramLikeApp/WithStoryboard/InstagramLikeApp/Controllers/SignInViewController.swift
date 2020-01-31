//
//  SignInViewController.swift
//  InstagramLikeApp
//
//  Created by Krzysztof Maraszkiewicz on 31/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import FirebaseAuthUI

class SignInViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }
    
    @IBAction func singInWithEmail(_ sender: Any) {
        let authUI = FUIAuth.defaultAuthUI()
        if let authViewController = authUI?.authViewController() {
            present(authViewController, animated: true)
        }
    }
    
}
