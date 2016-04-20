Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class LimitOnCloseOrder
		<JsonProperty(PropertyName := "size")> _
		Public Property Size() As Double

		<JsonProperty(PropertyName := "liability")> _
		Public Property Liability() As Double

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("Size={0}", Size).AppendFormat(" : Liability={0}", Liability).ToString()
		End Function
	End Class
End Namespace
