//
//  StartVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 15.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class StartVC: UIViewController {

    var naviColor = #colorLiteral(red: 0.8007361293, green: 0.9238154888, blue: 0.8164390922, alpha: 1)
    var sorryIwasGoneToBalancesVC = false
    
    @IBOutlet weak var btnLogin: UIButton!
    @IBOutlet weak var btnProsto: UIButton!
    @IBOutlet weak var lblVersion: UILabel!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        btnLogin.layer.cornerRadius = 10
        btnProsto.layer.cornerRadius = 10
        
        lblVersion.text = ver()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        if sorryIwasGoneToBalancesVC {
            DispatchQueue.main.async {
                if let mvc = self.storyboard?
                    .instantiateViewController(identifier: "MainVC") as? MainVC
                {
                    mvc.login = Login()
                }
            }
            navigationController?.navigationBar.backgroundColor = naviColor
            sorryIwasGoneToBalancesVC = false
        }
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.destination is LoginVC {
            let lvc = segue.destination as? LoginVC
            lvc?.loginSuccess = { login in
                if login.IsIn {
                    DispatchQueue.main.async {
                        self.showSpiner()
                        if let bvc = self.storyboard?
                            .instantiateViewController(identifier: "BalancesVC") as? BalancesVC
                        {
                            bvc.login = login
                            self.sorryIwasGoneToBalancesVC = true
                            self.navigationController?.pushViewController(bvc, animated: true)
                        }
                    }
                }
            }
        }
        if segue.destination is BalancesVC {
            showSpiner()
        }
    }
    
    func ver() -> String {
        let dic = Bundle.main.infoDictionary!
        let ver = dic["CFBundleShortVersionString"] as! String
        let bld = dic["CFBundleVersion"] as! String
        return "Версия: \(ver) (\(bld))"
    }
}
