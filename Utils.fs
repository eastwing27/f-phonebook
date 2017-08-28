module Utils
open System

let printString s =
    printfn "%s" s

let ask message =
    printf "%s: " message
    Console.ReadLine()