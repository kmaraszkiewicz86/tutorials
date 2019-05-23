//
//  CategoryModel.swift
//  Flashcard
//
//  Created by Krzysztof Maraszkiewicz on 12/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

/**
    Category model class.
*/
class CategoryModel: Decodable {
    
    /** The identifier */
    var id: Int
    
    /** The category name. */
    var name: String
    
    /**
     - parameter id: the identifier.
     - parameter name: The category name.
     */
    init (id: Int, name: String) {
        self.id = id
        self.name = name
    }
}
