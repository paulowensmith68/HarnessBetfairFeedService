Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports System.Runtime.Serialization

Namespace Api_ng_sample_code.TO
	Public Class APINGException
		Inherits System.Exception

		Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
			Me.ErrorDetails = info.GetString("errorDetails")
			Me.ErrorCode = info.GetString("errorCode")
			Me.RequestUUID = info.GetString("requestUUID")
		End Sub

		<JsonProperty(PropertyName := "errorDetails")> _
		Public Property ErrorDetails() As String

		<JsonProperty(PropertyName := "errorCode")> _
		Public Property ErrorCode() As String

		<JsonProperty(PropertyName := "requestUUID")> _
		Public Property RequestUUID() As String
	End Class
End Namespace
