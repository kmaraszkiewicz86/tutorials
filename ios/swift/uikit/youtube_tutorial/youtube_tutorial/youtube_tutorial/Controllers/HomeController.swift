//
//  HomeController.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 20/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class HomeController: UICollectionViewController, UICollectionViewDelegateFlowLayout {
    
    let cellId = "cellId"
    let trendingCellId = "trendingCellId"
    let subscriptionsCellId = "subscriptionsCellId"
    
    private let titles = ["Home", "Trending", "Subscriptions", "Account"]
    
    private lazy var menuBar: MenuBar = {
        let mb = MenuBar()
        
        mb.homeController = self
        
        return mb
    }()
    
    private lazy var settingsLauncher : SettingsLauncher = {
        let settingsLauncher = SettingsLauncher(view: self.view);
        settingsLauncher.homeController = self
        return settingsLauncher
    }();
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.navigationController?.navigationBar.isTranslucent = false
        self.navigationController?.navigationBar.autoresizingMask = [UIView.AutoresizingMask.flexibleHeight, UIView.AutoresizingMask.flexibleTopMargin]
        
        let titleLabel = UILabel(frame: CGRect(x: 0, y: 0, width: self.view.frame.width - 32, height: self.view.frame.height))
        titleLabel.textColor = .white
        titleLabel.text = "  Home"
        titleLabel.font = UIFont.systemFont(ofSize: 20)
        
        self.navigationItem.titleView = titleLabel
        self.navigationItem.titleView?.autoresizingMask = [UIView.AutoresizingMask.flexibleHeight, UIView.AutoresizingMask.flexibleTopMargin]
        
        setupCollectionView()
        setupMenuBar()
        setupNavBarButtons()
    }
    
    func setupCollectionView() {
        
        if let flowLayout = collectionView.collectionViewLayout as? UICollectionViewFlowLayout {
            flowLayout.scrollDirection = .horizontal
            flowLayout.minimumLineSpacing = 0
        }
        
        self.collectionView.register(FeedCell.self, forCellWithReuseIdentifier: cellId)
        self.collectionView.register(TrendingCell.self, forCellWithReuseIdentifier: trendingCellId)
        self.collectionView.register(SubscriptionsCell.self, forCellWithReuseIdentifier: subscriptionsCellId)
        
        self.collectionView.backgroundColor = .white
        
        self.collectionView.contentInset = UIEdgeInsets(top: 50, left: 0, bottom: 0, right: 0)
        self.collectionView.scrollIndicatorInsets = UIEdgeInsets(top: 50, left: 0, bottom: 0, right: 0)
        
        self.collectionView.isPagingEnabled = true
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
    
    func changeTitleText (titleTextIndex: Int) {
        if let titleLabel = navigationItem.titleView as? UILabel {
            titleLabel.text = "  \(titles[titleTextIndex])"
        }
    }
    
    func scrollToMenuIndex(menuIndex: Int) {
        let indexPath = IndexPath(item: menuIndex, section: 0)
        collectionView.scrollToItem(at: indexPath, at: UICollectionView.ScrollPosition.init(), animated: true)
    }
    
    @objc func handleSearch() {
        
        scrollToMenuIndex(menuIndex: 2)
        
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
    
    override func scrollViewWillEndDragging(_ scrollView: UIScrollView, withVelocity velocity: CGPoint, targetContentOffset: UnsafeMutablePointer<CGPoint>) {
        let index = Int(targetContentOffset.pointee.x / view.frame.width)
        let indexPath = IndexPath(item: index, section: 0)
        
        menuBar.colectionView.selectItem(at: indexPath, animated: true, scrollPosition: UICollectionView.ScrollPosition.init())
        
        changeTitleText(titleTextIndex: index)
    }
    
    override func scrollViewDidScroll(_ scrollView: UIScrollView) {
        menuBar.horizontalBarLeftAnchorContraint?.constant = scrollView.contentOffset.x / 4
    }
    
    override func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return 4
    }
    
    override func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        
        var cellIdTmp = cellId
        
        if indexPath.row == 1 {
            cellIdTmp = trendingCellId
        } else if indexPath.row == 2 {
            cellIdTmp = subscriptionsCellId
        }
        
        return collectionView.dequeueReusableCell(withReuseIdentifier: cellIdTmp, for: indexPath)
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath: IndexPath) -> CGSize {
        return CGSize(width: view.frame.width, height: view.frame.height - 50)
    }
    
    override func viewWillTransition(to size: CGSize, with coordinator: UIViewControllerTransitionCoordinator) {
        super.viewWillLayoutSubviews()
        
        guard let flowLayout = collectionView.collectionViewLayout as? UICollectionViewFlowLayout else {
            return
        }
        
        flowLayout.invalidateLayout()
    }
}
