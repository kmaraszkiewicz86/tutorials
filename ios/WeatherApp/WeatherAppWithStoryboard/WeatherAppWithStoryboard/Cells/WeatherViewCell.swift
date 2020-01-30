//
//  WeatherViewCell.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 30/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class WeatherViewCell: UICollectionViewCell {
    @IBOutlet weak var day: UILabel!
    
    @IBOutlet weak var icon: UIImageView!
    
    @IBOutlet weak var temperature: UILabel!
}
