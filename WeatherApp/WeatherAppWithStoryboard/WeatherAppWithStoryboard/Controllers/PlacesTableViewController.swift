//
//  PlacesTableViewController.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 12/01/2020.
//  Copyright © 2020 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class PlacesTableViewController: UITableViewController {

    var countries:[Country] = []
    
    let searchController = UISearchController(searchResultsController: nil)
    
    override func viewDidLoad() {
        super.viewDidLoad()

        countries = Country.getHardcodedData()
        

        // Wyszukiwanie.
        searchController.searchResultsUpdater = self
        searchController.obscuresBackgroundDuringPresentation = false
        definesPresentationContext = true
        tableView.tableHeaderView = searchController.searchBar
        
        // Przewinięcie i ukrycie wyszukiwania.
        tableView.tableHeaderView = searchController.searchBar
        tableView.contentOffset = CGPoint(x: 0, y: searchController.searchBar.frame.size.height)
    }

}

extension PlacesTableViewController {
    // MARK: metody UITableViewDataSource.
    
    public override func numberOfSections(in tableView: UITableView) -> Int {
        return countries.count
    }
    
    public override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        return countries[section].cities.count
    }
    
    public override func tableView(_ tableView: UITableView, titleForHeaderInSection section: Int) -> String? {
        return countries[section].name
    }
    
    public override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "PlaceTableViewCell", for: indexPath) as! PlaceTableViewCell
        
        let country = self.countries[indexPath.section]
        let city = country.cities[indexPath.row]
        
        cell.city.text = city.name
        
        return cell
        
    }
}

extension PlacesTableViewController {
    // MARK: UITableViewDelegate.
    // Procedura obsługi dotknięcia.
    public override func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        print("item: \(indexPath.row)" )
    }
}

// Lepsze podejście w zakresie implementacji interfejsu.
extension PlacesTableViewController: UISearchResultsUpdating {
    // MARK: - Delegat UISearchResultsUpdating.
    public func updateSearchResults(for searchController: UISearchController) {
        let searchText = searchController.searchBar.text!.localizedLowercase
        
        if searchText.count > 0 {
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
        
        
        tableView.reloadData()
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
