Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class MarketFilter
		<JsonProperty(PropertyName := "textQuery")> _
		Public Property TextQuery() As String

		<JsonProperty(PropertyName := "exchangeIds")> _
		Public Property ExchangeIds() As ISet(Of String)

		<JsonProperty(PropertyName := "eventTypeIds")> _
		Public Property EventTypeIds() As ISet(Of String)

		<JsonProperty(PropertyName := "eventIds")> _
		Public Property EventIds() As ISet(Of String)

		<JsonProperty(PropertyName := "competitionIds")> _
		Public Property CompetitionIds() As ISet(Of String)

		<JsonProperty(PropertyName := "marketIds")> _
		Public Property MarketIds() As ISet(Of String)

		<JsonProperty(PropertyName := "venues")> _
		Public Property Venues() As ISet(Of String)

		<JsonProperty(PropertyName := "bspOnly")> _
		Public Property BspOnly() As Boolean?

		<JsonProperty(PropertyName := "turnInPlayEnabled")> _
		Public Property TurnInPlayEnabled() As Boolean?

		<JsonProperty(PropertyName := "inPlayOnly")> _
		Public Property InPlayOnly() As Boolean?

		<JsonProperty(PropertyName := "marketBettingTypes")> _
		Public Property MarketBettingTypes() As ISet(Of MarketBettingType)

		<JsonProperty(PropertyName := "marketCountries")> _
		Public Property MarketCountries() As ISet(Of String)

		<JsonProperty(PropertyName := "marketTypeCodes")> _
		Public Property MarketTypeCodes() As ISet(Of String)

		<JsonProperty(PropertyName := "marketStartTime")> _
		Public Property MarketStartTime() As TimeRange

		<JsonProperty(PropertyName := "withOrders")> _
		Public Property WithOrders() As ISet(Of OrderStatus)



	End Class
End Namespace
