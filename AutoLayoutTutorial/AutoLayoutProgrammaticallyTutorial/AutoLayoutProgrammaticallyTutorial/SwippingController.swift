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
        btn.addTarget(self, action: #selector(prevBtnAction), for: .touchUpInside)
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
        navigateToPage(indexOfPage: min(pageControll.currentPage + 1, pages.count - 1))
    }
    
    @objc private func prevBtnAction() {
        navigateToPage(indexOfPage: max(pageControll.currentPage - 1, 0))
    }
    
    private func navigateToPage(indexOfPage: Int) {
        let indexPath = IndexPath(item: indexOfPage, section: 0)
        collectionView.scrollToItem(at: indexPath, at: .centeredHorizontally, animated: true)
        
        pageControll.currentPage = indexOfPage
    }
    
    private lazy var pageControll: UIPageControl = {
        let pageControll = UIPageControl()
        pageControll.currentPage = 0
        pageControll.numberOfPages = pages.count
        
        pageControll.currentPageIndicatorTintColor = UIColor.customPink
        pageControll.pageIndicatorTintColor = .gray
        
        return pageControll
    }()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
         collectionView.register(PageCell.self, forCellWithReuseIdentifier: "cellid")
        
        collectionView.backgroundColor = .white
        collectionView.isPagingEnabled = true
        
        setupBottomButtons()
    }
    
    override func viewWillTransition(to size: CGSize, with coordinator: UIViewControllerTransitionCoordinator) {
        
        coordinator.animate(alongsideTransition: { (_) in
            self.collectionViewLayout.invalidateLayout()
            
            
            if self.pageControll.currentPage == 0 {
                self.collectionView.contentOffset = .zero
            } else {
                let indexPath = IndexPath(item: self.pageControll.currentPage, section: 0)
                
                self.collectionView.scrollToItem(at: indexPath, at: .centeredHorizontally, animated: false)
            }
            
        }, completion: nil)
        
        collectionViewLayout.invalidateLayout()
    }
    
    override func scrollViewWillEndDragging(_ scrollView: UIScrollView, withVelocity velocity: CGPoint, targetContentOffset: UnsafeMutablePointer<CGPoint>) {
        let x = targetContentOffset.pointee.x
        
        let indexOfPage = Int(x / scrollView.frame.width)
        
        pageControll.currentPage = indexOfPage
        
        prevButton.setTitleColor(UIColor.gray, for: .disabled)
        nextButton.setTitleColor(UIColor.gray, for: .disabled)
        
        if (indexOfPage > 0) {
            prevButton.setTitleColor(UIColor.customPink, for: .normal)
        }
        
        if (indexOfPage < pages.count - 1) {
            nextButton.setTitleColor(UIColor.customPink, for: .normal)
        }

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
        let controlsStackViewContainer = UIStackView(arrangedSubviews:
            [prevButton, pageControll, nextButton])
        
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
