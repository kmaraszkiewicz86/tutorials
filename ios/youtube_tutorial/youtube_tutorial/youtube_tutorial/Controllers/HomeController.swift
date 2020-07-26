//
//  HomeController.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 20/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class HomeController: UICollectionViewController, UICollectionViewDelegateFlowLayout {
    
    private var menuBar: MenuBar = {
        let mb = MenuBar()
        
        return mb
    }()
    
    private var videos: [Video] {
        
        let channel = Channel(name: "PierdkoweKonto", profileImageName: "szarko")
        
        let dogsImage = Video(thumbailImageName: "dogs", title: "Pierdki - Se siedzą, sie gapia", numberOfView: 1300923900, uploadDate: NSDate(timeIntervalSince1970: TimeInterval(exactly: 1000000)!), channel: channel)
        
        let dogs2Image = Video(thumbailImageName: "dogs2", title: "Pierdki - Se stoja, sie gapia na pike, ktora jest u gory na suficie", numberOfView: 11111111, uploadDate: NSDate(timeIntervalSince1970: TimeInterval(exactly: 1000000)!), channel: channel)
        
        let dogs3Image = Video(thumbailImageName: "kotopitbulopies", title: "Pierdki - Kotopitbulopies po kapieli", numberOfView: 11111111, uploadDate: NSDate(timeIntervalSince1970: TimeInterval(exactly: 1000000)!), channel: channel)
        
        return [dogsImage, dogs2Image, dogs3Image]
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

        self.navigationItem.title = "Home"
        self.navigationController?.navigationBar.isTranslucent = false
        
        let titleLabel = UILabel(frame: CGRect(x: 0, y: 0, width: self.view.frame.width - 32, height: self.view.frame.height))
        titleLabel.textColor = .white
        titleLabel.text = "Home"
        titleLabel.font = UIFont.systemFont(ofSize: 20)
            
        self.navigationItem.titleView = titleLabel
        
        self.collectionView.backgroundColor = .white
        
        self.collectionView.register(VideoCell.self, forCellWithReuseIdentifier: "VideoCell")
        
        self.collectionView.contentInset = UIEdgeInsets(top: 50, left: 0, bottom: 0, right: 0)
        
        collectionView.scrollIndicatorInsets = UIEdgeInsets(top: 50, left: 0, bottom: 0, right: 0)
        
        setupMenuBar()
        setupNavBarButtons()
    }
    
    private func setupNavBarButtons()
    {
        let searchBarButtonItem = UIBarButtonItem.getImageBarButtonItemUsing(imageByName: "ellipsis.circle.fill", target: self, selector: #selector(handleSearch))
        
        let moreButtonBarButtonItem =  UIBarButtonItem.getImageBarButtonItemUsing(imageByName: "magnifyingglass", target: self, selector: #selector(handleMoreButton))
        
        navigationItem.rightBarButtonItems = [moreButtonBarButtonItem, searchBarButtonItem]
    }
    
    @objc func handleMoreButton() {
        let okAction = UIAlertAction(title: "OK", style: .cancel, handler: nil)
        
        let alertController = UIAlertController(title: "handleMoreButton", message: "Handle of more button", preferredStyle: .actionSheet)
        
        alertController.addAction(okAction)
        
        present(alertController, animated: false)
    }
    
    @objc func handleSearch() {
        let okAction = UIAlertAction(title: "OK", style: .cancel, handler: nil)
        
        let alertController = UIAlertController(title: "Search", message: "Handle of search button", preferredStyle: .actionSheet)
        
        alertController.addAction(okAction)
        
        present(alertController, animated: false)
    }
    
    private func setupMenuBar() {
        self.view.addSubview(menuBar)
        
        self.view.addViewConstraints(withVisualFormat: "H:|[v0]|", views: self.menuBar)
        self.view.addViewConstraints(withVisualFormat: "V:|[v0(50)]", views: self.menuBar)
    }
    
    override func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return self.videos.count
    }
    
    override func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = self.collectionView.dequeueReusableCell(withReuseIdentifier: "VideoCell", for: indexPath) as! VideoCell
    
        cell.video = videos[indexPath.item]
        
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath:  IndexPath) -> CGSize {
        
        let height = (view.frame.width - CGFloat(LayoutHelper.leftViewCellMargin) - CGFloat(LayoutHelper.rightViewCellMargin)) * 9 / 16
        
        return CGSize(width: self.view.frame.width, height: height + 16 + 90)
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAt section: Int) -> CGFloat {
        return 0
    }

}
