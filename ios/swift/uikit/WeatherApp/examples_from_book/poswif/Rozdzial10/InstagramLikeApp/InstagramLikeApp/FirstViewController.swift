//
//  FirstViewController.swift
//  InstagramLikeApp
//
//  Created by Emil Atanasov on 10.02.18.
//  Copyright © 2018 ApposeStudio Inc. All rights reserved.
//

import UIKit
import FirebaseAuthUI

class FirstViewController: UIViewController {

    @IBAction func onLogout(_ sender: Any) {
        let authUI = FUIAuth.defaultAuthUI()
        do {
            try authUI?.signOut()
            let nc = NotificationCenter.default
            nc.post(name: Notification.Name(rawValue: "userSignedOut"),
                    object: nil,
                    userInfo: nil)
            // Usunięcie aktywnego użytkownika.
            DataManager.shared.user = nil
            DataManager.shared.userUID = nil
        } catch let error {
            print("Błąd: \(error)")
        }
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        // Wszelka konfiguracja dodatkowa po wczytaniu widoku, najczęściej z pliku nib.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Usunięcie wszelkich zasobów, które można później ponownie utworzyć.
    }


}

