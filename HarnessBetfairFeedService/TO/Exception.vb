Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace Api_ng_sample_code.TO
	Public Class Exception
		' exception in json-rpc format
		<JsonProperty(PropertyName := "data")> _
		Public Property Data() As JObject


		' exception in rescript format
		<JsonProperty(PropertyName := "detail")> _
		Public Property Detail() As JObject

	End Class
End Namespace
