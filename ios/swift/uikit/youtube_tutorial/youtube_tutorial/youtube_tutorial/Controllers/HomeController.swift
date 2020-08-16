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
    
    private lazy var settingsLauncher : SettingsLauncher = {
        let settingsLauncher = SettingsLauncher(view: self.view);
        settingsLauncher.homeController = self
        return settingsLauncher
    }();
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        VideoService.shared.fetchVideos { (videos) in
            self.videos = videos
            
            self.collectionView.reloadData()
        }
        
        self.navigationController?.navigationBar.isTranslucent = false
        self.navigationController?.navigationBar.autoresizingMask = [UIView.AutoresizingMask.flexibleHeight, UIView.AutoresizingMask.flexibleTopMargin]
        
        let titleLabel = UILabel(frame: CGRect(x: 0, y: 0, width: self.view.frame.width - 32, height: self.view.frame.height))
        titleLabel.textColor = .white
        titleLabel.text = "  Home"
        titleLabel.font = UIFont.systemFont(ofSize: 20)
        
        self.navigationItem.titleView = titleLabel
        
        self.collectionView.backgroundColor = .white
        self.navigationItem.titleView?.autoresizingMask = [UIView.AutoresizingMask.flexibleHeight, UIView.AutoresizingMask.flexibleTopMargin]
        
        self.collectionView.register(VideoCell.self, forCellWithReuseIdentifier: "VideoCell")
        
        self.collectionView.contentInset = UIEdgeInsets(top: 50, left: 0, bottom: 0, right: 0)
        
        collectionView.scrollIndicatorInsets = UIEdgeInsets(top: 50, left: 0, bottom: 0, right: 0)
        
        setupMenuBar()
        setupNavBarButtons()
    }
    
    private func setupNavBarButtons()
    {
        let searchBarButtonItem = UIBarButtonItem.getImageBarButtonItemUsing(imageByName: "magnifyingglass", target: self, selector: #selector(handleSearch))
        
        let moreButtonBarButtonItem =  UIBarButtonItem.getImageBarButtonItemUsing(imageByName: "ellipsis.circle.fill", target: self, selector: #selector(handleMoreButton))
        
        navigationItem.rightBarButtonItems = [moreButtonBarButtonItem, searchBarButtonItem]
    }
    
    @objc func handleMoreButton() {
        self.settingsLauncher.showSettings()
    }
    
    public func showController(settings: Settings) {
        let controller = UIViewController()
        controller.navigationItem.title = settings.name.rawValue
        controller.view.backgroundColor = .white
        
        navigationController?.navigationBar.tintColor = .white
        navigationController?.navigationBar.titleTextAttributes = [NSAttributedString.Key.foregroundColor: UIColor.white]
        
        navigationController?.pushViewController(controller, animated: true)
    }
    
    @objc func handleSearch() {
        let okAction = UIAlertAction(title: "OK", style: .cancel, handler: nil)
        
        let alertController = UIAlertController(title: "Search", message: "Handle of search button", preferredStyle: .actionSheet)
        
        alertController.addAction(okAction)
        
        present(alertController, animated: false)
    }
    
    private func setupMenuBar() {
        navigationController?.hidesBarsOnSwipe = true
        
        let redView = UIView()
        view.addSubview(redView)
        
        redView.backgroundColor = UIColor.rgb(red: 230, green: 32, blue: 31)
        view.addViewConstraints(withVisualFormat: "H:|[v0]|", views: redView)
        view.addViewConstraints(withVisualFormat: "V:[v0(50)]", views: redView)
        
        self.view.addSubview(menuBar)
        
        self.view.addViewConstraints(withVisualFormat: "H:|[v0]|", views: self.menuBar)
        self.view.addViewConstraints(withVisualFormat: "V:[v0(50)]", views: self.menuBar)
        
        NSLayoutConstraint.activate([
            self.menuBar.topAnchor.constraint(equalTo: self.view.safeAreaLayoutGuide.topAnchor)
        ])
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
        
        self.navigationController?.navigationBar.invalidateIntrinsicContentSize()
        
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
