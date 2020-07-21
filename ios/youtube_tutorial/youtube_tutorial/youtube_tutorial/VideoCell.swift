//
//  VideoCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 20/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class VideoCell: UICollectionViewCell {
    
    let thumbailImageView: UIImageView = {
        let image = UIImageView()
        
        image.backgroundColor = .blue
        image.image = UIImage(named: "dogs")
        image.contentMode = .scaleAspectFill
        image.clipsToBounds = true
        
        return image
    }();
    
    let userProfileImage: UIImageView = {
        let image = UIImageView()
        
        image.backgroundColor = .green
        image.image = UIImage(named: "szarko")
        image.layer.cornerRadius = 22
        image.layer.masksToBounds = true
        
        return image
    }();
    
    let titleLabel: UILabel = {
        let label = UILabel();
        
        label.text = "Pierdki - Puste miejsce"
        label.translatesAutoresizingMaskIntoConstraints = false
        
        return label
    }();
    
    let subtitleTextView: UITextView = {
        let textView = UITextView()
        
        textView.text = "Pierdkowe wideo z domowego zacisza • 1,300,923,900 • 2 years ago"
        textView.textContainerInset = UIEdgeInsets(top: 0, left: -4, bottom: 0, right: 0)
        textView.textColor = .lightGray
        
        textView.translatesAutoresizingMaskIntoConstraints = false
        
        return textView
    }();
    
    let separator: UIView = {
        let view = UIView();
        
        view.backgroundColor = .black
        
        return view
    }();
    
    override init(frame: CGRect) {
        super.init(frame: frame)

        self.setupViews()
    }
    
    required init?(coder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    private func setupViews() {
        addSubview(self.thumbailImageView)
        addSubview(self.userProfileImage)
        addSubview(self.separator)
        addSubview(self.titleLabel)
        addSubview(self.subtitleTextView)
        
        addViewConstraints(withVisualFormat: "H:|-16-[v0]-16-|", views: self.thumbailImageView)
        addViewConstraints(withVisualFormat: "H:|-16-[v0(44)]", views: self.userProfileImage)
        
        addConstraint(NSLayoutConstraint(item: self.titleLabel, attribute: .top, relatedBy: .equal, toItem: self.thumbailImageView, attribute: .bottom, multiplier: 1, constant: 8))
        addConstraint(NSLayoutConstraint(item: self.titleLabel, attribute: .left, relatedBy: .equal, toItem: self.userProfileImage, attribute: .right, multiplier: 1, constant: 8))
        addConstraint(NSLayoutConstraint(item: self.titleLabel, attribute: .right, relatedBy: .equal, toItem: self.thumbailImageView, attribute: .right, multiplier: 1, constant: 0))
        addConstraint(NSLayoutConstraint(item: self.titleLabel, attribute: .height, relatedBy: .equal, toItem: self, attribute: .height, multiplier: 0, constant: 30))
        
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .top, relatedBy: .equal, toItem: self.titleLabel, attribute: .bottom, multiplier: 1, constant: 4))
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .left, relatedBy: .equal, toItem: self.userProfileImage, attribute: .right, multiplier: 1, constant: 8))
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .right, relatedBy: .equal, toItem: self.thumbailImageView, attribute: .right, multiplier: 1, constant: 0))
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .height, relatedBy: .equal, toItem: self.titleLabel, attribute: .height, multiplier: 1, constant: 0))
        
        addViewConstraints(withVisualFormat: "V:|-16-[v0]-8-[v1(44)]-26-[v2(1)]|", views:
            self.thumbailImageView, self.userProfileImage, self.separator)
        addViewConstraints(withVisualFormat: "H:|[v0]|", views: self.separator)
        
    }
}
