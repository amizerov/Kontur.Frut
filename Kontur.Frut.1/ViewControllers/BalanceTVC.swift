//
//  BalanceTVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 01.10.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class BalanceTVC: UITableViewCell {

    @IBOutlet weak var lblPosred: UILabel!
    @IBOutlet weak var lblDate: UILabel!
    @IBOutlet weak var lblBalTop: UILabel!
    @IBOutlet weak var lblBalBot: UILabel!
    
    override func awakeFromNib() {
        super.awakeFromNib()
        // Initialization code
    }

    override func setSelected(_ selected: Bool, animated: Bool) {
        super.setSelected(selected, animated: animated)

        // Configure the view for the selected state
    }

}
