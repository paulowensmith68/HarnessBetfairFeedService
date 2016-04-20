Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class EventResult
		<JsonProperty(PropertyName := "event")> _
		Public Property [Event]() As [Event]

		<JsonProperty(PropertyName := "marketCount")> _
		Public Property MarketCount() As Integer

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}", "EventResult").AppendFormat(" : {0}", [Event]).AppendFormat(" : MarketCount={0}", MarketCount).ToString()
		End Function
	End Class
End Namespace
