//
//  LoginRequest.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 19.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

public class ApiService {
    
    private let loginUrl = "https://frutwebapi.svr.vc/api/Login"

    public var loginDone: ((_ loginResult: Data) -> ())?
    
    public func DoLogin(_ lgn: String, _ pwd: String) {
        if let url = URL(string: loginUrl + "?lgn=\(lgn)&pwd=\(pwd)") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.loginDone?(data)
              }
           }.resume()
        }
    }
}