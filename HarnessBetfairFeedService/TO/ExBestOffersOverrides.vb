Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class ExBestOffersOverrides
		<JsonProperty(PropertyName := "bestPricesDepth")> _
		Public Property BestPricesDepth() As Integer

		<JsonProperty(PropertyName := "rollupModel")> _
		Public Property RollUpModel() As RollUpModel

		<JsonProperty(PropertyName := "rollupLimit")> _
		Public Property RollUpLimit() As Integer

		<JsonProperty(PropertyName := "rollupLiabilityThreshold")> _
		Public Property RollUpLiabilityThreshold() As Double

		<JsonProperty(PropertyName := "rollupLiabilityFactor")> _
		Public Property RollUpLiabilityFactor() As Integer
	End Class
End Namespace
