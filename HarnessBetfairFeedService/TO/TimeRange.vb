Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class TimeRange
		<JsonProperty(PropertyName := "from")> _
		Public Property [From]() As Date

		<JsonProperty(PropertyName := "to")> _
		Public Property [To]() As Date

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}", "TimeRange").AppendFormat(" : From={0}", [From]).AppendFormat(" : To={0}", [To]).ToString()
		End Function
	End Class
End Namespace
