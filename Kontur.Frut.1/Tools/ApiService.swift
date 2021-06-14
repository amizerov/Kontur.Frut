//
//  LoginRequest.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 19.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

public class ApiService {
    
    init() {
        
    }
    
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
    
    private let apiUrl = "https://frutwebapi.svr.vc/api"
    public var gotHistory: ((_ data: Data) -> ())?
    
    public func GetHistory(_ id_oplata: Int) {
        if let url = URL(string: apiUrl + "/values/\(id_oplata)") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.gotHistory?(data)
              }
           }.resume()
        }
    }
    
    public var gotPosrList: ((_ data: Data) -> ())?
    public func GetPosrList() {
        if let url = URL(string: apiUrl + "/posr") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.gotPosrList?(data)
              }
           }.resume()
        }
    }
    

    
}
