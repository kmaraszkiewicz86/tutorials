//
//  DataManager.swift
//  InstagramLikeApp
//
//  Created by Emil Atanasov on 5.03.18.
//  Copyright © 2018 ApposeStudio Inc. All rights reserved.
//

import Foundation
import Firebase
import FirebaseStorage

class PostModel {
    var photoURL:String?
    var description:String
    var author:String
    
    init(description: String, author:String) {
        self.photoURL = nil
        self.description = description
        self.author = author
    }
    
    init(photoURL: String, description: String, author:String) {
        self.photoURL = photoURL
        self.description = description
        self.author = author
    }
    
    var toDict:[String:String] {
        var dict:[String:String] = [:]
        
        dict["description"] = description
        dict["author"] = author
        if let photoURL = self.photoURL {
            dict["photo"] = photoURL
        }
        
        return dict
    }
}

final class DataManager {
    // Konstruktor prywatny.
    private init() {
        databaseRef = Database.database().reference()
    }
    // Pojedynczy egzemplarz.
    static let shared = DataManager()
    
    var databaseRef: DatabaseReference!
    var userUID: String?
    var user: User?
    
    
    func createPost(post: PostModel, callback: @escaping (Bool) -> ()) {
        if let userID = userUID {
            let key = databaseRef.child("posts").childByAutoId().key
            let postData = post.toDict
            let childUpdates = ["/posts/\(key)": postData,
                                "/myposts/\(userID)/\(key)/": postData]
            databaseRef.updateChildValues(childUpdates)
            
            callback(true)
        } else {
            callback(false)
        }
    
    }
    
    func createPost(post:PostModel, image:UIImage, progress: @escaping (Double)->(),callback: @escaping (Bool) -> () ) {
        
        guard let userID = userUID else {
            callback(false)
            return
        }
        // Klucz dla danych.
        let key = databaseRef.child("posts").childByAutoId().key

        let storageRef = Storage.storage().reference()
        // Położenie obrazu konkretnego postu.
        let photoPath = "posts/\(userID)/\(key)/photo.jpg"
        let imageRef = storageRef.child(photoPath)
        
        // Utworzenie metadanych pliku łącznie z typem treści.
        let metadata = StorageMetadata()
        metadata.contentType = "image/jpeg"
        metadata.customMetadata = ["userId": userID]
        
        
        let data = UIImageJPEGRepresentation(image, 0.9)
        // Przekazanie danych i metadanych.
        let uploadTask = imageRef.putData(data!, metadata: metadata)
        
        uploadTask.observe(.progress) { snapshot in
            // Otrzymane informacje o postępie przekazywania danych.
            let complete = 100.0 * Double(snapshot.progress!.completedUnitCount)
                / Double(snapshot.progress!.totalUnitCount)
            progress(complete)
        }

        uploadTask.observe(.success) { [unowned uploadTask, weak self] snapshot in
            // Przekazanie danych zakończyło się sukcesem.
            uploadTask.removeAllObservers()
            
            
            var postData = post.toDict
            postData["photo"] = photoPath
            let childUpdates = ["/posts/\(key)": postData,
                                "/myposts/\(userID)/\(key)/": postData]
            self?.databaseRef.updateChildValues(childUpdates)
            
            callback(true)
        }
        
        uploadTask.observe(.failure) { [unowned uploadTask] snapshot in
            uploadTask.removeAllObservers()
            callback(false)
            if let error = snapshot.error as NSError? {
                switch (StorageErrorCode(rawValue: error.code)!) {
                case .objectNotFound:
                    // Plik nie istnieje.
                    print("Obiekt nie został znaleziony.")
                    break
                case .unauthorized:
                    // Użytkownik nie ma uprawnień dostępu do pliku.
                    print("Użytkownik nie ma wystarczających uprawnień.")
                    break
                case .cancelled:
                    // Użytkownik przerwał operację przekazywania pliku.
                    print("Operacja przekazywania pliku została przerwana.")
                    break
                    
                    /* ... */
                    
                case .unknown:
                    // Wystąpił nieznany błąd, należy sprawdzić odpowiedź udzieloną przez serwer.
                    break
                default:
                    // Wystąpił inny błąd. To jest dobre miejsce na ponowne próby przekazania pliku.
                    break
                }
            }
        }
    }
}
