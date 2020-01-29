//
//  ViewController.swift
//  WeatherAppWithStoryboard
//
//  Created by Krzysztof Maraszkiewicz on 21/12/2019.
//  Copyright © 2019 Krzysztof Maraszkiewicz. All rights reserved.
//

import UIKit

class ViewController: UIViewController, UICollectionViewDataSource, UITableViewDataSource {

    var model: LocationForecast?
    
    @IBOutlet weak var details: UICollectionView!
    
    @IBOutlet weak var nextDays: UITableView!
    
    @IBOutlet weak var city: UILabel!
    
    @IBOutlet weak var cityWeather: UILabel!
    
    @IBOutlet weak var temperature: UILabel!
    
    var forecast:[Forecast] = []
    
    var degreeSymbol = "°"
    
    let collectionViewFormatter = DateFormatter()
    let tableViewFormatter = DateFormatter()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        model = LocationForecast.getTestData()
        
        collectionViewFormatter.dateFormat = "H:mm"
        tableViewFormatter.dateFormat = "EEEE"
        
        details.dataSource = self
        nextDays.dataSource = self
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        let currentCity = City.NewYork
        ForecastStore.shared.loadForecast(for: currentCity) { [weak self] (response, error) in
            if let error = error {
                switch error {
                case .invalidCity:
                    
                    let alert = UIAlertController(title: "Problem z siecią", message: "Wystąpił błąd podczas wczytywania danych prognozy podogdy. Proszę spróbować później.", preferredStyle: .alert)
                    
                    let okAction = UIAlertAction(title: "OK", style: .cancel, handler: nil)
                    
                    alert.addAction(okAction)
                    
                    self?.present(alert, animated: true)
                    
                case .noConnection:
                    break
                    
                case .invalidURL:
                    break
                    
                case .wrongResponse:
                    break
                }
            } else {
                DispatchQueue.main.async {
                    [weak self] in
                    self?.updateUI(city: currentCity, forecastModel: response)
                }
            }
        }
        
    }
    
    func updateUI(city aCity: City, forecastModel: WeatherResponse?) {
        city.text = aCity.name
        if let forecast = forecastModel, forecast.weather.count > 0 {
            cityWeather.text = forecast.weather[0].description
        }
        temperature.text = String(format: "%.0f", forecastModel?.forecast.temperature ?? 0)
    }
    
    fileprivate func getIcon(weather: String) -> UIImage? {
        return LocationForecast.getImageFor(weather: weather)
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let id = segue.identifier {
            switch id {
            case "showFavorites":
                guard let favViewController = segue.destination as? FavoritesTableViewController else {
                    return
                }
                
                favViewController.receivedData = 42
            default:
                break
            }
        }
    }
    
    @IBAction func unwindToHomeScreen(sender: UIStoryboardSegue) {
        if let favoritesVC = sender.source as? FavoritesTableViewController {
            model = LocationForecast()
            model?.location = favoritesVC.selectedItem
        }
    }
}

extension ViewController {
    func collectionView(_ collectionView
        : UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return model?.forecastForToday?.count ?? 0
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "WeatherViewCell", for: indexPath) as! WeatherViewCell
        let forecast = (model?.forecastForToday?[indexPath.row])!
        
        cell.day.text = collectionViewFormatter.string(from: forecast.date)
        cell.icon.image = getIcon(weather: forecast.weather)
        cell.temperature.text = "\(forecast.temperature)\(degreeSymbol)"
        
        return cell
    }
}

extension ViewController {
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return model?.forecastForNextDays?.count ?? 0
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "DailyForecastViewCell", for: indexPath) as! DailyForecastViewCell
        let forecast = (model?.forecastForNextDays?[indexPath.row])!
        
        cell.day.text = tableViewFormatter.string(from: forecast.date)
        cell.icon.image = getIcon(weather: forecast.weather)
        cell.temperature.text = "\(forecast.temperature)\(degreeSymbol)"
        
        return cell
    }
    
}
