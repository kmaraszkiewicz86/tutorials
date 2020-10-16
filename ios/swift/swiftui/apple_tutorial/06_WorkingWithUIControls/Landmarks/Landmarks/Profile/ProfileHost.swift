//
//  ProfileHost.swift
//  Landmarks
//
//  Created by Krzysztof Maraszkiewicz on 21/09/2020.
//  Copyright © 2020 Apple. All rights reserved.
//

import SwiftUI

struct ProfileHost: View {
    @State var draftProfile = Profile.default
    
    var body: some View {
        VStack(alignment: .leading, spacing: 20, content: {
            ProfileSummary(profile: draftProfile)
        })
    }
}

struct ProfileHost_Previews: PreviewProvider {
    static var previews: some View {
        ProfileHost()
    }
}
