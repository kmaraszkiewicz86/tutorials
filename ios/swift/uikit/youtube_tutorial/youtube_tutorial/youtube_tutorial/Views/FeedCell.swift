//
//  FeedCell.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 17/08/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class FeedCell : BaseCollectionViewCell, UICollectionViewDelegate, UICollectionViewDataSource,
UICollectionViewDelegateFlowLayout{
    
    private var videos: [Video]?
    
    var url: String = "http://flashcard.izabelamaraszkiewiczit.hostingasp.pl/api/home"
    
    lazy var collectionView: UICollectionView = {
        var layout = UICollectionViewFlowLayout()
        var cv = UICollectionView(frame: .zero, collectionViewLayout: layout)
        
        cv.delegate = self
        cv.dataSource = self
        
        cv.backgroundColor = .white
        
        return cv
    }()
    
    let cellId = "cellId"
    
    func fetchVideos() {
        VideoService.shared.fetchHomeVideos()
    }
    
    override func setupViews() {
        super.setupViews()
        
        VideoService.shared.completion = { (videos) in
            self.videos = videos
            
            self.collectionView.reloadData()
        }
        
        fetchVideos()
        
        backgroundColor = . brown
        
        addSubview(collectionView)
        addViewConstraints(withVisualFormat: "H:|[v0]|", views: collectionView)
        addViewConstraints(withVisualFormat: "V:|[v0]|", views: collectionView)
        
        collectionView.register(VideoCell.self, forCellWithReuseIdentifier: cellId)
    }
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return self.videos?.count ?? 0
    }

    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = self.collectionView.dequeueReusableCell(withReuseIdentifier: cellId, for: indexPath) as! VideoCell

        cell.video = videos![indexPath.item]

        return cell
    }

    func viewWillTransition(to size: CGSize, with coordinator: UIViewControllerTransitionCoordinator) {
        //super.viewWillLayoutSubviews()

        
        
        guard let flowLayout = collectionView.collectionViewLayout as? UICollectionViewFlowLayout else {
            return
        }

        flowLayout.invalidateLayout()
    }


    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath:  IndexPath) -> CGSize {

        let height = (frame.width - CGFloat(LayoutHelper.leftViewCellMargin) - CGFloat(LayoutHelper.rightViewCellMargin)) * 9 / 16

        return CGSize(width: self.frame.width, height: height + 16 + 90)
    }

    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAt section: Int) -> CGFloat {
        return 0
    }
    
}
