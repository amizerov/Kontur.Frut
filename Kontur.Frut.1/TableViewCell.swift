//
//  TableViewCell.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 30.04.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class TableViewCell: UITableViewCell {

    @IBOutlet weak var lbIsOplach: UILabel!
    @IBOutlet weak var lbIsPoluch: UILabel!
    @IBOutlet weak var lbIsVidano: UILabel!
    
    @IBOutlet weak var lblDatePP: UILabel!
    @IBOutlet weak var lblOrgany: UILabel!
    @IBOutlet weak var lblSummPP: UILabel!
    @IBOutlet weak var lblPosred: UILabel!
    
    @IBOutlet weak var lblProcent: UILabel!
    
    public func FillData(_ r: RowData) {
        
        let df = DateFormatter()
        df.dateFormat = "dd.MM.yyyy HH:mm"
        lblDatePP.text = df.string(from: r.DTC)
        lblOrgany.text = r.Organy
        lblSummPP.text = "Сумма: \(r.SummPP)"
        lblPosred.text = r.Posred
        
        // Если выдано авансом, но оплата не пришла,
        // то ячейка выделяется красным
        if r.IsPaied && r.IsVidan && !r.IsReced {
            alpha = 2
            backgroundColor = #colorLiteral(red: 0.9372549057, green: 0.3490196168, blue: 0.1921568662, alpha: 1)
        }
        else {
            backgroundColor = #colorLiteral(red: 1, green: 1, blue: 1, alpha: 1)
        }
        
        lbIsOplach.backgroundColor = r.IsPaied ? #colorLiteral(red: 0.3411764801, green: 0.6235294342, blue: 0.1686274558, alpha: 1) : #colorLiteral(red: 0.8039215803, green: 0.8039215803, blue: 0.8039215803, alpha: 1)
        lbIsPoluch.backgroundColor = r.IsReced ? #colorLiteral(red: 0.3411764801, green: 0.6235294342, blue: 0.1686274558, alpha: 1) : #colorLiteral(red: 0.8039215803, green: 0.8039215803, blue: 0.8039215803, alpha: 1)
        lbIsVidano.backgroundColor = r.IsVidan ? #colorLiteral(red: 0.3411764801, green: 0.6235294342, blue: 0.1686274558, alpha: 1) : #colorLiteral(red: 0.8039215803, green: 0.8039215803, blue: 0.8039215803, alpha: 1)
        
        lblProcent.text = "\(r.PenyPr)%"
    }
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
        lbIsOplach.layer.cornerRadius = 10
        lbIsOplach.layer.masksToBounds = true
        lbIsPoluch.layer.cornerRadius = 10
        lbIsPoluch.layer.masksToBounds = true
        lbIsVidano.layer.cornerRadius = 10
        lbIsVidano.layer.masksToBounds = true
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }
    
}
