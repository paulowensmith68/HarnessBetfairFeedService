Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters

Namespace Api_ng_sample_code.TO
	 <JsonConverter(GetType(StringEnumConverter))> _
	 Public Enum MarketSort
		 MINIMUM_TRADED
		 MAXIMUM_TRADED
		 MINIMUM_AVAILABLE
		 MAXIMUM_AVAILABLE
		 FIRST_TO_START
		 LAST_TO_START
	 End Enum
End Namespace
