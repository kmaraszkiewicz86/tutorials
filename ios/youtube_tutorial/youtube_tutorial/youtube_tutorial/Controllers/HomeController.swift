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
    
    private var videos: [Video]?
    
    private func fetchVideos() {
        let url = URLRequest(url: URL(string: "http://localhost:5000/api/Home")!)
        let session = URLSession.shared
        
        session.dataTask(with: url) { (data, response, error) in
            
            if error != nil {
                print(error)
                return
            }
            
            do {
                let json = try JSONSerialization.jsonObject(with: data!, options: .mutableContainers)
                
                self.videos = [Video]()
                
                for dictioniary in json as! [[String: Any]] {
                                        
                    let channelDictionaty = dictioniary["channel"] as! [String: Any]
                    let channel = Channel(name: channelDictionaty["name"] as! String,
                    profileImageName: channelDictionaty["profile_image_name"] as! String)
                    
                    let dogsImage = Video(thumbailImageName: dictioniary["thumbnail_image_name"] as! String,
                                          title: dictioniary["title"] as! String,
                                          numberOfView: 1300923900,
                                          uploadDate: NSDate(timeIntervalSince1970: TimeInterval(exactly: 1000000)!),
                                          channel: channel)
                    
                    self.videos?.append(dogsImage)
                }
                
                DispatchQueue.main.async {
                    self.collectionView.reloadData()
                }
                
                
            } catch let jsonError {
                print(jsonError)
            }
            
        }.resume()
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

        fetchVideos()
        
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
        return self.videos?.count ?? 0
    }
    
    override func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = self.collectionView.dequeueReusableCell(withReuseIdentifier: "VideoCell", for: indexPath) as! VideoCell
    
        cell.video = videos![indexPath.item]
        
        return cell
    }
    
    override func viewWillTransition(to size: CGSize, with coordinator: UIViewControllerTransitionCoordinator) {
        super.viewWillTransition(to: size, with: coordinator)

        self.collectionView?.reloadData()
        
        menuBar.colectionView.reloadData()
    }
    
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath:  IndexPath) -> CGSize {
        
        let height = (view.frame.width - CGFloat(LayoutHelper.leftViewCellMargin) - CGFloat(LayoutHelper.rightViewCellMargin)) * 9 / 16
        
        return CGSize(width: self.view.frame.width, height: height + 16 + 90)
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAt section: Int) -> CGFloat {
        return 0
    }

}
