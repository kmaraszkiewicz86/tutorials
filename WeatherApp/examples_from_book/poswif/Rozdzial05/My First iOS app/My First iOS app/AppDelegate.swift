//
//  AppDelegate.swift
//  My First iOS app
//
//  Created by Emil Atanasov on 5/8/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import UIKit

@UIApplicationMain
class AppDelegate: UIResponder, UIApplicationDelegate {

    var window: UIWindow?


    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplicationLaunchOptionsKey: Any]?) -> Bool {
        // Miejsce na przeprowadzenie własnej konfiguracji po uruchomieniu aplikacji.
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


}

