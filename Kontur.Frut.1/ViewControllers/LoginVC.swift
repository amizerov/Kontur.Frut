//
//  LoginViewController.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 15.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class LoginVC: UIViewController {

    public var login = Login()
    public var completion: (() -> ())?
    
    @IBOutlet weak var btnLogin: UIButton!
    @IBOutlet weak var btnCancel: UIButton!
    
    @IBAction func btnCancelClick(_ sender: UIButton) {
        dismiss(animated: true)
    }
    
    @IBAction func btnLoginClick(_ sender: UIButton) {
        login.IsIn = true
        completion?()
        dismiss(animated: true)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()

    }

}
