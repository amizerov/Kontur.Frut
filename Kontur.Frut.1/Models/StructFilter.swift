//
//  StructFilter.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 09.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

let defTimeInterval = 30 * 60 * 60 * 24 as TimeInterval
let defName = " --- "

class Filter {
    
    var HideVidano = false // Не показывать уже выданные Оплаты
    
    var ByDate = FilterDate()
    var ByPosr = ThePosr()
    var ByFirm = TheFirm()
    
    var UrlString: String {
        get {
            return FilterToUrlString()
        }
    }
    
    func FilterToUrlString() -> String {
        
        var url = ""
        
        let df = DateFormatter()
        df.dateFormat = "yyyyMMdd"
        let from = df.string(from: ByDate.DateFrom)
        let to = df.string(from: ByDate.DateTo)
        url = ApiUrlString + "?From=\(from)&To=\(to)&Posr_Id=\(ByPosr.ID)"
        
        return url
    }
}

class FilterDate {
    var DateFrom = ISO8601DateFormatter().date(from: "2021-01-01T00:00:00+0000")! //Date() - defTimeInterval
    var DateTo = Date()
    
    public func Clear() {
        DateFrom = ISO8601DateFormatter().date(from: "2021-01-01T00:00:00+0000")! //Date() - defTimeInterval
        DateTo = Date()
    }
}

class ThePosr: Equatable {
    static func == (lhs: ThePosr, rhs: ThePosr) -> Bool {
        return lhs.Name == rhs.Name
    }
    
    var ID: Int = 0
    var Name: String = defName
    
    public func Clear() {
        ID = 0
        Name = defName
    }
}

class TheFirm: Equatable {
    static func == (lhs: TheFirm, rhs: TheFirm) -> Bool {
        return lhs.Name == rhs.Name
    }
    
    var ID: Int = 0
    var Name: String = defName
    
    public func Clear() {
        ID = 0
        Name = defName
    }
}
