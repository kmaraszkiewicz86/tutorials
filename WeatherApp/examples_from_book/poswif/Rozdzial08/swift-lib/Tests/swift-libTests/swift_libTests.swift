import XCTest
@testable import swift_lib

class swift_libTests: XCTestCase {
    func testExample() {
        // To jest przykład zestawu testów.
        // Wykorzystaj XCTAssert i powiązane funkcje do upewnienia się, że testy powodują wygenerowanie prawidłowych wyników.
        XCTAssertEqual(swift_lib().text, "Witaj, świecie!")
    }
	
	func testToyDefaultValues() {
		let toy = Toy()
		XCTAssertEqual(toy.name, "Unknown")
		XCTAssertEqual(toy.age, 1)
		XCTAssertEqual(toy.price, 1.0)
	}

	func testToy() {
		let toy = Toy(name: "Rex", age: 2, price:99)
		XCTAssertEqual(toy.name, "Rex")
		XCTAssertEqual(toy.age, 2)
		XCTAssertEqual(toy.price, 99.0)
	}
	// Uaktualnienie dla systemu Linux.
    static var allTests = [
        ("testExample", testExample),
        ("testToyDefaultValues", testToyDefaultValues),
        ("testToy", testToy),
    ]
}
