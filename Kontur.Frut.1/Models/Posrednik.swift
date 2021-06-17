//
//  Posrednik.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 17.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

class Posreds {
    private var arr = [Posred]()
    private var str = [String]()
    public var ds: [String] { get { return str } }
    
    init(fromData: Data = Data()){
        let decoder = JSONDecoder()
        
        do {
            debugPrint(String(data: fromData, encoding: .utf8)!)
            arr = try decoder.decode([Posred].self, from: fromData)
            arr.forEach { a in
                str.append(a.Name)
            }
        } catch {
            debugPrint("Error parsing")
        }
    }
}

struct Posred: Codable {
    var ID = 0
    var Name = ""
    var Procent = 0
}


