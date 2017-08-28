module Book
open System
open System.IO
open Person

let private serialize book =
    book
    |> List.map (fun record -> 
        record.LastName + "," +
        record.FirstName + "," +
        record.Number +
        Environment.NewLine)
    |> List.reduce (fun r s -> r + s) 

let private store path book =
    File.WriteAllText(path, serialize book)

let private deserialize (csv:seq<string>) =
    csv 
    |> Seq.map (fun row ->
            let values = row.Split(",")
            {LastName=values.[0]; FirstName=values.[1]; Number=values.[2]})
    |> Seq.toList

let private load path =
    deserialize (path |> File.ReadAllLines)

let mutable private storage = load "book.csv"

let book = 
    seq{for person in storage do yield person}

let add person =
    storage <- person :: storage
    printfn "%s %s added!" person.FirstName person.LastName

let remove person =
    storage <- storage |> List.filter (fun p -> p <> person)
    printfn "%s %s removed!" person.FirstName person.LastName

let save path =
    printfn "Saving changes..."
    storage 
    |> List.sortBy (fun p -> p.LastName, p.FirstName)
    |> store path