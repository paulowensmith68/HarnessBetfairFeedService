Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters

Namespace Api_ng_sample_code.TO
	<JsonConverter(GetType(StringEnumConverter))> _
	Public Enum MatchProjection
		NO_ROLLUP
		ROLLED_UP_BY_PRICE
		ROLLED_UP_BY_AVG_PRICE
	End Enum
End Namespace
