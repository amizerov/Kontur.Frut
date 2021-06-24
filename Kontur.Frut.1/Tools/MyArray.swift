//
//  MyArray.swift
//  Kontur.Frut.1
//
//  Created by Andrey Mizerov on 17.06.2021.
//  Copyright © 2021 ООО "Ультра Зум". All rights reserved.
//

import Foundation

public class Arr<T: Equatable>: RangeReplaceableCollection {

    public typealias Element = T
    public typealias Index = Int
    public typealias SubSequence = Arr<T>
    public typealias Indices = Range<Int>
    fileprivate var array = Array<T>()

    public var startIndex: Int { return array.startIndex }
    public var endIndex: Int { return array.endIndex }
    public var indices: Range<Int> { return array.indices }


    public func index(after i: Int) -> Int {
        return array.index(after: i)
    }

    public required init() {}
}

// Instance Methods

extension Arr {

    public func append(_ newElement: Arr.Element) {
        array.append(newElement)
    }

    public func append<S>(contentsOf newElements: S) where S : Sequence, Arr.Element == S.Element {
        array.append(contentsOf: newElements)
    }

    func filter(_ isIncluded: (Arr.Element) throws -> Bool) rethrows -> Arr {
        let subArray = try array.filter(isIncluded)
        return Arr(subArray)
    }

    public func insert(_ newElement: Arr.Element, at i: Arr.Index) {
        array.insert(newElement, at: i)
    }

    public func insert<S>(contentsOf newElements: S, at i: Arr.Index) where S : Collection, Arr.Element == S.Element {
        array.insert(contentsOf: newElements, at: i)
    }

    func popLast() -> Arr.Element? {
        return array.popLast()
    }

    @discardableResult public func remove(at i: Arr.Index) -> Arr.Element {
        return array.remove(at: i)
    }

    public func removeAll(keepingCapacity keepCapacity: Bool) {
        array.removeAll()
    }

    public func removeAll(where shouldBeRemoved: (Arr.Element) throws -> Bool) rethrows {
        try array.removeAll(where: shouldBeRemoved)
    }

    @discardableResult public func removeFirst() -> Arr.Element {
        return array.removeFirst()
    }

    public func removeFirst(_ k: Int) {
        array.removeFirst(k)
    }
    @discardableResult func removeLast() -> Arr.Element {
        return array.removeLast()
    }

    func removeLast(_ k: Int) {
        array.removeLast(k)
    }

    public func removeSubrange(_ bounds: Range<Int>) {
        array.removeSubrange(bounds)
    }

    public func replaceSubrange<C, R>(_ subrange: R, with newElements: C) where C : Collection, R : RangeExpression, T == C.Element, Arr<T>.Index == R.Bound {
        array.replaceSubrange(subrange, with: newElements)
    }

    public func reserveCapacity(_ n: Int) {
        array.reserveCapacity(n)
    }
}

// Subscripts

extension Arr {

    public subscript(bounds: Range<Arr.Index>) -> Arr.SubSequence {
        get { return Arr(array[bounds]) }
    }

    public subscript(bounds: Arr.Index) -> Arr.Element {
        get { return array[bounds] }
        set(value) { array[bounds] = value }
    }
}

// Operator Functions

extension Arr {

    static func + <Other>(lhs: Other, rhs: Arr) -> Arr where Other : Sequence, Arr.Element == Other.Element {
        return Arr(lhs + rhs.array)
    }

    static func + <Other>(lhs: Arr, rhs: Other) -> Arr where Other : Sequence, Arr.Element == Other.Element{
         return Arr(lhs.array + rhs)
    }

    static func + <Other>(lhs: Arr, rhs: Other) -> Arr where Other : RangeReplaceableCollection, Arr.Element == Other.Element {
        return Arr(lhs.array + rhs)
    }

    static func + (lhs: Arr<T>, rhs: Arr<T>) -> Arr {
        return Arr(lhs.array + rhs.array)
    }

    static func += <Other>(lhs: inout Arr, rhs: Other) where Other : Sequence, Arr.Element == Other.Element {
        lhs.array += rhs
    }
}

extension Arr: Equatable {
    public static func == (lhs: Arr<T>, rhs: Arr<T>) -> Bool {
        return lhs.array == rhs.array
    }
}

extension Arr: CustomStringConvertible {
    public var description: String { return "\(array)" }
}
