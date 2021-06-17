//
//  NewOplVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 14.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit
import DropDown

class NewOplVC: UIViewController {

    var ps = Posreds()
    var fs = Firmas()
    var cs = Contras()
    
    @IBOutlet weak var txtPosr: UITextField!
    let ddPosred = DropDown()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }
    
    @IBAction func tapChoosePosr(_ sender: UIButton) {
        ddPosred.dataSource = ps.ds
        ddPosred.anchorView = txtPosr
        ddPosred.bottomOffset = CGPoint(x: 0, y: sender.frame.size.height)
        ddPosred.show()
        ddPosred.selectionAction = { [weak self] (index: Int, item: String) in //8
            guard let _ = self else { return }
            self?.txtPosr.text = item
        }
    }
}
