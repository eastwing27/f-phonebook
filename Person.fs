module Person
open System
open Utils

type Person =
    {LastName: string
     FirstName: string
     Number: string}

let directName p =
    p.FirstName + " " + p.LastName

let backwardName p =
    p.LastName + " " + p.FirstName

let newPerson (person:string) = 
    let data = person.Split(" ")
    let count = data |> Array.length
    let first = 
        if count > 0 && data.[0] <> "" then
            data.[0]
        else
            (ask "First name")

    let last =
        if count > 1 then
            data.[1]
        else
            (ask "Last name")

    {LastName=last; 
     FirstName=first; 
     Number=(askNumber "Phone number (digits only)")}