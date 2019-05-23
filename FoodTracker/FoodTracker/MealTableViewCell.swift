//
//  MealTableViewCell.swift
//  FoodTracker
//
//  Created by Krzysztof Maraszkiewicz on 23/04/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import os.log

class MealTableViewCell: UITableViewCell {
    
    //MARK: properties
    @IBOutlet weak var mealLabel: UILabel!
    
    @IBOutlet weak var mealImageView: UIImageView!
    
    @IBOutlet weak var ratingControl: RatingControl!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
        
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

}
