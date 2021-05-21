//
//  LoginRequest.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 19.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

public class RestApiClient {
    
    private let LoginUrlString = "https://frutwebapi.svr.vc/api/Login"

    public var completion: ((_ d: Data) -> ())?
    
    public func DoLogin(lgn: String, pwd: String) {
        if let url = URL(string: LoginUrlString + "?lgn=\(lgn)&pwd=\(pwd)") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.completion?(data)
              }
           }.resume()
        }
    }
}
