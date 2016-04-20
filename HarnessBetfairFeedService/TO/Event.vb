Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json

Namespace Api_ng_sample_code.TO
	Public Class [Event]
		<JsonProperty(PropertyName := "id")> _
		Public Property Id() As String

		<JsonProperty(PropertyName := "name")> _
		Public Property Name() As String

		<JsonProperty(PropertyName := "countryCode")> _
		Public Property CountryCode() As String

		<JsonProperty(PropertyName := "timezone")> _
		Public Property Timezone() As String

		<JsonProperty(PropertyName := "venue")> _
		Public Property Venue() As String

		<JsonProperty(PropertyName := "openDate")> _
		Public Property OpenDate() As Date?

		Public Overrides Function ToString() As String
			Return (New StringBuilder()).AppendFormat("{0}", "Event").AppendFormat(" : Id={0}", Id).AppendFormat(" : Name={0}", Name).AppendFormat(" : CountryCode={0}", CountryCode).AppendFormat(" : Venue={0}", Venue).AppendFormat(" : Timezone={0}", Timezone).AppendFormat(" : OpenDate={0}", OpenDate).ToString()
		End Function
	End Class
End Namespace
