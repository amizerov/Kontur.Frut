//
//  StructRow.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 28.04.2021.
//

import Foundation

class RowData
{
    /*0*/var ID: Int = 0
    /*1*/var DTC: Date = Date()
    /*2*/var DataPP: Date = Date()
    /*3*/var Organy: String = ""
    /*4*/var Posred: String = ""
    /*5*/var Kontra: String = ""
    /*6*/var NomrPP: String = ""
    /*7*/var INN: String = ""
    /*8*/var SummPP: Double = 0.0
    /*9*/var PenyPr: Double = 0.0
    /*0*/var PenySu: Double = 0.0
    /*1*/var SummaZ: Double = 0.0
    /*2*/var Naznch: String = ""
    /*3*/var IsPaied: Bool = false
    /*4*/var IsReced: Bool = false
    /*5*/var IsVidan: Bool = false

    static func LoadRows(fromData d: Data) -> [RowData] {
        var ar = [RowData]()
        let a: [String] = String(data: d, encoding: .utf8)!.components(separatedBy: "},{")
        if (a.count == 0 || a[0] == "[]") {
            return ar
        }
        for e in a {
            let r = RowData.Load(from: e)
            ar.append(r)
        }
        return ar
    }
    
    static func Load(from s: String) -> RowData
    {
        let ss: String
            = s.replacingOccurrences(of: "{", with: "")
               .replacingOccurrences(of: "}", with: "")
               .replacingOccurrences(of: "]", with: "")
               .replacingOccurrences(of: "[", with: "")

        let a: [String] = ss.components(separatedBy: ",")
        
        let r = RowData()

        r.ID      = GetIntValue(a[0])
        r.DTC     = GetDateValue(a[1])
        r.DataPP  = GetDateValue(a[2])
        
        r.Organy  = GetStrValue(a[3])
        r.Kontra  = GetStrValue(a[5])
        
        r.Posred  = GetStrValue(a[4])
        r.NomrPP  = GetStrValue(a[6])
        r.INN     = GetStrValue(a[7])
        r.Naznch  = GetStrValue(a[12])

        r.SummPP  = GetDblValue(a[8])
        r.PenyPr  = GetDblValue(a[9])
        r.PenySu  = GetDblValue(a[10])
        r.SummaZ  = GetDblValue(a[11])
        
        r.IsPaied = GetBooValue(a[13])
        r.IsReced = GetBooValue(a[14])
        r.IsVidan = GetBooValue(a[15])

        return r
    }
    
    static func GetStrValue(_ s: String) -> String
    {
        var res =  s.components(separatedBy: ":")[1]
                    .replacingOccurrences(of: "\\\"", with: "'")
                    .replacingOccurrences(of: "\"", with: "")
        
        res = res.replacingOccurrences(of: "\'", with: "\"")
        
        return res
    }
    static func GetIntValue(_ s: String) -> Int {
        return Int(s.components(separatedBy: ":")[1])!
    }
    static func GetDateValue(_ s: String) -> Date {
        var isoDate = s.components(separatedBy: ":\"")[1]
        isoDate = isoDate.replacingOccurrences(of: "\"", with: "")
        isoDate = isoDate.replacingOccurrences(of: "T", with: " ")
        isoDate = isoDate.padding(toLength: 19, withPad: "", startingAt: 0)

        let dateFormatter = DateFormatter()
        dateFormatter.dateFormat = "yyyy-MM-dd HH:mm:ss"
        if let d = dateFormatter.date(from:isoDate) {
            return d
        }
        else {
            return Date()
        }
    }
    static func GetDblValue(_ s: String) -> Double {
        return Double(s.components(separatedBy: ":")[1])!
    }
    static func GetBooValue(_ s: String) -> Bool {
        return Bool(s.components(separatedBy: ":")[1]) ?? false
    }
}

/*
 [
     {
         \"ID\":58,
         \"Дата\":\"2021-04-28T00:00:00\”,
         \"Организация\":\"ООО \\\"МР-Ложистик\\\"\”,
         \"Посредник\":\"Халиддин\”,
         \"Контрагент\":null,
         \"№\":0,
         \"ИНН\":null,
         \"Сумма\":230.0000,
         \"%\":0,
         \"Штраф\":0.0000,
         \"Зачислено\":230.0000,
         \"Назначение\":\"За машину\”,
         \"Оплачено\":false,
         "Получено\":false,
         \"Выдано\”:false
     }
 ]
*/
