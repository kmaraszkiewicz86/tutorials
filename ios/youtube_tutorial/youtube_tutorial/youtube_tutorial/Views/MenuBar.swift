//
//  MenuBar.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 23/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class MenuBar: UIView, UICollectionViewDataSource, UICollectionViewDelegate,
    UICollectionViewDelegateFlowLayout{

    let cellId = "cellid"
    
    lazy var colectionView: UICollectionView = {
        let layout = UICollectionViewFlowLayout()
        let collectionView = UICollectionView(frame: .zero, collectionViewLayout: layout)
        
        collectionView.backgroundColor = UIColor.rgb(red: 230, green: 32, blue: 31)
        
        collectionView.dataSource = self
        collectionView.delegate = self
        
        return collectionView
    }()
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        
        self.colectionView.register(UICollectionViewCell.self, forCellWithReuseIdentifier: cellId)
        
        addSubview(self.colectionView)
        addViewConstraints(withVisualFormat: "H:|[v0]|", views: self.colectionView)
        addViewConstraints(withVisualFormat: "V:|[v0]|", views: self.colectionView)
        
        backgroundColor = UIColor.rgb(red: 230, green: 32, blue: 31)
    }
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return 4
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: cellId, for: indexPath)
        
        cell.backgroundColor = UIColor.blue
        
        return cell
    }
    
    required init?(coder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }

}
