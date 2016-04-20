Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class ExchangePrices
		<JsonProperty(PropertyName := "availableToBack")> _
		Public Property AvailableToBack() As List(Of PriceSize)

		<JsonProperty(PropertyName := "availableToLay")> _
		Public Property AvailableToLay() As List(Of PriceSize)

		<JsonProperty(PropertyName := "tradedVolume")> _
		Public Property TradedVolume() As List(Of PriceSize)

		Public Overrides Function ToString() As String
			Dim sb = (New StringBuilder()).AppendFormat("{0}", "ExchangePrices")

			If AvailableToBack IsNot Nothing AndAlso AvailableToBack.Count > 0 Then
				Dim idx As Integer = 0
'INSTANT VB NOTE: The variable availableToBack was renamed since Visual Basic does not handle local variables named the same as class members well:
				For Each availableToBack_Renamed In AvailableToBack
					sb.AppendFormat(" : AvailableToBack[{0}]={1}", idx, availableToBack_Renamed)
					idx += 1
				Next availableToBack_Renamed
			End If

			If AvailableToLay IsNot Nothing AndAlso AvailableToLay.Count > 0 Then
				Dim idx As Integer = 0
'INSTANT VB NOTE: The variable availableToLay was renamed since Visual Basic does not handle local variables named the same as class members well:
				For Each availableToLay_Renamed In AvailableToLay
					sb.AppendFormat(" : AvailableToLay[{0}]={1}", idx, availableToLay_Renamed)
					idx += 1
				Next availableToLay_Renamed
			End If

			If TradedVolume IsNot Nothing AndAlso TradedVolume.Count > 0 Then
				Dim idx As Integer = 0
'INSTANT VB NOTE: The variable tradedVolume was renamed since Visual Basic does not handle local variables named the same as class members well:
				For Each tradedVolume_Renamed In TradedVolume
					sb.AppendFormat(" : TradedVolume[{0}]={1}", idx, tradedVolume_Renamed)
					idx += 1
				Next tradedVolume_Renamed
			End If

			Return sb.ToString()
		End Function
	End Class
End Namespace
