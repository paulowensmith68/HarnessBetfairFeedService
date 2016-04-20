Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters

Namespace Api_ng_sample_code.TO
	<JsonConverter(GetType(StringEnumConverter))> _
	Public Enum PriceData
		SP_AVAILABLE
		SP_TRADED
		EX_BEST_OFFERS
		EX_ALL_OFFERS
		EX_TRADED
	End Enum
End Namespace
