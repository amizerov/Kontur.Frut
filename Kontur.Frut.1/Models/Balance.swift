//
//  Balance.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 30.09.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

struct Balance: Codable {
    var ID: Int
    var Name: String
    var Rub: Decimal
    var Dol: Decimal
    var dtc: String
}

class Balances {
    
    var arr = Array<Balance>()
    
    init(_ data: Data) {
        let str = String(data: data, encoding: .utf8)!
        if(str.count > 0) {
            do{
                arr = try JSONDecoder().decode(Array<Balance>.self, from: data)
            }
            catch {
                print("Error parsing Balance: " + error.localizedDescription)
                print(str)
            }
        }
    }
}
