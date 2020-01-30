//
//  FavoritesViewController.swift
//  InstagramLikeApp
//
//  Created by Emil Atanasov on 22.04.18.
//  Copyright © 2018 ApposeStudio Inc. All rights reserved.
//

import UIKit

class FavoritesViewController: UIViewController {
    
    @IBOutlet weak var collectionView: UICollectionView!
    @IBOutlet weak var noItems: UIView!
    override func viewDidLoad() {
        super.viewDidLoad()
        showEmptyView()
        loadData()
    }
    
    func loadData() {
        //TODO: wczytanie wszystkich ulubionych postów.
    }
}


protocol EmptyCollectionView {
    func showCollectionView()
    func showEmptyView()
    var collectionView: UICollectionView! { get }
    var emptyView: UIView? { get }
}

extension EmptyCollectionView {
    func showCollectionView() {
        self.emptyView?.isHidden = true
        self.collectionView.isHidden = false
    }
    
    func showEmptyView() {
        if self.emptyView != nil {
            self.emptyView?.isHidden = false
            self.collectionView.isHidden = true
        }
    }
}


extension FavoritesViewController: EmptyCollectionView {
    var emptyView: UIView? {
        return noItems
    }
}

