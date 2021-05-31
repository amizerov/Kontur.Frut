//
//  FirmPopUpViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 10.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class FirmPopUpVC: UIViewController {

    @IBOutlet weak var popUpViewFirm: UIView!
    @IBOutlet weak var picFirm: UIPickerView!
    @IBOutlet weak var btnCancel: UIButton!
    
    public var completion: (() -> ())?
    public var filter = TheFirm()
    public var ListOfFirm = [TheFirm]()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        btnCancel.layer.cornerRadius = 10
        popUpViewFirm.layer.cornerRadius = 10
        
        picFirm.delegate = self
        picFirm.dataSource = self
        
        let idxCurrRow = ListOfFirm.firstIndex(where: {$0.Name == filter.Name})
        picFirm.selectRow(idxCurrRow!, inComponent: 0, animated: true)
    }
    
    @IBAction func btnCancel_Click(_ sender: UIButton) {
        dismiss(animated: true)
    }
    @IBAction func Submit(_ sender: UIButton) {
        self.completion?()
        dismiss(animated: true)
    }
    @IBAction func Clear(_ sender: UIButton) {
        filter = TheFirm()
        completion?()
        dismiss(animated: true)
    }
}

extension FirmPopUpVC: UIPickerViewDelegate, UIPickerViewDataSource {
    func numberOfComponents(in pickerView: UIPickerView) -> Int {
        return 1
    }
    
    func pickerView(_ pickerView: UIPickerView, numberOfRowsInComponent component: Int) -> Int {
        return ListOfFirm.count
    }
    
    func pickerView(_ pickerView: UIPickerView, didSelectRow row: Int, inComponent component: Int) {
        filter.Name = ListOfFirm[row].Name
    }
    
    func pickerView(_ pickerView: UIPickerView, viewForRow row: Int, forComponent component: Int, reusing view: UIView?) -> UIView {
        var lbl: UILabel? = (view as? UILabel)
        if lbl == nil {
            lbl = UILabel()
            lbl?.font = UIFont(name: "Arial", size: CGFloat(45))
            lbl?.textAlignment = .center
        }
        lbl?.text = ListOfFirm[row].Name
        
        return lbl!
    }
}

