//
//  Progress.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 12.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import UIKit

fileprivate var aView: UIView?

extension UIViewController {
    
    func showSpiner() {
        aView = UIView(frame: self.view.bounds)
        aView?.backgroundColor = #colorLiteral(red: 0.2857941389, green: 0.5679222345, blue: 0.6074842215, alpha: 1)
        
        let ai = UIActivityIndicatorView(style: .large)
        ai.center = aView!.center
        ai.startAnimating()
        aView?.addSubview(ai)
        self.view.addSubview(aView!)
        
        Timer.scheduledTimer(withTimeInterval: 60, repeats: false) { (t) in
            self.removeSpiner()
        }
    }
    
    func removeSpiner() {
        aView?.removeFromSuperview()
        aView = nil
    }
}
