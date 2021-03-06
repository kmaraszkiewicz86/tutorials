//
//  SettingCollectionViewCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 29/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class SettingCollectionViewCell: BaseCollectionViewCell {
    
    override var isSelected: Bool {
        didSet {
            
            if (isSelected) {
                backgroundColor = .darkGray
                textLabel.textColor = .white
                imageView.tintColor = .white
            } else {
                backgroundColor = .white
                textLabel.textColor = .darkGray
                imageView.tintColor = .darkGray
            }
        }
    }
    
    var settings: Settings? {
        didSet {
            
            guard let settingsTmp = settings else {
                return
            }
            
            textLabel.text = settings?.name.rawValue
            imageView.image = UIImage(systemName: settingsTmp.iconName)
            imageView.image?.withRenderingMode(.alwaysTemplate)
            imageView.image?.withTintColor(.darkGray)
            
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
