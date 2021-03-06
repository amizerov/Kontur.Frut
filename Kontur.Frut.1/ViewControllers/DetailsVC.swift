//
//  DetailsViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 30.04.2021.
//

import UIKit

class DetailsVC: UIViewController {

    var login = Login()
    public var imgPP: UIImage?
    
    @IBOutlet weak var btnPhoto: UIBarButtonItem!
    
    @IBOutlet weak var lblHeader: UILabel!
    @IBOutlet weak var lblOrgany: UILabel!
    @IBOutlet weak var lblPosred: UILabel!
    @IBOutlet weak var lblKontra: UILabel!
    @IBOutlet weak var lblNomrPP: UILabel!
    @IBOutlet weak var lblSummPP: UILabel!
    @IBOutlet weak var lblNaznch: UILabel!
    @IBOutlet weak var lblSumVid: UILabel!
    
    @IBOutlet weak var lblProcent: UILabel!
    @IBOutlet weak var stpProcent: UIStepper!
    
    @IBOutlet weak var swIsOplach: UISwitch!
    @IBOutlet weak var swIsPoluch: UISwitch!
    @IBOutlet weak var swIsVidano: UISwitch!
    
    var mainVC: MainVC?
    var theRow = RowData()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        //if screen height < 500 lblNaznch.Vizible = false
        login = mainVC!.login
        
        let df = DateFormatter()
        df.dateFormat = "dd/MM/yyyy"

        lblHeader.text = "от " + df.string(from: theRow.DataPP)
        lblNomrPP.text = "ПП №: \(theRow.NomrPP)"
        lblOrgany.text = theRow.Organy
        lblKontra.text = theRow.Kontra == "" || theRow.Kontra == "null"
            ? "Фирма не выбрана" : theRow.Kontra
        lblSummPP.text = "Сумма: \(theRow.SummPP)"
        lblPosred.text = theRow.Posred
        lblNaznch.text = "Назначение: " + theRow.Naznch
        
        stpProcent.value = theRow.PenyPr
        lblProcent.text = "\(Int(theRow.PenyPr)) %"
        
        let com = theRow.SummPP * theRow.PenyPr / 100
        let vid = theRow.SummPP * (1 - theRow.PenyPr / 100)
        lblSumVid.text =
            "Комис: " + String(format: "%.2f", com) + " " +
            "Выдать: " + String(format: "%.2f", vid)
        
        swIsOplach.isOn = theRow.IsPaied
        swIsPoluch.isOn = theRow.IsReced
        swIsVidano.isOn = theRow.IsVidan
        
        if login.Role != 1 {
            //swIsOplach.isEnabled = false
            //swIsPoluch.isEnabled = false
            //swIsVidano.isEnabled = false
            stpProcent.isHidden = true
        }
    }

    @IBAction func stpProcentChanges(_ sender: UIStepper) {
        lblProcent.text = "\(Int(stpProcent.value)) %"
        let com = theRow.SummPP * stpProcent.value / 100
        let vid = theRow.SummPP * (1 - stpProcent.value / 100)
        lblSumVid.text =
            "Комис: " + String(format: "%.2f", com) + " " +
            "Выдать: " + String(format: "%.2f", vid)
        SetProcent(oid: theRow.ID, fin: "Procent", val: stpProcent.value, user: login.Name)
        mainVC?.NeedToReload = true
    }
    @IBAction func IsOplaChanged(_ sender: UISwitch) {
        SetValue(oid: theRow.ID, fin: "IsPaied", val: sender.isOn, user: login.Name)
        mainVC?.NeedToReload = true
    }
    @IBAction func IsPoluChanged(_ sender: UISwitch) {
        SetValue(oid: theRow.ID, fin: "IsRecieved", val: sender.isOn, user: login.Name)
        mainVC?.NeedToReload = true
    }
    @IBAction func IsVidaChanged(_ sender: UISwitch) {
        SetValue(oid: theRow.ID, fin: "IsVidano", val: sender.isOn, user: login.Name)
        mainVC?.NeedToReload = true
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.destination is HistoryTVC {
            let hvc = segue.destination as? HistoryTVC
            hvc?.cngs = theRow.Changes
        }
        if segue.destination is PictOrderVC {
            let pvc = segue.destination as? PictOrderVC
            pvc?.img = theRow.PictOrder
        }
    }
}
