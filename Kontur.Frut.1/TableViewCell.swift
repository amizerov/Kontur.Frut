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
