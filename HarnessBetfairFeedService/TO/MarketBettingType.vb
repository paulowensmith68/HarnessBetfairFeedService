Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters

Namespace Api_ng_sample_code.TO
	<JsonConverter(GetType(StringEnumConverter))> _
	Public Enum MarketBettingType
		ODDS
		LINE
		RANGE
		ASIAN_HANDICAP_DOUBLE_LINE
		ASIAN_HANDICAP_SINGLE_LINE
		FIXED_ODDS
	End Enum
End Namespace
