//
//  CategoryTableViewCell.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 12/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

///The category table view cell
class CategoryTableViewCell: UITableViewCell {
    
    ///The category image controll handler
    @IBOutlet weak var categoryImage: UIImageView!
    
    ///The category name label cntrol handler
    @IBOutlet weak var categoryNameLabel: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

}
