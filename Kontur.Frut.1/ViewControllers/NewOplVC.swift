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

    var PosredLst = [Posred]()
    var PosredStr = [String]()
    
    @IBOutlet weak var txtPosr: UITextField!
    let dropDown = DropDown()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }
    
    @IBAction func tapChoosePosr(_ sender: UIButton) {
        dropDown.dataSource = PosredStr//4
        dropDown.anchorView = txtPosr //5
        dropDown.bottomOffset = CGPoint(x: 0, y: sender.frame.size.height) //6
        dropDown.show() //7
        dropDown.selectionAction = { [weak self] (index: Int, item: String) in //8
            guard let _ = self else { return }
            self?.txtPosr.text = item
        }
    }
}
