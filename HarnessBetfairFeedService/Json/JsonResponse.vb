Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports HarnessBetfairFeedService.Api_ng_sample_code.TO

Namespace Api_ng_sample_code.Json
	<JsonObject(MemberSerialization.OptIn)> _
	Public Class JsonResponse(Of T)
		<JsonProperty(PropertyName := "jsonrpc", NullValueHandling := NullValueHandling.Ignore)> _
		Public Property JsonRpc() As String

		<JsonProperty(PropertyName := "result", NullValueHandling := NullValueHandling.Ignore)> _
		Public Property Result() As T

		<JsonProperty(PropertyName := "error", NullValueHandling := NullValueHandling.Ignore)> _
		Public Property [Error]() As Api_ng_sample_code.TO.Exception

		<JsonProperty(PropertyName := "id")> _
		Public Property Id() As Object

		<JsonIgnore> _
		Public ReadOnly Property HasError() As Boolean
			Get
				Return [Error] IsNot Nothing
			End Get
		End Property
	End Class
End Namespace
