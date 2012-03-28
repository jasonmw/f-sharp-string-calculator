    module Tests
    open Xunit
    open System

    type Calculator() =
        let delimiters = ",\n"
        member x.Add (m:int, n:int list) =
            match n with
            |[] -> m
            |y::ys -> 
                if y < 0 then failwith "No Negative Numbers"
                x.Add((m+y),ys)
        member x.Add (y:string) =
            let numList = List.map (fun x -> x.ToString() |> Convert.ToInt32) (delimiters.ToCharArray() |> y.Split |> Seq.toList)
            x.Add(0,numList)

    [<Fact>]
    let ReturnsNotNull() = 
        let calc = new Calculator()
        Assert.NotNull (calc.Add "0,0")

    [<Fact>]
    let ReturnsZeroWhenZeros() = 
        let calc = new Calculator()
        Assert.Equal(0,(calc.Add "0,0"))

    [<Fact>]
    let ReturnsOneWhenShouldBeOneOnLeft() = 
        let calc = new Calculator()
        Assert.Equal(1,(calc.Add "1,0"))
    [<Fact>]
    let ReturnsOneWhenShouldBeOneOnRight() = 
        let calc = new Calculator()
        Assert.Equal(1,(calc.Add "0,1"))
    [<Fact>]
    let ReturnsElevenWithStringOfNumbersThatTotalEleven() = 
        let calc = new Calculator()
        Assert.Equal(11,(calc.Add "0,1,1,1,1,1,6"))
    [<Fact>]
    let ReturnsElevenWithStringOfNumbersThatTotalElevenDelimitedByNewLine() = 
        let calc = new Calculator()
        Assert.Equal(11,(calc.Add "0,1,1\n1,2\n6"))
    [<Fact>]
    let ReturnsElevenWithStringOfNumbersThatTotalElevenDelimitedByNewLineNoNegativeNumbers() = 
        let calc = new Calculator()
        try
            calc.Add("0,1,-1\n1,2\n6") |> ignore
            Assert.False true
        with
            | _ -> Assert.True true
