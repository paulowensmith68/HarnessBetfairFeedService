Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class Runner
		<JsonProperty(PropertyName := "selectionId")> _
		Public Property SelectionId() As Long

		<JsonProperty(PropertyName := "handicap")> _
		Public Property Handicap() As Double?

		<JsonProperty(PropertyName := "status")> _
		Public Property Status() As RunnerStatus

		<JsonProperty(PropertyName := "adjustmentFactor")> _
		Public Property AdjustmentFactor() As Double?

		<JsonProperty(PropertyName := "lastPriceTraded")> _
		Public Property LastPriceTraded() As Double?

		<JsonProperty(PropertyName := "totalMatched")> _
		Public Property TotalMatched() As Double

		<JsonProperty(PropertyName := "removalDate")> _
		Public Property RemovalDate() As Date?

		<JsonProperty(PropertyName := "sp")> _
		Public Property StartingPrices() As StartingPrices

		<JsonProperty(PropertyName := "ex")> _
		Public Property ExchangePrices() As ExchangePrices

		<JsonProperty(PropertyName := "orders")> _
		Public Property Orders() As List(Of Order)

		<JsonProperty(PropertyName := "matches")> _
		Public Property Matches() As List(Of Match)

		Public Overrides Function ToString() As String
			Dim sb = (New StringBuilder()).AppendFormat("SelectionId={0}", SelectionId).AppendFormat(" : Handicap={0}", Handicap).AppendFormat(" : Status={0}", Status).AppendFormat(" : AdjustmentFactor={0}", AdjustmentFactor).AppendFormat(" : LastPriceTraded={0}", LastPriceTraded).AppendFormat(" : TotalMatched={0}", TotalMatched).AppendFormat(" : RemovalDate={0}", RemovalDate)

			If StartingPrices IsNot Nothing Then
				sb.AppendFormat(": {0}", StartingPrices)
			End If

			If ExchangePrices IsNot Nothing Then
				sb.AppendFormat(": {0}", ExchangePrices)

			End If

			If Orders IsNot Nothing AndAlso Orders.Count > 0 Then
				Dim idx As Integer = 0
				For Each order In Orders
					sb.AppendFormat(" : Order[{0}]={1}", idx, order)
					idx += 1
				Next order
			End If

			If Matches IsNot Nothing AndAlso Matches.Count > 0 Then
				Dim idx As Integer = 0
				For Each match In Matches
					sb.AppendFormat(" : Match[{0}]={1}", idx, match)
					idx += 1
				Next match
			End If

			Return sb.ToString()
		End Function
	End Class
End Namespace
