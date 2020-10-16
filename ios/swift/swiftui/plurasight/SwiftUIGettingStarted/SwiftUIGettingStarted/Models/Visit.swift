//
//  Visit.swift
//  SwiftUIGettingStarted
//
//  Created by Krzysztof Maraszkiewicz on 16/10/2020.
//

import Foundation

struct Visit: Identifiable {
  let id = UUID()
  var views: Int
  var events: Int
  var badges: Int
  var actions: Int
  var duration: Int
}
