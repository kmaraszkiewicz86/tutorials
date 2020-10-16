//
//  ImageLabelView.swift
//  SwiftUIGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 16/10/2020.
//

import SwiftUI

struct ImageLabelView: View {
    
    var imageName: String
    @Binding var text: String
    
    var body: some View {
        HStack {
            Image(systemName: imageName)
                .foregroundColor(.orange)
            Text(text)
        }
    }
}

struct ImageLabelView_Previews: PreviewProvider {
    static var previews: some View {
        ImageLabelView(imageName: "envelope.fill", text: .constant("kmaraszkiewicz86@gmail.com"))
    }
}
