//
//  VisitorInfoView.swift
//  SwiftUIGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 16/10/2020.
//

import SwiftUI

struct VisitorInfoView: View {
    
    @State var user: User
    @State var isChartHidden = true
    
    var body: some View {
        VStack {
            
            CardView(user: user)
                .gesture(TapGesture()
                            .onEnded {
                                withAnimation(.easeInOut) {
                                    isChartHidden.toggle()
                                }
                            })
            
            
            if !isChartHidden {
                GeometryReader { 
                    proxy in
                    HStack(alignment: .bottom) {
                        BarView(color: Color(#colorLiteral(red: 0.4666666687, green: 0.7647058964, blue: 0.2666666806, alpha: 1)), width: proxy.size.width * 0.16,
                                height: CGFloat(self.user.visit.views), text: "Views")
                        
                        BarView(color: Color(#colorLiteral(red: 0.4666666687, green: 0.7647058964, blue: 0.2666666806, alpha: 1)), width: proxy.size.width * 0.16,
                                height: CGFloat(self.user.visit.events), text: "Events")
                        
                        BarView(color: Color(#colorLiteral(red: 0.4666666687, green: 0.7647058964, blue: 0.2666666806, alpha: 1)), width: proxy.size.width * 0.16,
                                height: CGFloat(self.user.visit.badges), text: "Badges")
                        
                        BarView(color: Color(#colorLiteral(red: 0.4666666687, green: 0.7647058964, blue: 0.2666666806, alpha: 1)), width: proxy.size.width * 0.16,
                                height: CGFloat(self.user.visit.actions), text: "Actions")
                        
                        BarView(color: Color(#colorLiteral(red: 0.4666666687, green: 0.7647058964, blue: 0.2666666806, alpha: 1)), width: proxy.size.width * 0.16,
                                height: CGFloat(self.user.visit.duration), text: "Durations")
                    }.frame(height: 0.5 * proxy.size.height)
                }.transition(.move(edge: .bottom   ))
            }
        }
    }
}

struct VisitorInfoView_Previews: PreviewProvider {
    static var previews: some View {
        VisitorInfoView(user: User(firstName: "Krzysztof", lastName: "Maraszkiewicz", title: "Developer", email: "kmaraszkiewicz86@gmail.com", companyUrl: "kmaraszkiewicz86.pl", address: "62-002 Suchy Las", visit: Visit(views: 20, events: 100, badges: 30, actions: 40, duration: 80)))
    }
}
