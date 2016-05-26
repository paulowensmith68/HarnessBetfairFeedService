Public Class frmMain
    Private Sub btnRunFeed_Click(sender As Object, e As EventArgs) Handles btnRunFeed.Click

        Try

            Dim marketCountriesUkOnly As HashSet(Of String)
            marketCountriesUkOnly = New HashSet(Of String)({"GB"})
            Dim marketCountriesEurope As HashSet(Of String)
            marketCountriesEurope = New HashSet(Of String)({"FR", "DE", "IT", "ES", "PT", "NL", "GR"})

            ' Refresh database from BetFair API interface


            ' Soccer - eventTypeId = 1
            ' MarketTypeCodes applicable : "MATCH_ODDS", "HALF_TIME", "HALF_TIME_FULL_TIME", "OVER_UNDER_25", "CORRECT_SCORE", "BOTH_TEAMS_TO_SCORE"
            '
            If My.Settings.StreamSportId = 1 Then

                'gobjEvent.WriteToEventLog("StartProcess:    *--------------------------------------------------")
                'gobjEvent.WriteToEventLog("StartProcess:    *-----  BefFairFeedService - Updating Football ----")
                'gobjEvent.WriteToEventLog("StartProcess:    *--------------------------------------------------")
                'Dim BetFairDatabase1 As New BetFairDatabaseClass()
                'BetFairDatabase1.PollBetFairEvents(1, "MATCH_ODDS", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                'BetFairDatabase1 = Nothing

                ''' Europe leagues
                'Dim BetFairDatabase2 As New BetFairDatabaseClass()
                'BetFairDatabase2.PollBetFairEvents(1, "MATCH_ODDS", My.Settings.NumberOfEuropenEvents, marketCountriesEurope, False)
                'BetFairDatabase2 = Nothing

                ' Match new odds
                Dim BetFairDbOddsMatch1 As New BetFairDatabaseClass()
                BetFairDbOddsMatch1.MatchSoccerWithBookmakers(1, "MATCH_ODDS")
                BetFairDbOddsMatch1 = Nothing

                'Dim BetFairDatabase3 As New BetFairDatabaseClass()
                'BetFairDatabase3.PollBetFairEvents(1, "HALF_TIME", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                'BetFairDatabase3 = Nothing

                'Dim BetFairDatabase4 As New BetFairDatabaseClass()
                'BetFairDatabase4.PollBetFairEvents(1, "HALF_TIME_FULL_TIME", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                'BetFairDatabase4 = Nothing

                'Dim BetFairDatabase5 As New BetFairDatabaseClass()
                'BetFairDatabase5.PollBetFairEvents(1, "OVER_UNDER_25", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                'BetFairDatabase5 = Nothing

                ''' Match new odds
                'Dim BetFairDbOddsMatch5 As New BetFairDatabaseClass()
                'BetFairDbOddsMatch5.MatchSoccerWithBookmakers(1, "OVER_UNDER_25")
                'BetFairDbOddsMatch5 = Nothing

                'If My.Settings.Prcoess_CORRECT_SCORE Then

                '    'Dim BetFairDatabase6 As New BetFairDatabaseClass()
                '    'BetFairDatabase6.PollBetFairEvents(1, "CORRECT_SCORE", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                '    'BetFairDatabase6 = Nothing

                '    ' Match new odds
                '    Dim BetFairDbOddsMatch5 As New BetFairDatabaseClass()
                '    BetFairDbOddsMatch5.MatchSoccerWithBookmakers(1, "CORRECT_SCORE")
                '    BetFairDbOddsMatch5 = Nothing

                'End If
                ''' Match new odds
                'Dim BetFairDbOddsMatch5 As New BetFairDatabaseClass()
                'BetFairDbOddsMatch5.MatchSoccerWithBookmakers(1, "HALF_TIME_FULL_TIME")
                'BetFairDbOddsMatch5 = Nothing


                'Dim BetFairDatabase7 As New BetFairDatabaseClass()
                'BetFairDatabase7.PollBetFairEvents(1, "BOTH_TEAMS_TO_SCORE", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                'BetFairDatabase7 = Nothing

            End If

            ' ------------------------------
            ' Horse Racing - eventTypeId = 7
            ' ------------------------------
            ' MarketTypeCodes applicable : "WIN", "PLACE"
            '
            If My.Settings.StreamSportId = 7 Then

                'gobjEvent.WriteToEventLog("StartProcess:    *------------------------------------------------------")
                'gobjEvent.WriteToEventLog("StartProcess:    *-----  BefFairFeedService - Updating Horse Racing ----")
                'gobjEvent.WriteToEventLog("StartProcess:    *------------------------------------------------------")
                Dim BetFairHorseRacingDatabase1 As New BetFairDatabaseClass()
                BetFairHorseRacingDatabase1.PollBetFairEvents(7, "WIN", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                BetFairHorseRacingDatabase1 = Nothing

                'Dim BetFairHorseRacingDatabase2 As New BetFairDatabaseClass()
                'BetFairHorseRacingDatabase2.PollBetFairEvents(7, "PLACE", My.Settings.NumberOfUkEvents, marketCountriesUkOnly, True)
                'BetFairHorseRacingDatabase2 = Nothing

            End If

        Catch ex As Exception

            gobjEvent.WriteToEventLog("StartProcess : Process has been killed, general error : " & ex.Message, EventLogEntryType.Error)

        End Try

    End Sub
End Class
