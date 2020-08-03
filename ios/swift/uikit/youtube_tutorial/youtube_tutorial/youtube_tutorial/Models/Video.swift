//
//  Video.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 25/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class Video : NSObject {
    
    var thumbailImageName: String
    var title: String
    var numberOfView: NSNumber
    var uploadDate: NSDate
    
    var channel: Channel
    
    init(thumbailImageName: String, title: String,
         numberOfView: NSNumber, uploadDate: NSDate,
         channel: Channel) {
        
        self.thumbailImageName = thumbailImageName
        self.title = title
        self.numberOfView = numberOfView
        self.uploadDate = uploadDate
        self.channel = channel
        
    }
}
