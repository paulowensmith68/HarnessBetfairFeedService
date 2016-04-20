Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class EventType
		<JsonProperty(PropertyName := "id")> _
		Public Property Id() As String

		<JsonProperty(PropertyName := "name")> _
		Public Property Name() As String

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}", "EventType").AppendFormat(" : Id={0}", Id).AppendFormat(" : Name={0}", Name).ToString()
		End Function
	End Class
End Namespace
