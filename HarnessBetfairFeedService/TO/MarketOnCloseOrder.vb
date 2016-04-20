Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class MarketOnCloseOrder
		<JsonProperty(PropertyName := "size")> _
		Public Property Size() As Double

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("Size={0}", Size).ToString()
		End Function
	End Class
End Namespace
