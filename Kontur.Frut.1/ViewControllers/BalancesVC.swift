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
        let cell = tableView.dequeueReusableCell(withIdentifier: "BalanceCell")
        let b = bals.arr[indexPath.row]
        
        var str = ""
        if(b.Rub > 0) { str = "\(b.Rub) Рублей" }
        if(b.Dol > 0) { str = "\(b.Dol) Долларов" }

        cell?.textLabel?.text = b.Name + "  Баланс: " + str
        
        return cell!
    }
}
