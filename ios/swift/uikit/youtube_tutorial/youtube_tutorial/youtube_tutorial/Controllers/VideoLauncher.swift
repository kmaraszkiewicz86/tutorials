//
//  VideoLauncher.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 26/08/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class VideoLauncher: NSObject {
    
    public func showVideoPlayer() {
        print("showVideoPlayer")
        
        if let keyWindow = UIApplication.shared.windows.first(where: { (window) -> Bool in
            return window.isKeyWindow
        }) {
            let view = UIView(frame: keyWindow.frame)
            view.backgroundColor = .red
            
            view.frame = CGRect(x: keyWindow.frame.width - 10, y: keyWindow.frame.height - 10, width: 10, height: 10 )
            
            keyWindow.addSubview(view)
            
            UIView.animate(withDuration: 0.5, delay: 0, usingSpringWithDamping: 1, initialSpringVelocity: 1, options: .curveEaseOut, animations: {
                view.frame = keyWindow.frame
            }) { (comletedAnimation) in
                
            }
        }
    }
    
}
