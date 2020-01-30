//
//  CreatePostViewController.swift
//  InstagramLikeApp
//
//  Created by Emil Atanasov on 3.03.18.
//  Copyright © 2018 ApposeStudio Inc. All rights reserved.
//

import UIKit

class CreatePostViewController: UIViewController {
    
    override var prefersStatusBarHidden: Bool { return true }

    private let placeholder = "Write a caption..."
    
    public var image:UIImage?
    
    @IBOutlet weak var photo: UIImageView!
    
    @IBOutlet weak var textView: UITextView! {
        didSet {
            textView.textColor = UIColor.gray
            textView.text = placeholder
            textView.selectedRange = NSRange(location: 0, length: 0)
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        textView.delegate = self
        photo.image = image
        
        // Dodanie przycisku Udostępnij.
        
        navigationItem.rightBarButtonItem = UIBarButtonItem(title: "Udostępnij",
                                                            style: .done,
                                                            target: self,
                                                            action: #selector(createPost))
    }
    
    @objc func createPost() {
        guard let image = self.image else {
            return
        }
        
        let description = (textView.text != placeholder ? textView.text : "") ?? ""
        var post = PostModel(description: description, author: DataManager.shared.userUID ?? "Brak id użytkownika." )
        
        DataManager.shared.createPost(post: post, image: image, progress: { (progress) in
            print("Przekazywanie danych \(progress)")
        }) { (success) in
            if success {
                print("Przekazanie danych zakończyło się sukcesem.")
            } else {
                print("Nie udało się utworzyć postu.")
            }
            self.dismiss(animated: true, completion: nil)
        }
    }
}

extension CreatePostViewController: UITextViewDelegate {
    
    func textViewDidChangeSelection(_ textView: UITextView) {
        // Przesunięcie kursora na początek.
        if textView.text == placeholder {
            textView.selectedRange = NSRange(location: 0, length: 0)
        }
    }
    
    func textView(_ textView: UITextView, shouldChangeTextIn range: NSRange, replacementText text: String) -> Bool {
        // Jeżeli cokolwiek zostało wpisane.
        if textView.text == placeholder && !text.isEmpty {
            textView.text = nil
            textView.textColor = UIColor.black
            textView.selectedRange = NSRange(location: 0, length: 0)
        }
        return true
    }
    
    func textViewDidChange(_ textView: UITextView) {
        // Jeżeli pole nie zawiera tekstu.
        if textView.text.isEmpty {
            textView.textColor = UIColor.lightGray
            textView.text = placeholder
        }
    }
}
