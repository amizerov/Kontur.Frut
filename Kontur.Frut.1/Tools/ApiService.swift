//
//  LoginRequest.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 19.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

//public let api = ApiService()
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
    
    public var gotPictOrder: ((_ data: Data) -> ())?
    public func GetPictOrder(_ id_oplata: Int) {
        if let url = URL(string: apiUrl + "/picor/\(id_oplata)") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.gotPictOrder?(data)
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
 
    public var gotFirmaList: ((_ data: Data) -> ())?
    public func GetFirmaList() {
        if let url = URL(string: apiUrl + "/Firma") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.gotFirmaList?(data)
              }
           }.resume()
        }
    }

    public var gotContraList: ((_ data: Data) -> ())?
    public func GetContraList() {
        if let url = URL(string: apiUrl + "/Contra") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.gotContraList?(data)
              }
           }.resume()
        }
    }
    
    public var gotNaznachList: ((_ data: Data) -> ())?
    public func GetNaznachList() {
        if let url = URL(string: apiUrl + "/Naznach") {
           URLSession.shared.dataTask(with: url) { data, response, error in
              if let data = data {
                self.gotNaznachList?(data)
              }
           }.resume()
        }
    }

}
