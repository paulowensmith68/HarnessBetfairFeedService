Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class MarketCatalogue
		<JsonProperty(PropertyName := "marketId")> _
		Public Property MarketId() As String

		<JsonProperty(PropertyName := "marketName")> _
		Public Property MarketName() As String

		<JsonProperty(PropertyName := "isMarketDataDelayed")> _
		Public Property IsMarketDataDelayed() As Boolean

		<JsonProperty(PropertyName := "description")> _
		Public Property Description() As MarketDescription

		<JsonProperty(PropertyName := "runners")> _
		Public Property Runners() As List(Of RunnerDescription)

		<JsonProperty(PropertyName := "eventType")> _
		Public Property EventType() As EventType

		<JsonProperty(PropertyName := "event")> _
		Public Property [Event]() As [Event]

		<JsonProperty(PropertyName := "competition")> _
		Public Property Competition() As Competition

		Public Overrides Function ToString() As String
			' well, don't bother displaying event/event type/competition
			Dim sb = (New StringBuilder()).AppendFormat("{0}", "MarketCatalogue").AppendFormat(" : Market={0}[{1}]", MarketId, MarketName).AppendFormat(" : IsMarketDataDelayed={0}", IsMarketDataDelayed)

			If Description IsNot Nothing Then
				sb.AppendFormat(" : {0}", Description)
			End If

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
