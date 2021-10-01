//
//  HttpRequest.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 03.05.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

var CurrentUser = "none"
var ApiUrlString = "https://frutwebapi.svr.vc/api/values"

func SetProcent(oid: Int, fin: String, val: Double, user: String = "iPhone") {
    let body: [String: Any] =
    [
        "Oplata_ID": oid,
        "FieldName": fin,
        "IntVal": Int(val),
        "usr": user
    ]
    Post(body: body) {_ in }
}

func SetValue(oid: Int, fin: String, val: Bool, user: String = "iPhone")
{
    let body: [String: Any] =
    [
        "Oplata_ID": oid,
        "FieldName": fin,
        "BoolVal": val,
        "usr": user
    ]
    Post(body: body) { _ in }
}

func Post(body: [String: Any], completion: @escaping (_ id: Int) -> ())
{
    let api_url = ApiUrlString
    let url = URL(string: api_url)!
    var request = URLRequest(url: url)

    do {
        let jsonData = try JSONSerialization.data(withJSONObject: body, options: .prettyPrinted)
        request.httpBody = jsonData
    } catch let e {
        print(e)
    }

    request.httpMethod = "POST"
    request.addValue("application/json", forHTTPHeaderField: "Content-Type")

    let task = URLSession.shared.dataTask(with: request) { data, response, error in
        guard let data = data, error == nil else {
            print(error?.localizedDescription ?? "No data")
            return
        }
        do {
            let jsonArr = try JSONSerialization.jsonObject(with: data, options: []) as! [[String: Any]]
            
            var id = 0
            for dic in jsonArr {
                id = dic["id"] as! Int
                completion(id)
            }
            print("json: \(jsonArr)")
        }
        catch let err {
            print("Error: \(err)")
        }
    }

    task.resume()
}

