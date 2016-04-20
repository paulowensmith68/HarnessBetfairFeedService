Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters

Namespace Api_ng_sample_code.TO
	<JsonConverter(GetType(StringEnumConverter))> _
	Public Enum PersistenceType
		LAPSE ' lapse the order at turn-in-play
		PERSIST ' put the order into the auction (SP) at turn-in-play
		MARKET_ON_CLOSE ' put the order into the auction (SP) at turn-in-play
	End Enum
End Namespace
