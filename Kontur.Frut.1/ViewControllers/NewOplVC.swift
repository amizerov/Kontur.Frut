//
//  NewOplVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 14.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit
import DropDown

class NewOplVC: UIViewController,
                UINavigationControllerDelegate,
                UIImagePickerControllerDelegate {

    var ps = Posreds()
    var fs = Firmas()
    var cs = Contras()
    var ns = Naznachs()
    
    var imageData = Data()
    let imagePicker = UIImagePickerController()
    @IBOutlet weak var imageView: UIImageView!
    
    @IBOutlet weak var txtPosr: UITextField!
    @IBOutlet weak var txtNomer: UITextField!
    @IBOutlet weak var txtSumma: UITextField!
    @IBOutlet weak var txtFirma: UITextField!
    @IBOutlet weak var txtContra: UITextField!
    @IBOutlet weak var txtNaznach: UITextField!
    @IBOutlet weak var lblProcent: UILabel!
    @IBOutlet weak var dpDatePP: UIDatePicker!
    
    let ddPosred = DropDown()
    let ddFirma = DropDown()
    let ddContra = DropDown()
    let ddNaznach = DropDown()
        
    @IBAction func editBegin(_ sender: UITextField) {
        sender.backgroundColor = #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0)
    }
    @IBAction func valueChanged(_ sender: UITextField) {
        sender.backgroundColor = #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        dpDatePP.preferredDatePickerStyle = .compact
        let toolBar = UIToolbar()
        toolBar.sizeToFit()
        let doneButton = UIBarButtonItem(barButtonSystemItem: .done, target: self, action: #selector(doneTapped))
        toolBar.setItems([doneButton], animated: true)
        
        imagePicker.delegate = self
    }
    @objc func doneTapped() {
        view.endEditing(true)
    }
    
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        view.endEditing(true)
    }
    
    @IBAction func step_ValueChanged(_ sender: UIStepper) {
        lblProcent.text = "\(Int(sender.value)) %"
    }
    
    @IBAction func tapChooseNaznach(_ sender: UIButton) {
        ddNaznach.dataSource = ns.ds
        ddNaznach.anchorView = txtNaznach
        ddNaznach.bottomOffset = CGPoint(x: 0, y: sender.frame.size.height)
        ddNaznach.show()
        ddNaznach.selectionAction = { [weak self] (index: Int, item: String) in //8
            guard let _ = self else { return }
            self?.txtNaznach.text = item
            self?.txtNaznach.backgroundColor = #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0)
        }
    }
    @IBAction func tapChooseContra(_ sender: UIButton) {
        ddContra.dataSource = cs.ds
        ddContra.anchorView = txtContra
        ddContra.bottomOffset = CGPoint(x: 0, y: sender.frame.size.height)
        ddContra.show()
        ddContra.selectionAction = { [weak self] (index: Int, item: String) in //8
            guard let _ = self else { return }
            self?.txtContra.text = item
            self?.txtContra.backgroundColor = #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0)
        }
    }
    @IBAction func tapChooseFirma(_ sender: UIButton) {
        ddFirma.dataSource = fs.ds
        ddFirma.anchorView = txtFirma
        ddFirma.bottomOffset = CGPoint(x: 0, y: sender.frame.size.height)
        ddFirma.show()
        ddFirma.selectionAction = { [weak self] (index: Int, item: String) in //8
            guard let _ = self else { return }
            self?.txtFirma.text = item
            self?.txtFirma.backgroundColor = #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0)
        }
    }
    @IBAction func tapChoosePosr(_ sender: UIButton) {
        ddPosred.dataSource = ps.ds
        ddPosred.anchorView = txtPosr
        ddPosred.bottomOffset = CGPoint(x: 0, y: sender.frame.size.height)
        ddPosred.show()
        ddPosred.selectionAction = { [weak self] (index: Int, item: String) in //8
            guard let _ = self else { return }
            self?.txtPosr.text = item
            self?.txtPosr.backgroundColor = #colorLiteral(red: 1.0, green: 1.0, blue: 1.0, alpha: 1.0)
        }
    }
    
    @IBAction func takePictureClicked(_ sender: UIButton) {
 
        imagePicker.sourceType = .camera
        imagePicker.allowsEditing = false
        
        present(imagePicker, animated: true, completion: nil)
    }
    
    func imagePickerController(_ picker: UIImagePickerController,
                    didFinishPickingMediaWithInfo info: [UIImagePickerController.InfoKey : Any]) {
        if let img = info[UIImagePickerController.InfoKey.originalImage] as? UIImage {
            imageView.image = img
            imageData = img.jpegData(compressionQuality: 0.8) ?? Data()
        }
        imagePicker.dismiss(animated: true, completion: nil)
    }
    func imagePickerControllerDidCancel(_ picker: UIImagePickerController) {
        imagePicker.dismiss(animated: true, completion: nil)
    }
    
    @IBAction func btnSaveClicked(_ sender: Any) {
    
        if validateFields() {
        
            let p = ps.id(txtPosr.text!)
            let f = fs.id(txtFirma.text!)
            let c = cs.id(txtContra.text!)
            let a = ns.id(txtNaznach.text!)
            
            let n = Int(txtNomer.text!) ?? 0
            let s = Double(txtSumma.text!) ?? 0
            var proc = lblProcent.text!
            proc = proc.replacingOccurrences(of: "%", with: "")
            proc = proc.replacingOccurrences(of: " ", with: "")
            let r = Int(proc) ?? 0
            let d = dpDatePP.date
            let frmr = DateFormatter()
            frmr.dateFormat = "yyyy-MM-dd"
            
            let opl = Oplata(n,p,s,f,c,a,r,frmr.string(from: d))
            opl.Save()
            
            dismiss(animated: true)
        }
    }
    func validateFields() -> Bool {
        
        if txtPosr.text == "" {
            txtPosr.backgroundColor = #colorLiteral(red: 0.9372549057, green: 0.3490196168, blue: 0.1921568662, alpha: 1)
            return false
        }
        if txtNomer.text == "" {
            txtNomer.backgroundColor = #colorLiteral(red: 0.9372549057, green: 0.3490196168, blue: 0.1921568662, alpha: 1)
            return false
        }
        if txtSumma.text == "" {
            txtSumma.backgroundColor = #colorLiteral(red: 0.9372549057, green: 0.3490196168, blue: 0.1921568662, alpha: 1)
            return false
        }
        if txtFirma.text == "" {
            txtFirma.backgroundColor = #colorLiteral(red: 0.9372549057, green: 0.3490196168, blue: 0.1921568662, alpha: 1)
            return false
        }
        if txtContra.text == "" {
            txtContra.backgroundColor = #colorLiteral(red: 0.9372549057, green: 0.3490196168, blue: 0.1921568662, alpha: 1)
            return false
        }
        if txtNaznach.text == "" {
            txtNaznach.backgroundColor = #colorLiteral(red: 0.9372549057, green: 0.3490196168, blue: 0.1921568662, alpha: 1)
            return false
        }
        
        return true
    }
    
}
