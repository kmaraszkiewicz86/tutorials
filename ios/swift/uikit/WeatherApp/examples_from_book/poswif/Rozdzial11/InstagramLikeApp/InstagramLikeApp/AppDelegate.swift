//
//  AppDelegate.swift
//  InstagramLikeApp
//
//  Created by Emil Atanasov on 10.02.18.
//  Copyright © 2018 ApposeStudio Inc. All rights reserved.
//

import UIKit
import Firebase
import FirebaseAuthUI

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate, FUIAuthDelegate {

    var window: UIWindow?


    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplicationLaunchOptionsKey: Any]?) -> Bool {
        // Miejsce na przeprowadzenie własnej konfiguracji po uruchomieniu aplikacji.
        FirebaseApp.configure()
        
        let nc = NotificationCenter.default
        nc.addObserver(forName: Notification.Name(rawValue: "userSignedOut"),
                       object: nil, queue: nil) { [weak self]
                        notification in
                        //TODO: Usunięcie przechowywanych informacji o użytkowniku.
                        self?.openSingInScreen()
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

    func applicationWillResignActive(_ application: UIApplication) {
        // Ta metoda jest wywoływana, gdy aplikacja przechodzi ze stanu aktywnego do nieaktywnego. Może być wywołana w określonych sytuacjach (na przykład w chwili odbywania rozmowy telefonicznej lub po otrzymaniu wiadomości SMS) bądź gdy użytkownik kończy działanie aplikacji. Metoda rozpoczyna proces przejścia do stanu pozostania w tle.
        // Tę metodę można wykorzystać do wstrzymania wykonywania bieżących zadań, wyłączenia liczników czasu, unieważnienia wywołań zwrotnych odpowiedzialnych za generowanie grafiki. W grach ta metoda powinna być użyta do wstrzymania gry (pauza).
    }

    func applicationDidEnterBackground(_ application: UIApplication) {
        // Tę metodę należy wykorzystać do zwolnienia zasobów współdzielonych, zapisania danych użytkownika, wyzerowania liczników czasu oraz do przechowania takiej ilości informacji o stanie, która pozwoli na przywrócenie aplikacji do stanu bieżącego.
        // Jeżeli aplikacja obsługuje działanie w tle, w chwili kończenia działania należy wywołać tę metodę zamiast applicationWillTerminate:.
    }

    func applicationWillEnterForeground(_ application: UIApplication) {
        // Ta metoda jest wywoływana podczas przechodzenia aplikacji ze stanu aktywnego do działania w tle. Można więc tutaj wycofać wiele zmian wprowadzonych w chwili przechodzenia do stanu działania w tle.
    }

    func applicationDidBecomeActive(_ application: UIApplication) {
        // W tej metodzie można wznowić działanie zadań zatrzymanych (lub nieuruchomionych), gdy aplikacja była nieaktywna. Jeżeli aplikacja znajdowała się w trybie działania w tle, w metodzie można przeprowadzić odświeżenie interfejsu użytkownika.
    }

    func applicationWillTerminate(_ application: UIApplication) {
        // Ta metoda jest wywoływana, gdy działanie aplikacji ma zostać zakończone. Zapisz dane jeśli zachodzi taka potrzeba. Zapoznaj się również z opisem metody applicationDidEnterBackground:.
    }
    
    //MARK:- Funkcja pomocnicza.
    
    func save(user: User) {
        //TODO: Zapisanie użytkownika w pamięci.
        DataManager.shared.user = user
        DataManager.shared.userUID = user.uid
    }
    
    func openSingInScreen() {
        if let signInViewController = self.window?.rootViewController?.storyboard?.instantiateViewController(withIdentifier: "SignInViewController") as? SignInViewController {
            
            signInViewController.view.frame = (self.window?.rootViewController?.view.frame)!
            signInViewController.view.layoutIfNeeded()
            
            UIView.transition(with: window!, duration: 0.3, options: .transitionCrossDissolve, animations: {
                self.window?.rootViewController = signInViewController
            }, completion: { completed in
                // Brak kodu.
            })
            
        }
    }
    
    func openMainViewController() {
        if let rootViewController = self.window?.rootViewController?.storyboard?.instantiateViewController(withIdentifier: "TabbarViewController") {
            
            rootViewController.view.frame = (self.window?.rootViewController?.view.frame)!
            rootViewController.view.layoutIfNeeded()
            
            UIView.transition(with: window!, duration: 0.3, options: .transitionCrossDissolve, animations: {
                self.window?.rootViewController = rootViewController
            }, completion: { completed in
                // Miejsce na kod dodatkowy.
            })
            
        }
    }
    
    //MARK:- FUIAuthDelegate.
    func authUI(_ authUI: FUIAuth, didSignInWith user: User?, error: Error?) {
        // Niezbędna obsługa użytkownika i błędów.
        if let user = user {
            save(user: user)
            self.openMainViewController()
        }
    }
}

