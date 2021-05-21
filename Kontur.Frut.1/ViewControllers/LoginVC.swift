//
//  LoginViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 15.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class LoginVC: UIViewController {

    @IBOutlet weak var txtLogin: UITextField!
    @IBOutlet weak var txtPassword: UITextField!
    
    let com = RestApiClient()
    public var login = Login()
    public var completion: (() -> ())?
    
    @IBOutlet weak var btnLogin: UIButton!
    @IBOutlet weak var btnCancel: UIButton!
    
    @IBAction func btnCancelClick(_ sender: UIButton) {
        dismiss(animated: true)
    }
    
    @IBAction func btnLoginClick(_ sender: UIButton) {
        
        com.DoLogin(lgn: txtLogin.text!, pwd: txtPassword.text!)
        com.completion = { d in
            
            self.login.Load(from: d)

            if self.login.IsIn {
                DispatchQueue.main.async
                {
                    self.completion?()
                    self.dismiss(animated: true)
                }
            }
            else {
                self.txtLogin.text = ""
                self.txtPassword.text = ""
            }
        }
    }

    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        self.view.endEditing(true)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

    }

}
