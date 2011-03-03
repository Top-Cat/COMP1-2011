Module Module1
 
    'Skeleton Program code for the AQA COMP1 Summer 2011 examination
    'this code should be used in conjunction with the Preliminary Material
    'written by the AQA COMP1 Programmer Team
    'developed in the Visual Studio 2008 (Console Mode) programming environment (VB.NET)
 
    Structure TTopScore
        Dim Name As String
        Dim Score As Integer
    End Structure
 
    Const MaxSize = 4
 
    Sub Main()
        Dim TopScores(MaxSize) As TTopScore
        Dim PlayerOneName As String
        Dim PlayerTwoName As String
        Dim OptionSelected As Integer
        Randomize()
        ResetTopScores(TopScores)
        Console.Write("What is player one's name? ")
        PlayerOneName = GetValidPlayerName()
        Console.Write("What is player two's name? ")
        PlayerTwoName = GetValidPlayerName()
        Do
            Do
                DisplayMenu()
                OptionSelected = GetMenuChoice()
            Loop Until (OptionSelected >= 1 And OptionSelected <= 4) Or OptionSelected = 9
            Console.WriteLine()
            If OptionSelected >= 1 And OptionSelected <= 4 Then
                Select Case OptionSelected
                    Case 1 : PlayDiceGame(PlayerOneName, PlayerTwoName, True, TopScores)
                    Case 2 : PlayDiceGame(PlayerOneName, PlayerTwoName, False, TopScores)
                    Case 3 : LoadTopScores(TopScores)
                    Case 4 : DisplayTopScores(TopScores)
                End Select
            End If
        Loop Until OptionSelected = 9
    End Sub
 
    Sub ResetTopScores(ByRef TopScores() As TTopScore)
        Dim Count As Integer
        For Count = 1 To MaxSize
            TopScores(Count).Name = "-"
            TopScores(Count).Score = 0
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
        OptionChosen = Console.ReadLine
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
            BowlDieResult = Console.ReadLine
            Console.WriteLine()
        End If
        RollBowlDie = BowlDieResult
    End Function
 
    Function CalculateRunsScored(ByVal BowlDieResult As Integer) As Integer
        Dim RunsScored As Integer
        Select Case BowlDieResult
            Case 1
                RunsScored = 1
            Case 2
                RunsScored = 2
            Case 3
                RunsScored = 4
            Case 4
                RunsScored = 6
            Case 5, 6
                RunsScored = 0
        End Select
        CalculateRunsScored = RunsScored
    End Function
 
    Sub DisplayRunsScored(ByVal RunsScored As Integer)
        Select Case RunsScored
            Case 1
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
        Console.WriteLine("Your new score is: " & CurrentPlayerScore)
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
 
    Sub DisplayResult(ByVal PlayerOneName As String, ByVal PlayerOneScore As Integer, _
  ByVal PlayerTwoName As String, ByVal PlayerTwoScore As Integer)
        Console.WriteLine()
        Console.WriteLine(PlayerOneName & " your score was: " & PlayerOneScore)
        Console.WriteLine(PlayerTwoName & " your score was: " & PlayerTwoScore)
        Console.WriteLine()
        If PlayerOneScore > PlayerTwoScore Then
            Console.WriteLine(PlayerOneName & " wins!")
        End If
        If PlayerTwoScore > PlayerOneScore Then
            Console.WriteLine(PlayerTwoName & " wins!")
        End If
        Console.WriteLine()
    End Sub
 
    Sub UpdateTopScores(ByRef TopScores() As TTopScore, ByVal PlayerName As String, _
  ByVal PlayerScore As Integer)
        Dim LowestCurrentTopScore As Integer
        Dim PositionOfLowestCurrentTopScore As Integer
        Dim Count As Integer
        LowestCurrentTopScore = TopScores(1).Score
        PositionOfLowestCurrentTopScore = 1
        ' Find the lowest of the current top scores       
        For Count = 2 To MaxSize
            If TopScores(Count).Score < LowestCurrentTopScore Then
                LowestCurrentTopScore = TopScores(Count).Score
                PositionOfLowestCurrentTopScore = Count
            End If
        Next
        If PlayerScore > LowestCurrentTopScore Then
            TopScores(PositionOfLowestCurrentTopScore).Score = PlayerScore
            TopScores(PositionOfLowestCurrentTopScore).Name = PlayerName
            Console.WriteLine("Well done " & PlayerName & " you have one of the top scores!")
        End If
    End Sub
 
    Sub DisplayTopScores(ByVal TopScores() As TTopScore)
        Dim Count As Integer
        Console.WriteLine("The current top scores are: ")
        Console.WriteLine()
        For Count = 1 To MaxSize
            Console.WriteLine(TopScores(Count).Name & " " & TopScores(Count).Score)
        Next
        Console.WriteLine()
        Console.WriteLine("Press the Enter key to return to the main menu")
        Console.ReadLine()
    End Sub
 
    Sub LoadTopScores(ByRef TopScores() As TTopScore)
        Dim Count As Integer
        Dim Count2 As Integer
        Dim LineFromFile As String
        Dim ValuesOnLine(2) As String
        FileOpen(1, "HiScores.txt", OpenMode.Input)
        For Count = 1 To MaxSize
            ValuesOnLine(1) = ""
            ValuesOnLine(2) = ""
            LineFromFile = LineInput(1)
            Count2 = 0
            Do
                ValuesOnLine(1) = ValuesOnLine(1) + LineFromFile(Count2)
                Count2 = Count2 + 1
            Loop Until LineFromFile(Count2) = ","
            Count2 = Count2 + 1
            Do
                ValuesOnLine(2) = ValuesOnLine(2) + LineFromFile(Count2)
                Count2 = Count2 + 1
            Loop Until Count2 = LineFromFile.Length
            TopScores(Count).Name = ValuesOnLine(1)
            TopScores(Count).Score = CInt(ValuesOnLine(2))
        Next
        FileClose(1)
    End Sub
 
    Sub PlayDiceGame(ByVal PlayerOneName As String, ByVal PlayerTwoName As String, _
  ByVal VirtualDiceGame As Boolean, ByRef TopScores() As TTopScore)
        Dim PlayerOut As Boolean
        Dim CurrentPlayerScore As Integer
        Dim AppealDieResult As Integer
        Dim PlayerNo As Integer
        Dim PlayerOneScore As Integer
        Dim PlayerTwoScore As Integer
        Dim BowlDieResult As Integer
        Dim RunsScored As Integer
        For PlayerNo = 1 To 2
            CurrentPlayerScore = 0
            PlayerOut = False
            Console.WriteLine()
            If PlayerNo = 1 Then
                Console.WriteLine(PlayerOneName & " is batting")
            Else
                Console.WriteLine(PlayerTwoName & " is batting")
            End If
            Console.WriteLine()
            Console.WriteLine("Press the Enter key to continue")
            Console.ReadLine()
            Do
                BowlDieResult = RollBowlDie(VirtualDiceGame)
                If BowlDieResult >= 1 And BowlDieResult <= 4 Then
                    RunsScored = CalculateRunsScored(BowlDieResult)
                    DisplayRunsScored(RunsScored)
                    CurrentPlayerScore = CurrentPlayerScore + RunsScored
                    Console.WriteLine("Your new score is: " & CurrentPlayerScore)
                End If
                If BowlDieResult = 5 Then
                    Console.WriteLine("No runs scored this time.  Your score is still: " & _
                                      CurrentPlayerScore)
                End If
                If BowlDieResult = 6 Then
                    Console.WriteLine("This could be out... press the Enter key to find out.")
                    Console.ReadLine()
                    AppealDieResult = RollAppealDie(VirtualDiceGame)
                    DisplayAppealDieResult(AppealDieResult)
                    If AppealDieResult >= 2 Then
                        PlayerOut = True
                    Else
                        PlayerOut = False
                    End If
                End If
                Console.WriteLine()
                Console.WriteLine("Press the Enter key to continue")
                Console.ReadLine()
            Loop Until PlayerOut
            Console.WriteLine("You are out.  Your final score was: " & CurrentPlayerScore)
            Console.WriteLine()
            Console.Write("Press the Enter key to continue")
            Console.ReadLine()
            If PlayerNo = 1 Then
                PlayerOneScore = CurrentPlayerScore
            Else
                PlayerTwoScore = CurrentPlayerScore
            End If
        Next
        DisplayResult(PlayerOneName, PlayerOneScore, PlayerTwoName, PlayerTwoScore)
        If PlayerOneScore >= PlayerTwoScore Then
            UpdateTopScores(TopScores, PlayerOneName, PlayerOneScore)
            UpdateTopScores(TopScores, PlayerTwoName, PlayerTwoScore)
        Else
            UpdateTopScores(TopScores, PlayerTwoName, PlayerTwoScore)
            UpdateTopScores(TopScores, PlayerOneName, PlayerOneScore)
        End If
        Console.WriteLine()
        Console.WriteLine("Press the Enter key to continue")
        Console.ReadLine()
    End Sub
End Module