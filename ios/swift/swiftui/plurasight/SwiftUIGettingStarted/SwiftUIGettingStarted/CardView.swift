//
//  CardView.swift
//  SwiftUIGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 16/10/2020.
//

import SwiftUI

struct CardView: View {
    
    @State var user: User
    
    var body: some View {
        ZStack {
            
            Rectangle()
                .fill(Color("CardBackground"))
                .frame(width: 300, height: 200)
                .cornerRadius(20)
                .shadow(radius: 10)
            
            HStack {
                VStack(alignment: .leading) {
                    Text("\(user.firstName) \(user.lastName)")
                        .font(.title)
                        .fixedSize(horizontal: false, vertical: true)
                    Text("\(user.title )")
                        .italic()
                    
                    Spacer()
                    
                    ImageLabelView(imageName: "envelope.fill", text: $user.email)
                    
                    ImageLabelView(imageName: "link", text: $user.companyUrl)
                    
                    ImageLabelView(imageName: "location.fill", text: $user.address)
                }
                .padding()
                .foregroundColor(.white)
                
                Spacer()
            }
        }.frame(width: 300, height: 200)
    }
}

struct CardView_Previews: PreviewProvider {
    static var previews: some View {
        Group {
            CardView(user: User(firstName: "Krzysztof", lastName: "Maraszkiewicz", title: "Developer", email: "kmaraszkiewicz86@gmail.com", companyUrl: "kmaraszkiewicz86.pl", address: "62-002 Suchy Las", visit: Visit(views: 20, events: 100, badges: 30, actions: 40, duration: 80)))
        }
    }
}
