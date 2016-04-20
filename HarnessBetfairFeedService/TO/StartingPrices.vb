Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class StartingPrices
		<JsonProperty(PropertyName := "nearPrice")> _
		Public Property NearPrice() As Double

		<JsonProperty(PropertyName := "farPrice")> _
		Public Property FarPrice() As Double

		<JsonProperty(PropertyName := "backStakeTaken")> _
		Public Property BackStakeTaken() As List(Of PriceSize)

		<JsonProperty(PropertyName := "layLiabilityTaken")> _
		Public Property LayLiabilityTaken() As List(Of PriceSize)

		<JsonProperty(PropertyName := "actualSP")> _
		Public Property ActualSP() As Double

		Public Overrides Function ToString() As String
			Dim sb = (New StringBuilder()).AppendFormat("{0}", "StartingPrices").AppendFormat(" : NearPrice={0}", NearPrice).AppendFormat(" : FarPrice={0}", FarPrice).AppendFormat(" : ActualSP={0}", ActualSP)

			If BackStakeTaken IsNot Nothing AndAlso BackStakeTaken.Count > 0 Then
				Dim idx As Integer = 0
				For Each backStake In BackStakeTaken
					sb.AppendFormat(" : BackStake[{0}]={1}", idx, backStake)
					idx += 1
				Next backStake
			End If

			If LayLiabilityTaken IsNot Nothing AndAlso LayLiabilityTaken.Count > 0 Then
				Dim idx As Integer = 0
				For Each layLiability In LayLiabilityTaken
					sb.AppendFormat(" : LayLiability{0}]={1}", idx, layLiability)
					idx += 1
				Next layLiability
			End If

			Return sb.ToString()
		End Function
	End Class
End Namespace
