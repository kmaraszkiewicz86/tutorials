//
//  Channel.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 25/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class Channel: NSObject {
    
    var name: String?
    var profile_image_name: String?
    
    init(name: String, profileImageName: String) {
        self.name = name
        self.profile_image_name = profileImageName
    }
}
