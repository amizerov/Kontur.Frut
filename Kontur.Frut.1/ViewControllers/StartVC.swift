//
//  StartVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 15.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class StartVC: UIViewController {

    @IBOutlet weak var btnLogin: UIButton!
    @IBOutlet weak var btnProsto: UIButton!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        btnLogin.layer.cornerRadius = 10
        btnProsto.layer.cornerRadius = 10
        
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.destination is LoginVC {
            let vc = segue.destination as? LoginVC
            vc?.completion = {
                if let login = vc?.login {
                    if login.IsIn {
                        DispatchQueue.main.async {
                            if let mvc = self.storyboard?
                                .instantiateViewController(identifier: "MainVC") as? MainVC
                            {
                                mvc.login = login
                                self.navigationController?.pushViewController(mvc, animated: true)
                            }
                        }
                    }
                }
            }
        }
    }
}
