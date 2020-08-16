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
    
    public var homeController : HomeController?
    
    private var view: UIView?
    private var collectionViewConstraints: [NSLayoutConstraint]?
    private var blackViewConstraints: [NSLayoutConstraint]?
    
    private let settingCellHeight : CGFloat = 50
    
    let settings : [Settings] = {
        
        var setting1 = Settings(name: .settings, iconName: "gear")
        var setting2 = Settings(name: .account, iconName: "person")
        var setting3 = Settings(name: .help, iconName: "questionmark")
        var setting4 = Settings(name: .cancel, iconName: "clear")
        
        return [setting1, setting2, setting3, setting4]
    }();
    
    lazy var collectionView: UICollectionView = {
        
        let layout = UICollectionViewFlowLayout()
        var cv = UICollectionView(frame: .zero, collectionViewLayout: layout)
        
        cv.delegate = self
        cv.dataSource = self
        
        cv.translatesAutoresizingMaskIntoConstraints = false
        cv.backgroundColor = .white
        
        return cv
    }();
    
    init(view: UIView?) {
        super.init()
        
        self.view = view
        
        self.collectionView.register(SettingCollectionViewCell.self, forCellWithReuseIdentifier: "SettingCollectionViewCell")
    }
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return settings.count
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "SettingCollectionViewCell", for: indexPath) as! SettingCollectionViewCell
        
        cell.settings = settings[indexPath.item]
        
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath:  IndexPath) -> CGSize {
        
        return CGSize(width: self.view!.frame.width, height: self.settingCellHeight)
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAt section: Int) -> CGFloat {
        return 0
    }
    
    func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        
        dismissMoreButton() {
            (completed: Bool) in
            
            if self.settings[indexPath.row].name == .cancel {
                self.dissmisAction()
                return
            }
            
            self.homeController?.showController(settings: self.settings[indexPath.row])
        }
    }
    
    func showSettings() {
        
        blackView.backgroundColor = UIColor(white: 0, alpha: 0.5)
        blackView.translatesAutoresizingMaskIntoConstraints = false
        blackView.alpha = 0
        
        blackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(dismissMoreButtonAction)))
        
        view?.addSubview(blackView)
            
        view?.addSubview(collectionView)
        
        
        collectionViewConstraints = [
            collectionView.bottomAnchor.constraint(equalTo: view!.bottomAnchor),
            collectionView.leadingAnchor.constraint(equalTo: view!.leadingAnchor),
            collectionView.trailingAnchor.constraint(equalTo: view!.trailingAnchor),
            collectionView.heightAnchor.constraint(equalToConstant: CGFloat(settings.count) * self.settingCellHeight)
        ]
        
        let height: CGFloat = 200
        let y = view!.frame.height - height
        self.collectionView.frame = CGRect(x: 0, y: view!.frame.height, width: self.collectionView.frame.width, height: height)
        
        blackViewConstraints = [
            blackView.topAnchor.constraint(equalTo: self.view!.safeAreaLayoutGuide.topAnchor),
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
    
    @objc func dismissMoreButtonAction() {
        dismissMoreButton(nil)
    }
    
    func dismissMoreButton(_ completion: ((Bool) -> Void)?) {
        
        UIView.animate(withDuration: 0.5, delay: 0, usingSpringWithDamping: 1, initialSpringVelocity: 1, options: .curveEaseOut, animations: {
            self.dissmisAction();
            
        }, completion: completion)
    }
    
    private func dissmisAction() {
        self.blackView.alpha = 0
        
        self.collectionView.frame = CGRect(x: 0, y: self.view!.frame.height, width: self.view!.frame.width, height: 0)
        
        self.collectionView.removeConstraints(self.collectionViewConstraints!)
        self.blackView.removeConstraints(self.blackViewConstraints!)
    }
}
