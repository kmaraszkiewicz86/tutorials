//
//  UIImageView.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 27/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit



class CustomImage: UIImageView {
    
    static let imageCache = NSCache<NSString, UIImage>()
    
    //private var imageUrlString: String?
    
    func loadThumbailImageAndSetToImageView(url: String) {
        
        //self.imageUrlString = url
        self.image = nil
        
        if let image : UIImage = CustomImage.imageCache.object(forKey: NSString(string: url)) {
            self.image = image
            return
        }
        
        let request = URLRequest(url: URL(string: url)!)
        
        let session = URLSession.shared
        
        session.dataTask(with: request) { (data, response, error) in
            
            if let err = error {
                print(err)
                return
            }
            
            let imageToCache = UIImage(data: data!)!
            CustomImage.imageCache.setObject(imageToCache, forKey: NSString(string: url))
            
            //if self.imageUrlString == url {
                DispatchQueue.main.async {
                    self.image = imageToCache
                }
            //}
        }.resume()
    }
}
