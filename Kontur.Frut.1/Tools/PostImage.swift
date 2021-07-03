//
//  PostImage.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 30.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

class PostImage {
    
    static private let urlString = "https://frutwebapi.svr.vc/api/NewOpla/ImgUpload"
    static private var request = NSMutableURLRequest()
    static private var Opla_Id = 0
    static private var imageData = Data()
    
    static func uploadMultipart(oplId: Int, img: Data) {
        
        Opla_Id = oplId
        imageData = img
        
        createRequest()
        doit()
    }
    
    static private func createRequest()
    {
        request = NSMutableURLRequest(url: URL(string: urlString)!)
        request.httpMethod = "POST"
        
        let boundary = "---------------\(Opla_Id)";
        let contentType = "multipart/form-data;boundary=" + boundary
        request.setValue(contentType, forHTTPHeaderField: "Content-Type")
        
        let body = NSMutableData()
        
        body.append(Data("\r\n--\(boundary)\r\n".utf8))
        body.append(Data("Content-Disposition: form-data; name=\"image\"; filename=\"\(Opla_Id).png\"\r\n".utf8))
        body.append(Data("Content-Type: image/png\r\n\r\n".utf8))
        body.append(imageData)
        body.append(Data("\r\n--\(boundary)--\r\n".utf8))
        
        request.setValue("\(body.length)", forHTTPHeaderField: "Content-Length")
        request.httpBody = body as Data
    }
    
    static private func doit()
    {
        let session = URLSession(configuration: URLSessionConfiguration.default)
        let task = session.dataTask(with: request as URLRequest,
                                    completionHandler: { (data, response, error) -> Void in
            if error == nil {
                // Image uploaded
            }
        })
        task.resume()
        
    }
}
