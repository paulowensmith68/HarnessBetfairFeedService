Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class MarketDescription
		<JsonProperty(PropertyName := "persistenceEnabled")> _
		Public Property IsPersistenceEnabled() As Boolean

		<JsonProperty(PropertyName := "bspMarket")> _
		Public Property IsBspMarket() As Boolean

		<JsonProperty(PropertyName := "marketTime")> _
		Public Property MarketTime() As Date

		<JsonProperty(PropertyName := "suspendTime")> _
		Public Property SuspendTime() As Date?

		<JsonProperty(PropertyName := "settleTime")> _
		Public Property SettleTime() As Date?

		<JsonProperty(PropertyName := "bettingType")> _
		Public Property BettingType() As MarketBettingType

		<JsonProperty(PropertyName := "turnInPlayEnabled")> _
		Public Property IsTurnInPlayEnabled() As Boolean

		<JsonProperty(PropertyName := "marketType")> _
		Public Property MarketType() As String

		<JsonProperty(PropertyName := "regulator")> _
		Public Property Regulator() As String

		<JsonProperty(PropertyName := "marketBaseRate")> _
		Public Property MarketBaseRate() As Double

		<JsonProperty(PropertyName := "discountAllowed")> _
		Public Property IsDiscountAllowed() As Boolean

		<JsonProperty(PropertyName := "wallet")> _
		Public Property Wallet() As String

		<JsonProperty(PropertyName := "rules")> _
		Public Property Rules() As String

		<JsonProperty(PropertyName := "rulesHasDate")> _
		Public Property RulesHasDate() As Boolean

		<JsonProperty(PropertyName := "clarifications")> _
		Public Property Clarifications() As String

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}", "MarketDescription").AppendFormat(" : BspMarket={0}", IsBspMarket).AppendFormat(" : BettingType={0}", BettingType).AppendFormat(" : MarketType={0}", MarketType).AppendFormat(" : MarketTime={0}", MarketTime).AppendFormat(" : SuspendTime={0}", SuspendTime).AppendFormat(" : SettleTime={0}", SettleTime).AppendFormat(" : MarketBaseRate={0}", MarketBaseRate).AppendFormat(" : IsPersistenceEnabled={0}", IsPersistenceEnabled).AppendFormat(" : IsTurnInPlayEnabled={0}", IsTurnInPlayEnabled).AppendFormat(" : IsDiscountAllowed={0}", IsDiscountAllowed).AppendFormat(" : Regulator={0}", Regulator).AppendFormat(" : Rules={0}", Rules).AppendFormat(" : RulesHasDate={0}", RulesHasDate).AppendFormat(" : Clarifications={0}", Clarifications).AppendFormat(" : Wallet={0}", Wallet).ToString()
		End Function
	End Class
End Namespace
