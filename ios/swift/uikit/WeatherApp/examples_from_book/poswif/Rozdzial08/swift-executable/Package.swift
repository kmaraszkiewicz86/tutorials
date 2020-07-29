// swift-tools-version:4.0
// Element swift-tools-version deklaruje minimalną wersję Swift wymaganą do kompilacji tego pakietu.

import PackageDescription

let package = Package(
    name: "swift-executable",
    dependencies: [
        // Zależności deklarują inne pakiety zależne dla danego pakietu.
        .package(url:"../swift-lib/", from: "1.0.0"),
    ],
    targets: [
        // Cele to podstawowe elementy konstrukcyjne pakietu. Cel może definiować moduł lub pakiet test. 
        // Cel może być zależny od innych celów w pakiecie, a także od produktów w pakietach, od których zależy dany pakiet.
        .target(
            name: "swift-executable",
            dependencies: ["swift-lib"]),
    ]
)
