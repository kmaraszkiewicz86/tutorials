//
//  AppDelegate.swift
//  InstagramLikeApp
//
//  Created by Krzysztof Maraszkiewicz on 31/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit
import Firebase
import FirebaseAuthUI

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate, FUIAuthDelegate {

    var window: UIWindow?

    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        FirebaseApp.configure()
        
        let nc = NotificationCenter.default
        nc.addObserver(forName: Notification.Name(rawValue: "userSignedOut"),
                       object: nil, queue: nil) { [weak self]
                        notification in
                        //TODO: Usunięcie przechowywanych informacji o użytkowniku.
                        self?.openSignInScreen()
        }
        
        // Obsługa logowania zakończonego sukcesem.
        let authUI = FUIAuth.defaultAuthUI()
        authUI?.delegate = self
        
        let user = Auth.auth().currentUser
        if let user = user {
            save(user: user)
            self.openMainViewController()
        }
        return true
    }
    
    func save(user: User) {
        
    }
    
    func openSignInScreen() {
        if let signInViewController = self.window?.rootViewController?.storyboard?.instantiateViewController(withIdentifier: "SignInViewController") as? SignInViewController {
            signInViewController.view.frame = (self.window?.rootViewController?.view.frame)!
            signInViewController.view.layoutIfNeeded()
            UIView.transition(with: window!, duration: 0.3, options: .transitionCrossDissolve, animations: {
                self.window?.rootViewController = signInViewController
            }) { (completion) in
                
            }
        }
    }
    
    func openMainViewController() {
        if let rootViewController = self.window?.rootViewController?.storyboard?.instantiateViewController(withIdentifier: "TabBarController") {
            rootViewController.view.frame = (self.window?.rootViewController?.view.frame)!
            rootViewController.view.layoutIfNeeded()
            UIView.transition(with: window!, duration: 0.3, options: .transitionCrossDissolve, animations: {
                self.window?.rootViewController = rootViewController
            }) { (completed) in
                
            }
        }
    }
    
    //MARK:- FUIAuthDelegate
    func authUI(_ authUI: FUIAuth, didSignInWith user: User?, error: Error?) {
        // Niezbędna obsługa użytkownika i błędów.
        if let user = user {
            save(user: user)
            self.openMainViewController()
        }
    }

    // MARK: UISceneSession Lifecycle

    func application(_ application: UIApplication, configurationForConnecting connectingSceneSession: UISceneSession, options: UIScene.ConnectionOptions) -> UISceneConfiguration {
        // Called when a new scene session is being created.
        // Use this method to select a configuration to create the new scene with.
        return UISceneConfiguration(name: "Default Configuration", sessionRole: connectingSceneSession.role)
    }

    func application(_ application: UIApplication, didDiscardSceneSessions sceneSessions: Set<UISceneSession>) {
        // Called when the user discards a scene session.
        // If any sessions were discarded while the application was not running, this will be called shortly after application:didFinishLaunchingWithOptions.
        // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
    }


}

