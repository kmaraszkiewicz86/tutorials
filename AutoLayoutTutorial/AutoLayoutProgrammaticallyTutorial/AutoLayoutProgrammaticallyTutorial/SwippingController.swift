//
//  SwippingController.swift
//  AutoLayoutProgrammaticallyTutorial
//
//  Created by Krzysztof Maraszkiewicz on 30/07/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class SwippingController: UICollectionViewController, UICollectionViewDelegateFlowLayout {
    
    let pages = [
        Page(image: UIImage(named: "krowa")!, headerText: "To jest testowy nagłówek 1!", bodyText: "To jest test testów, tej testowej aplikacji, którą tworze dla testowego wyniku"),
        Page(image: UIImage(named: "Pierdzioch")!, headerText: "To jest testowy nagłówek 2!", bodyText: "Tworze dla testowego wyniku"),
        Page(image: UIImage(named: "rycka")!, headerText: "To jest testowy nagłówek 3!", bodyText: "To jest test testów, tej testowej aplikacji")
    ]
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        collectionView.backgroundColor = .white
        collectionView.register(PageCell.self, forCellWithReuseIdentifier: "cellid")
        
        collectionView.isPagingEnabled = true
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAt section: Int) -> CGFloat {
        return 0
    }
    
    override func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return pages.count
    }
    
    override func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "cellid", for: indexPath) as! PageCell
        
        let currentPage = pages[indexPath.item]
        
        cell.page = currentPage
        
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath: IndexPath) -> CGSize {
        return CGSize(width: view.frame.width, height: view.frame.height)
    }
}
