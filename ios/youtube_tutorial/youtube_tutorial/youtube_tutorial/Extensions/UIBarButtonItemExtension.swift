//
//  UIBarButtonItemExtension.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 25/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

extension UIBarButtonItem {
    
    public static func getImageBarButtonItemUsing(imageByName: String, target: Any, selector: Selector) -> UIBarButtonItem {
        
        let image = UIImage(systemName: imageByName)?.withRenderingMode(.alwaysOriginal)

        return UIBarButtonItem(image: image?.withTintColor(.white), style: .plain, target: target, action: selector)
    }
    
}
