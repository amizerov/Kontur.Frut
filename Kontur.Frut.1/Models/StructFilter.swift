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
            return FilterToUrlString(self)
        }
    }
}

class FilterDate {
    var DateFrom: Date = Date() - defTimeInterval
    var DateTo: Date = Date()
    
    public func Clear() {
        DateFrom = Date() - defTimeInterval
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
