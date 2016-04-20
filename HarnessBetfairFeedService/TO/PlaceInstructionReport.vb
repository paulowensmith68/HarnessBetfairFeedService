Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class PlaceInstructionReport
		<JsonProperty(PropertyName := "status")> _
		Public Property Status() As InstructionReportStatus

		<JsonProperty(PropertyName := "errorCode")> _
		Public Property ErrorCode() As InstructionReportErrorCode

		<JsonProperty(PropertyName := "instruction")> _
		Public Property Instruction() As PlaceInstruction

		<JsonProperty(PropertyName := "betId")> _
		Public Property BetId() As String

		<JsonProperty(PropertyName := "placedDate")> _
		Public Property PlacedDate() As Date?

		<JsonProperty(PropertyName := "averagePriceMatched")> _
		Public Property AveragePriceMatched() As Double?

		<JsonProperty(PropertyName := "sizeMatched")> _
		Public Property SizeMatched() As Double?

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("Status={0}", Status).AppendFormat(" : ErrorCode={0}", ErrorCode).AppendFormat(" : Instruction={{{0}}}", Instruction).AppendFormat(" : BetId={0}", BetId).AppendFormat(" : PlacedDate={0}", PlacedDate).AppendFormat(" : AveragePriceMatched={0}", AveragePriceMatched).AppendFormat(" : SizeMatched={0}", SizeMatched).ToString()
		End Function
	End Class
End Namespace
