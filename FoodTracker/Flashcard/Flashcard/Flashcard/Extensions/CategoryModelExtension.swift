//
//  CategoryModelExtension.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 18/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import CoreData

///Extension method for [NSManagedObject] array type
extension Array where Element: NSManagedObject {
    
    ///Convert array of NSManagedObject types to array of CategoryModel types
    func toCategoryModelArray () -> [CategoryModel] {
        var items = [CategoryModel]()
        
        for managedObject in self {
            items.append(CategoryModel(
                id: managedObject.value(forKey: "id") as! Int,
                name: managedObject.value(forKey: "name") as! String))
        }
        
        return items
    }
}
