Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class PlaceInstruction
		<JsonProperty(PropertyName := "orderType")> _
		Public Property OrderType() As OrderType

		<JsonProperty(PropertyName := "selectionId")> _
		Public Property SelectionId() As Long

		<JsonProperty(PropertyName := "handicap")> _
		Public Property Handicap() As Double?

		<JsonProperty(PropertyName := "side")> _
		Public Property Side() As Side

		<JsonProperty(PropertyName := "limitOrder")> _
		Public Property LimitOrder() As LimitOrder

		<JsonProperty(PropertyName := "limitOnCloseOrder")> _
		Public Property LimitOnCloseOrder() As LimitOnCloseOrder

		<JsonProperty(PropertyName := "marketOnCloseOrder")> _
		Public Property MarketOnCloseOrder() As MarketOnCloseOrder

		Public Overrides Function ToString() As String
			Dim sb = (New StringBuilder()).AppendFormat("OrderType={0}", OrderType).AppendFormat(" : SelectionId={0}", SelectionId).AppendFormat(" : Handicap={0}", Handicap).AppendFormat(" : Side={0}", Side)

			Select Case OrderType
				Case OrderType.LIMIT
					sb.AppendFormat(" : LimitOrder={0}", LimitOrder)
				Case OrderType.LIMIT_ON_CLOSE
					sb.AppendFormat(" : LimitOnCloseOrder={0}", LimitOnCloseOrder)
				Case OrderType.MARKET_ON_CLOSE
					sb.AppendFormat(" : MarketOnCloseOrder={0}", MarketOnCloseOrder)
			End Select

			Return sb.ToString()
		End Function
	End Class
End Namespace
