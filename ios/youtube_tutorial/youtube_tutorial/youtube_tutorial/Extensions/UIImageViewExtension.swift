//
//  UIImageView.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 27/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

extension UIImageView {
    func loadThumbailImageAndSetToImageView(url: String) {
        let request = URLRequest(url: URL(string: url)!)
        
        let session = URLSession.shared
        
        session.dataTask(with: request) { (data, response, error) in
            
            if let err = error {
                print(err)
                return
            }
            
            DispatchQueue.main.async {
                self.image = UIImage(data: data!)
            }
        }.resume()
    }
}
