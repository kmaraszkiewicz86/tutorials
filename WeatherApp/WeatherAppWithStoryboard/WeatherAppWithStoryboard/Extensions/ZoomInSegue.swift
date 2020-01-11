//
//  ZoomInSegue.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 11/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ZoomInSegue: UIStoryboardSegue {
    override func perform() {
        
    }
    
    func zoomIn() {
        let superView = self.source.view.superview
        let center = self.source.view.center
        
        self.destination.view.transform = CGAffineTransform.init(scaleX: 0.05, y: 0.05).rotated(by: 90 * .pi / 180)
        self.destination.view.center = center
        
        superView?.addSubview(self.destination.view)
        
        UIView.animate(withDuration: 0.5, delay: 0, options: .curveEaseIn, animations: {
            self.destination.view.transform = CGAffineTransform.identity
        }, completion: { success in
             self.source.present(self.destination, animated: false, completion: nil)
        })
    }
}
