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

    let cellId = "MenuCollectionViewCell"
    
    let menuIcons = ["house.fill", "flame", "rectangle.stack", "person.circle.fill"]
    
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
        
        self.colectionView.register(MenuCollectionViewCell.self, forCellWithReuseIdentifier: cellId)
        
        addSubview(self.colectionView)
        addViewConstraints(withVisualFormat: "H:|[v0]|", views: self.colectionView)
        addViewConstraints(withVisualFormat: "V:|[v0]|", views: self.colectionView)
        
        backgroundColor = UIColor.rgb(red: 230, green: 32, blue: 31)
        
        let indexPath = IndexPath(item: 0, section: 0)
        DispatchQueue.main.async {
            self.colectionView.selectItem(at: indexPath, animated: true, scrollPosition: .left)
        }
    }
    
    required init?(coder: NSCoder) {
        fatalError("init(coder:) has not been implemented")
    }
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return 4
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: cellId, for: indexPath) as! MenuCollectionViewCell
        
        cell.imageView.image = UIImage(named: self.menuIcons[indexPath.row])?.withRenderingMode(.alwaysTemplate)
        
        cell.imageView.image = UIImage(systemName: self.menuIcons[indexPath.row])?.withRenderingMode(.alwaysTemplate)
        cell.imageView.tintColor = UIColor.rgb(red: 91, green: 14, blue: 13)
        
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath: IndexPath) -> CGSize {
        
        return CGSize(width: frame.width / 4, height: frame.height)
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumInteritemSpacingForSectionAt section: Int) -> CGFloat {
        return 0
    }
}
