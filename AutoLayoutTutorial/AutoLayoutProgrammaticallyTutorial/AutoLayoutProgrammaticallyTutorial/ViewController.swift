//
//  ViewController.swift
//  AutoLayoutProgrammaticallyTutorial
//
//  Created by Krzysztof Maraszkiewicz on 25/07/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

extension UIColor {
    public static let customPink: UIColor =  UIColor(red: 232/255, green: 68/255, blue: 133/255, alpha: 1)
}

class ViewController: UIViewController {

    let imageView: UIImageView = {
        let imageView = UIImageView(image: #imageLiteral(resourceName: "rycka"))
        //enable autolayout for this imageView
        imageView.translatesAutoresizingMaskIntoConstraints = false
        imageView.contentMode = .scaleAspectFit
        
        return imageView
    }()
    
    let descriptionTextView: UITextView = {
        let textView = UITextView()
        
        textView.text = "To jest testowy nagłówek!"
        
        let attributeText = NSMutableAttributedString(string: "To jest testowy nagłówek!", attributes: [NSAttributedString.Key.font: UIFont.boldSystemFont(ofSize: 18)])
        
        attributeText.append(NSMutableAttributedString(string: "\n\n\nTo jest test testów, tej testowej aplikacji, którą tworze dla testowego wyniku", attributes: [NSAttributedString.Key.font: UIFont.systemFont(ofSize: 13), NSAttributedString.Key.foregroundColor: UIColor.gray]))
        
        textView.attributedText = attributeText
        
        textView.translatesAutoresizingMaskIntoConstraints = false
        textView.textAlignment = .center
        textView.isEditable = false
        textView.isScrollEnabled = false
        
        return textView
    }()
    
    private let prevButton: UIButton = {
        let btn = UIButton(type: .system)
        btn.setTitle("NEXT", for: .normal)
        btn.titleLabel?.font = UIFont.boldSystemFont(ofSize: 14)
        btn.setTitleColor(.gray, for: .normal)
        btn.translatesAutoresizingMaskIntoConstraints = false
        
        return btn
    }()
    
    private let nextButton: UIButton = {
        let btn = UIButton(type: .system)
        btn.setTitle("PREV", for: .normal)
        btn.translatesAutoresizingMaskIntoConstraints = false
        btn.titleLabel?.font = UIFont.boldSystemFont(ofSize: 14)
        
        btn.setTitleColor(UIColor.customPink, for: .normal)
        
        return btn
    }()
    
    private let pageControll: UIPageControl = {
        let pageControll = UIPageControl()
        pageControll.currentPage = 0
        pageControll.numberOfPages = 4
        
        pageControll.currentPageIndicatorTintColor = UIColor.customPink
        pageControll.pageIndicatorTintColor = .gray
        
        return pageControll
    }()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        self.view.addSubview(descriptionTextView)
        
        self.setupBottomButtons()
        self.setupLayout()
    }
    
    private func setupBottomButtons() {
        
        let greenView = UIView()
        greenView.backgroundColor = .green
        
        let controlsStackViewContainer = UIStackView(arrangedSubviews: [prevButton,
            pageControll, nextButton])

        controlsStackViewContainer.translatesAutoresizingMaskIntoConstraints = false

        controlsStackViewContainer.distribution = .fillEqually
        
        
        view.addSubview(controlsStackViewContainer)
        
        NSLayoutConstraint.activate([
            controlsStackViewContainer.bottomAnchor.constraint(equalTo: view.safeAreaLayoutGuide.bottomAnchor),
            controlsStackViewContainer.leadingAnchor.constraint(equalTo: view.safeAreaLayoutGuide.leadingAnchor),
            controlsStackViewContainer.trailingAnchor.constraint(equalTo: view.safeAreaLayoutGuide.trailingAnchor),
            controlsStackViewContainer.heightAnchor.constraint(equalToConstant: 50)
            
            ])
        
    }

    private func setupLayout() {
        
        let imageViewContainerView = UIView()
        imageViewContainerView.translatesAutoresizingMaskIntoConstraints = false
        
        view.addSubview(imageViewContainerView)
        
        imageViewContainerView.topAnchor.constraint(equalTo: view.topAnchor, constant: 0).isActive = true
        imageViewContainerView.leadingAnchor.constraint(equalTo: view.leadingAnchor).isActive = true
        imageViewContainerView.trailingAnchor.constraint(equalTo: view.trailingAnchor).isActive = true
        imageViewContainerView.heightAnchor.constraint(equalTo: view.heightAnchor, multiplier: 0.5).isActive = true
        
        imageViewContainerView.layoutMargins.top = 0
        
        imageViewContainerView.addSubview(imageView)
        
        imageView.centerXAnchor.constraint(equalTo: imageViewContainerView.centerXAnchor).isActive = true
        imageView.centerYAnchor.constraint(equalTo: imageViewContainerView.centerYAnchor).isActive = true
        imageView.heightAnchor.constraint(equalTo: imageViewContainerView.heightAnchor, multiplier: 0.5).isActive = true
        
        descriptionTextView.topAnchor.constraint(equalTo: imageViewContainerView.bottomAnchor).isActive = true
        descriptionTextView.leadingAnchor.constraint(equalTo: view.leadingAnchor, constant: 24).isActive = true
        descriptionTextView.trailingAnchor.constraint(equalTo: view.trailingAnchor, constant: -24).isActive = true

        descriptionTextView.bottomAnchor.constraint(equalTo: view.bottomAnchor, constant: 0).isActive = true
    }
}
