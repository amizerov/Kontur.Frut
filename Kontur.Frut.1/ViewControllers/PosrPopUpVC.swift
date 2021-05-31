//
//  PosrPopUpViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 10.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class PosrPopUpVC: UIViewController {

    @IBOutlet weak var popUpViewPosr: UIView!
    @IBOutlet weak var picPosr: UIPickerView!
    @IBOutlet weak var btnCancel: UIButton!
    
    public var completion: (() -> ())?
    public var filter = ThePosr()
    public var ListOfPosr = [ThePosr]()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        btnCancel.layer.cornerRadius = 10
        popUpViewPosr.layer.cornerRadius = 10
        
        picPosr.delegate = self
        picPosr.dataSource = self
        
        let idxCurrRow = ListOfPosr.firstIndex(where: {$0.Name == filter.Name})
        picPosr.selectRow(idxCurrRow!, inComponent: 0, animated: true)
    }
    
    @IBAction func btnCancel_Click(_ sender: UIButton) {
        dismiss(animated: true)
    }
    @IBAction func Submit(_ sender: UIButton) {
        self.completion?()
        dismiss(animated: true)
    }
    @IBAction func Clear(_ sender: UIButton) {
        filter.Name = defName
        completion?()
        dismiss(animated: true)
    }
}

extension PosrPopUpVC: UIPickerViewDelegate, UIPickerViewDataSource {
    func numberOfComponents(in pickerView: UIPickerView) -> Int {
        return 1
    }
    
    func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return ListOfPosr.count
    }
    
    func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        filter.Name = ListOfPosr[row].Name
    }
    
    func pickerView(_ pickerView: UIPickerView, viewForRow row: Int, forComponent component: Int, reusing view: UIView?) -> UIView {
        var lbl: UILabel? = (view as? UILabel)
        if lbl == nil {
            lbl = UILabel()
            lbl?.font = UIFont(name: "Arial", size: CGFloat(45))
            lbl?.textAlignment = .center
        }
        lbl?.text = ListOfPosr[row].Name
        
        return lbl!
    }
}
