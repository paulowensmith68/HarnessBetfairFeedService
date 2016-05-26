Imports MySql.Data.MySqlClient
Imports HarnessBetfairFeedService.Api_ng_sample_code.TO
Imports HarnessBetfairFeedService.Api_ng_sample_code
Public Class BetFairDatabaseClass

    ' Holds the connection string to the database used.
    Public connectionString As String = globalConnectionString
    Public eventList As New List(Of BefFairFootballEventClass)
    Public matchedList As New List(Of MatchedEventClass)

    'Holds message received back from class
    Public returnMessage As String = ""

    'Vars used for output message
    Private insertCount As Integer = 0
    Private updateCount As Integer = 0

    Public Sub PollBetFairEvents(eventTypeId As Integer, marketTypeCode As String, maxResults As String, marketCountries As HashSet(Of String), blnDeleteAll As Boolean)

        Dim Account As New AccountClass()
        Dim newEvent As BefFairFootballEventClass

        ' Login
        Account.Login()

        Dim client As IClient = Nothing
        Dim clientType As String = Nothing
        client = New JsonRpcClient(globalBetFairUrl, globalBetFairAppKey, globalBetFairToken)
        gobjEvent.WriteToEventLog("BetFairDatabaseClass : Starting to get list from marketCatalogue for Event Id: " + eventTypeId.ToString + " Market Type :" + marketTypeCode + " Market Countries: " + DisplaySet(marketCountries), EventLogEntryType.Information)

        Try

            Dim marketFilter = New MarketFilter()
            Dim eventTypes = client.listEventTypes(marketFilter)
            Dim eventypeIds As ISet(Of String) = New HashSet(Of String)()

            ' Football is eventId 1
            eventypeIds.Add(eventTypeId)

            'ListMarketCatalogue parameters
            Dim time = New TimeRange()
            time.From = Date.Now
            time.To = Date.Now.AddDays(globalBetFairDaysAhead)

            marketFilter = New MarketFilter()
            marketFilter.EventTypeIds = eventypeIds
            marketFilter.MarketStartTime = time

            ' Setup country codes required
            marketFilter.MarketCountries = marketCountries

            ' Set-up market type codes e.g. WIN or MATCH ODDS
            marketFilter.MarketTypeCodes = New HashSet(Of String)() From {marketTypeCode}

            ' Set InPlayOnly : Restrict to markets that are currently in play if True or are not currently in play if false. If not specified, returns both.
            marketFilter.InPlayOnly = False

            ' Set-up
            Dim marketSort = Api_ng_sample_code.TO.MarketSort.MAXIMUM_TRADED

            ' Set-up market projection
            Dim marketProjections As ISet(Of MarketProjection) = New HashSet(Of MarketProjection)()
            marketProjections.Add(MarketProjection.RUNNER_METADATA)
            marketProjections.Add(MarketProjection.EVENT)

            Dim marketCatalogues = client.listMarketCatalogue(marketFilter, marketProjections, marketSort, maxResults)
            gobjEvent.WriteToEventLog("BetFairDatabaseClass : Response from MarketCatalogue (event objects) : " + marketCatalogues.Count.ToString, EventLogEntryType.Information)

            For Each book In marketCatalogues
                Dim marketId As String = book.MarketId
                Dim marketIds As IList(Of String) = New List(Of String)()
                marketIds.Add(marketId)

                Dim priceData As ISet(Of PriceData) = New HashSet(Of PriceData)()
                'get all prices from the exchange
                priceData.Add(Api_ng_sample_code.TO.PriceData.EX_BEST_OFFERS)
                priceData.Add(Api_ng_sample_code.TO.PriceData.EX_TRADED)

                Dim priceProjection = New PriceProjection()
                priceProjection.PriceData = priceData

                Dim marketBook = client.listMarketBook(marketIds, priceProjection)

                ' Look through the market books, there should only be 1
                For Each layBet In marketBook

                    If marketBook.Count = 1 Then

                        ' Processing event...
                        gobjEvent.WriteToEventLog("BetFairDatabaseClass : Processing event : " + book.Event.Name, EventLogEntryType.Information)

                        ' Convert date to localtime
                        Dim gmtOpenDate As DateTime
                        gmtOpenDate = book.Event.OpenDate

                        'GMT Standard Time
                        Dim gmt As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time")
                        gmtOpenDate = TimeZoneInfo.ConvertTimeFromUtc(gmtOpenDate, gmt)

                        For i = 0 To layBet.Runners.Count - 1

                            If layBet.Runners(i).ExchangePrices.AvailableToLay.Count > 0 Then

                                If layBet.Runners(i).SelectionId = book.Runners(i).SelectionId Then

                                    'Create instance of database class
                                    newEvent = New BefFairFootballEventClass With {
                                     .eventTypeId = eventTypeId,
                                     .eventId = book.Event.Id.ToString,
                                     .name = book.Event.Name,
                                     .timezone = book.Event.Timezone,
                                     .countryCode = book.Event.CountryCode,
                                     .openDate = gmtOpenDate,
                                     .marketId = book.MarketId,
                                     .marketTypeCode = marketTypeCode,
                                     .marketName = book.MarketName,
                                     .betName = book.Runners(i).RunnerName,
                                     .price = layBet.Runners(i).ExchangePrices.AvailableToLay(0).Price,
                                     .size = layBet.Runners(i).ExchangePrices.AvailableToLay(0).Size
                                    }

                                    ' Add to list
                                    eventList.Add(newEvent)

                                Else
                                    gobjEvent.WriteToEventLog("BetFairDatabaseClass : SelectionId's do not match between marketCatalogue and marketBook for : " + book.Event.Name, EventLogEntryType.Warning)
                                End If

                            End If

                        Next ' End of runners

                    End If

                Next ' End of layBet

            Next ' End of book

        Catch apiExcepion As APINGException
            gobjEvent.WriteToEventLog("BetFairDatabaseClass : Error getting Api data, APINGExcepion msg : " + apiExcepion.Message, EventLogEntryType.Error)
            Exit Sub
        Catch ex As System.Exception
            gobjEvent.WriteToEventLog("BetFairDatabaseClass : Error getting Api data, system exception: " + ex.Message, EventLogEntryType.Error)
            Exit Sub

        Finally

            ' Logout
            Account.Logout()

        End Try


        '' Write to database
        Dim strResult As String
        gobjEvent.WriteToEventLog("BetFairDatabaseClass : Starting database update . . . . ", EventLogEntryType.Information)
        strResult = WriteEventList(eventTypeId, marketTypeCode, blnDeleteAll)
        gobjEvent.WriteToEventLog("BetFairDatabaseClass : Response from Database Update: : " + strResult, EventLogEntryType.Information)

    End Sub
    Private Shared Function MarketIdNothing(ByVal s As BefFairFootballEventClass) _
        As Boolean

        Return s.marketId Is Nothing

    End Function
    Public Sub MatchSoccerWithBookmakers(ByVal eventTypeId As Integer, ByVal marketTypeCode As String)
        '-----------------------------------------------------------------------*
        ' Sub Routine parameters                                                *
        ' -----------------------                                               *
        '   * eventTypeId   - Betfair eventTypeId e.g. 1=Soccer, 7=Horse Racing *
        '   * marketCode    - Betfair marketTypeCode e.g. MATCH_ODDS            *
        '-----------------------------------------------------------------------*
        Dim newMatched As MatchedEventClass

        Dim cno As MySqlConnection = New MySqlConnection(connectionString)
        Dim drBetfairEvents As MySqlDataReader
        Dim cmdBetfairEvents As New MySqlCommand

        ' /----------------------------------------------------------------\
        ' | MySql Select                                                   |
        ' | Get all betfair_events for this eventTypeId and marketTypeCode |
        ' |     * Event Type Id                                            | 
        ' |     * Market Type Code                                         |
        ' \----------------------------------------------------------------/
        cmdBetfairEvents.CommandText = "select `name`, openDate, price, size, betName, marketName from betfair_event AS bfe " &
                                           "where bfe.`eventTypeId` =@eventTypeId AND bfe.`marketTypeCode` =@marketTypeCode"
        cmdBetfairEvents.Parameters.AddWithValue("eventTypeId", eventTypeId)
        cmdBetfairEvents.Parameters.AddWithValue("marketTypeCode", marketTypeCode)
        cmdBetfairEvents.Connection = cno

        Try
            cno.Open()
            drBetfairEvents = cmdBetfairEvents.ExecuteReader

            If drBetfairEvents.HasRows Then

                While drBetfairEvents.Read()

                    ' Declare for later
                    Dim intEventIdSpocosy As Integer
                    Dim strEventStageSpocosy As String = ""

                    ' Declare and populate fields
                    Dim strBetfairEventName As String = drBetfairEvents.GetString(0)
                    Dim dtBetfairOpenDate As DateTime = drBetfairEvents.GetDateTime(1)
                    Dim dbBetfairPrice As Double = drBetfairEvents.GetDouble(2)
                    Dim dbBetfairSize As Double = drBetfairEvents.GetDouble(3)
                    Dim strBetfairBetName As String = drBetfairEvents.GetString(4)
                    Dim strMarketName As String = drBetfairEvents.GetString(5)

                    ' Open new cursor
                    Dim cno2 As MySqlConnection = New MySqlConnection(connectionString)
                    Dim drEvent As MySqlDataReader
                    Dim cmdEvent As New MySqlCommand
                    Dim strConvertedDetails As String
                    Dim strBetfairHomeTeam As String = GetBetfairFootballTeam(True, strBetfairEventName)
                    Dim strBetfairAwayTeam As String = GetBetfairFootballTeam(False, strBetfairEventName)

                    ' /----------------------------------------------------------------\
                    ' | MySql Select                                                   |
                    ' | Get Spocosy event identifier using:-                           |
                    ' |     * Event Name                                               | 
                    ' |     * Event Date                                               |
                    ' \----------------------------------------------------------------/
                    cmdEvent.CommandText = "select e.`id`, date_format(e.startDate, '%Y-%m-%d'), ts.name from event AS e INNER JOIN tournament_stage AS ts ON e.tournament_stageFK = ts.id where e.`name` =@eventName AND " &
                                           "date(e.startdate) = str_to_date(@startDate, '%Y-%m-%d')"
                    strConvertedDetails = ConvertEventName(eventTypeId, strBetfairEventName)
                    cmdEvent.Parameters.AddWithValue("eventName", strConvertedDetails)
                    Dim strBetfairOpenDate As String = dtBetfairOpenDate.ToString("yyyy-MM-dd")
                    cmdEvent.Parameters.AddWithValue("startDate", strBetfairOpenDate)

                    ' Reset matched event id
                    intEventIdSpocosy = 0

                    Try
                        cno2.Open()
                        cmdEvent.Connection = cno2
                        drEvent = cmdEvent.ExecuteReader()
                        While drEvent.Read()

                            Dim event_date As DateTime = drEvent.GetDateTime(1)
                            Dim strEventDate As String = event_date.ToString("yyyy-MM-dd")
                            If strBetfairOpenDate = strEventDate Then

                                ' Declare and populate fields
                                intEventIdSpocosy = drEvent.GetInt64(0)
                                strEventStageSpocosy = drEvent.GetString(2)

                            End If
                            ' TO ADD
                            ' Check whether more rows and report error, only expecting 1 row

                        End While
                        drEvent.Close()
                    Finally
                        cno2.Close()
                    End Try


                    '
                    ' Attempt a fuzzy match, using wilcard name and date
                    '
                    If intEventIdSpocosy = 0 Then

                        ' Open new cursor
                        Dim cno3 As MySqlConnection = New MySqlConnection(connectionString)
                        Dim drFuzzyEvent As MySqlDataReader
                        Dim cmdFuzzyEvent As New MySqlCommand

                        ' /----------------------------------------------------------------\
                        ' | MySql Select                                                   |
                        ' | Get Spocosy event identifier using:-                           |
                        ' |     * Event Name                                               | 
                        ' |     * Event Date                                               |
                        ' \----------------------------------------------------------------/
                        cmdFuzzyEvent.CommandText = "select e.`id`, date_format(e.startDate, '%Y-%m-%d'), ts.name from event AS e INNER JOIN tournament_stage AS ts ON e.tournament_stageFK = ts.id where e.`name` like @eventFuzzyName AND " &
                                                    "date(e.startdate) = str_to_date(@startDate, '%Y-%m-%d')"
                        Dim strFuzzyEventNameHome As String = strBetfairEventName.Substring(0, InStr(1, strBetfairEventName, " v ")).Trim
                        Dim strFuzzyEventNameAway As String = strBetfairEventName.Substring(Len(strFuzzyEventNameHome) + 2).Trim
                        cmdFuzzyEvent.Parameters.AddWithValue("eventFuzzyName", "%" + strFuzzyEventNameHome + "%-%" + strFuzzyEventNameAway + "%")
                        cmdFuzzyEvent.Parameters.AddWithValue("startDate", strBetfairOpenDate)

                        ' Reset matched id
                        intEventIdSpocosy = 0

                        Try
                            cno3.Open()
                            cmdFuzzyEvent.Connection = cno3
                            drFuzzyEvent = cmdFuzzyEvent.ExecuteReader()
                            While drFuzzyEvent.Read()

                                Dim event_date As DateTime = drFuzzyEvent.GetDateTime(1)
                                Dim strEventDate As String = event_date.ToString("yyyy-MM-dd")
                                If strBetfairOpenDate = strEventDate Then

                                    ' Declare and populate fields
                                    intEventIdSpocosy = drFuzzyEvent.GetInt64(0)
                                    strEventStageSpocosy = drEvent.GetString(2)

                                End If
                            End While
                        Catch ex As System.Exception
                        Finally
                            cno3.Close()
                        End Try

                    End If


                    '
                    ' Get the bet offers
                    '
                    If intEventIdSpocosy > 0 Then

                        Dim drBetOffer As MySqlDataReader
                        Dim cmdBetOffer As New MySqlCommand

                        ' /----------------------------------------------------------------\
                        ' | MySql Select                                                   |
                        ' | Get Spocosy betting odds                                       |
                        ' \----------------------------------------------------------------/
                        cmdBetOffer.CommandText = "SELECT op.`name` AS provider_name, ou.`scope`, ou.iparam, bt.`odds`, bt.`odds_old`, pa.name As participant_name, ou.dparam, ou.iparam2, ou.dparam2, ou.subtype, pa2.name As participant_name2 FROM " &
                                                "bettingoffer AS bt INNER JOIN outcome AS ou ON bt.`outcomeFK`=ou.`id` INNER JOIN " &
                                                "odds_provider AS op ON op.`id` = bt.`odds_providerFK` LEFT JOIN " &
                                                "participant as pa ON pa.id = ou.`iparam`  LEFT JOIN " &
                                                "participant As pa2 ON pa2.id = ou.`iparam2` " &
                                                "WHERE ou.`Object`='event' AND " &
                                                "ou.`objectFK`=@eventId AND " &
                                                "ou.`del`='no' AND " &
                                                "ou.`type` =@matchTypeCode AND " &
                                                "ou.`scope` =@scope AND " &
                                                "bt.`del`='no' AND " &
                                                "op.`del`='no' AND " &
                                                "bt.`active`='yes' AND " &
                                                "bt.`is_live`='no'"
                        cmdBetOffer.Parameters.AddWithValue("eventId", intEventIdSpocosy)
                        Select Case marketTypeCode
                            Case "MATCH_ODDS"
                                cmdBetOffer.Parameters.AddWithValue("scope", "ord")
                                cmdBetOffer.Parameters.AddWithValue("matchTypeCode", "1x2")
                            Case "HALF_TIME"
                                cmdBetOffer.Parameters.AddWithValue("scope", "1h")
                                cmdBetOffer.Parameters.AddWithValue("matchTypeCode", "1x2")
                            Case "HALF_TIME_FULL_TIME"
                                cmdBetOffer.Parameters.AddWithValue("scope", "ord")
                                cmdBetOffer.Parameters.AddWithValue("matchTypeCode", "ht_ft")
                            Case "OVER_UNDER_25"
                                cmdBetOffer.Parameters.AddWithValue("scope", "ord")
                                cmdBetOffer.Parameters.AddWithValue("matchTypeCode", "ou")
                            Case "CORRECT_SCORE"
                                cmdBetOffer.Parameters.AddWithValue("scope", "ord")
                                cmdBetOffer.Parameters.AddWithValue("matchTypeCode", "cs")
                        End Select

                        Try
                            cno2.Open()
                            cmdBetOffer.Connection = cno2
                            drBetOffer = cmdBetOffer.ExecuteReader

                            If drBetOffer.HasRows Then

                                While drBetOffer.Read()

                                    Dim provider_name As String = drBetOffer.GetString(0)
                                    Dim scope As String = drBetOffer.GetString(1)
                                    Dim iparam As Integer = drBetOffer.GetInt64(2)
                                    Dim odds As Double = drBetOffer.GetDouble(3)
                                    Dim odds_old As Double = drBetOffer.GetDouble(4)
                                    Dim strParticipant_name As String = ""
                                    If drBetOffer.IsDBNull(5) Then
                                        Select Case marketTypeCode
                                            Case "MATCH_ODDS"
                                                strParticipant_name = "The Draw"
                                            Case "HALF_TIME"
                                                strParticipant_name = "The Draw"
                                        End Select
                                    Else
                                        strParticipant_name = drBetOffer.GetString(5)
                                    End If
                                    Dim dparam As Double = drBetOffer.GetDouble(6)
                                    Dim iparam2 As Integer = drBetOffer.GetInt64(7)
                                    Dim dparam2 As Double = drBetOffer.GetDouble(8)
                                    Dim subtype As String = drBetOffer.GetString(9)
                                    Dim strParticipant_name2 As String = ""
                                    If drBetOffer.IsDBNull(10) Then
                                    Else
                                        strParticipant_name2 = drBetOffer.GetString(10)
                                    End If


                                    Dim blnStore As Boolean = False
                                    Select Case marketTypeCode
                                        Case "MATCH_ODDS"
                                            If strParticipant_name = strBetfairBetName Then
                                                blnStore = True
                                            End If

                                        Case "HALF_TIME"
                                            If strParticipant_name = strBetfairBetName Then
                                                blnStore = True
                                            End If

                                        Case "HALF_TIME_FULL_TIME"

                                            Dim strHalfTimeName As String = GetBetfairHalfTimeFullTimeTeam(True, strBetfairBetName)
                                            Dim strFullTimeName As String = GetBetfairHalfTimeFullTimeTeam(False, strBetfairBetName)
                                            Dim strBookmakersBetName As String = ""
                                            Dim strBookmakersHalfTime As String = ""
                                            Dim strBookmakersFullTime As String = ""

                                            ' Build bet name equivalent
                                            If strParticipant_name = "" Then
                                                strBookmakersHalfTime = "Draw"
                                            End If
                                            If strParticipant_name2 = "" Then
                                                strBookmakersFullTime = "Draw"
                                            End If
                                            If strParticipant_name.Contains(strBetfairHomeTeam) Then
                                                strBookmakersHalfTime = strBetfairHomeTeam
                                            End If
                                            If strParticipant_name.Contains(strBetfairAwayTeam) Then
                                                strBookmakersHalfTime = strBetfairAwayTeam
                                            End If
                                            If strParticipant_name2.Contains(strBetfairHomeTeam) Then
                                                strBookmakersFullTime = strBetfairHomeTeam
                                            End If
                                            If strParticipant_name2.Contains(strBetfairAwayTeam) Then
                                                strBookmakersFullTime = strBetfairAwayTeam
                                            End If
                                            strBookmakersBetName = strBookmakersHalfTime + "/" + strBookmakersFullTime

                                            ' Handle win/win
                                            If subtype = "win" Or subtype = "win_draw" Or subtype = "draw" Then
                                                If strBetfairBetName = strBookmakersBetName Then
                                                    strParticipant_name = strBetfairBetName
                                                    blnStore = True
                                                End If
                                            End If

                                        Case "OVER_UNDER_25"
                                            If dparam = 2.5 Then
                                                If subtype = "under" Then
                                                    If strBetfairBetName = "Under 2.5 Goals" Then
                                                        strParticipant_name = "Under 2.5"
                                                        blnStore = True
                                                    End If
                                                End If
                                                If subtype = "over" Then
                                                    If strBetfairBetName = "Over 2.5 Goals" Then
                                                        strParticipant_name = "Over 2.5"
                                                        blnStore = True
                                                    End If
                                                End If

                                            End If

                                        Case "CORRECT_SCORE"
                                            If subtype = "score" Then
                                                ' Check which way around correct score is stored
                                                ' Home team was in iparam, Away team in iparam2?
                                                If strParticipant_name.Contains(strBetfairHomeTeam) Then
                                                    If strBetfairBetName = dparam.ToString + " - " + dparam2.ToString Then
                                                        strParticipant_name = strBetfairBetName
                                                        blnStore = True
                                                    End If
                                                End If
                                                ' Reversed, Away team in iparam, Home team was in iparam2?
                                                If strParticipant_name.Contains(strBetfairAwayTeam) Then
                                                    If strBetfairBetName = dparam2.ToString + " - " + dparam.ToString Then
                                                        strParticipant_name = strBetfairBetName
                                                        blnStore = True
                                                    End If
                                                End If

                                            End If

                                    End Select

                                    ' Store the match
                                    If blnStore Then

                                        ' Calculate rating 
                                        Dim dblRating As Double = odds / dbBetfairPrice * 100

                                        ' Resolve bookmaker name to image
                                        Dim strBookmakerImageName = provider_name
                                        strBookmakerImageName = strBookmakerImageName.Replace(" ", "_")
                                        strBookmakerImageName = strBookmakerImageName.Replace(".", "_")
                                        strBookmakerImageName = strBookmakerImageName.Replace("-", "_")
                                        strBookmakerImageName = strBookmakerImageName.ToLower
                                        Dim strBookmakerImage As String = "/images/" + strBookmakerImageName + ".gif"

                                        'Create instance of Matched Event class
                                        newMatched = New MatchedEventClass With {
                                         .openDate = dtBetfairOpenDate,
                                         .eventTypeId = eventTypeId,
                                         .lay = dbBetfairPrice,
                                         .available = dbBetfairSize,
                                         .details = strBetfairEventName,
                                         .bookMaker = strBookmakerImage,
                                         .bookMakerName = provider_name,
                                         .bet = strParticipant_name,
                                         .exchange = "/images/betfair_exchange.gif",
                                         .type = ConvertMarketTypeCodeToDisplay(marketTypeCode),
                                         .back = odds,
                                         .rating = dblRating,
                                         .competitionName = strEventStageSpocosy
                                        }

                                        ' Add to list
                                        matchedList.Add(newMatched)

                                    End If

                                End While

                            Else

                                ' Report no bets found

                            End If

                            drBetOffer.Close()
                        Finally
                            cno2.Close()
                        End Try

                    End If

                End While ' End: Outer Loop

            End If

            ' Close the Data reader
            drBetfairEvents.Close()

        Finally
            cno.Close()
        End Try

        '' Write to database
        Dim strResult As String
        gobjEvent.WriteToEventLog("BetFairDatabaseClass : Starting database update for matched_events . . . . ", EventLogEntryType.Information)
        strResult = WriteMatchedList(eventTypeId, marketTypeCode)
        gobjEvent.WriteToEventLog("BetFairDatabaseClass : Response from matched_events Database Update: : " + strResult, EventLogEntryType.Information)


    End Sub

    Public Function ConvertEventName(ByVal eventTypeId As Integer, ByVal eventName As String) As String

        Dim strReturn As String = eventName

        ' Uses Betfair event type codes e.g. 1=Soccer, 7=Horse Racing
        If eventTypeId = 1 Then

            ' Soccer

            ' Replace v
            Dim strReplace As String = " v "
            Dim strWith As String = "-"
            strReturn = eventName.Replace(strReplace, strWith)

        ElseIf eventTypeId = 7 Then

            ' Horse Racing
            strReturn = eventName

        End If

        Return strReturn

    End Function

    Public Function GetBetfairFootballTeam(ByVal homeTeam As Boolean, ByVal eventName As String) As String

        Dim strReturn As String = eventName

        Dim strVersus As String = " v "
        Dim posVersus As Integer = eventName.IndexOf(strVersus, 0)
        If homeTeam Then
            strReturn = eventName.Substring(0, posVersus)
        Else
            strReturn = eventName.Substring(posVersus + 3)
        End If

        Return strReturn

    End Function
    Public Function GetBetfairHalfTimeFullTimeTeam(ByVal halftime As Boolean, ByVal betName As String) As String

        Dim strReturn As String = betName

        Dim strSlash As String = "/"
        Dim posSlash As Integer = betName.IndexOf(strSlash, 0)
        If halftime Then
            strReturn = betName.Substring(0, posSlash)
        Else
            strReturn = betName.Substring(posSlash + 1)
        End If

        Return strReturn

    End Function

    Public Function ConvertMarketTypeCodeToDisplay(ByVal marketTypeCode As String) As String

        Dim strReturn As String

        Select Case marketTypeCode
            Case "MATCH_ODDS"
                strReturn = "Match Odds"

            Case "HALF_TIME"
                strReturn = "Half Time"

            Case "HALF_TIME_FULL_TIME"
                strReturn = "HT/FT"

            Case "OVER_UNDER_25"
                strReturn = "Over Under"

            Case "CORRECT_SCORE"
                strReturn = "Correct Score"

            Case Else

                strReturn = marketTypeCode

        End Select

        Return strReturn
    End Function

    Private Function DeleteEvent(id As Integer) As Boolean

        Dim iReturn As Boolean
        Using SQLConnection As New MySqlConnection(connectionString)
            Using sqlCommand As New MySqlCommand()
                With sqlCommand
                    .CommandText = "delete From `betfair_football_event` where id =@id"
                    .Connection = SQLConnection
                    .CommandType = CommandType.Text
                    .Parameters.Add(New MySqlParameter("id", id))

                End With
                Try
                    sqlCommand.CommandTimeout = 300
                    SQLConnection.Open()
                    sqlCommand.ExecuteNonQuery()
                    iReturn = True
                    SQLConnection.Close()
                    iReturn = True

                Catch ex As MySqlException
                    gobjEvent.WriteToEventLog("BetFairDatabaseClass : Database Error deleting from betfair_football_event: " + ex.Message, EventLogEntryType.Error)
                    iReturn = False
                End Try
            End Using
        End Using

        Return iReturn

    End Function
    Private Shared Function DisplaySet(ByVal coll As HashSet(Of String)) As String
        Dim strReturn As String
        strReturn = "{"
        For Each i As String In coll
            strReturn = strReturn + " " + i
        Next i
        strReturn = strReturn + "}"
        Return strReturn
    End Function

    Private Function WriteEventList(eventTypeId As Integer, marketTypeCode As String, blnDeleteAll As Boolean) As String ''
        Dim cno As New MySqlConnection
        Dim cmd_del As New MySqlCommand
        Dim cmd As New MySqlCommand
        Dim SQLtrans As MySqlTransaction
        Dim num, num_del, i As Integer
        Dim msg As String = ""

        'Hard coding the connString this way is bad, but hopefully informative here.
        cno.ConnectionString = globalConnectionString

        ' Establish delete command
        cmd_del.Connection = cno
        cmd_del.CommandText = "delete From `betfair_event` where eventTypeId =@eventTypeId And marketTypeCode =@marketTypeCode"
        cmd_del.Parameters.Add("@eventTypeId", MySqlDbType.Int16)
        cmd_del.Parameters.Add("@marketTypeCode", MySqlDbType.String)

        ' Establish insert command
        cmd.Connection = cno
        cmd.Parameters.Add("@eventTypeId", MySqlDbType.Int16)
        cmd.Parameters.Add("@eventId", MySqlDbType.String)
        cmd.Parameters.Add("@marketId", MySqlDbType.String)
        cmd.Parameters.Add("@name", MySqlDbType.String)
        cmd.Parameters.Add("@countryCode", MySqlDbType.String)
        cmd.Parameters.Add("@timezone", MySqlDbType.String)
        cmd.Parameters.Add("@openDate", MySqlDbType.Timestamp)
        cmd.Parameters.Add("@price", MySqlDbType.Double)
        cmd.Parameters.Add("@size", MySqlDbType.Double)
        cmd.Parameters.Add("@betName", MySqlDbType.String)
        cmd.Parameters.Add("@marketTypeCode", MySqlDbType.String)
        cmd.Parameters.Add("@marketName", MySqlDbType.String)
        cmd.CommandText = "INSERT INTO `betfair_event` (`eventTypeId`,`eventId`,`marketId`,`name`,`countryCode`,`timezone`,`openDate`,`price`,`size`,`betName`,`marketTypeCode`,`marketName`) VALUES (@eventTypeId,@eventId,@marketId,@name,@countryCode,@timezone,@openDate,@price,@size,@betName,@marketTypeCode,@marketName)"

        num = 0
        Try
            cno.Open()
            'Must open connection before starting transaction.
            SQLtrans = cno.BeginTransaction()
            cmd.Transaction = SQLtrans
            Try

                ' Delete all first at start of refresh
                If blnDeleteAll Then

                    'Ok, delete all rows first
                    cmd_del.Parameters("@eventTypeId").Value = eventTypeId
                    cmd_del.Parameters("@marketTypeCode").Value = marketTypeCode
                    num_del += cmd_del.ExecuteNonQuery

                End If

                'Ok, this is where the inserts really take place. All the stuff around
                'is just to prepare for this and handle errors that may occur.
                For i = 0 To eventList.Count - 1
                    cmd.Parameters("@eventTypeId").Value = eventList(i).eventTypeId
                    cmd.Parameters("@eventId").Value = eventList(i).eventId
                    cmd.Parameters("@marketId").Value = eventList(i).marketId
                    cmd.Parameters("@name").Value = eventList(i).name
                    cmd.Parameters("@countryCode").Value = eventList(i).countryCode
                    cmd.Parameters("@timezone").Value = eventList(i).timezone
                    cmd.Parameters("@openDate").Value = eventList(i).openDate
                    cmd.Parameters("@price").Value = eventList(i).price
                    cmd.Parameters("@size").Value = eventList(i).size
                    cmd.Parameters("@betName").Value = eventList(i).betName
                    cmd.Parameters("@marketTypeCode").Value = eventList(i).marketTypeCode
                    cmd.Parameters("@marketName").Value = eventList(i).marketName
                    num += cmd.ExecuteNonQuery
                Next i
                'We are done. Now commit the transaction - actually change the DB.
                SQLtrans.Commit()
            Catch e1 As System.Exception
                'If anything went wrong attempt to rollback transaction
                Try
                    SQLtrans.Rollback()
                Catch e2 As System.Exception
                    'This is where you will be if the write went wrong AND the rollback failed.
                    'It's a bad place to be: Unable to rollback transaction - this REALLY hurts...
                    msg += "Unable To rollback transaction. " & e2.Message
                End Try
                msg += "Insert failed, transaction rolled back. " & e1.Message
            End Try
        Catch e3 As System.Exception
            msg += "Insert failed, might be unable To open connection. " & e3.Message
        Finally
            Try
                'Whatever happens, you will land here and attempt to close the connection.
                cno.Close()
            Catch e4 As System.Exception
                'If closing the connection goes wrong...
                msg += "I can't close connection. " & e4.Message
            End Try
        End Try

        msg += "Deleted rows : " + num_del.ToString + " Inserted rows : " + num.ToString
        Return msg

    End Function

    Private Function WriteMatchedList(eventTypeId As Integer, marketTypeCode As String) As String ''
        Dim cno As New MySqlConnection
        Dim cmd_del As New MySqlCommand
        Dim cmd As New MySqlCommand
        Dim SQLtrans As MySqlTransaction
        Dim num, num_del, i As Integer
        Dim msg As String = ""

        'Hard coding the connString this way is bad, but hopefully informative here.
        cno.ConnectionString = globalConnectionString

        ' Establish delete command
        cmd_del.Connection = cno
        cmd_del.CommandText = "delete From `matched_event` where betfairEventTypeId =@eventTypeId And betfairMarketTypeCode =@marketTypeCode"
        cmd_del.Parameters.Add("@eventTypeId", MySqlDbType.Int16)
        cmd_del.Parameters.Add("@marketTypeCode", MySqlDbType.String)

        ' Establish insert command
        cmd.Connection = cno
        cmd.Parameters.Add("@eventDate", MySqlDbType.Timestamp)
        cmd.Parameters.Add("@sport", MySqlDbType.String)
        cmd.Parameters.Add("@details", MySqlDbType.String)
        cmd.Parameters.Add("@betName", MySqlDbType.String)
        cmd.Parameters.Add("@marketName", MySqlDbType.String)
        cmd.Parameters.Add("@rating", MySqlDbType.Double)
        cmd.Parameters.Add("@info", MySqlDbType.String)
        cmd.Parameters.Add("@bookmaker", MySqlDbType.String)
        cmd.Parameters.Add("@bookmaker_name", MySqlDbType.String)
        cmd.Parameters.Add("@back", MySqlDbType.Double)
        cmd.Parameters.Add("@exchange", MySqlDbType.String)
        cmd.Parameters.Add("@lay", MySqlDbType.Double)
        cmd.Parameters.Add("@size", MySqlDbType.Double)
        cmd.Parameters.Add("@betfairEventTypeId", MySqlDbType.Int16)
        cmd.Parameters.Add("@betfairMarketTypeCode", MySqlDbType.String)
        cmd.Parameters.Add("@competitionName", MySqlDbType.String)
        cmd.Parameters.Add("@countryCode", MySqlDbType.String)
        cmd.Parameters.Add("@timezone", MySqlDbType.String)

        cmd.CommandText = "INSERT INTO `matched_event` (`eventDate`,`sport`,`details`,`betName`,`marketName`,`rating`,`info`,`bookmaker`,`bookmaker_name`,`back`,`exchange`,`lay`,`size`,`betfairEventTypeId`,`betfairMarketTypeCode`,`competitionName`,`countryCode`,`timezone`) VALUES (@eventDate,@sport,@details,@betName,@marketName,@rating,@info,@bookmaker,@bookmaker_name,@back,@exchange,@lay,@size,@betfairEventTypeId,@betfairMarketTypeCode,@competitionName,@countryCode,@timezone)"

        num = 0
        Try
            cno.Open()
            'Must open connection before starting transaction.
            SQLtrans = cno.BeginTransaction()
            cmd.Transaction = SQLtrans
            Try

                'Ok, delete all rows first
                cmd_del.Parameters("@eventTypeId").Value = eventTypeId
                cmd_del.Parameters("@marketTypeCode").Value = marketTypeCode
                num_del += cmd_del.ExecuteNonQuery

                'Ok, this is where the inserts really take place. All the stuff around
                'is just to prepare for this and handle errors that may occur.
                For i = 0 To matchedList.Count - 1

                    cmd.Parameters("@eventDate").Value = matchedList(i).openDate
                    If eventTypeId = 1 Then
                        cmd.Parameters("@sport").Value = "/images/football.png"
                    ElseIf eventTypeId = 7 Then
                        cmd.Parameters("@sport").Value = "/images/horse.png"
                    End If
                    cmd.Parameters("@details").Value = matchedList(i).details
                    cmd.Parameters("@betName").Value = matchedList(i).bet
                    cmd.Parameters("@marketName").Value = matchedList(i).type
                    cmd.Parameters("@rating").Value = matchedList(i).rating
                    cmd.Parameters("@info").Value = "/images/info.png"
                    cmd.Parameters("@bookmaker").Value = matchedList(i).bookMaker
                    cmd.Parameters("@bookmaker_name").Value = matchedList(i).bookMakerName
                    cmd.Parameters("@back").Value = matchedList(i).back
                    cmd.Parameters("@exchange").Value = matchedList(i).exchange
                    cmd.Parameters("@lay").Value = matchedList(i).lay
                    cmd.Parameters("@size").Value = matchedList(i).available
                    cmd.Parameters("@betfairEventTypeId").Value = matchedList(i).eventTypeId
                    cmd.Parameters("@betfairMarketTypeCode").Value = marketTypeCode
                    cmd.Parameters("@competitionName").Value = matchedList(i).competitionName
                    cmd.Parameters("@countryCode").Value = "GB"
                    cmd.Parameters("@timezone").Value = "tbc"

                    num += cmd.ExecuteNonQuery
                Next i
                'We are done. Now commit the transaction - actually change the DB.
                SQLtrans.Commit()
            Catch e1 As System.Exception
                'If anything went wrong attempt to rollback transaction
                Try
                    SQLtrans.Rollback()
                Catch e2 As System.Exception
                    'This is where you will be if the write went wrong AND the rollback failed.
                    'It's a bad place to be: Unable to rollback transaction - this REALLY hurts...
                    msg += "Unable To rollback transaction. " & e2.Message
                End Try
                msg += "Insert failed, transaction rolled back. " & e1.Message
            End Try
        Catch e3 As System.Exception
            msg += "Insert failed, might be unable To open connection. " & e3.Message
        Finally
            Try
                'Whatever happens, you will land here and attempt to close the connection.
                cno.Close()
            Catch e4 As System.Exception
                'If closing the connection goes wrong...
                msg += "I can't close connection. " & e4.Message
            End Try
        End Try

        msg += "Deleted rows : " + num_del.ToString + " Inserted rows : " + num.ToString
        Return msg

    End Function
End Class
