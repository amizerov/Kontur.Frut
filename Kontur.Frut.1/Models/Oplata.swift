//
//  Oplata.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 21.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

class Oplata {
    var nomerp = 0
    var posred = 0
    var summap = 0.00
    var firmap = 0
    var contra = 0
    var naznac = 0
    var procen = 0
    var datepp = ""

    init(_ n:Int,_ p:Int,_ s:Double,_ f:Int,_ c:Int,_ a:Int,_ r:Int,_ d:String) {
        nomerp = n
        posred = p
        summap = s
        firmap = f
        contra = c
        naznac = a
        procen = r
        datepp = d
    }
    func Save() {
        
    }
}
