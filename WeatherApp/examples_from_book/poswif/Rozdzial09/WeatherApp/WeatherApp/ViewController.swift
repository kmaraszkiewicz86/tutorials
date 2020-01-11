//
//  ViewController.swift
//  WeatherApp
//
//  Created by Emil Atanasov on 9/4/17.
//  Copyright © 2017 Appose Studio Inc. All rights reserved.
//

import UIKit
import Toast_Swift

class ViewController: UIViewController, UICollectionViewDataSource, UITableViewDataSource {

    @IBAction func onFavoritesClicked(_ sender: Any) {
        performSegue(withIdentifier: "showFavorites", sender: sender)
    }
    
    @IBAction func onAboutClicked(_ sender: Any) {
        performSegue(withIdentifier: "showAbout", sender: sender)
    }
    
    var model:LocationForecast?
    // Szczegóły dotyczące outletów.
    @IBOutlet weak var details: UICollectionView!
    
    @IBOutlet weak var nextDays: UITableView!
    
    @IBOutlet weak var city: UILabel!
    
    @IBOutlet weak var cityWeather: UILabel!
    
    @IBOutlet weak var temperature: UILabel!
    
    var forecast:[Forecast] = []
    
    var degreeSymbol = "°"
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Wypełnienie modelu przykładowymi danymi.
//        model = LocationForecast.getTestData()
        // Klasa implementuje odpowiednie protokoły w rozszerzeniach.
        details.dataSource = self
        nextDays.dataSource = self
        // Obsługa przypadku, gdy lokalizacja nie ma nazwy.
        city.text = model?.location?.name ?? "???"
        cityWeather.text = model?.weather ?? "???"
        temperature.text = getCurrentTemperature()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        let currentCity = City.NewYork
        ForecastStore.instance.loadForecastAlamofire(for: currentCity) { [weak self](response, error)  in
            if let error = error {
                print("there is an error")
                switch error {
                case .invalidCity:
                    let alert = UIAlertController(title: "Network problem", message: "We've faced a problem while tying to load the forecast data. Please, try later.", preferredStyle: UIAlertControllerStyle.alert)
                    alert.addAction(UIAlertAction(title: "OK", style: UIAlertActionStyle.default, handler: nil))
                    self?.present(alert, animated: true, completion: nil)
                    // Powiadomienie o określonym czasie trwania i miejscu wyświetlenia.
//                    self?.view.makeToast("Wystąpił problem podczas pobierania danych prognozy pogody. Proszę spróbować później.", duration: 1.5, position: .top)
                case .noConnection:
                    // Obsługa tego przypadku.
                    break
                case .invalidURL:
                    // Obsługa tego przypadku.
                    break
                case .wrongResponse:
                    // Obsługa tego przypadku.
                    break
                }
            } else if let responseModel = response {
                print("Wszystko jest świetnie.")
                DispatchQueue.main.async { [weak self] in
                    self?.updateUI(city: currentCity, forecast:responseModel)
                }
            }
        }
    }
    
    func updateUI(city aCity:City, forecast:WeatherResponse) {
        city.text = aCity.name
        if forecast.weather.count > 0 {
            cityWeather.text = forecast.weather[0].description ?? "???"
        }
        temperature.text = String(format: "%.0f", forecast.forecast.temperature)
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Usunięcie wszelkich zasobów, które można później ponownie utworzyć.
    }
    
    // MARK: Funkcja pomocnicza.
    
    func getCurrentTemperature() -> String {
        var lastTemperature = "?"
        if let forecastList = model?.forecastForToday {
            let currentDate = Date()
            
            for forecast in forecastList {
                if forecast.date < currentDate {
                    lastTemperature = "\(forecast.temperature)"
                }
            }
        }
        
        return lastTemperature
    }
    
    // MARK: Protokół UICollectionViewDataSource.
    
    public func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return model?.forecastForToday?.count ?? 0
    }
    
    public func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        
        let cell:WeatherViewCell = collectionView.dequeueReusableCell(withReuseIdentifier: "WeatherCell", for: indexPath) as! WeatherViewCell
        
        let forecast:Forecast = (model?.forecastForToday?[indexPath.row])!
        let formatter = DateFormatter()
        formatter.dateFormat = "H:mm"
        cell.time.text = formatter.string(from: forecast.date)
        cell.icon.image =  getIcon(weather: forecast.weather)
        cell.temperature.text = "\(forecast.temperature)\(self.degreeSymbol)"
        
        return cell
        
    }
    
    // MARK: UITableViewCollectionDataSource.
    
    public func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return model?.forecastForNextDays?.count ?? 0
    }
    
    public func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell:DailyForecastViewCell = tableView.dequeueReusableCell(withIdentifier: "FullDayWeatherCell", for: indexPath) as! DailyForecastViewCell
        
        let forecast:DailyForecast = (model?.forecastForNextDays?[indexPath.row])!
        
        let formatter = DateFormatter()
        formatter.dateFormat = "EEEE"
        
        cell.day.text = formatter.string(from: forecast.date)
        
        cell.icon.image =  getIcon(weather: forecast.weather)
        
        cell.temperature.text = "\(forecast.maxTemp)\(self.degreeSymbol)/\(forecast.minTemp)\(self.degreeSymbol)"
        
        return cell
    }
    
    // MARK: metoda prywatna.
    
    private func getIcon(weather:String) -> UIImage? {
        return LocationForecast.getImageFor(weather:weather)
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if let id = segue.identifier {
            switch id {
            case "showFavorites":
                var favVC: FavoritesViewController = segue.destination as! FavoritesViewController
                favVC.receivedData = 42
                print("Przekazanie danych.");
            default:
                break;
            }
        }
    }
    
    @IBAction func unwindToHomeScreen(sender: UIStoryboardSegue) {
        if let favoritesVC = sender.source as? FavoritesViewController {
            model = LocationForecast()
            model?.location = favoritesVC.selectedItem
        }
    }

}

