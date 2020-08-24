//
//  VideoCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 20/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class VideoCell: BaseCollectionViewCell {
    
    var video : Video? {
        didSet {
            
            guard let videoTmp = video else {
                return
            }
            
            let numberFormatter = NumberFormatter()
            numberFormatter.numberStyle = .decimal
            
            titleLabel.text = videoTmp.title ?? ""
            
            if let thumbailImageName = videoTmp.thumbnail_image_name {
                self.thumbailImageView.loadThumbailImageAndSetToImageView(url: thumbailImageName)
            }
            
            measureTitleText(video: videoTmp)
            
            guard let channel = videoTmp.channel else {
                return
            }
            
            if let channelName = channel.name {
                subtitleTextView.text = "\(channelName) • \(numberFormatter.string(from: videoTmp.number_of_views ?? 0)!) • 2 years ago"
            }
            
            if let profileImageName = channel.profile_image_name {
                userProfileImage.loadThumbailImageAndSetToImageView(url: profileImageName)
            }
        }
    }
    
    func loadThumbailImageAndSetToImageView(video: Video) {
        guard let thumbailImageName = video.thumbnail_image_name else {
            return
        }
        let request = URLRequest(url: URL(string: thumbailImageName)!)
        
        let session = URLSession.shared
        
        session.dataTask(with: request) { (data, response, error) in
            
            if let err = error {
                print(err)
                return
            }
            
            DispatchQueue.main.async {
                self.thumbailImageView.image = UIImage(data: data!)
            }
        }.resume()
        
    }
    
    func measureTitleText(video: Video) {
        
        guard let videoTitle = video.title else {
            return
        }
        
        let size = CGSize(width: frame.width - CGFloat(LayoutHelper.getSumOfLayoutHelperValues()), height: 1000)
        let options = NSStringDrawingOptions.usesFontLeading.union(.usesLineFragmentOrigin)
        let estimatedRect = NSString(string: videoTitle).boundingRect(with: size, options: options, attributes: [NSAttributedString.Key.font: UIFont.systemFont(ofSize: 14)], context: nil)
        
        if (estimatedRect.size.height > 20) {
            self.titleLabelLayoutConstrinat?.constant = 44
        } else {
            self.titleLabelLayoutConstrinat?.constant = 20
        }
    }
    
    var titleLabelLayoutConstrinat: NSLayoutConstraint?
    
    let thumbailImageView: CustomImage = {
        let image = CustomImage()
        
        image.backgroundColor = .blue
        image.image = UIImage(named: "dogs")
        image.contentMode = .scaleAspectFill
        image.clipsToBounds = true
        
        return image
    }();
    
    let userProfileImage: CustomImage = {
        let image = CustomImage()
        
        image.backgroundColor = .green
        image.image = UIImage(named: "szarko")
        image.layer.cornerRadius = 22
        image.layer.masksToBounds = true
        image.contentMode = .scaleAspectFill
        
        return image
    }();
    
    let titleLabel: UILabel = {
        let label = UILabel();
        
        label.text = "Pierdki - Puste miejsce"
        label.translatesAutoresizingMaskIntoConstraints = false
        label.textColor = .black
        
        label.numberOfLines = 2
        
        return label
    }();
    
    let subtitleTextView: UITextView = {
        let textView = UITextView()
        
        textView.text = "Pierdkowe wideo z domowego zacisza • 1,300,923,900 • 2 years ago"
        textView.textContainerInset = UIEdgeInsets(top: 0, left: -4, bottom: 0, right: 0)
        textView.textColor = .lightGray
        textView.isEditable = false
        textView.backgroundColor = .white
        
        textView.translatesAutoresizingMaskIntoConstraints = false
        
        return textView
    }();
    
    
    
    let separator: UIView = {
        let view = UIView();
        
        view.backgroundColor = UIColor(red: 230/255, green: 230/255, blue: 230/255, alpha: 1)
        
        return view
    }();
    
    
    
    internal override func setupViews() {
        addSubview(self.thumbailImageView)
        addSubview(self.userProfileImage)
        addSubview(self.separator)
        addSubview(self.titleLabel)
        addSubview(self.subtitleTextView)
        
        addViewConstraints(withVisualFormat: "H:|-\(LayoutHelper.leftViewCellMargin)-[v0]-\(LayoutHelper.rightViewCellMargin)-|", views: self.thumbailImageView)
        addViewConstraints(withVisualFormat: "H:|-\(LayoutHelper.leftViewCellMargin)-[v0(\(LayoutHelper.userProfileImageWidth))]", views: self.userProfileImage)
        
        addConstraint(NSLayoutConstraint(item: self.titleLabel, attribute: .top, relatedBy: .equal, toItem: self.thumbailImageView, attribute: .bottom, multiplier: 1, constant: 8))
        addConstraint(NSLayoutConstraint(item: self.titleLabel, attribute: .left, relatedBy: .equal, toItem: self.userProfileImage, attribute: .right, multiplier: 1, constant: 8))
        addConstraint(NSLayoutConstraint(item: self.titleLabel, attribute: .right, relatedBy: .equal, toItem: self.thumbailImageView, attribute: .right, multiplier: 1, constant: 0))
        
        self.titleLabelLayoutConstrinat = NSLayoutConstraint(item: self.titleLabel, attribute: .height, relatedBy: .equal, toItem: self, attribute: .height, multiplier: 0, constant: 44)
        
        addConstraint(self.titleLabelLayoutConstrinat!)
        
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .top, relatedBy: .equal, toItem: self.titleLabel, attribute: .bottom, multiplier: 1, constant: 4))
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .left, relatedBy: .equal, toItem: self.userProfileImage, attribute: .right, multiplier: 1, constant: 8))
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .right, relatedBy: .equal, toItem: self.thumbailImageView, attribute: .right, multiplier: 1, constant: 0))
        addConstraint(NSLayoutConstraint(item: self.subtitleTextView, attribute: .height, relatedBy: .equal, toItem: self.titleLabel, attribute: .height, multiplier: 1, constant: 0))
        
        addViewConstraints(withVisualFormat: "V:|-\(LayoutHelper.leftViewCellMargin)-[v0]-\(LayoutHelper.userProfileImageRightMargin)-[v1(44)]-50-[v2(1)]|", views:
            self.thumbailImageView, self.userProfileImage, self.separator)
        addViewConstraints(withVisualFormat: "H:|[v0]|", views: self.separator)
        
    }
    
    func fetchVideos() {
        //let url = NSURL("")
    }
}
