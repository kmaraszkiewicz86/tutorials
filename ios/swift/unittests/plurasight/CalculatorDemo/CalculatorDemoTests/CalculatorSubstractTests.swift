//
//  CalculatorSubstractTests.swift
//  CalculatorDemoTests
//
//  Created by Krzysztof Maraszkiewicz on 17/10/2020.
//

import XCTest
@testable import CalculatorDemo

class CalculatorSubstractTests: XCTestCase {

    var calculator: Calculator!
    
    override func setUpWithError() throws {
        self.calculator = Calculator()
    }

    override func tearDownWithError() throws {
        self.calculator = nil
    }
    
    func testSubtract() {
        let result = self.calculator.subtract(10, 1)
        XCTAssertEqual(result, 9, "Expected 9, but found \(result)")
    }

}
