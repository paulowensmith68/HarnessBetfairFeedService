Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class PriceSize
		<JsonProperty(PropertyName := "price")> _
		Public Property Price() As Double

		<JsonProperty(PropertyName := "size")> _
		Public Property Size() As Double

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}@{1}", Size, Price).ToString()
		End Function
	End Class
End Namespace
