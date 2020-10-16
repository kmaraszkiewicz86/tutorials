//
//  CalculatorAddTests.swift
//  CalculatorDemoTests
//
//  Created by Krzysztof Maraszkiewicz on 17/10/2020.
//

import XCTest
@testable import CalculatorDemo

class CalculatorAddTests: XCTestCase {

    var calculator: Calculator!
    
    override func setUpWithError() throws {
        self.calculator = Calculator()
    }

    override func tearDownWithError() throws {
        self.calculator = nil
    }

    func testAdd() {
        let result = self.calculator.add(2, 1)
        XCTAssertEqual(result, 3, "Expected 3, but found \(result)")
    }

}
