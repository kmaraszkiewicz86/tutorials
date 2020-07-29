//
//  My_First_iOS_appUITests.swift
//  My First iOS appUITests
//
//  Created by Emil Atanasov on 5/8/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import XCTest

class My_First_iOS_appUITests: XCTestCase {
        
    override func setUp() {
        super.setUp()
        
        // Miejsce na kod konfiguracyjny. Ta metoda jest wykonywana przed wywołaniem poszczególnych metod testowych klasy.
        
        // W testach UI najlepiej zakończyć działanie tuż po niepowodzeniu.
        continueAfterFailure = false
        // Test UI musi uruchomić testowaną aplikację. Jeżeli to nastąpi w tej metodzie, masz gwarancję uruchomienia aplikacji w każdej metodzie testowej.
        XCUIApplication().launch()

        // W testach UI bardzo ważne znaczenie ma stan początkowy - na przykład orientacja interfejsu - wymaganego do przygotowania przed wykonaniem testu. Metoda setUp() to dobre miejsce na taki kod.
    }
    
    override func tearDown() {
        // Miejsce na kod wykonujący operacje porządkowe. Ta metoda jest wykonywana po wywołaniu poszczególnych metod testowych klasy.
        super.tearDown()
    }
    
    func testExample() {
        // Aby rozpocząć tworzenie testów UI, wykorzystaj możliwość nagrywania.
        // Do sprawdzenia poprawności wyniku testu użyj XCTAssert i funkcji powiązanych.
    }
    
}
