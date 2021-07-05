//
//  PictureOrderVC.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 04.07.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

class PictOrderVC: UIViewController {

    public var img: UIImage?
    @IBOutlet weak var imageView: UIImageView!
    
    override func viewDidLoad() {
        super.viewDidLoad()

        imageView.image = img
    }
}
