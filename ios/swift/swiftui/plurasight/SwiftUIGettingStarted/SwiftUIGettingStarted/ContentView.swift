//
//  ContentView.swift
//  SwiftUIGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 16/10/2020.
//

import SwiftUI

struct ContentView: View {
    var body: some View {
        NavigationView {
            ScrollView(.vertical, showsIndicators: false, content: {
                VStack {
                    ForEach(User.users) {
                        user in
                        NavigationLink(
                            destination: VisitorInfoView(user: user),
                            label: {
                                CardView(user: user)
                            })
                    }
                }
            }).navigationTitle("Quick card")
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
