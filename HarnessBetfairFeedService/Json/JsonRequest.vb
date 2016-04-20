Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.Json
	<JsonObject(MemberSerialization.OptIn)> _
	Public Class JsonRequest
		Public Sub New()
			JsonRpc = "2.0"
		End Sub

		<JsonProperty(PropertyName := "jsonrpc", NullValueHandling := NullValueHandling.Ignore)> _
		Public Property JsonRpc() As String

		<JsonProperty(PropertyName := "method")> _
		Public Property Method() As String

		<JsonProperty(PropertyName := "params")> _
		Public Property Params() As Object

		<JsonProperty(PropertyName := "id")> _
		Public Property Id() As Object
	End Class
End Namespace
