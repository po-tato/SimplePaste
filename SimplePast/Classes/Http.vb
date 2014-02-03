Imports System.Net
Imports System.IO
Imports System.Text

''' <summary>
''' Httpclasses for SimplePast
''' </summary>
''' <remarks></remarks>

Public Class Http
    Private Shared cookie As New CookieContainer

    Public Shared Function GetCookies() As CookieContainer
        Return cookie
    End Function
    Public Shared Sub SetCookies(ByVal cook As CookieContainer)
        cookie = cook
    End Sub
    Public Shared Sub ClearCookies()
        cookie = New CookieContainer
    End Sub

    Public Shared Function Getreq(ByVal Url As String, Optional ByVal refferer As String = "", Optional ByVal ssl As Boolean = False) As String
        Dim reffereral As String = String.Empty
        If refferer IsNot String.Empty Then
            reffereral = refferer
        End If
        Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(Url), HttpWebRequest)
        request.CookieContainer = cookie
        If reffereral IsNot String.Empty Then
            request.Referer = reffereral
        End If
        request.Method = WebRequestMethods.Http.Get
        request.ContentType = "application/x-www-form-urlencoded"
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"
        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        Dim reader As New StreamReader(response.GetResponseStream())
        Dim html As String = reader.ReadToEnd()
        Return html
    End Function
    Public Shared Function Postreq(ByVal Url As String, ByVal post As String, Optional ByVal refferer As String = "", Optional ByVal ssl As Boolean = False) As String
            Dim reffereral As String = String.Empty
            If refferer IsNot String.Empty Then
                reffereral = refferer
            End If
            Dim request As HttpWebRequest
            request = CType(HttpWebRequest.Create(Url), HttpWebRequest)
            request.Method = WebRequestMethods.Http.Post

            request.CookieContainer = cookie
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"
            request.ContentType = "application/x-www-form-urlencoded"
            If reffereral IsNot String.Empty Then
                request.Referer = reffereral
            End If
            Dim byteArr() As Byte = Encoding.Default.GetBytes(post)
            request.ContentLength = byteArr.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArr, 0, byteArr.Length)
            Dim response As HttpWebResponse
            response = CType(request.GetResponse(), HttpWebResponse)
            Return New StreamReader(response.GetResponseStream()).ReadToEnd()

    End Function
    Public Shared Function MultiPartPostreq(ByVal Url As String, ByVal post As String, Optional ByVal refferer As String = "", Optional boundary As String = "") As String
        Dim reffereral As String = String.Empty
        If refferer IsNot String.Empty Then
            reffereral = refferer
        End If
        Dim request As HttpWebRequest
        request = CType(HttpWebRequest.Create(Url), HttpWebRequest)
        request.Method = WebRequestMethods.Http.Post
        request.CookieContainer = cookie
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"
        request.ContentType = "multipart/form-data; boundary=---------------------------" & boundary
        If reffereral IsNot String.Empty Then
            request.Referer = reffereral
        End If
        Dim byteArr() As Byte = Encoding.Default.GetBytes(post)
        request.ContentLength = byteArr.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArr, 0, byteArr.Length)
        Dim response As HttpWebResponse
        response = CType(request.GetResponse(), HttpWebResponse)
        Return New StreamReader(response.GetResponseStream()).ReadToEnd()
    End Function
    Public Shared Function JsonPostreq(ByVal Url As String, ByVal post As String, Optional ByVal refferer As String = "", Optional ByVal ssl As Boolean = False) As String
        Try
            Dim reffereral As String = String.Empty
            If refferer IsNot String.Empty Then
                reffereral = refferer
            End If
            Dim request As HttpWebRequest
            request = CType(HttpWebRequest.Create(Url), HttpWebRequest)
            request.Method = WebRequestMethods.Http.Post

            request.CookieContainer = cookie
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"
            request.ContentType = "application/json"
            If reffereral IsNot String.Empty Then
                request.Referer = reffereral
            End If
            Dim byteArr() As Byte = Encoding.Default.GetBytes(post)
            request.ContentLength = byteArr.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArr, 0, byteArr.Length)
            Dim response As HttpWebResponse
            response = CType(request.GetResponse(), HttpWebResponse)
            Return New StreamReader(response.GetResponseStream()).ReadToEnd()

        Catch ex As Exception
            If ex.Message.Contains("401") Then
                MessageBox.Show("We were unable to shorten your link. This can happen if the access key from Google is not valid. Please authorize your account again")
            End If
            frmMain.btn_ggOpen.Enabled = True
            frmMain.btn_ggStartAuth.Enabled = True
            frmMain.btn_ggComplete.Enabled = True
            frmMain.txt_ggCode.Enabled = True
        End Try
        
    End Function
End Class
