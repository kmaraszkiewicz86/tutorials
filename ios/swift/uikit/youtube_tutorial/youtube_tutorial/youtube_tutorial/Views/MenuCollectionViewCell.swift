//
//  MenuCollectionViewCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 23/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class MenuCollectionViewCell: BaseCollectionViewCell {
    
    let imageView: UIImageView = {
        let imageView = UIImageView()
        
        imageView.image = UIImage(named: "home")?.withRenderingMode(.alwaysTemplate)
        imageView.tintColor = UIColor.rgb(red: 91, green: 14, blue: 13)
        
        return imageView
    }();
    
    override var isHighlighted: Bool {
        didSet {
            self.imageView.tintColor = isHighlighted
                ? .white
                : UIColor.rgb(red: 91, green: 14, blue: 13)
        }
    }
    
    override var isSelected: Bool {
        didSet {
            self.imageView.tintColor = isSelected
            ? .white
            : UIColor.rgb(red: 91, green: 14, blue: 13)
        }
    }
    
     override func setupViews() {
        super.setupViews()
        
        addSubview(self.imageView)
        
        addViewConstraints(withVisualFormat: "H:[v0(28)]", views: self.imageView)
        addViewConstraints(withVisualFormat: "V:[v0(28)]", views: self.imageView)
        
        
        NSLayoutConstraint.activate([self.imageView.centerYAnchor.constraint(equalTo: centerYAnchor),
            self.imageView.centerXAnchor.constraint(equalTo: centerXAnchor)])
    }
    
}
