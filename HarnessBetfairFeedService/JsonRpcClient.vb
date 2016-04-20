Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Text
Imports HarnessBetfairFeedService.Api_ng_sample_code.TO
Imports System.Web.Services.Protocols
Imports System.Net
Imports System.IO
Imports HarnessBetfairFeedService.Api_ng_sample_code.Json

Namespace Api_ng_sample_code
	Public Class JsonRpcClient
		Inherits HttpWebClientProtocol
		Implements IClient

		Private privateEndPoint As String
		Public Property EndPoint() As String
			Get
				Return privateEndPoint
			End Get
			Private Set(ByVal value As String)
				privateEndPoint = value
			End Set
		End Property
		Private Shared ReadOnly operationReturnTypeMap As IDictionary(Of String, Type) = New Dictionary(Of String, Type)()
		Public Const APPKEY_HEADER As String = "X-Application"
		Public Const SESSION_TOKEN_HEADER As String = "X-Authentication"
		Public Property CustomHeaders() As NameValueCollection
		Private Shared ReadOnly LIST_EVENT_TYPES_METHOD As String = "SportsAPING/v1.0/listEventTypes"
		Private Shared ReadOnly LIST_MARKET_CATALOGUE_METHOD As String = "SportsAPING/v1.0/listMarketCatalogue"
		Private Shared ReadOnly LIST_MARKET_BOOK_METHOD As String = "SportsAPING/v1.0/listMarketBook"
		Private Shared ReadOnly PLACE_ORDERS_METHOD As String = "SportsAPING/v1.0/placeOrders"
		Private Shared ReadOnly FILTER As String = "filter"
		Private Shared ReadOnly LOCALE As String = "locale"
		Private Shared ReadOnly CURRENCY_CODE As String = "currencyCode"
		Private Shared ReadOnly MARKET_PROJECTION As String = "marketProjection"
		Private Shared ReadOnly MATCH_PROJECTION As String = "matchProjection"
		Private Shared ReadOnly ORDER_PROJECTION As String = "orderProjection"
		Private Shared ReadOnly PRICE_PROJECTION As String = "priceProjection"
		Private Shared ReadOnly SORT As String = "sort"
		Private Shared ReadOnly MAX_RESULTS As String = "maxResults"
		Private Shared ReadOnly MARKET_IDS As String = "marketIds"
		Private Shared ReadOnly MARKET_ID As String = "marketId"
		Private Shared ReadOnly INSTRUCTIONS As String = "instructions"
		Private Shared ReadOnly CUSTOMER_REFERENCE As String = "customerRef"

		Public Sub New(ByVal endPoint As String, ByVal appKey As String, ByVal sessionToken As String)
			Me.EndPoint = endPoint & "/json-rpc/v1"
			CustomHeaders = New NameValueCollection()
			If appKey IsNot Nothing Then
				CustomHeaders(APPKEY_HEADER) = appKey
			End If
			If sessionToken IsNot Nothing Then
				CustomHeaders(SESSION_TOKEN_HEADER) = sessionToken
			End If
		End Sub


		Public Function listEventTypes(ByVal marketFilter As MarketFilter, Optional ByVal locale As String = Nothing) As IList(Of EventTypeResult) Implements IClient.listEventTypes
			Dim args = New Dictionary(Of String, Object)()
			args(FILTER) = marketFilter
			args(JsonRpcClient.LOCALE) = locale
			Return Invoke(Of List(Of EventTypeResult))(LIST_EVENT_TYPES_METHOD, args)

		End Function

		Public Function listMarketCatalogue(ByVal marketFilter As MarketFilter, ByVal marketProjections As ISet(Of MarketProjection), ByVal marketSort As MarketSort, Optional ByVal maxResult As String = "1", Optional ByVal locale As String = Nothing) As IList(Of MarketCatalogue) Implements IClient.listMarketCatalogue
			Dim args = New Dictionary(Of String, Object)()
			args(FILTER) = marketFilter
			args(MARKET_PROJECTION) = marketProjections
			args(SORT) = marketSort
			args(MAX_RESULTS) = maxResult
			args(JsonRpcClient.LOCALE) = locale
			Return Invoke(Of List(Of MarketCatalogue))(LIST_MARKET_CATALOGUE_METHOD, args)
		End Function

		Public Function listMarketBook(ByVal marketIds As IList(Of String), ByVal priceProjection As PriceProjection, Optional ByVal orderProjection? As OrderProjection = Nothing, Optional ByVal matchProjection? As MatchProjection = Nothing, Optional ByVal currencyCode As String = Nothing, Optional ByVal locale As String = Nothing) As IList(Of MarketBook) Implements IClient.listMarketBook
			Dim args = New Dictionary(Of String, Object)()
			args(MARKET_IDS)= marketIds
			args(PRICE_PROJECTION) = priceProjection
			args(ORDER_PROJECTION) = orderProjection
			args(MATCH_PROJECTION) = matchProjection
			args(JsonRpcClient.LOCALE) = locale
			args(CURRENCY_CODE) = currencyCode
			Return Invoke(Of List(Of MarketBook))(LIST_MARKET_BOOK_METHOD, args)
		End Function

		Public Function placeOrders(ByVal marketId As String, ByVal customerRef As String, ByVal placeInstructions As IList(Of PlaceInstruction), Optional ByVal locale As String = Nothing) As PlaceExecutionReport Implements IClient.placeOrders
			Dim args = New Dictionary(Of String, Object)()

			args(MARKET_ID) = marketId
			args(INSTRUCTIONS) = placeInstructions
			args(CUSTOMER_REFERENCE) = customerRef
			args(JsonRpcClient.LOCALE) = locale

			Return Invoke(Of PlaceExecutionReport)(PLACE_ORDERS_METHOD, args)
		End Function

        Protected Function CreateWebRequest(ByVal uri As Uri) As WebRequest
            Dim request As WebRequest = WebRequest.Create(New Uri(EndPoint))
            request.Method = "POST"
            request.ContentType = "application/json-rpc"
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "ISO-8859-1,utf-8")
            request.Headers.Add(CustomHeaders)
            Return request
        End Function

        Public Function Invoke(Of T)(ByVal method As String, Optional ByVal args As IDictionary(Of String, Object) = Nothing) As T
			If method Is Nothing Then
				Throw New ArgumentNullException("method")
			End If
			If method.Length = 0 Then
				Throw New ArgumentException(Nothing, "method")
			End If

			Dim request = CreateWebRequest(New Uri(EndPoint))

			Using stream As Stream = request.GetRequestStream()
			Using writer As New StreamWriter(stream, Encoding.UTF8)
				Dim [call] = New JsonRequest With {.Method = method, .Id = 1, .Params = args}
				JsonConvert.Export([call], writer)
			End Using
			End Using
			Console.WriteLine(vbLf & "Calling: " & method & " With args: " & JsonConvert.Serialize(Of IDictionary(Of String, Object))(args))

			Using response As WebResponse = GetWebResponse(request)
			Using stream As Stream = response.GetResponseStream()
			Using reader As New StreamReader(stream, Encoding.UTF8)
				Dim jsonResponse = JsonConvert.Import(Of T)(reader)
				Console.WriteLine(vbLf & "Got Response: " & JsonConvert.Serialize(Of JsonResponse(Of T))(jsonResponse))
				If jsonResponse.HasError Then
					Throw ReconstituteException(jsonResponse.Error)
				Else
					Return jsonResponse.Result
				End If
			End Using
			End Using
			End Using
		End Function


		Private Shared Function ReconstituteException(ByVal ex As Api_ng_sample_code.TO.Exception) As System.Exception
			Dim data = ex.Data

			' API-NG exception -- it must have "data" element to tell us which exception
			Dim exceptionName = data.Property("exceptionname").Value.ToString()
			Dim exceptionData = data.Property(exceptionName).Value.ToString()
			Return JsonConvert.Deserialize(Of APINGException)(exceptionData)
		End Function
	End Class
End Namespace
