//
//  FavoritesTableViewController.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 02/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class FavoritesTableViewController: UITableViewController {

    var favorites:[Location] = []
    let formatter: DateFormatter = DateFormatter()
    
    var filePath: String {
        get {
            let documnetsDirectoryPathString = NSSearchPathForDirectoriesInDomains(.documentDirectory, .userDomainMask, true).first!
            return "\(documnetsDirectoryPathString)/favorites.json"
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

        formatter.dateFormat = "H:mm"
        loadFavorites()
        if favorites.count == 0 {
            var loc = Location.init(city: City.NewYork)
            loc.timeZone = -5 * 3600
            favorites.append(loc)
        }
    }
    
    // MARK: - Table view data source

    override func numberOfSections(in tableView: UITableView) -> Int {
        // #warning Incomplete implementation, return the number of sections
        return 1
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        // #warning Incomplete implementation, return the number of rows
        return favorites.count + 1
    }

    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {

        if indexPath.row < favorites.count {
            
            let cell = tableView.dequeueReusableCell(withIdentifier: "LocationTableViewCell", for: indexPath) as! LocationTableViewCell
            
            let location = favorites[indexPath.row]
            cell.city.text = location.city.name
            cell.temperature.text = "\(location.temperature)°"
            let time = Date()
            formatter.timeZone = TimeZone(secondsFromGMT: location.timeZone)
            
            cell.time.text = formatter.string(from: time)
            
            return cell
        }

        let cell = tableView.dequeueReusableCell(withIdentifier: "AddLocationTableViawCell", for: indexPath) as! AddLocationTableViewCell

        return cell
    }
    
    override func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        if indexPath.row == favorites.count {
            //TODO: tworzenie nowego kontenera
        } else {
            saveFavorites()
        }
        
    }
    
    private func loadFavorites() {
        if FileManager.default.fileExists(atPath: filePath) {
            if let file = FileHandle(forReadingAtPath: filePath) {
                let data = file.readDataToEndOfFile()
                let favs = try? JSONDecoder().decode([Location].self, from: data)
                favorites = favs!
            }
        }
        
    }
    
    private func saveFavorites() {
        let encoded = try? JSONEncoder().encode(favorites)
        
        if !FileManager.default.fileExists(atPath: filePath) {
            FileManager.default.createFile(atPath: filePath, contents: encoded, attributes: nil)
        } else {
            if let file = FileHandle(forWritingAtPath: filePath) {
                file.write(encoded!)
            }
        }
    }
}
