//
//  PlacesViewController
//  WeatherApp
//
//  Created by Emil Atanasov on 10/5/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import Foundation
import UIKit

public class PlacesViewController: UIViewController, UITableViewDataSource, UITableViewDelegate {
    
    @IBOutlet weak var placesTableView: UITableView!
    
    var countries:[Country] = []
    
    let searchController = UISearchController(searchResultsController: nil)
    
    public override func viewDidLoad() {
        super.viewDidLoad();
        
        countries = Country.getHardcodedData()
        

        // Wyszukiwanie.
        searchController.searchResultsUpdater = self
        searchController.dimsBackgroundDuringPresentation = false
        definesPresentationContext = true
        placesTableView.tableHeaderView = searchController.searchBar
        
        placesTableView.dataSource = self
        placesTableView.delegate = self
        // Przewinięcie i ukrycie wyszukiwania.
        placesTableView.tableHeaderView = searchController.searchBar
        placesTableView.contentOffset = CGPoint(x: 0, y: searchController.searchBar.frame.size.height)


    }
    
    // MARK: metody UITableViewDataSource.
    
    public func numberOfSections(in tableView: UITableView) -> Int {
        return countries.count
    }
    
    public func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return countries[section].cities.count
    }
    
    public func tableView(_ tableView: UITableView, titleForHeaderInSection section: Int) -> String? {
        return countries[section].name
    }
    
    public func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell:PlacesViewCell = tableView.dequeueReusableCell(withIdentifier: "LocationDetailsCell", for: indexPath) as! PlacesViewCell
        
        let country = self.countries[indexPath.section]
        let city = country.cities[indexPath.row]
        
        cell.city.text = city.name
        
        return cell

    }
    
    // MARK: UITableViewDelegate
    // Procedura obsługi dotknięcia.
    public func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        print("item: \(indexPath.row)" )
    }
    
}
// Lepsze podejście w zakresie implementacji interfejsu.
extension PlacesViewController: UISearchResultsUpdating {
    // MARK: - Delegat UISearchResultsUpdating.
    public func updateSearchResults(for searchController: UISearchController) {
        let searchText = searchController.searchBar.text!.localizedLowercase
        
        if searchText.characters.count > 0 {
            var filteredCountries:[Country] = []
            
            for country in countries {
                if let filteredCountry = filteredCities(in: country, searchText: searchText) {
                    filteredCountries.append(filteredCountry)
                }
                
            }
            
            countries = filteredCountries
        } else {
            countries = Country.getHardcodedData()
        }
        
        
        placesTableView.reloadData()
    }
    // Funkcja pomocnicza do prawidłowego filtrowania.
    private func filteredCities(in country:Country, searchText:String) -> Country? {
        
        let c = Country(name: country.name)
        
        c.cities = country.cities.filter {
            city in
            return city.name.localizedLowercase.contains(searchText)
        }
        
        return c.cities.count > 0 ? c : nil
        
    }
}
