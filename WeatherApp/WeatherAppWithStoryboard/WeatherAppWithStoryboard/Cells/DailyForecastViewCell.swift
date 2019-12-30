//
//  DailyForecastViewCell.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 30/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class DailyForecastViewCell: UITableViewCell {

    @IBOutlet weak var day: UILabel!
    
    @IBOutlet weak var icon: UIImageView!
    
    @IBOutlet weak var temperature: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)
    }

}
