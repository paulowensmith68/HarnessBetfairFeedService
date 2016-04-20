Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class LimitOrder
		<JsonProperty(PropertyName := "size")> _
		Public Property Size() As Double

		<JsonProperty(PropertyName := "price")> _
		Public Property Price() As Double

		<JsonProperty(PropertyName := "persistenceType")> _
		Public Property PersistenceType() As PersistenceType

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("Size={0}", Size).AppendFormat(" : Price={0}", Price).AppendFormat(" : PersistenceType={0}", PersistenceType).ToString()
		End Function
	End Class
End Namespace
