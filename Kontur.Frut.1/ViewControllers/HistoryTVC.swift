//
//  HistoryTVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 09.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

struct Change {
    var dtc = Date()
    var fld = ""
    var old = ""
    var new = ""
    var usr = ""
    init (_ d: Date = Date(), _ f: String = "", _ o: String = "", _ n: String = "", _ u: String = "")
    {
        dtc = d
        fld = f
        old = o
        new = n
        usr = u
    }
    static func Load(from s: String) -> Change {
        let ss: String
            = s.replacingOccurrences(of: "{", with: "")
               .replacingOccurrences(of: "}", with: "")
               .replacingOccurrences(of: "]", with: "")
               .replacingOccurrences(of: "[", with: "")

        let a: [String] = ss.components(separatedBy: ",")
        
        var  r = Change()
        
        if(a.count == 5) {
            r.dtc = GetDateValue(a[0])
            r.fld = GetStrValue(a[1])
            r.old = GetStrValue(a[2])
            r.new = GetStrValue(a[3])
            r.usr = GetStrValue(a[4])
        }
        return r
    }
    static func GetStrValue(_ s: String) -> String
    {
        var res =  s.components(separatedBy: ":")[1]
                    .replacingOccurrences(of: "\\\"", with: "'")
                    .replacingOccurrences(of: "\"", with: "")
        
        res = res.replacingOccurrences(of: "\'", with: "\"")
        
        return res
    }
    static func GetDateValue(_ s: String) -> Date {
        let a = s.components(separatedBy: ":\"")
        if(a.count < 2) { return Date() }
        var isoDate = a[1]
        isoDate = isoDate.replacingOccurrences(of: "\"", with: "")
        isoDate = isoDate.replacingOccurrences(of: "T", with: " ")
        isoDate = isoDate.padding(toLength: 16, withPad: "", startingAt: 0)

        let dateFormatter = DateFormatter()
        dateFormatter.dateFormat = "yyyy-MM-dd HH:mm"
        if let d = dateFormatter.date(from:isoDate) {
            return d
        }
        else {
            return Date()
        }
    }

}

class HistoryTVCell: UITableViewCell {

    @IBOutlet weak var lblDate: UILabel!
    @IBOutlet weak var lblUser: UILabel!
    @IBOutlet weak var lblField: UILabel!
    
    @IBOutlet weak var lblOld: UILabel!
    @IBOutlet weak var lblNew: UILabel!
    
    func showData(_ cng: Change) {
        
        let df = DateFormatter()
        df.dateFormat = "dd.MM.yyyy HH:mm"
        lblDate.text = df.string(from: cng.dtc)

        lblUser.text = cng.usr

        lblField.text = "изменил " + cng.fld
        lblOld.text = "с " + cng.old
        lblNew.text = "на " + cng.new

    }
}

class HistoryTVC: UITableViewController {

    var cngs = [Change]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
    }

    // MARK: - Table view data source

    override func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 100
    }
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return cngs.count
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "HistoryTVCell", for: indexPath) as! HistoryTVCell

        cell.showData(cngs[indexPath.row])

        return cell
    }
    
}
