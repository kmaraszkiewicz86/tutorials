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
    
    var player : AVPlayer?
    
    var observer: NSKeyValueObservation?
    
    var isPlaying = false
    
    let controlsContainerView: UIView  = {
        let view = UIView()
        
        view.backgroundColor = UIColor(white: 0, alpha: 1)
        
        return view
    }();
    
    let videoLengthLabel: UILabel = {
        let label = UILabel()
        
        label.text = "00:00"
        label.textColor = .white
        label.textAlignment = .right
        label.font = UIFont.boldSystemFont(ofSize: 13)
        
        label.translatesAutoresizingMaskIntoConstraints = false
        
        return label
    }()
    
    let currentTimeLabel: UILabel = {
        let label = UILabel()
        
        label.text = "00:00"
        label.textColor = .white
        label.textAlignment = .left
        label.font = UIFont.boldSystemFont(ofSize: 13)
        
        label.translatesAutoresizingMaskIntoConstraints = false
        
        return label
    }()
    
    let videoSlider:  UISlider = {
        let slider = UISlider()
        
        slider.minimumTrackTintColor = .red
        slider.thumbTintColor = .red
        slider.maximumTrackTintColor = .white
        
        slider.addTarget(self, action: #selector(handleSliderChange), for: .valueChanged)
        
        slider.translatesAutoresizingMaskIntoConstraints = false
        
        return slider
    }()
    
    let activityIndicatorView: UIActivityIndicatorView = {
        let activityIndicatorView = UIActivityIndicatorView(style: .large)
        
        activityIndicatorView.color = .white
        
        activityIndicatorView.translatesAutoresizingMaskIntoConstraints = false
        activityIndicatorView.startAnimating()
        return activityIndicatorView
    }()
    
    let pausePlayButton: UIButton = {
        let pauseButton = UIButton(type: .system)
        
        let image = UIImage(named: "pause")
        pauseButton.setImage(image, for: .normal)
        
        pauseButton.translatesAutoresizingMaskIntoConstraints = false
        pauseButton.addTarget(self, action: #selector(handlePause), for: .touchUpInside)
        pauseButton.tintColor = .white
        
        return pauseButton
    }();
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        backgroundColor = .black
        
        prepareToPlay()
        setupGradientLayer()
        
        controlsContainerView.frame = frame
        
        addSubview(controlsContainerView)
        controlsContainerView.addSubview(activityIndicatorView)
        controlsContainerView.addSubview(pausePlayButton)
        controlsContainerView.addSubview(videoLengthLabel)
        controlsContainerView.addSubview(videoSlider)
        controlsContainerView.addSubview(currentTimeLabel)
        
        NSLayoutConstraint.activate([
            activityIndicatorView.centerYAnchor.constraint(equalTo: centerYAnchor),
            activityIndicatorView.centerXAnchor.constraint(equalTo: centerXAnchor)
        ])
        
        NSLayoutConstraint.activate([
            pausePlayButton.centerYAnchor.constraint(equalTo: centerYAnchor),
            pausePlayButton.centerXAnchor.constraint(equalTo: centerXAnchor),
            pausePlayButton.widthAnchor.constraint(equalToConstant: 50),
            pausePlayButton.heightAnchor.constraint(equalToConstant: 50)
        ])
        
        NSLayoutConstraint.activate([
            videoLengthLabel.trailingAnchor.constraint(equalTo: trailingAnchor, constant: -8),
            videoLengthLabel.bottomAnchor.constraint(equalTo: bottomAnchor, constant: -2),
            videoLengthLabel.widthAnchor.constraint(equalToConstant: 50),
            videoLengthLabel.heightAnchor.constraint(equalToConstant: 24)
        ])
        
        NSLayoutConstraint.activate([
            currentTimeLabel.leadingAnchor.constraint(equalTo: leadingAnchor, constant: 8),
            currentTimeLabel.bottomAnchor.constraint(equalTo: bottomAnchor, constant: -2),
            currentTimeLabel.widthAnchor.constraint(equalToConstant: 50),
            currentTimeLabel.heightAnchor.constraint(equalToConstant: 24)
        ])
        
        NSLayoutConstraint.activate([
            videoSlider.bottomAnchor.constraint(equalTo: bottomAnchor),
            videoSlider.trailingAnchor.constraint(equalTo: videoLengthLabel.leadingAnchor),
            videoSlider.leadingAnchor.constraint(equalTo: currentTimeLabel.trailingAnchor),
            videoSlider.heightAnchor.constraint(equalToConstant: 24)
        ])
        
        pausePlayButton.isHidden = true
        videoSlider.isHidden = true
        videoLengthLabel.isHidden = true
        currentTimeLabel.isHidden = true
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
            
            player?.play()
            
            player?.addObserver(self, forKeyPath: "currentItem.loadedTimeRanges", options: [], context: nil)
        }
    }
    
    private func prepareToPlay() {
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
            self.observer = playerItem.observe(\.status, options:  [.new, .old, .prior], changeHandler: { (playerItem, change) in
                if playerItem.status == .readyToPlay {
                    
                    self.activityIndicatorView.stopAnimating()
                    self.controlsContainerView.backgroundColor = .clear
                    
                    if let duration = self.player?.currentItem?.duration {
                        let seconds = CMTimeGetSeconds(duration)
                        
                        self.videoLengthLabel.text = self.getTimeString(seconds)
                    }
                    
                    self.pausePlayButton.isHidden = false
                    self.videoSlider.isHidden = false
                    self.videoLengthLabel.isHidden = false
                    self.currentTimeLabel.isHidden = false
                    
                    self.isPlaying = true
                }
            })
            
            let interval = CMTime(value: 1, timescale: 2)
            
            player = AVPlayer(playerItem: playerItem)
            
            let playerLayer = AVPlayerLayer(player: player)
            self.layer.addSublayer(playerLayer)
            playerLayer.frame = self.frame
            
            //track player progress
            player?.addPeriodicTimeObserver(forInterval: interval, queue: DispatchQueue.main, using: { (progressTime) in
                let seconds = CMTimeGetSeconds(progressTime)
                
                self.currentTimeLabel.text = self.getTimeString(seconds)
                
                if let duration = self.player?.currentItem?.duration {
                    
                    let durationSeconds = CMTimeGetSeconds(duration)
                    
                    let sliderTime = seconds / Float64(durationSeconds)
                    self.videoSlider.value = Float(sliderTime)
                }
            })
            
            player?.play()
        }
    }
    
    private func setupGradientLayer() {
        let gradientLayer = CAGradientLayer()
        
        gradientLayer.frame = bounds
        gradientLayer.colors = [UIColor.clear.cgColor, UIColor.black.cgColor]
        gradientLayer.locations = [0.7, 1.2]
        
        controlsContainerView.layer.addSublayer(gradientLayer)
    }
    
    private func getTimeString(_ seconds: Float64) -> String {
        let secondsText = String(format: "%02d", Int(seconds) % 60)
        let minutesText = String(format: "%02d", Int(seconds) / 60)
        
        return "\(minutesText):\(secondsText)"
    }
    
    @objc private func handlePause() {
        
        if isPlaying {
            self.player?.pause()
            self.pausePlayButton.setImage(UIImage(named: "play"), for: .normal)
        } else {
            self.player?.play()
            self.pausePlayButton.setImage(UIImage(named: "pause"), for: .normal)
        }
        
        isPlaying = !isPlaying
    }
    
    @objc private func handleSliderChange() {

        if let duration = self.player?.currentItem?.duration {
            let seconds = CMTimeGetSeconds(duration)
            let value = Float64(videoSlider.value) * seconds
            
            let seekTime = CMTime(value: Int64(value), timescale: 1)
            self.player?.seek(to: seekTime, completionHandler: {
                completedSeek in
                self.currentTimeLabel.text = self.getTimeString(value)
            })
        }
        
    }
}

class VideoLauncher: NSObject {
    
    public func showVideoPlayer() {
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
