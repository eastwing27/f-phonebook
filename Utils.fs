module Utils
open System

let printString s =
    printfn "%s" s

let ask message =
    printf "%s: " message
    Console.ReadLine()

let rec askNumber message =
    printf "%s: " message
    let input = Console.ReadLine()
    let valid =
        (input |> Seq.where (fun c -> "1234567890" |> Seq.contains c) |> Seq.length) 
            = Seq.length input
    if valid then
        input
    else 
        printfn "Please provide a correct integer value!"
        askNumber message

let format (number:string) =
    let count = Seq.length number
    match count with
     | i when i >= 11 -> 
        let head   = number |> Seq.take (i-10) |> String.Concat
        let tail   = number |> Seq.skip (Seq.length head)
        let first  = tail   |> Seq.take 3 |> String.Concat
        let second = tail   |> Seq.skip 3 |> Seq.take 3 |> String.Concat
        let third  = tail   |> Seq.skip 6 |> Seq.take 2 |>  String.Concat
        let fourth = tail   |> Seq.skip 8 |> String.Concat
        String.Format("{0} ({1}) {2}-{3}-{4}", head, first, second, third, fourth)
     | i when i = 6 || i = 7 ->
        let head = Seq.take (i-4) |> String.Concat
        let tail = number |> Seq.skip (Seq.length head)
        let first  = tail |> Seq.take 2 |> String.Concat
        let second = tail |> Seq.skip 2 |> String.Concat
        String.Format("{0}-{1}-{2}")
     | _ -> number