Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class EventTypeResult
		<JsonProperty(PropertyName := "eventType")> _
		Public Property EventType() As EventType

		<JsonProperty(PropertyName := "marketCount")> _
		Public Property MarketCount() As Integer

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}", "EventTypeResult").AppendFormat(" : {0}", EventType).AppendFormat(" : MarketCount={0}", MarketCount).ToString()
		End Function
	End Class
End Namespace
