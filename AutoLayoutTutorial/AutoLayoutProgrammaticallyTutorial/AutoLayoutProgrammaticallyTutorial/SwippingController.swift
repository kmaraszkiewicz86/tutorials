//
//  SwippingController.swift
//  AutoLayoutProgrammaticallyTutorial
//
//  Created by Krzysztof Maraszkiewicz on 30/07/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class SwippingController: UICollectionViewController, UICollectionViewDelegateFlowLayout {
    
    private let pages = [
        Page(image: UIImage(named: "krowa")!, headerText: "To jest testowy nagłówek 1!", bodyText: "To jest test testów, tej testowej aplikacji, którą tworze dla testowego wyniku"),
        Page(image: UIImage(named: "Pierdzioch")!, headerText: "To jest testowy nagłówek 2!", bodyText: "Tworze dla testowego wyniku"),
        Page(image: UIImage(named: "rycka")!, headerText: "To jest testowy nagłówek 3!", bodyText: "To jest test testów, tej testowej aplikacji")
    ]
    
    private let prevButton: UIButton = {
        let btn = UIButton(type: .system)
        btn.setTitle("NEXT", for: .normal)
        btn.titleLabel?.font = UIFont.boldSystemFont(ofSize: 14)
        btn.setTitleColor(.gray, for: .normal)
        btn.translatesAutoresizingMaskIntoConstraints = false
        
        return btn
    }()
    
    private let nextButton: UIButton = {
        let btn = UIButton(type: .system)
        btn.setTitle("PREV", for: .normal)
        btn.translatesAutoresizingMaskIntoConstraints = false
        btn.titleLabel?.font = UIFont.boldSystemFont(ofSize: 14)
        btn.addTarget(self, action: #selector(nextBtnAction), for: .touchUpInside)
        
        btn.setTitleColor(UIColor.customPink, for: .normal)
        
        return btn
    }()
    
    @objc private func nextBtnAction () {
    
    }
    
    private let pageControll: UIPageControl = {
        let pageControll = UIPageControl()
        pageControll.currentPage = 0
        pageControll.numberOfPages = 4
        
        pageControll.currentPageIndicatorTintColor = UIColor.customPink
        pageControll.pageIndicatorTintColor = .gray
        
        return pageControll
    }()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        collectionView.backgroundColor = .white
        collectionView.register(PageCell.self, forCellWithReuseIdentifier: "cellid")
        
        collectionView.isPagingEnabled = true
        
        setupBottomButtons()
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
    
    private func setupBottomButtons() {
        
        let greenView = UIView()
        greenView.backgroundColor = .green
        
        let controlsStackViewContainer = UIStackView(arrangedSubviews: [prevButton,
                                                                        pageControll, nextButton])
        
        controlsStackViewContainer.translatesAutoresizingMaskIntoConstraints = false
        
        controlsStackViewContainer.distribution = .fillEqually
        
        
        view.addSubview(controlsStackViewContainer)
        
        NSLayoutConstraint.activate([
            controlsStackViewContainer.bottomAnchor.constraint(equalTo: view.safeAreaLayoutGuide.bottomAnchor),
            controlsStackViewContainer.leadingAnchor.constraint(equalTo: view.safeAreaLayoutGuide.leadingAnchor),
            controlsStackViewContainer.trailingAnchor.constraint(equalTo: view.safeAreaLayoutGuide.trailingAnchor),
            controlsStackViewContainer.heightAnchor.constraint(equalToConstant: 50)
            
            ])
        
    }
}
