//
//  LayoutHelper.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 25/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class LayoutHelper: NSObject {
    static let leftViewCellMargin: Int = 16
    static let rightViewCellMargin: Int = 16
    static let userProfileImageWidth: Int = 44
    static let userProfileImageRightMargin: Int = 8;
    
    static func getSumOfLayoutHelperValues() -> Int {
        let result = (LayoutHelper.leftViewCellMargin + LayoutHelper.userProfileImageWidth + LayoutHelper.userProfileImageRightMargin + LayoutHelper.rightViewCellMargin)
        
        return result;
    }
}
