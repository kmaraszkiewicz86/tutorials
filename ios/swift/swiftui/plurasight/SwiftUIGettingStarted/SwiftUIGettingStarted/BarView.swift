//
//  BarView.swift
//  SwiftUIGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 16/10/2020.
//

import SwiftUI

struct BarView: View {
    
    var color = Color.clear
    var width: CGFloat
    var height: CGFloat
    var text: String
    
    var body: some View {
        VStack {
            Rectangle()
                .fill(color)
                .frame(width: width, height: height)
            
            Text(text)
                .rotationEffect(.degrees(-75))
                .offset(y: 20)
        }
    }
}

struct BarView_Previews: PreviewProvider {
    static var previews: some View {
        BarView(color: Color(#colorLiteral(red: 0.4666666687, green: 0.7647058964, blue: 0.2666666806, alpha: 1)), width: 20, height: 100, text: "test")
    }
}
