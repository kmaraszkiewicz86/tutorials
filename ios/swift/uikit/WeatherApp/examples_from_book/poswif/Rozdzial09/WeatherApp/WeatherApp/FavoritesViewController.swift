//
//  FavoritesViewController.swift
//  WeatherApp
//
//  Created by Emil Atanasov on 9/25/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import Foundation
import UIKit

public class FavoritesViewController: UIViewController, UITableViewDataSource, UITableViewDelegate {
    
    @IBOutlet weak var favoritesTableView: UITableView!
    
    public var receivedData:Int?
    
    var favorites:[Location] = []
    
    public var selectedItem:Location?
    
    public override func viewDidLoad() {
        super.viewDidLoad();
        
        if receivedData != nil {
            print(receivedData)
        }
        
        loadFavorites()
        
        if favorites.count == 0 {
            // Domyślną lokalizacją jest Nowy Jork.
            var loc = Location.init(city: City.NewYork)
            // W przypadku strefy czasowej DST wartością loc.timeZone powinno być -4 * 3600.
            loc.timeZone = -5 * 3600
            favorites.append(loc)
        }
        
        favoritesTableView.dataSource = self
        favoritesTableView.delegate = self
    }
    
//    public override func viewDidDisappear(_ animated: Bool) {
//        
//        saveFavorites(favorites: favorites)
//    }
    
    // MARK: metody UITableViewDataSource.
    
    public func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        // Wartość 1 dla ostatniej komórki specjalnej.
        return favorites.count + 1
    }
    
    public func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let index = indexPath.row
        
        if index < favorites.count {
            
            let location = favorites[index]
            
            let cell:FavoriteViewCell = tableView.dequeueReusableCell(withIdentifier: "LocationCell", for: indexPath) as! FavoriteViewCell
            
            cell.city.text = location.name
            
            cell.temperature.text =  location.temperature + LocationForecast.degreeSymbol
            
            let date = Date()
            print("Bieżąca data: \(date)")
            let formatter = DateFormatter()
            formatter.dateFormat = "H:mm"
            formatter.timeZone = TimeZone(secondsFromGMT: location.timeZone)
            print("Data w NY: \(formatter.string(from: date))")
            cell.time.text = formatter.string(from: date)
        
            return cell
            
        }
        // Ostatnia komórka jest statyczna.
        let cell:StaticViewCell = tableView.dequeueReusableCell(withIdentifier: "AddLocationCell", for: indexPath) as! StaticViewCell
        
        return cell
    }
    
    // MARK: UITableViewDelegate
    // Procedura obsługi dotknięcia.
    public func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if indexPath.row == favorites.count {
            //TODO: otworzenie nowego kontrolera.
        } else {
            selectedItem = favorites[indexPath.row]
            //TODO: wybór lokalizacji i zapisanie wszystkich.
            print("Lokalizacja: \(favorites[indexPath.row].name)")
            saveFavorites(favorites: favorites)
        }
    }
    
    // MARK: zapisywanie ulubionych lokalizacji.
    
    func saveFavorites(favorites:[Location]) {
        //TODO: ... rzeczywista operacja zapisu.
        
        let encoded = try? JSONEncoder().encode(favorites)
        let documentsDirectoryPathString = NSSearchPathForDirectoriesInDomains(.documentDirectory, .userDomainMask, true).first!
        let filePath = documentsDirectoryPathString + "/favorites.json"
        
        if !FileManager.default.fileExists(atPath: filePath) {
            FileManager.default.createFile(atPath: filePath, contents: encoded, attributes: nil)
        } else {
            if let file = FileHandle(forWritingAtPath:filePath) {
                file.write(encoded!)
            }
        }
    }
    
    func loadFavorites() {
        let documentsDirectoryPathString = NSSearchPathForDirectoriesInDomains(.documentDirectory, .userDomainMask, true).first!
        let filePath = documentsDirectoryPathString + "/favorites.json"
        
        if FileManager.default.fileExists(atPath: filePath) {
            if let file = FileHandle(forReadingAtPath:filePath) {
                let data = file.readDataToEndOfFile()
                
                let favs = try? JSONDecoder().decode([Location].self, from: data)
                favorites = favs!
            }
        }
    }
    
}
