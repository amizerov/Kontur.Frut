//
//  Posrednik.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 17.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

class Contras {
    private var arr = [Contra]()
    private var str = [String]()
    public var ds: [String] { get { return str } }
    
    init(fromData: Data = Data()){
        let decoder = JSONDecoder()
        
        do {
            debugPrint(String(data: fromData, encoding: .utf8)!)
            arr = try decoder.decode([Contra].self, from: fromData)
            arr.forEach { a in
                str.append(a.Name)
            }
        } catch {
            debugPrint("Error parsing Contra")
        }
    }
    
    func id(_ Name: String) -> Int {
        var v: Int
        v = arr.first(where: {$0.Name == Name})!.ID
        return v
    }
}

struct Contra: Codable {
    var ID = 0
    var Name = ""
}


