Imports System.IO
Module Module1

    Const MaxSize = 4
    Dim scoretxts() As String = {"You got one run!", "You got two runs!", "", "You got four runs!", "", "You got six runs!"}
    Dim appealtxts() As String = {"Not out!", "Caught!", "LBW!", "Bowled!"}
    Dim menu As SortedList = New SortedList

    Sub Main()
        Dim TopNames(MaxSize - 1) As String
        Dim TopScores(MaxSize - 1) As Integer
        Dim PlayerNames(2) As String
        Dim OptionSelected As Integer
        menu.Add(1, "1.  Play game version with virtual dice")
        menu.Add(2, "2.  Play game version with real dice")
        menu.Add(3, "3.  Load top scores")
        menu.Add(4, "4.  Display top scores")
        menu.Add(9, "9.  Quit")
        Randomize()
        ResetTopScores(TopNames, TopScores)
        For i = 0 To 1
            Console.Write("What is player #" & (i + 1) & "'s name? ")
            PlayerNames(i) = GetValidPlayerName()
        Next
        Do
            Do
                DisplayMenu()
                OptionSelected = GetMenuChoice()
            Loop Until menu.Contains(OptionSelected)
            Console.WriteLine()
            Select Case OptionSelected
                Case 1 : PlayDiceGame(PlayerNames, True, TopNames, TopScores)
                Case 2 : PlayDiceGame(PlayerNames, False, TopNames, TopScores)
                Case 3 : LoadTopScores(TopNames, TopScores)
                Case 4 : DisplayTopScores(TopNames, TopScores)
            End Select
        Loop Until OptionSelected = 9
    End Sub

    Sub ResetTopScores(ByRef TopNames() As String, ByRef TopScores() As Integer)
        For Count = 0 To MaxSize - 1
            TopNames(Count) = "-"
            TopScores(Count) = 0
        Next
    End Sub

    Function GetValidPlayerName() As String
        Dim PlayerName As String
        Do
            PlayerName = Console.ReadLine()
            If PlayerName = "" Then
                Console.Write("That is not a valid name.  Please try again: ")
            End If
        Loop Until PlayerName <> ""
        GetValidPlayerName = PlayerName
    End Function

    Sub DisplayMenu()
        Console.Clear()
        Console.WriteLine(vbCrLf + "Dice Cricket" + vbCrLf)
        For Each i As String In menu.Values
            Console.WriteLine(i)
        Next
        Console.WriteLine()
    End Sub

    Function GetMenuChoice() As Integer
        Console.Write("Please enter your choice: ") : Dim tmp As String = Console.ReadLine()
        If isWholeNumber(tmp) Then
            GetMenuChoice = tmp
            If Not menu.Contains(GetMenuChoice) Then Console.WriteLine(vbCrLf + "That was not one of the allowed options.  Please try again: ")
        Else : Console.WriteLine("Invalid input")
        End If
    End Function

    Function isWholeNumber(ByVal s As String) As Boolean
        Try
            Dim i As Integer = s
            Return i = s
        Catch : End Try
    End Function

    Function RollBowlDie(ByVal VirtualDiceGame As Boolean) As Integer
        Dim BowlDieResult As Integer
        If VirtualDiceGame Then
            BowlDieResult = Int(Rnd() * 6) + 1
        Else
            Console.WriteLine("Please roll the bowling die and then enter your result.")
            Console.WriteLine()
            Console.WriteLine("Enter 1 if the result is a 1")
            Console.WriteLine("Enter 2 if the result is a 2")
            Console.WriteLine("Enter 4 if the result is a 4")
            Console.WriteLine("Enter 6 if the result is a 6")
            Console.WriteLine("Enter 3 if the result is a 0")
            Console.WriteLine("Enter 5 if the result is OUT")
            Console.WriteLine()
            Console.Write("Result: ")
            BowlDieResult = Console.ReadLine 'Could cause crash if text entered
            Console.WriteLine()
        End If
        RollBowlDie = BowlDieResult
    End Function

    Sub DisplayRunsScored(ByVal RunsScored As Integer)
        Console.WriteLine(scoretxts(RunsScored - 1))
    End Sub

    Sub DisplayCurrentPlayerNewScore(ByVal CurrentPlayerScore As Integer)
        Console.WriteLine("Your new score is: " & CurrentPlayerScore) 'A whole sub for this :L
    End Sub

    Function RollAppealDie(ByVal VirtualDiceGame As Boolean) As Integer
        Dim AppealDieResult As Integer
        If VirtualDiceGame Then
            AppealDieResult = Int(Rnd() * 4) + 1
        Else
            Console.WriteLine("Please roll the appeal die and then enter your result." + vbCrLf)
            For i = 1 To appealtxts.Length
                Console.WriteLine("Enter " + i + " if the result is " + appealtxts(i - 1))
            Next
            Console.Write(vbCrLf + "Result: ")
            AppealDieResult = Console.ReadLine
            Console.WriteLine()
        End If
        RollAppealDie = AppealDieResult
    End Function

    Sub DisplayAppealDieResult(ByVal AppealDieResult As Integer)
        Console.WriteLine(appealtxts(AppealDieResult - 1))
    End Sub

    Sub DisplayResult(ByVal PlayerNames() As String, ByVal PlayerScores() As Integer)
        Console.WriteLine(vbCrLf + PlayerNames(0) & " your score was: " & PlayerScores(0))
        Console.WriteLine(PlayerNames(1) & " your score was: " & PlayerScores(1) & vbCrLf)
        If PlayerScores(0) > PlayerScores(1) Then
            Console.WriteLine(PlayerNames(0) & " wins!")
        ElseIf PlayerScores(1) > PlayerScores(0) Then
            Console.WriteLine(PlayerNames(1) & " wins!")
        Else
            Console.WriteLine("It's a draw!")
        End If
        Console.WriteLine()
    End Sub

    Sub UpdateTopScores(ByRef TopNames() As String, ByRef TopScores() As Integer, ByVal PlayerName As String, _
  ByVal PlayerScore As Integer)
        s(TopScores, TopNames)
        If PlayerScore > TopScores(MaxSize - 1) Then
            TopScores(MaxSize - 1) = PlayerScore
            TopNames(MaxSize - 1) = PlayerName
            Console.WriteLine("Well done " & PlayerName & " you have one of the top scores!")
        End If
        s(TopScores, TopNames)
        Dim fileWriter As StreamWriter = New StreamWriter("HiScores.txt")
        For i = 0 To MaxSize - 1
            fileWriter.WriteLine(TopNames(i) & "," & TopScores(i))
        Next
        fileWriter.Close()
    End Sub

    Sub s(ByRef s() As Integer, ByRef n() As String)
        Array.Sort(s, n)
        Array.Reverse(s)
        Array.Reverse(n)
    End Sub

    Sub DisplayTopScores(ByVal TopNames() As String, ByVal TopScores() As Integer)
        s(TopScores, TopNames)
        Console.WriteLine("The current top scores are: " + vbCrLf)
        For Count = 0 To MaxSize - 1
            Console.WriteLine(TopNames(Count) & " " & TopScores(Count))
        Next
        e()
    End Sub

    Sub LoadTopScores(ByRef TopNames() As String, ByRef TopScores() As Integer)
        FileOpen(1, "HiScores.txt", OpenMode.Input)
        Dim lineParts() As String
        Dim Count As Byte = 0
        While Not EOF(1)
            lineParts = LineInput(1).Split(",")
            TopNames(Count) = lineParts(0)
            TopScores(Count) = lineParts(1)
            Count += 1
        End While
        FileClose(1)
    End Sub

    Sub PlayDiceGame(ByVal PlayerNames() As String, ByVal VirtualDiceGame As Boolean, ByRef TopNames() As String, ByRef TopScores() As Integer)
        Dim PlayerOut As Boolean
        Dim CurrentPlayerScore As Integer
        Dim AppealDieResult As Integer
        Dim PlayerScores(2) As Integer
        Dim BowlDieResult As Integer
        For PlayerNo = 0 To 1
            CurrentPlayerScore = 0
            PlayerOut = False
            Console.WriteLine(vbCrLf + PlayerNames(PlayerNo) & " is batting")
            e()
            Do
                BowlDieResult = RollBowlDie(VirtualDiceGame)
                If BowlDieResult = 3 Then
                    Console.WriteLine("No runs scored this time.  Your score is still: " & _
                  CurrentPlayerScore)
                ElseIf BowlDieResult = 5 Then
                    Console.WriteLine("This could be out... press the Enter key to find out.")
                    Console.ReadLine()
                    AppealDieResult = RollAppealDie(VirtualDiceGame)
                    DisplayAppealDieResult(AppealDieResult)
                    If AppealDieResult >= 2 Then
                        PlayerOut = True
                    Else
                        PlayerOut = False
                    End If
                Else
                    DisplayRunsScored(BowlDieResult)
                    CurrentPlayerScore = CurrentPlayerScore + BowlDieResult
                    Console.WriteLine("Your new score is: " & CurrentPlayerScore)
                End If
                e()
            Loop Until PlayerOut
            Console.WriteLine("You are out.  Your final score was: " & CurrentPlayerScore)
            e()
            PlayerScores(PlayerNo) = CurrentPlayerScore
        Next
        DisplayResult(PlayerNames, PlayerScores)
        If PlayerScores(0) >= PlayerScores(1) Then
            UpdateTopScores(TopNames, TopScores, PlayerNames(0), PlayerScores(0))
            UpdateTopScores(TopNames, TopScores, PlayerNames(1), PlayerScores(1))
        Else
            UpdateTopScores(TopNames, TopScores, PlayerNames(1), PlayerScores(1))
            UpdateTopScores(TopNames, TopScores, PlayerNames(0), PlayerScores(0))
        End If
        e()
    End Sub

    Sub e()
        Console.WriteLine(vbCrLf + "Press the Enter key to continue")
        Console.ReadLine()
    End Sub

End Module