//
//  ViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 25.04.2021.
//
import Foundation
import UIKit

class MainVC: UIViewController {

    let api = ApiService()
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
        
        if login.Role == 1 {
            navigationController?.navigationBar.backgroundColor = #colorLiteral(red: 0.4745098054, green: 0.8392156959, blue: 0.9764705896, alpha: 1)
        }
        if login.Role == 2 {
            navigationController?.navigationBar.backgroundColor = #colorLiteral(red: 1, green: 0.8798277978, blue: 0.8770594175, alpha: 1)
        }
        
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

            self.removeSpiner()
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
        if segue.destination is DatePopupVC {
            let vc = segue.destination as? DatePopupVC
            vc?.filter = self.filter.ByDate
            vc?.completion = {
                self.LoadDataFromServer()
            }
        }
        else
        if segue.destination is PosrPopUpVC {
            let vc = segue.destination as? PosrPopUpVC
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
        else
        if segue.destination is FirmPopUpVC {
            let vc = segue.destination as? FirmPopUpVC
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
        else
        if segue.destination is NewOplVC {
            let vc = segue.destination as? NewOplVC
            
            api.GetPosrList()
            api.GetFirmaList()
            api.GetContraList()
            api.GetNaznachList()
            
            api.gotPosrList = { data in
                vc!.ps = Posreds(fromData: data)
            }
            api.gotFirmaList = { data in
                vc!.fs = Firmas(fromData: data)
            }
            api.gotContraList = { data in
                vc!.cs = Contras(fromData: data)
            }
            api.gotNaznachList = { data in
                vc!.ns = Naznachs(fromData: data)
            }
        }
    }
}

extension MainVC: UITableViewDataSource, UITableViewDelegate
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
            
            cell!.FillData(r)
            
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
        if login.Role == 0 {
            return
        }
        // При нажатии на ячейку таблицы, переходим на экран редактирования
        if let dv = storyboard?
            .instantiateViewController(identifier: "DetailsVC") as? DetailsVC
        {
            // передаем в редактор ссылку на главный контроллер
            dv.tableVC = self
            // и передаем данные по выбранной ячейке
            dv.theRow = arRows[indexPath.row]
            dv.theRow.LoadHistory()
 
            self.navigationController?.pushViewController(dv, animated: true)
        }
    }
}
