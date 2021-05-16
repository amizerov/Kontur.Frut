//
//  StartVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 15.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class StartVC: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()

        // Do any additional setup after loading the view.
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if segue.destination is LoginVC {
            let vc = segue.destination as? LoginVC
            vc?.completion = {
                if let login = vc?.login {
                    if login.IsIn {
                        if let mvc = self.storyboard?
                            .instantiateViewController(identifier: "MainVС") as? MainVС
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
