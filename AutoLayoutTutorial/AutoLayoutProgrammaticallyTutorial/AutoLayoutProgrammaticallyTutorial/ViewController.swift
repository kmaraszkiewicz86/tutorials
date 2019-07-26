//
//  ViewController.swift
//  AutoLayoutProgrammaticallyTutorial
//
//  Created by Krzysztof Maraszkiewicz on 25/07/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    let imageView: UIImageView = {
        let imageView = UIImageView(image: #imageLiteral(resourceName: "krowa"))
        //enable autolayout for this imageView
        imageView.translatesAutoresizingMaskIntoConstraints = false
        imageView.contentMode = .scaleAspectFit
        
        return imageView
    }()
    
    let descriptionTextView: UITextView = {
        let textView = UITextView()
        
        textView.text = "To jest testowy nagłówek!"
        textView.translatesAutoresizingMaskIntoConstraints = false
        textView.font = UIFont.boldSystemFont(ofSize: 18)
        textView.textAlignment = .center
        textView.isEditable = false
        
        return textView
    }()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        
        self.view.addSubview(imageView)
        self.view.addSubview(descriptionTextView)
        
        self.setupLayout()
    }

    private func setupLayout() {
        
        let imageViewContainerView = UIView()
        imageViewContainerView.translatesAutoresizingMaskIntoConstraints = false
        
        view.addSubview(imageViewContainerView)
        
        imageViewContainerView.topAnchor.constraint(equalTo: view.topAnchor).isActive = true
        imageViewContainerView.leadingAnchor.constraint(equalTo: view.leadingAnchor).isActive = true
        imageViewContainerView.trailingAnchor.constraint(equalTo: view.trailingAnchor).isActive = true
        imageViewContainerView.heightAnchor.constraint(equalTo: view.heightAnchor, multiplier: 0.5).isActive = true
        
        imageViewContainerView.addSubview(imageView)
        imageView.centerXAnchor.constraint(equalTo: imageViewContainerView.centerXAnchor).isActive = true
        imageView.centerYAnchor.constraint(equalTo: imageViewContainerView.centerYAnchor).isActive = true
        imageView.heightAnchor.constraint(equalTo: imageViewContainerView.heightAnchor, multiplier: 0.5).isActive = true
        imageView.widthAnchor.constraint(equalTo: imageViewContainerView.widthAnchor, multiplier: 0.5).isActive = true
        
        descriptionTextView.topAnchor.constraint(equalTo: imageViewContainerView.bottomAnchor).isActive = true
        descriptionTextView.leadingAnchor.constraint(equalTo: view.leadingAnchor).isActive = true
        descriptionTextView.trailingAnchor.constraint(equalTo: view.trailingAnchor).isActive = true
        descriptionTextView.bottomAnchor.constraint(equalTo: view.bottomAnchor, constant: 0).isActive = true
    }
}
