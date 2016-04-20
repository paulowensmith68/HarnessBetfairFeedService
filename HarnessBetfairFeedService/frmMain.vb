Public Class frmMain
    Private Sub btnRunFeed_Click(sender As Object, e As EventArgs) Handles btnRunFeed.Click

        Try

            Dim marketCountriesUkOnly As HashSet(Of String)
            marketCountriesUkOnly = New HashSet(Of String)({"GB"})
            Dim marketCountriesEurope As HashSet(Of String)
            marketCountriesEurope = New HashSet(Of String)({"FR", "DE", "IT", "ES"})

            ' Refresh database from BetFair API interface


            ' Football - eventTypeId = 1
            ' MarketTypeCodes applicable : "MATCH_ODDS", "HALF_TIME", "HALF_TIME_FULL_TIME", "OVER_UNDER_25", "CORRECT_SCORE", "BOTH_TEAMS_TO_SCORE"
            '
            gobjEvent.WriteToEventLog("StartProcess:    *--------------------------------------------------")
            gobjEvent.WriteToEventLog("StartProcess:    *-----  BefFairFeedService - Updating Football ----")
            gobjEvent.WriteToEventLog("StartProcess:    *--------------------------------------------------")
            'Dim BetFairDatabase1 As New BetFairDatabaseClass()
            'BetFairDatabase1.PollBetFairEvents(1, "MATCH_ODDS", 10, marketCountriesUkOnly)
            'BetFairDatabase1 = Nothing

            ' Match new odds
            Dim BetFairDbOddsMatch1 As New BetFairDatabaseClass()
            BetFairDbOddsMatch1.MatchWithBookmakers(1, "MATCH_ODDS")
            BetFairDbOddsMatch1 = Nothing

            '' Europe leagues
            'Dim BetFairDatabase2 As New BetFairDatabaseClass()
            'BetFairDatabase2.PollBetFairEvents(1, "MATCH_ODDS", 20, marketCountriesEurope)
            'BetFairDatabase2 = Nothing

            'Dim BetFairDatabase3 As New BetFairDatabaseClass()
            'BetFairDatabase3.PollBetFairEvents(1, "HALF_TIME", 50, marketCountriesUkOnly)
            'BetFairDatabase3 = Nothing

            'Dim BetFairDatabase4 As New BetFairDatabaseClass()
            'BetFairDatabase4.PollBetFairEvents(1, "HALF_TIME_FULL_TIME", 50, marketCountriesUkOnly)
            'BetFairDatabase4 = Nothing

            'Dim BetFairDatabase5 As New BetFairDatabaseClass()
            'BetFairDatabase5.PollBetFairEvents(1, "OVER_UNDER_25", 50, marketCountriesUkOnly)
            'BetFairDatabase5 = Nothing

            'Dim BetFairDatabase6 As New BetFairDatabaseClass()
            'BetFairDatabase6.PollBetFairEvents(1, "CORRECT_SCORE", 50, marketCountriesUkOnly)
            'BetFairDatabase6 = Nothing

            'Dim BetFairDatabase7 As New BetFairDatabaseClass()
            'BetFairDatabase7.PollBetFairEvents(1, "BOTH_TEAMS_TO_SCORE", 50, marketCountriesUkOnly)
            'BetFairDatabase7 = Nothing

            '' ------------------------------
            '' Horse Racing - eventTypeId = 7
            '' ------------------------------
            '' MarketTypeCodes applicable : "WIN", "PLACE"
            ''
            'gobjEvent.WriteToEventLog("StartProcess:    *------------------------------------------------------")
            'gobjEvent.WriteToEventLog("StartProcess:    *-----  BefFairFeedService - Updating Horse Racing ----")
            'gobjEvent.WriteToEventLog("StartProcess:    *------------------------------------------------------")
            'Dim BetFairHorseRacingDatabase1 As New BetFairDatabaseClass()
            'BetFairHorseRacingDatabase1.PollBetFairEvents(7, "WIN", 50, marketCountriesUkOnly)
            'BetFairHorseRacingDatabase1 = Nothing

            'Dim BetFairHorseRacingDatabase2 As New BetFairDatabaseClass()
            'BetFairHorseRacingDatabase2.PollBetFairEvents(7, "PLACE", 50, marketCountriesUkOnly)
            'BetFairHorseRacingDatabase2 = Nothing

        Catch ex As Exception

            gobjEvent.WriteToEventLog("StartProcess : Process has been killed, general error : " & ex.Message, EventLogEntryType.Error)

        End Try

    End Sub
End Class
