//
//  Settings.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 06/08/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

class Settings: NSObject {
    
    var name: String
    
    var iconName: String
    
    init(name: String, iconName: String) {
        
        self.name = name
        self.iconName = iconName
        
        super.init()
    }
    
}
