//
//  BalancesVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 30.09.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class BalancesVC: UITableViewController {

    let api = ApiService()
    var login = Login()
    var bals = Balances(Data())
    
    override func viewDidLoad() {
        super.viewDidLoad()

        api.GetBalances() { data in
            DispatchQueue.main.async {
                self.bals = Balances(data)
                self.tableView.reloadData()
            }
        }
    }

    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return bals.arr.count
    }
    
    override func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 100
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {

        let cell = tableView.dequeueReusableCell(withIdentifier: "BalanceCell") as! BalanceTVC
        let b = bals.arr[indexPath.row]
        
        cell.lblPosred.text = b.Name
        cell.lblDate.text = b.dtc
        cell.lblBalTop.text = b.Rub > 0 ? "\(b.Rub)" : "\(b.Dol)"
        cell.lblBalBot.text = b.Rub > 0 ? "Рублей" : "Долларов"
        
        cell.backgroundColor = indexPath.row % 2 == 0 ? #colorLiteral(red: 0.921431005, green: 0.9214526415, blue: 0.9214410186, alpha: 1) : #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0)
        
        return cell
    }
    
    override func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if login.Role == 0 {
            return
        }
        // При нажатии на ячейку баланса посредника, переходим на список операций этого посредника
        if let mvc = storyboard?
            .instantiateViewController(identifier: "MainVC") as? MainVC
        {
            let filter = Filter()
            filter.ByPosr.Name = bals.arr[indexPath.row].Name
            filter.ByPosr.ID = bals.arr[indexPath.row].ID
            mvc.filter = filter
            mvc.login = login
                
            self.navigationController?.pushViewController(mvc, animated: true)
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.destination is MainVC {
            let mvc = segue.destination as? MainVC
            mvc?.login = login
        }
    }
}
