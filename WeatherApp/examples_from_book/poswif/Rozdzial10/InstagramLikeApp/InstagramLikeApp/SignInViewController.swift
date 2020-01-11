//
//  SigInViewController.swift
//  InstagramLikeApp
//
//  Created by Emil Atanasov on 11.02.18.
//  Copyright © 2018 ApposeStudio Inc. All rights reserved.
//

import UIKit
import FirebaseAuthUI

class SignInViewController: UIViewController {
  
    override func viewDidLoad() {
        super.viewDidLoad()

        // Wszelka konfiguracja dodatkowa po wczytaniu widoku.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Usunięcie wszelkich zasobów, które można później ponownie utworzyć.
    }
    
    @IBAction func signInWithEmail(_ sender: Any) {
        let authUI = FUIAuth.defaultAuthUI()
        if let authViewController = authUI?.authViewController() {
            present(authViewController, animated: true, completion: nil)
        }
    }

}
