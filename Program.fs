open System
open Utils
open Person
open Book

let isIntersect (first:string) (second:string) =
    let upperFirst = first.ToUpper()
    let upperSecond = second.ToUpper()
    upperFirst.Contains(upperSecond) || upperSecond.Contains(upperFirst)

let toString p =
    String.Format("Person: {0} {1}, Phone: {2}", p.FirstName, p.LastName, p.Number)

let flatten pair =
    let head = string (fst pair) |> Seq.singleton
    let tail = (snd pair) |> Seq.map toString
    Seq.concat [head; tail]

let print set =
    if set |> Seq.length < 4 then
        set
        |> Seq.iter (toString >> printString)
    else
        (Seq.collect 
            flatten 
                (set
                 |> Seq.groupBy(fun p -> p.LastName |> Seq.head)
                 |> Seq.sortBy(fun pair -> fst pair)))
        |> Seq.iter (fun s -> printfn "%s" s)

let find request =
    if request |> String.IsNullOrEmpty then
        printfn "Please provide the search criteria. If you want to get all records, type \"list\""
        Seq.empty
    else
        book
        |> Seq.filter (fun p -> 
            request |> isIntersect p.LastName || 
            request |> isIntersect p.FirstName || 
            request |> isIntersect p.Number || 
            request |> isIntersect (p |> directName) || 
            request |> isIntersect (p |> backwardName))

let findAndPrint request =
    let result = find request
    if result |> Seq.isEmpty then
        printfn "Nothing found"
    else
        print result

let rec chooseFromSet set choice =
    if set |> Seq.map fst |> Seq.contains choice then
        set 
        |> Seq.find (fun p -> choice = fst p)
        |> snd
    else
        printfn "\nPlease specify record to remove"
        set |> Seq.iter (fun item -> printfn "%d) %s, %s" (fst item) ((snd item).LastName) ((snd item).FirstName))
        ask "Number from list above" |> int |> chooseFromSet set

let findAndRemove person =
    let found = find person
    match found |> Seq.length with
     | 0 -> printfn "There is no such record!"
     | 1 -> remove (found |> Seq.head)
     | _ -> -1 |> chooseFromSet (found |> Seq.indexed) |> remove

let rec react request =
    if request = "exit" || request = "cancel" then
        printfn "Bye!"
        0
    else
        let input = ask ">>"
        let cmd = input.Split(" ")

        match cmd.[0] with
         | "list" -> print book
         | "find" -> findAndPrint (String.Join(' ', cmd |> Array.skip 1))
         | "add" -> add newPerson
         | "remove" -> findAndRemove (String.Join(' ', cmd |> Array.skip 1))
         | "save" -> save "book.csv"
         | "cancel" -> printfn "Changes weren't saved"
         | "exit" -> save "book.csv"
         | "" -> printf ""
         | _ -> printfn "%s unrecognized" cmd.[0]
         
        react input

[<EntryPoint>]
let main argv =
    "" |> react
