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
    var imageData = Data()

    init(_ n:Int,_ p:Int,_ s:Double,_ f:Int,_ c:Int,_ a:Int,_ r:Int,_ d:String,_ imDa:Data) {
        nomerp = n
        posred = p
        summap = s
        firmap = f
        contra = c
        naznac = a
        procen = r
        datepp = d
        imageData = imDa
    }
    func Save() {
        let body: [String: Any] =
        [
            "nomerp": nomerp,
            "posred": posred,
            "summap": summap,
            "firmap": firmap,
            "contra": contra,
            "naznac": naznac,
            "procen": procen,
            "datepp": datepp,
            "usr": CurrentUser
        ]
        let s = ApiUrlString
        ApiUrlString = "https://frutwebapi.svr.vc/api/NewOpla"
        Post(body: body) { id in
            PostImage.uploadMultipart(oplId: id, img: self.imageData)
        }
        ApiUrlString = s
    }
}
