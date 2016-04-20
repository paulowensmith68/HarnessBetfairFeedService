Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class Order
		<JsonProperty(PropertyName := "betId")> _
		Public Property BetId() As String

		<JsonProperty(PropertyName := "orderType")> _
		Public Property OrderType() As OrderType

		<JsonProperty(PropertyName := "status")> _
		Public Property Status() As OrderStatus

		<JsonProperty(PropertyName := "persistenceType")> _
		Public Property PersistenceType() As PersistenceType

		<JsonProperty(PropertyName := "side")> _
		Public Property Side() As Side

		<JsonProperty(PropertyName := "price")> _
		Public Property Price() As Double

		<JsonProperty(PropertyName := "size")> _
		Public Property Size() As Double

		<JsonProperty(PropertyName := "bspLiability")> _
		Public Property BspLiability() As Double?

		<JsonProperty(PropertyName := "placedDate")> _
		Public Property PlacedDate() As Date?

		<JsonProperty(PropertyName := "avgPriceMatched")> _
		Public Property AvgPriceMatched() As Double?

		<JsonProperty(PropertyName := "sizeMatched")> _
		Public Property SizeMatched() As Double?

		<JsonProperty(PropertyName := "sizeRemaining")> _
		Public Property SizeRemaining() As Double?

		<JsonProperty(PropertyName := "sizeLapsed")> _
		Public Property SizeLapsed() As Double?

		<JsonProperty(PropertyName := "sizeCancelled")> _
		Public Property SizeCancelled() As Double?

		<JsonProperty(PropertyName := "sizeVoided")> _
		Public Property SizeVoided() As Double?

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("BetId={0}", BetId).AppendFormat(" : OrderType={0}", OrderType).AppendFormat(" : OrderStatus={0}", Status).AppendFormat(" : PersistenceType={0}", PersistenceType).AppendFormat(" : Side={0}", Side).AppendFormat(" : Size@Price={0}@{1}", SizeRemaining, Price).AppendFormat(" : BspLiability={0}", BspLiability).AppendFormat(" : PlacedDate={0}", PlacedDate).AppendFormat(" : AvgPriceMatched={0}", AvgPriceMatched).AppendFormat(" : SizeMatched={0}", SizeMatched).AppendFormat(" : SizeRemaining={0}", SizeRemaining).AppendFormat(" : SizeLapsed={0}", SizeLapsed).AppendFormat(" : SizeCancelled={0}", SizeCancelled).AppendFormat(" : SizeVoided={0}", SizeVoided).ToString()
		End Function
	End Class
End Namespace
