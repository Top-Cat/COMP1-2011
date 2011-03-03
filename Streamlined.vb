Module Module1

    Const MaxSize = 4

    Sub Main()
        Dim TopNames(MaxSize) As String
        Dim TopScores(MaxSize) As Integer
        Dim PlayerNames(2) As String
        Dim OptionSelected As Integer
        Randomize()
        ResetTopScores(TopNames, TopScores)
        Console.Write("What is player one's name? ")
        PlayerNames(0) = GetValidPlayerName()
        Console.Write("What is player two's name? ")
        PlayerNames(1) = GetValidPlayerName()
        Do
            Do
                DisplayMenu()
                OptionSelected = GetMenuChoice()
            Loop Until (OptionSelected >= 1 And OptionSelected <= 4) Or OptionSelected = 9
            Console.WriteLine()
            If OptionSelected >= 1 And OptionSelected <= 4 Then
                Select Case OptionSelected
                    Case 1 : PlayDiceGame(PlayerNames, True, TopNames, TopScores)
                    Case 2 : PlayDiceGame(PlayerNames, False, TopNames, TopScores)
                    Case 3 : LoadTopScores(TopNames, TopScores)
                    Case 4 : DisplayTopScores(TopNames, TopScores)
                End Select
            End If
        Loop Until OptionSelected = 9
    End Sub

    Sub ResetTopScores(ByRef TopNames() As String, ByRef TopScores() As Integer)
        For Count = 1 To MaxSize
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
        Console.WriteLine()
        Console.WriteLine("Dice Cricket")
        Console.WriteLine()
        Console.WriteLine("1.  Play game version with virtual dice")
        Console.WriteLine("2.  Play game version with real dice")
        Console.WriteLine("3.  Load top scores")
        Console.WriteLine("4.  Display top scores")
        Console.WriteLine("9.  Quit")
        Console.WriteLine()
    End Sub

    Function GetMenuChoice() As Integer
        Dim OptionChosen As Integer
        Console.Write("Please enter your choice: ")
        OptionChosen = Console.ReadLine 'Could cause crash if text entered
        If OptionChosen < 1 Or (OptionChosen > 4 And OptionChosen <> 9) Then
            Console.WriteLine()
            Console.WriteLine("That was not one of the allowed options.  Please try again: ")
        End If
        GetMenuChoice = OptionChosen
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
            Console.WriteLine("Enter 3 if the result is a 4")
            Console.WriteLine("Enter 4 if the result is a 6")
            Console.WriteLine("Enter 5 if the result is a 0")
            Console.WriteLine("Enter 6 if the result is OUT")
            Console.WriteLine()
            Console.Write("Result: ")
            BowlDieResult = Console.ReadLine 'Could cause crash if text entered
            Console.WriteLine()
        End If
        RollBowlDie = BowlDieResult
    End Function

    Sub DisplayRunsScored(ByVal RunsScored As Integer)
        Select Case RunsScored 'Would be faster to use the number and not have to have seperate text for each
            Case 1 'or you could use a shared string and put the number into it
                Console.WriteLine("You got one run!")
            Case 2
                Console.WriteLine("You got two runs!")
            Case 4
                Console.WriteLine("You got four runs!")
            Case 6
                Console.WriteLine("You got six runs!")
        End Select
    End Sub

    Sub DisplayCurrentPlayerNewScore(ByVal CurrentPlayerScore As Integer)
        Console.WriteLine("Your new score is: " & CurrentPlayerScore) 'A whole sub for this :L
    End Sub

    Function RollAppealDie(ByVal VirtualDiceGame As Boolean) As Integer
        Dim AppealDieResult As Integer
        If VirtualDiceGame Then
            AppealDieResult = Int(Rnd() * 4) + 1
        Else
            Console.WriteLine("Please roll the appeal die and then enter your result.")
            Console.WriteLine()
            Console.WriteLine("Enter 1 if the result is NOT OUT")
            Console.WriteLine("Enter 2 if the result is CAUGHT")
            Console.WriteLine("Enter 3 if the result is LBW")
            Console.WriteLine("Enter 4 if the result is BOWLED")
            Console.WriteLine()
            Console.Write("Result: ")
            AppealDieResult = Console.ReadLine
            Console.WriteLine()
        End If
        RollAppealDie = AppealDieResult
    End Function

    Sub DisplayAppealDieResult(ByVal AppealDieResult As Integer)
        Select Case AppealDieResult
            Case 1
                Console.WriteLine("Not out!")
            Case 2
                Console.WriteLine("Caught!")
            Case 3
                Console.WriteLine("LBW!")
            Case 4
                Console.WriteLine("Bowled!")
        End Select
    End Sub

    Sub DisplayResult(ByVal PlayerNames() As String, ByVal PlayerOneScore As Integer, ByVal PlayerTwoScore As Integer)
        Console.WriteLine()
        Console.WriteLine(PlayerNames(0) & " your score was: " & PlayerOneScore)
        Console.WriteLine(PlayerNames(1) & " your score was: " & PlayerTwoScore)
        Console.WriteLine()
        If PlayerOneScore > PlayerTwoScore Then 'What if draw???
            Console.WriteLine(PlayerNames(0) & " wins!")
        End If
        If PlayerTwoScore > PlayerOneScore Then
            Console.WriteLine(PlayerNames(1) & " wins!")
        End If
        Console.WriteLine()
    End Sub

    Sub UpdateTopScores(ByRef TopNames() As String, ByRef TopScores() As Integer, ByVal PlayerName As String, _
  ByVal PlayerScore As Integer) ' May be asked to save new set of top scores
        Array.Sort(TopScores, TopNames)
        If PlayerScore > TopScores(MaxSize) Then
            TopScores(MaxSize) = PlayerScore
            TopNames(MaxSize) = PlayerName
            Console.WriteLine("Well done " & PlayerName & " you have one of the top scores!")
        End If
    End Sub

    Sub DisplayTopScores(ByVal TopNames() As String, ByVal TopScores() As Integer) 'May be asked to sort
        Array.Sort(TopScores, TopNames)
        Console.WriteLine("The current top scores are: " + vbCrLf)
        For Count = 1 To MaxSize
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
            Count += 1
            TopNames(Count) = lineParts(0)
            TopScores(Count) = lineParts(1)
        End While
        FileClose(1)
    End Sub

    Sub PlayDiceGame(ByVal PlayerNames() As String, ByVal VirtualDiceGame As Boolean, ByRef TopNames() As String, ByRef TopScores() As Integer)
        Dim PlayerOut As Boolean
        Dim CurrentPlayerScore As Integer
        Dim AppealDieResult As Integer
        Dim PlayerNo As Integer
        Dim PlayerOneScore As Integer
        Dim PlayerTwoScore As Integer
        Dim BowlDieResult As Integer
        For PlayerNo = 0 To 1
            CurrentPlayerScore = 0
            PlayerOut = False
            Console.WriteLine(vbCrLf + PlayerNames(PlayerNo) & " is batting")
            e()
            Do
                BowlDieResult = RollBowlDie(VirtualDiceGame)
                If BowlDieResult = 5 Then
                    Console.WriteLine("No runs scored this time.  Your score is still: " & _
                  CurrentPlayerScore)
                ElseIf BowlDieResult = 3 Then
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
            If PlayerNo = 0 Then
                PlayerOneScore = CurrentPlayerScore
            Else
                PlayerTwoScore = CurrentPlayerScore
            End If
        Next
        DisplayResult(PlayerNames, PlayerOneScore, PlayerTwoScore)
        If PlayerOneScore >= PlayerTwoScore Then
            UpdateTopScores(TopNames, TopScores, PlayerNames(0), PlayerOneScore)
            UpdateTopScores(TopNames, TopScores, PlayerNames(1), PlayerTwoScore)
        Else
            UpdateTopScores(TopNames, TopScores, PlayerNames(1), PlayerTwoScore)
            UpdateTopScores(TopNames, TopScores, PlayerNames(0), PlayerOneScore)
        End If
        e()
    End Sub

    Sub e()
        Console.WriteLine(vbCrLf + "Press the Enter key to continue")
        Console.ReadLine()
    End Sub
End Module