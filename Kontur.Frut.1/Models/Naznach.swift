//
//  Posrednik.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 17.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

class Naznachs {
    private var arr = [Naznach]()
    private var str = [String]()
    public var ds: [String] { get { return str } }
    
    init(fromData: Data = Data(count: 0)){
        let decoder = JSONDecoder()
        
        do {
            debugPrint(String(data: fromData, encoding: .utf8)!)
            arr = try decoder.decode([Naznach].self, from: fromData)
            arr.forEach { a in
                str.append(a.Text)
            }
        } catch {
            debugPrint("Error parsing Naznach")
        }
    }
    
    func id(_ Name: String) -> Int {
        var v: Int
        v = arr.first(where: {$0.Text == Name})!.ID
        return v
    }
}

struct Naznach: Codable {
    var ID = 0
    var Text = ""
}


