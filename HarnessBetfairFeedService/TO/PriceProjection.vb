Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class PriceProjection
		<JsonProperty(PropertyName := "priceData")> _
		Public Property PriceData() As ISet(Of PriceData)

		<JsonProperty(PropertyName := "exBestOffersOverrides")> _
		Public Property ExBestOffersOverrides() As ExBestOffersOverrides

		<JsonProperty(PropertyName := "virtualise")> _
		Public Property Virtualise() As Boolean?

		<JsonProperty(PropertyName := "rolloverStakes")> _
		Public Property RolloverStakes() As Boolean?


	End Class
End Namespace
