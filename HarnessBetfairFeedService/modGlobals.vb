Module modGlobals
    Public Account As New AccountClass()

    '-------------------------------------------------
    '-   OddsMatching database connection string                             -
    '-------------------------------------------------
    Public globalConnectionString = My.Settings.ConnectionString

    '-------------------------------------------------
    '-   BetFair Account credentials                             -
    '-------------------------------------------------
    Public globalBetFairUsername As String = My.Settings.BetFairAccountName
    Public globalBetFairPassword As String = My.Settings.BetFairAccountPassword
    Public globalBetFairAppKey As String = My.Settings.BetFairAccountAppKey
    Public globalBetFairToken As String = ""
    Public globalBetFairUrl As String = My.Settings.BetFairApiUrl

    '-------------------------------------------------
    '-   BetFair Filter arrays                             -
    '-------------------------------------------------
    Public globalBetFairFootballDaysAhead As Integer = My.Settings.FootballDaysAhead
    Public globalBetFairHorseRacingDaysAhead As Integer = My.Settings.HorseRacingDaysAhead

    '-------------------------------------------------
    '-   Logging objects                             -
    '-------------------------------------------------
    Public gobjEvent As EventLogger = New EventLogger

    '-------------------------------------------------
    '-   Constants                                   -
    '-------------------------------------------------
    Public Const cintShortWaitMillisecs As Integer = 1000
    Public Const cintLongWaitMillisecs As Integer = 5000
    Public Const cintCheckProcessActiveDelayMillisecs As Integer = 5000

    '-------------------------------------------------
    '-   Global                                      -
    '-------------------------------------------------
    Public gintMaximumExecThressholdMillisecs As Integer
    Public gintKillRuntimeThressholdMillisecs As Integer
    Public gblnProcessHasExited As Boolean

End Module
