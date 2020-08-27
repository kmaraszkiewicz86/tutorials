//
//  VideoLauncher.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 26/08/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import AVFoundation

class VideoPlayerView : UIView {
    
    var player : AVPlayer!
    
    let activityIndicatorView: UIActivityIndicatorView = {
        let activityIndicatorView = UIActivityIndicatorView(style: .large)
        activityIndicatorView.translatesAutoresizingMaskIntoConstraints = false
        activityIndicatorView.startAnimating()
        return activityIndicatorView
    }()
    
    let controlsContainerView: UIView  = {
        let view = UIView()
        view.backgroundColor = UIColor(white: 0, alpha: 1)
        return view
    }();
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        backgroundColor = .black
        
        prepareToPlay()
        
        controlsContainerView.frame = frame
        
        addSubview(controlsContainerView)
        controlsContainerView.addSubview(activityIndicatorView)
        
        NSLayoutConstraint.activate([
            activityIndicatorView.centerYAnchor.constraint(equalTo: centerYAnchor),
            activityIndicatorView.centerXAnchor.constraint(equalTo: centerXAnchor)
        ])
    }
    
    required init?(coder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    private func setupPalyerView() {
        let urlString = "https://firebasestorage.googleapis.com/v0/b/gameofchats-762ca.appspot.com/o/message_movies%2F12323439-9729-4941-BA07-2BAE970967C7.mov?alt=media&token=3e37a093-3bc8-410f-84d3-38332af9c726"
        
        if let url = URL(string: urlString) {
            player = AVPlayer(url: url)
            
            let playerLayer = AVPlayerLayer(player: player)
            self.layer.addSublayer(playerLayer)
            playerLayer.frame = self.frame
            
            player.play()
            
            player.addObserver(self, forKeyPath: "currentItem.loadedTimeRanges", options: [], context: nil)
        }
    }
    
    var observer: NSKeyValueObservation?

    func prepareToPlay() {
        let urlString = "https://firebasestorage.googleapis.com/v0/b/gameofchats-762ca.appspot.com/o/message_movies%2F12323439-9729-4941-BA07-2BAE970967C7.mov?alt=media&token=3e37a093-3bc8-410f-84d3-38332af9c726"
        
        if let url = URL(string: urlString) {
            
            // Create asset to be played
            let asset = AVAsset(url: url)
            
            let assetKeys = [
                "playable",
                "hasProtectedContent"
            ]
            // Create a new AVPlayerItem with the asset and an
            // array of asset keys to be automatically loaded
            let playerItem = AVPlayerItem(asset: asset,
                                      automaticallyLoadedAssetKeys: assetKeys)
            
            // Register as an observer of the player item's status property
            self.observer = playerItem.observe(\.status, options:  [.new, .old], changeHandler: { (playerItem, change) in
                if playerItem.status == .readyToPlay {
                    self.activityIndicatorView.stopAnimating()
                    self.controlsContainerView.backgroundColor = .clear
                }
            })
            
            player = AVPlayer(playerItem: playerItem)
            
            let playerLayer = AVPlayerLayer(player: player)
            self.layer.addSublayer(playerLayer)
            playerLayer.frame = self.frame
            
            player.play()
        }
    }
}

class VideoLauncher: NSObject {
    
    public func showVideoPlayer() {
        print("showVideoPlayer")
        
        if let keyWindow = UIApplication.shared.windows.first(where: { (window) -> Bool in
            return window.isKeyWindow
        }) {
            let view = UIView(frame: keyWindow.frame)
            view.frame = CGRect(x: keyWindow.frame.width - 10, y: keyWindow.frame.height - 10, width: 10, height: 10 )
            view.backgroundColor = .white
            
            //16 x 9 is the aspect ratio of all HD videos
            let height = keyWindow.frame.width * 9 / 16
            let videoPlayerViewFrame = CGRect(x: 0, y: 0, width: keyWindow.frame.width, height: height)
            let videoPlayerView = VideoPlayerView(frame: videoPlayerViewFrame)
            
            keyWindow.addSubview(view)
            view.addSubview(videoPlayerView)
            
            UIView.animate(withDuration: 0.5, delay: 0, usingSpringWithDamping: 1, initialSpringVelocity: 1, options: .curveEaseOut, animations: {
                view.frame = keyWindow.frame
            }) { (comletedAnimation) in
                UIApplication.shared.setStatusBarHidden(true, with: .fade)
            }
        }
    }
    
}
