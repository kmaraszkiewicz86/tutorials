//
//  HomeFeedViewController.swift
//  InstagramLikeApp
//
//  Created by Emil Atanasov on 13.03.18.
//  Copyright © 2018 ApposeStudio Inc. All rights reserved.
//

import UIKit
import Firebase
import FirebaseStorageUI

class HomeFeedViewController: UIViewController {
    private let reuseIdentifier = "FeedCell"
    var model:[PostModel]?
    
    var users = [String: UserModel?]()
    
    @IBOutlet weak var collectionView: UICollectionView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Wszelka konfiguracja dodatkowa po wczytaniu widoku.
        let cellNib = UINib(nibName: "FeedViewCell", bundle: nil)
        collectionView.register(cellNib, forCellWithReuseIdentifier: reuseIdentifier)
        collectionView.translatesAutoresizingMaskIntoConstraints = false
        collectionView.contentInset = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 0)
        if let layout = collectionView.collectionViewLayout as? UICollectionViewFlowLayout {
            layout.minimumLineSpacing = 0
            layout.minimumInteritemSpacing = 0
            layout.sectionInset = UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 0)
            
            let imageWidth = collectionView.frame.size.width
            layout.estimatedItemSize = CGSize(width: imageWidth, height: imageWidth)
        }
        
        loadData()
    }
    
    func loadData() {
        model = []
        DataManager.shared.fetchHomeFeed {[weak self] items in
            if items.count > 0 {
                self?.model? += items
                self?.loadAllUsers()
                self?.collectionView.reloadData()
            }
        }
    }
    
    func loadAllUsers() {
        var usersInfoToLoad = 0
        var usersInfoLoaded = 0
        
        
        if let model = self.model {
            for item in model {
                let userId = item.author
                if users[userId] == nil {
                    usersInfoToLoad += 1
                    // Przypisanie pustego modelu.
                    users[userId] = UserModel()
                }
            }
            
            let reloadView = { [weak self] in
                if usersInfoLoaded == usersInfoToLoad {
                    self?.collectionView.reloadData()
                }
            }
            
            // Rozpoczęcie wczytywania.
            for author in users.keys {
                let userId = author
                DataManager.shared.loadUserInfo(userId: userId) { [weak self] userModel in
                    if let userModel = userModel {
                        self?.users[userId] = userModel
                        usersInfoLoaded += 1
                        // Uaktualnienie interfejsu użytkownika po wczytaniu danych.
                        reloadView()
                    }
                }
            }
            
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.identifier == "openProfile"  {
            if let navController = segue.destination as? UINavigationController {
                if let profileVC = navController.topViewController as? ProfileViewController {
                    profileVC.userUDID = sender as? String
                }
            }
        }
    }
}

extension HomeFeedViewController: UICollectionViewDataSource {
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return model?.count ?? 0
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        guard let cell = collectionView.dequeueReusableCell(withReuseIdentifier: reuseIdentifier, for: indexPath) as? FeedViewCell else {
            return UICollectionViewCell()
        }
        
        guard let post = model?[indexPath.row] else {
            return cell
        }
        // Konfiguracja komórki.
        cell.avatarName.text = post.author
        cell.avatarImage.image = #imageLiteral(resourceName: "user")
        
        cell.delegate = self
        
        cell.imageDimentions = CGSize(width: post.width, height: post.height)
        if let image = post.photoURL {
            let imgRef = Storage.storage().reference().child(image)
            
            cell.image.sd_setImage(with: imgRef, placeholderImage: #imageLiteral(resourceName: "loading"), completion: nil)
        }
        // Uaktualnienie informacji o użytkowniku.
        if let user = self.users[post.author] {
            cell.avatarName.text = user?.username ?? post.author
            if let avatarPath = user?.avatarPhoto {
                let imgRef = Storage.storage().reference().child(avatarPath)
                cell.avatarImage.sd_setImage(with: imgRef, placeholderImage: #imageLiteral(resourceName: "user"), completion: nil)
            }
        }
        
        return cell
    }
}

extension HomeFeedViewController: ProfileHandler {
    func openProfile(cell: UICollectionViewCell) {
        guard let indexPath = self.collectionView.indexPath(for: cell),
        let post = model?[indexPath.row] else {
            return
        }
        
        print("UserId \(post.author)")
        
        performSegue(withIdentifier: "openProfile", sender: post.author)
        
        
    }
    
}
