//
//  VideoService.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 17/08/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import Foundation

class VideoService : NSObject {
    
    static let shared = VideoService()
    
    var completion: (([Video]) -> Void)?
    
    public func fetchHomeVideos() {
        fetchVideos(url: "http://flashcard.izabelamaraszkiewiczit.hostingasp.pl/api/home") { (videos) in
            self.completion?(videos)
        }
    }
    
    public func fetchTrendingsVideos() {
        fetchVideos(url: "https://s3-us-west-2.amazonaws.com/youtubeassets/trending.json") { (videos) in
            self.completion?(videos)
        }
    }
    
    public func fetchSubscriptionsVideos() {
        fetchVideos(url: "https://s3-us-west-2.amazonaws.com/youtubeassets/subscriptions.json") { (videos) in
            self.completion?(videos)
        }
    }
    
    public func fetchVideos(url: String, completion: @escaping ([Video]) -> Void) {
        let url = URLRequest(url: URL(string: url)!)
        let session = URLSession.shared
        
        session.dataTask(with: url) { (data, response, error) in
            
            if error != nil {
                print(error)
                return
            }
            
            do {
                let json = try JSONSerialization.jsonObject(with: data!, options: .mutableContainers)
                
                var videos = [Video]()
                
                for dictioniary in json as! [[String: Any]] {
                    
                    let channelDictionaty = dictioniary["channel"] as! [String: Any]
                    let channel = Channel(name: channelDictionaty["name"] as! String,
                                          profileImageName: channelDictionaty["profile_image_name"] as! String)
                    
                    let dogsImage = Video(thumbailImageName: dictioniary["thumbnail_image_name"] as! String,
                                          title: dictioniary["title"] as! String,
                                          numberOfView: 1300923900,
                                          uploadDate: NSDate(timeIntervalSince1970: TimeInterval(exactly: 1000000)!),
                                          channel: channel)
                    
                    videos.append(dogsImage)
                }
                
                DispatchQueue.main.async {
                    completion(videos)
                }
                
                
            } catch let jsonError {
                print(jsonError)
            }
            
        }.resume()
    }
    
}
