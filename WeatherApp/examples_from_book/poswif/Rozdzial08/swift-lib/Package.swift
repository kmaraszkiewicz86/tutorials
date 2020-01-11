// swift-tools-version:4.0
// Element swift-tools-version deklaruje minimalną wersję Swift wymaganą do kompilacji tego pakietu.

import PackageDescription

let package = Package(
    name: "swift-lib",
    products: [
        // Tutaj są zdefiniowane pliki wykonywalne i biblioteki wygenerowane przez pakiet. Ponadto stają się widoczne dla innych pakietów.
        .library(
            name: "swift-lib",
            targets: ["swift-lib"]),
    ],
    dependencies: [
        // Zależności deklarują inne pakiety zależne dla danego pakietu.
        // .package(url: /* package url */, from: "1.0.0"),
    ],
    targets: [
        // Cele to podstawowe elementy konstrukcyjne pakietu. Cel może definiować moduł lub pakiet test. 
        // Cel może być zależny od innych celów w pakiecie, a także od produktów w pakietach, od których zależy dany pakiet.
        .target(
            name: "swift-lib",
            dependencies: []),
        .testTarget(
            name: "swift-libTests",
            dependencies: ["swift-lib"]),
    ]
)
