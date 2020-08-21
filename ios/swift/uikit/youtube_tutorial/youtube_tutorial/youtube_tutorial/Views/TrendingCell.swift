//
//  TrendingCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 21/08/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class TrendingCell: FeedCell {
    override func fetchVideos() {
        VideoService.shared.fetchTrendingsVideos()
    }
}
