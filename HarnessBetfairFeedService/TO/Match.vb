Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class Match
		<JsonProperty(PropertyName := "betId")> _
		Public Property BetId() As String

		<JsonProperty(PropertyName := "side")> _
		Public Property Side() As Side

		<JsonProperty(PropertyName := "price")> _
		Public Property Price() As Double

		<JsonProperty(PropertyName := "size")> _
		Public Property Size() As Double

		<JsonProperty(PropertyName := "matchDate")> _
		Public Property MatchDate() As Date

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat(" : BetId={0}", BetId).AppendFormat(" : Side={0}", Side).AppendFormat(" : Size@Price={0}@{1}", Size, Price).AppendFormat(" : MatchDate={0}", MatchDate).ToString()
		End Function
	End Class
End Namespace
