//
//  SettingCollectionViewCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 29/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class SettingCollectionViewCell: UICollectionViewCell {
    
    var text: String? {
        didSet {
            
            guard let t = text else {
                return
            }
            
            textLabel.text = t
            
        }
    }
    
    private let textLabel: UILabel = {
        let label = UILabel()
        label.translatesAutoresizingMaskIntoConstraints = false
        
        return label
    }();
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        setupView()
    }
    
    func setupView() {
        
        addSubview(textLabel)
        
        NSLayoutConstraint.activate([
            textLabel.centerYAnchor.constraint(equalTo: centerYAnchor),
            textLabel.centerXAnchor.constraint(equalTo: centerXAnchor)
        ])
        
    }
    
    required init?(coder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
}
