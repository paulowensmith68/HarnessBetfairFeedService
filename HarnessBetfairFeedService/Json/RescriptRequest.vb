Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.Json
	<JsonObject(MemberSerialization.OptIn)> _
	Public Class RescriptRequest

		<JsonProperty(PropertyName := "")> _
		Public Property args() As IDictionary(Of String, Object)

		Public Sub New(ByVal args As IDictionary(Of String, Object))
			Me.args = args
		End Sub
	End Class
End Namespace
