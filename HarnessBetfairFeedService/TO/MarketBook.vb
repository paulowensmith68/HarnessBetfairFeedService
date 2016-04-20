Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class MarketBook
		<JsonProperty(PropertyName := "marketId")> _
		Public Property MarketId() As String

		<JsonProperty(PropertyName := "isMarketDataDelayed")> _
		Public Property IsMarketDataDelayed() As Boolean

		<JsonProperty(PropertyName := "status")> _
		Public Property Status() As MarketStatus

		<JsonProperty(PropertyName := "betDelay")> _
		Public Property BetDelay() As Integer

		<JsonProperty(PropertyName := "bspReconciled")> _
		Public Property IsBspReconciled() As Boolean

		<JsonProperty(PropertyName := "complete")> _
		Public Property IsComplete() As Boolean

		<JsonProperty(PropertyName := "inplay")> _
		Public Property IsInplay() As Boolean

		<JsonProperty(PropertyName := "numberOfWinners")> _
		Public Property NumberOfWinners() As Integer

		<JsonProperty(PropertyName := "numberOfRunners")> _
		Public Property NumberOfRunners() As Integer

		<JsonProperty(PropertyName := "numberOfActiveRunners")> _
		Public Property NumberOfActiveRunners() As Integer

		<JsonProperty(PropertyName := "lastMatchTime")> _
		Public Property LastMatchTime() As Date?

		<JsonProperty(PropertyName := "totalMatched")> _
		Public Property TotalMatched() As Double

		<JsonProperty(PropertyName := "totalAvailable")> _
		Public Property TotalAvailable() As Double

		<JsonProperty(PropertyName := "crossMatching")> _
		Public Property IsCrossMatching() As Boolean

		<JsonProperty(PropertyName := "runnersVoidable")> _
		Public Property IsRunnersVoidable() As Boolean

		<JsonProperty(PropertyName := "version")> _
		Public Property Version() As Long

		<JsonProperty(PropertyName := "runners")> _
		Public Property Runners() As List(Of Runner)

		Public Overrides Function ToString() As String
			Dim sb = (New StringBuilder()).AppendFormat("{0}", "MarketBook").AppendFormat(" : MarketId={0}", MarketId).AppendFormat(" : Status={0}", Status).AppendFormat(" : BetDelay={0}", BetDelay).AppendFormat(" : IsMarketDataDelayed={0}", IsMarketDataDelayed).AppendFormat(" : IsBspReconciled={0}", IsBspReconciled).AppendFormat(" : IsComplete={0}", IsComplete).AppendFormat(" : IsInplay={0}", IsInplay).AppendFormat(" : IsCrossMatching={0}", IsCrossMatching).AppendFormat(" : IsRunnersVoidable={0}", IsRunnersVoidable).AppendFormat(" : NumberOfWinners={0}", NumberOfWinners).AppendFormat(" : NumberOfRunners={0}", NumberOfRunners).AppendFormat(" : NumberOfActiveRunners={0}", NumberOfActiveRunners).AppendFormat(" : LastMatchTime={0}", LastMatchTime).AppendFormat(" : TotalMatched={0}", TotalMatched).AppendFormat(" : TotalAvailable={0}", TotalAvailable).AppendFormat(" : Version={0}", Version)

			If Runners IsNot Nothing AndAlso Runners.Count > 0 Then
				Dim idx As Integer = 0
				For Each runner In Runners
					sb.AppendFormat(" : Runner[{0}]={1}", idx, runner)
					idx += 1
				Next runner
			End If

			Return sb.ToString()
		End Function
	End Class
End Namespace
