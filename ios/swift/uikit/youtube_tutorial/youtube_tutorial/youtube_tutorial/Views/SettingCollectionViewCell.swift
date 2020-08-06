//
//  SettingCollectionViewCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 29/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class SettingCollectionViewCell: BaseCollectionViewCell {
    
    var settings: Settings? {
        didSet {
            
            guard let settingsTmp = settings else {
                return
            }
            
            textLabel.text = settingsTmp.name!
            imageView.image = UIImage(systemName: settingsTmp.iconName!)
            
        }
    }
    
    private let textLabel: UILabel = {
        let label = UILabel()
        label.translatesAutoresizingMaskIntoConstraints = false
        
        return label
    }();
    
    private let imageView: UIImageView = {
        let imageView = UIImageView()
        
        imageView.image = UIImage(systemName: "gear")
        imageView.contentMode = .scaleAspectFit
        imageView.tintColor = .gray
        
        return imageView
    }();
    
    override func setupViews() {
        super.setupViews()
        
        addSubview(textLabel)
        addSubview(imageView)
        
        addViewConstraints(withVisualFormat: "H:|-16-[v0(30)]-8-[v1]|", views: imageView, textLabel)
        addViewConstraints(withVisualFormat: "V:[v0(30)]", views: imageView)
        addViewConstraints(withVisualFormat: "V:|[v0]|", views: textLabel)
        
        NSLayoutConstraint.activate([
            imageView.centerYAnchor.constraint(equalTo: centerYAnchor)
        ])
        
    }
}
