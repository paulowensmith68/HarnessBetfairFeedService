Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

'Public Class JsonRequest

'        Public Property jsonrpc As String
'        Public Property method As String
'        Public Property params As Parameter
'        Public Property id As Integer

'    End Class

'    Public Class Parameter
'        Public Property filter As Request_Filter
'    End Class

'Public Class Ident
'    Public Property id As Integer
'End Class

'Public Class Request_Filter

'End Class

'Public Class J_Response
'    Public Property jsonrpc As String
'    Public Property result As IList(Of Event_Summary)
'    Public Property id As Integer
'End Class

'Public Class Event_Summary
'    Public Property eventType As Event_Type
'    Public Property marketCount As Integer
'End Class

'Public Class Event_Type
'    Public Property id As Integer
'    Public Property name As String
'End Class

Public Class J_Response_Login
    Public Property token As String
    Public Property product As String
    Public Property status As String
    Public Property error_msg As String
End Class

