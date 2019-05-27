//
//  NSManagedObjectExtension.swift
//  ActivityTimer
//
//  Created by Krzysztof Maraszkiewicz on 27/05/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import CoreData

extension NSManagedObject {
    
    func toActivityModel () -> ActivityModel {
        return ActivityModel(id: objectID.uriRepresentation(),
            name: self.value(forKey: "name") as! String)
    }
}
