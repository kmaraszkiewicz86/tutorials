//
//  SceneDelegate.swift
//  youtube_tutorial
//
//  Created by Krzysztof Maraszkiewicz on 20/07/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class SceneDelegate: UIResponder, UIWindowSceneDelegate {

    var window: UIWindow?


    func scene(_ scene: UIScene, willConnectTo session: UISceneSession, options connectionOptions: UIScene.ConnectionOptions) {
        
        guard let windoScene = (scene as? UIWindowScene) else { return }
        
        self.window = self.getRootController(windoScene);
        
        setCustomStatusBarBackground()
    }
    
    private func getRootController(_ windoScene: UIWindowScene) -> UIWindow {
        let window = UIWindow(windowScene: windoScene)
        
        let layout = UICollectionViewFlowLayout()
        
        window.rootViewController = UINavigationController(rootViewController: HomeController(collectionViewLayout: layout));
        window.makeKeyAndVisible();
        
        return window;
    }
    
    private func setCustomStatusBarBackground() {
        let statusBackgroundView = UIView()
        statusBackgroundView.backgroundColor = UIColor.rgb(red: 194, green: 31, blue: 31)
        
        self.window?.addSubview(statusBackgroundView)
        self.window?.addViewConstraints(withVisualFormat: "H:|[v0]|", views: statusBackgroundView)
        
        self.window?.addViewConstraints(withVisualFormat: "V:|[v0(20)]", views: statusBackgroundView)
    }

    func sceneDidDisconnect(_ scene: UIScene) {
        // Called as the scene is being released by the system.
        // This occurs shortly after the scene enters the background, or when its session is discarded.
        // Release any resources associated with this scene that can be re-created the next time the scene connects.
        // The scene may re-connect later, as its session was not neccessarily discarded (see `application:didDiscardSceneSessions` instead).
    }

    func sceneDidBecomeActive(_ scene: UIScene) {
        // Called when the scene has moved from an inactive state to an active state.
        // Use this method to restart any tasks that were paused (or not yet started) when the scene was inactive.
    }

    func sceneWillResignActive(_ scene: UIScene) {
        // Called when the scene will move from an active state to an inactive state.
        // This may occur due to temporary interruptions (ex. an incoming phone call).
    }

    func sceneWillEnterForeground(_ scene: UIScene) {
        // Called as the scene transitions from the background to the foreground.
        // Use this method to undo the changes made on entering the background.
    }

    func sceneDidEnterBackground(_ scene: UIScene) {
        // Called as the scene transitions from the foreground to the background.
        // Use this method to save data, release shared resources, and store enough scene-specific state information
        // to restore the scene back to its current state.
    }


}

