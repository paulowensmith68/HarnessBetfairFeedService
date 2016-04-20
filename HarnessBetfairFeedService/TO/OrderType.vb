Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters


Namespace Api_ng_sample_code.TO
	<JsonConverter(GetType(StringEnumConverter))> _
	Public Enum OrderType
		LIMIT ' normal exchange limit order for immediate execution
		LIMIT_ON_CLOSE ' limit order for the auction (SP)
		MARKET_ON_CLOSE ' market order for the auction (SP)
	End Enum
End Namespace
