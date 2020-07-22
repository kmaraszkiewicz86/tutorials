//
//  HomeController.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 20/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

private let reuseIdentifier = "Cell"

class HomeController: UICollectionViewController, UICollectionViewDelegateFlowLayout {

    private var menuBar: MenuBar = {
        let mb = MenuBar()
        
        return mb
    }()
    
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
        
        self.collectionView.register(VideoCell.self, forCellWithReuseIdentifier: "cellid")
        
        setupMenuBar()
    }
    
    private func setupMenuBar() {
        self.view.addSubview(menuBar)
        
        self.view.addViewConstraints(withVisualFormat: "H:|[v0]|", views: self.menuBar)
        self.view.addViewConstraints(withVisualFormat: "V:|[v0(50)]", views: self.menuBar)
    }
    
    override func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return 5
    }
    
    override func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = self.collectionView.dequeueReusableCell(withReuseIdentifier: "cellid", for: indexPath)
    
        
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath:  IndexPath) -> CGSize {
        
        let height = (view.frame.width - 16 - 16) * 9 / 16
        
        return CGSize(width: self.view.frame.width, height: height + 16 + 78)
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAt section: Int) -> CGFloat {
        return 0
    }

}
