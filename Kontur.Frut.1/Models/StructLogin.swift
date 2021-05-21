//
//  StructLogin.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 15.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

class Login {
    var IsIn = false
    var Name = ""
    var Role = 0
    var Pass = ""
    func Load(from: Data) {
        let s: String = String(data: from, encoding: .utf8)!
        let ss: String
            = s.replacingOccurrences(of: "{", with: "")
               .replacingOccurrences(of: "}", with: "")
               .replacingOccurrences(of: "]", with: "")
               .replacingOccurrences(of: "[", with: "")

        let a: [String] = ss.components(separatedBy: ",")
        if a.count > 2 {
            //[{"ID":1,"lgn":"dir","pwd":"123","role":1,"dtc":"2021-05-20T11:41:17.783"}]
            IsIn = true
            Name = GetStrValue(a[1])
            Role = GetIntValue(a[3])
        }
    }
    func GetStrValue(_ s: String) -> String
    {
        var res =  s.components(separatedBy: ":")[1]
                    .replacingOccurrences(of: "\\\"", with: "'")
                    .replacingOccurrences(of: "\"", with: "")
        
        res = res.replacingOccurrences(of: "\'", with: "\"")
        
        return res
    }
    func GetIntValue(_ s: String) -> Int {
        return Int(s.components(separatedBy: ":")[1])!
    }
}
