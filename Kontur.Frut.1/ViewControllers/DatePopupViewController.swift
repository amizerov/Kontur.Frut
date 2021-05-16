//
//  DatePopupViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 09.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class DatePopupViewController: UIViewController {

    @IBOutlet weak var popUpView: UIView!
    @IBOutlet weak var dpDateFrom: UIDatePicker!
    @IBOutlet weak var dpDateTo: UIDatePicker!
    @IBOutlet weak var btnSubmit: UIButton!
    @IBOutlet weak var btnCancel: UIButton!
    
    public var filter = FilterDate()
    public var completion: (() -> ())?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.popUpView.layer.cornerRadius = 10
        self.btnCancel.layer.cornerRadius = 10
        
        dpDateFrom.maximumDate = Date() - 100
        dpDateTo.maximumDate = Date()
        
        dpDateFrom.date = filter.DateFrom
        dpDateTo.date = filter.DateTo
    }
    
    @IBAction func btnSubmit_TouchUpInside(_ sender: Any) {
        
        if filter.DateFrom != dpDateFrom.date
        || filter.DateTo != dpDateTo.date {
            filter.DateFrom = dpDateFrom.date
            filter.DateTo = dpDateTo.date
            completion?()
        }
        dismiss(animated: true)
    }
    
    @IBAction func CancelFilter(_ sender: Any) {
        filter.Clear()
        completion?()
        dismiss(animated: true)
    }
    @IBAction func btnCancel_Click(_ sender: UIButton) {
        dismiss(animated: true)
    }
}
