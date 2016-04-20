Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports Newtonsoft.Json
Public Class AccountClass
    Inherits WebClient

    Public Sub Login()

        Try
            Dim postData As String = "username=" + globalBetFairUsername + "&password=" + globalBetFairPassword

            ' For logincert change url to logincert and add the commented out lines (2)....
            ' Dim cert As New X509Certificate2("E:\CCC.p12", "")

            System.Net.ServicePointManager.Expect100Continue = False

            Dim request As HttpWebRequest = WebRequest.Create("https://identitysso.betfair.com/api/login")

            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.Headers.Add("X-Application: " + globalBetFairAppKey)
            ' request.ClientCertificates.Add(cert)
            request.Accept = "application/json"

            Using dataStream As Stream = request.GetRequestStream()
                Using writer As New StreamWriter(dataStream, Encoding.[Default])
                    writer.Write(postData)
                End Using
            End Using

            Using stream As Stream = DirectCast(request.GetResponse(), HttpWebResponse).GetResponseStream()
                Using reader As New StreamReader(stream, Encoding.[Default])
                    Dim responseFromServer As String = reader.ReadToEnd()

                    Dim login_response As J_Response_Login = JsonConvert.DeserializeObject(Of J_Response_Login)(responseFromServer)

                    globalBetFairToken = login_response.token

                    gobjEvent.WriteToEventLog("AccountClass : Response from login: " + login_response.status, EventLogEntryType.Information)

                End Using
            End Using


        Catch ex As Exception

            gobjEvent.WriteToEventLog("AccountClass : Error logging in to BetFair API, message : " + ex.Message, EventLogEntryType.Error)

        End Try

    End Sub

    Public Sub Logout()

        Try
            Dim postData As String = ""

            System.Net.ServicePointManager.Expect100Continue = False

            Dim request As HttpWebRequest = WebRequest.Create("https://identitysso.betfair.com/api/logout")

            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.Headers.Add("X-Application: " + globalBetFairAppKey)
            request.Headers.Add("X-Authentication: " + globalBetFairToken)
            request.Accept = "application/json"

            Using dataStream As Stream = request.GetRequestStream()
                Using writer As New StreamWriter(dataStream, Encoding.[Default])
                    writer.Write(postData)
                End Using
            End Using

            Using stream As Stream = DirectCast(request.GetResponse(), HttpWebResponse).GetResponseStream()
                Using reader As New StreamReader(stream, Encoding.[Default])
                    Dim responseFromServer As String = reader.ReadToEnd()

                    Dim logout_response As J_Response_Login = JsonConvert.DeserializeObject(Of J_Response_Login)(responseFromServer)

                    gobjEvent.WriteToEventLog("AccountClass : Response from logout: " + logout_response.status, EventLogEntryType.Information)

                End Using
            End Using


        Catch ex As Exception

            gobjEvent.WriteToEventLog("AccountClass : Error logging out onto BetFair API, message : " + ex.Message, EventLogEntryType.Error)

        End Try

    End Sub
End Class
