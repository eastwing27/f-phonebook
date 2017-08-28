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

let newPerson = 
    {LastName=(ask "Last name"); 
     FirstName=(ask "First name"); 
     Number=(ask "Phone number")}