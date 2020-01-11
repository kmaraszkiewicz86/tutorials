//
//  ViewController.swift
//  My First iOS app
//
//  Created by Emil Atanasov on 5/8/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import UIKit
// Przykładowy komentarz.
class ViewController: UIViewController {
    
    @IBOutlet var fireButton:UIButton!

    @IBAction func clickHandler(_ sender: UIButton) {
        
        let red:CGFloat = CGFloat(drand48())
        let green:CGFloat = CGFloat(drand48())
        let blue:CGFloat = CGFloat(drand48())
        // Zmiana koloru tła.
        self.view.backgroundColor = UIColor.init(red: red, green: green, blue: blue, alpha: 1)
    }
    
    // Egzemplarz UIImageView.
    @IBOutlet var imageView:UIImageView!
    
    @IBAction func switchHandle(_ sender: UISwitch) {
        
        imageView.isHidden = !sender.isOn
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        // Wszelka konfiguracja dodatkowa po wczytaniu widoku, najczęściej z pliku nib.
        
        fireButton.addTarget(self, action: #selector(ViewController.fireClickHandler(_:)), for: UIControlEvents.touchUpInside)
        
        imageView.isHidden = true
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Usunięcie wszelkich zasobów, które można później ponownie utworzyć.
    }
    
    func fireClickHandler(_ sender: UIButton) {
        print("Przycisk Ognia! został naciśnięty!")
        self.view.backgroundColor = UIColor.red
    }
    
}

