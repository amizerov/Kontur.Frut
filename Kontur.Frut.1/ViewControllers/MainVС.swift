//
//  ViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 25.04.2021.
//
import Foundation
import UIKit

class MainVС: UIViewController {

    var login = Login()
    
    private var ListOfPosr = [ThePosr()]
    private var ListOfFirm = [TheFirm()]
    
    var NeedToReload: Bool = false
    var arRows = [RowData]()
    var filter = Filter()
    
    @IBOutlet weak var tbvOpla: UITableView!
    let searchController = UISearchController()
    
    override func viewDidLoad() {
        
        super.viewDidLoad()
        
        // Регистрация кастомного вью для ячейки таблицы
        let nib = UINib(nibName: "TableViewCell", bundle: nil)
        tbvOpla.register(nib, forCellReuseIdentifier: "TableViewCell")

        // Первоначальная загрузка данных в таблицу
        LoadDataFromServer()
        tbvOpla.dataSource = self
        tbvOpla.delegate = self
        
        // Обновление данных когда потянешь экран вниз
        tbvOpla.refreshControl = UIRefreshControl()
        tbvOpla.refreshControl?.addTarget(self, action: #selector(qqq), for: .valueChanged)
    }
    
    @objc func qqq() {
        // .. для обновления данных когда потянешь экран вниз
        LoadDataFromServer()
    }
    
    // Асинхронная функция получения данных с сервера через JSON REST API
    // с учетом значений, заданных в фильтре
    func LoadDataFromServer() {
        // Здесь из фильтра строится строка http запроса типа GET
        if let url = URL(string: filter.UrlString) {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                    self.ShowData(data)
              }
           }.resume()
        }
    }
    
    func ShowData(_ d: Data) {
        // Доступ к контролам на форме из другого потока
        DispatchQueue.main.async {
            self.arRows = RowData.LoadRows(fromData: d)
            self.tbvOpla.reloadData()
            self.tbvOpla.refreshControl?.endRefreshing()
        }
    }
    
    override func viewDidAppear(_ animated: Bool) {
        // Вызывается при возврате и любом появлении таблицы на экране
        // Если на экране редактирования оплаты поменяли данные,
        // то надо обновить таблицу
        if NeedToReload {
            LoadDataFromServer()
            NeedToReload = false
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        // Перед тем как открыть модальное окно фильтра
        // надо передать в него соответствующий объект фильтра
        // и указать, что будем делать после закрытия модального окна
        if segue.destination is DatePopupViewController {
            let vc = segue.destination as? DatePopupViewController
            vc?.filter = self.filter.ByDate
            vc?.completion = {
                self.LoadDataFromServer()
            }
        }
        else if segue.destination is PosrPopUpViewController {
            let vc = segue.destination as? PosrPopUpViewController
            vc?.filter = self.filter.ByPosr
            vc?.ListOfPosr = self.ListOfPosr
            vc?.completion = {
                if self.filter.ByPosr.Name == defName {
                    self.LoadDataFromServer()
                }
                else {
                    self.arRows =
                        self.arRows.filter{ $0.Posred == self.filter.ByPosr.Name }
                    self.tbvOpla.reloadData()
                }
            }
        }
        else if segue.destination is FirmPopUpViewController {
            let vc = segue.destination as? FirmPopUpViewController
            vc?.filter = self.filter.ByFirm
            vc?.ListOfFirm = self.ListOfFirm
            vc?.completion = {
                if self.filter.ByFirm.Name == defName {
                    self.LoadDataFromServer()
                }
                else {
                    self.arRows =
                        self.arRows.filter{ $0.Organy == self.filter.ByFirm.Name }
                    self.tbvOpla.reloadData()
                }
            }
        }
    }
}

extension MainVС: UITableViewDataSource, UITableViewDelegate
{
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return arRows.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell
    {
        let cell = tableView.dequeueReusableCell(withIdentifier: "TableViewCell") as? TableViewCell
        if arRows.count > 0
        {
            let r = arRows[indexPath.row]
            
            cell!.FillData(arRows[indexPath.row])
            
            let posr = ThePosr()
            posr.Name = r.Posred
            if !ListOfPosr.contains(posr) {
                ListOfPosr.append(posr)
            }
            let firm = TheFirm()
            firm.Name = r.Organy
            if !ListOfFirm.contains(firm) {
                ListOfFirm.append(firm)
            }
        }
        return cell!
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat
    {
        // Высота ячейки таблицы
        return 100
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath)
    {
        // При нажатии на ячейку таблицы, переходим на экран редактирования
        if let dv = storyboard?
            .instantiateViewController(identifier: "DetailsViewController") as? DetailsViewController
        {
            // передаем в редактор ссылку на главный контроллер
            dv.tableViewController = self
            // и передаем данные по выбранной ячейке
            dv.theRow = arRows[indexPath.row]
 
            self.navigationController?.pushViewController(dv, animated: true)
        }
    }
}
