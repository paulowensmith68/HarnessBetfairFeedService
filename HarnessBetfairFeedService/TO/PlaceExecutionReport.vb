Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class PlaceExecutionReport
		<JsonProperty(PropertyName := "status")> _
		Public Property Status() As ExecutionReportStatus

		<JsonProperty(PropertyName := "errorCode")> _
		Public Property ErrorCode() As ExecutionReportErrorCode

		<JsonProperty(PropertyName := "marketId")> _
		Public Property MarketId() As String

		<JsonProperty(PropertyName := "instructionReports")> _
		Public Property InstructionReports() As IList(Of PlaceInstructionReport)

		<JsonProperty(PropertyName := "customerRef")> _
		Public Property CustomerRef() As String

		Public Overrides Function ToString() As String
			Dim sb = (New StringBuilder()).AppendFormat("{0}", "PlaceExecutionReport").AppendFormat(" : Status={0}", Status).AppendFormat(" : ErrorCode={0}", ErrorCode).AppendFormat(" : MarketId={0}", MarketId).AppendFormat(" : CustomerRef={0}", CustomerRef)

			If InstructionReports IsNot Nothing AndAlso InstructionReports.Count > 0 Then
				Dim idx As Integer = 0
				For Each instructionReport In InstructionReports
					sb.AppendFormat(" : InstructionReport[{0}]={{{1}}}", idx, instructionReport)
					idx += 1
				Next instructionReport
			End If

			Return sb.ToString()
		End Function
	End Class
End Namespace
