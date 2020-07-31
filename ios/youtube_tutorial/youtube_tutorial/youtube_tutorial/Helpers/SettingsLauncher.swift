//
//  SettingsLauncher.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 28/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class SettingsLauncher : NSObject, UICollectionViewDataSource,
    UICollectionViewDelegate,
    UICollectionViewDelegateFlowLayout {
    
    let blackView = UIView()
    
    private var view: UIView?
    private var navigationController: UINavigationController?
    
    let settingNames = ["tset1", "tset2", "test3"]
    
    lazy var collectionView: UICollectionView = {
        
        let layout = UICollectionViewFlowLayout()
        var cv = UICollectionView(frame: .zero, collectionViewLayout: layout)
        
        cv.delegate = self
        cv.dataSource = self
        
        cv.translatesAutoresizingMaskIntoConstraints = false
        cv.backgroundColor = .red
        
        return cv
    }();
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return settingNames.count
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "SettingCollectionViewCell", for: indexPath) as! SettingCollectionViewCell
        
        cell.text = settingNames[indexPath.item]
        
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath:  IndexPath) -> CGSize {
        
        return CGSize(width: self.view!.frame.width, height: 50)
    }
    
    init(view: UIView?, navigationController: UINavigationController?) {
        super.init()
        
        self.view = view
        self.navigationController = navigationController
        
        self.collectionView.register(SettingCollectionViewCell.self, forCellWithReuseIdentifier: "SettingCollectionViewCell")
    }
    
    private var collectionViewConstraints: [NSLayoutConstraint]?
    private var blackViewConstraints: [NSLayoutConstraint]?
    
    func showSettings() {
        
        blackView.backgroundColor = UIColor(white: 0, alpha: 0.5)
        blackView.translatesAutoresizingMaskIntoConstraints = false
        blackView.alpha = 0
        
        blackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(dismissMoreButton)))
        
        view?.addSubview(blackView)
        self.navigationController?.view.addSubview(blackView)
        
        view?.addSubview(collectionView)
        
        
        collectionViewConstraints = [
            collectionView.bottomAnchor.constraint(equalTo: view!.bottomAnchor),
            collectionView.leadingAnchor.constraint(equalTo: view!.leadingAnchor),
            collectionView.trailingAnchor.constraint(equalTo: view!.trailingAnchor),
            collectionView.heightAnchor.constraint(equalToConstant: 300)
        ]
        
        let height: CGFloat = 200
        let y = view!.frame.height - height
        self.collectionView.frame = CGRect(x: 0, y: view!.frame.height, width: self.collectionView.frame.width, height: height)
        
        blackViewConstraints = [
            blackView.topAnchor.constraint(equalTo: self.navigationController!.view!.topAnchor),
            blackView.trailingAnchor.constraint(equalTo: view!.trailingAnchor),
            blackView.leadingAnchor.constraint(equalTo: view!.leadingAnchor),
            blackView.bottomAnchor.constraint(equalTo: view!.bottomAnchor)
        ]
        
        NSLayoutConstraint.activate(collectionViewConstraints!)
        NSLayoutConstraint.activate(blackViewConstraints!)
        
        UIView.animate(withDuration: 0.5, delay: 0, usingSpringWithDamping: 1, initialSpringVelocity: 1, options: .curveEaseOut, animations: {
            self.blackView.alpha = 1
            
            self.collectionView.frame = CGRect(x: 0, y: y, width: self.collectionView.frame.width, height: height)
        }, completion: nil)
    }
    
    @objc func dismissMoreButton() {
        
        UIView.animate(withDuration: 0.5, delay: 0, usingSpringWithDamping: 1, initialSpringVelocity: 1, options: .curveEaseOut, animations: {
            self.blackView.alpha = 0
            
            self.collectionView.frame = CGRect(x: 0, y: self.view!.frame.height, width: self.view!.frame.width, height: 0)
            
        }, completion: nil)
        
        self.collectionView.removeConstraints(collectionViewConstraints!)
        self.blackView.removeConstraints(blackViewConstraints!)
    }
}