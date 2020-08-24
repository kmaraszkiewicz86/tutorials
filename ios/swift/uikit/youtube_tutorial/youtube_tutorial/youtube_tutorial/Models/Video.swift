//
//  Video.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 25/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class Video : NSObject {
    var thumbnail_image_name: String?
    var title: String?
    var number_of_views: NSNumber?
    var uploadDate: NSDate?
    var duration: NSNumber?
    
    var channel: Channel?
    
    init(dictionary: [String: Any]) {
        let channelDictionaty = dictionary["channel"] as! [String: Any]
        
        thumbnail_image_name = dictionary["thumbnail_image_name"] as? String ?? ""
        title = dictionary["title"] as? String ?? ""
        duration = dictionary["duration"] as? NSNumber ?? 0
        number_of_views = dictionary["number_of_views"] as? NSNumber ?? 0
        uploadDate = NSDate(timeIntervalSince1970: TimeInterval(exactly: 1000000)!)
        channel = Channel(name: channelDictionaty["name"] as! String,
                          profileImageName: channelDictionaty["profile_image_name"] as! String)
    }
}
