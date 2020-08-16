//
//  Settings.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 06/08/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

class Settings: NSObject {
    
    var name: SettingName
    
    var iconName: String
    
    init(name: SettingName, iconName: String) {
        
        self.name = name
        self.iconName = iconName
        
        super.init()
    }
    
}
