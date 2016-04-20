Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class CompetitionResult
		<JsonProperty(PropertyName := "competition")> _
		Public Property Competition() As Competition

		<JsonProperty(PropertyName := "marketCount")> _
		Public Property MarketCount() As Integer

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}", "CompetitionResult").AppendFormat(" : {0}", Competition).AppendFormat(" : MarketCount={0}", MarketCount).ToString()
		End Function
	End Class
End Namespace
