//
//  LocationTableViewCell.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 02/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class LocationTableViewCell: UITableViewCell {

    @IBOutlet weak var time: UILabel!
    
    @IBOutlet weak var city: UILabel!
    
    @IBOutlet weak var temperature: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

}
