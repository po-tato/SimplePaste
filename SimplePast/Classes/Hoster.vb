Option Strict On
Option Explicit On
Imports SimplePast.Http
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Net.NetworkInformation


Namespace Hoster
    Public Class Pastebin
        Private Shared Property dev_key As String = "33e51a3e268238bfbf11bcce76753479" 'Your dev-key
        Public Shared Property Username As String 'Property "Username" - must be setted first
        Public Shared Property Password As String 'Property "Password" - must be setted first
        ''' <summary>
        ''' Returns the UserAPIKey, also know as session key
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetUserAPIKey() As String
            
            If String.IsNullOrEmpty(Username) OrElse String.IsNullOrEmpty(Password) Then
                Return "False"
                Exit Function
            End If
            Dim post As String = "api_dev_key=" & dev_key & "&api_user_name=" & Username & "&api_user_password=" & Password
            Dim url As String = "http://pastebin.com/api/api_login.php"
            Dim UserAPIKey As String = CStr(Postreq(url, post))
            Return UserAPIKey
        End Function
        Public Structure UserInformations
            Dim Username As String
            Dim UserFormat As String
            Dim UserExpiration As String
            Dim AvatarUrl As Uri
            Dim UserPrivate As String
            Dim UserEmail As String
            Dim UserWebsite As Uri
            Dim UserLocation As String
            Dim UserIsPro As Boolean
        End Structure
        ''' <summary>
        ''' Function to get Userinformations
        ''' </summary>
        ''' <returns>Specific Userinformations</returns>
        ''' <remarks></remarks>
        Public Shared Function GetUserInformations() As UserInformations
            Dim Info As New UserInformations
            Dim UserAPIKey As String = GetUserAPIKey()
            Dim post As String = "api_option=userdetails&api_user_key=" & UserAPIKey & "&api_dev_key=" & dev_key
            Dim Url As String = "http://pastebin.com/api/api_post.php"
            Dim html As String = CStr(Postreq(Url, post))
            Using reader As XmlReader = XmlReader.Create(New StringReader(html))
                With New StringBuilder
                    reader.ReadToFollowing("user_name")
                    reader.MoveToFirstAttribute()
                    Info.Username = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("user_format_short")
                    Info.UserFormat = reader.ReadElementContentAsString()
                    reader.ReadToFollowing("user_expiration")
                    Dim exp As String = reader.ReadElementContentAsString()
                    Select Case exp
                        Case CStr(exp = "N")
                            exp = "Never"
                            Info.UserExpiration = exp
                        Case Else
                            Info.UserExpiration = exp
                    End Select
                    '      Info.UserExpiration = exp
                    reader.ReadToFollowing("user_avatar_url")
                    Info.AvatarUrl = New Uri(reader.ReadElementContentAsString)
                    reader.ReadToFollowing("user_private")
                    Dim private_ As String = reader.ReadElementContentAsString
                    Select Case private_
                        Case "0"
                            private_ = "Public"
                            Info.UserPrivate = private_
                        Case "1"
                            private_ = "Unlisted"
                            Info.UserPrivate = private_
                        Case "2"
                            private_ = "Private"
                            Info.UserPrivate = private_
                    End Select
                    reader.ReadToFollowing("user_website")
                    Dim Website_s As String = reader.ReadElementContentAsString
                    If Not String.IsNullOrEmpty(Website_s) Then
                        Dim Website As Uri = New Uri(reader.ReadElementContentAsString)
                        Info.UserWebsite = Website
                    End If
                    reader.ReadToFollowing("user_email")
                    Info.UserEmail = reader.ReadElementContentAsString
                    reader.ReadToFollowing("user_location")
                    Info.UserLocation = reader.ReadElementContentAsString
                    reader.ReadToFollowing("user_account_type")
                    Dim UserIsPro_s As String = reader.ReadElementContentAsString
                    Dim UserIsPro As Boolean
                    Select Case UserIsPro_s
                        Case "0"
                            UserIsPro = False
                        Case "1"
                            UserIsPro = True
                    End Select
                    Info.UserIsPro = UserIsPro
                End With
            End Using
            Return Info
        End Function
        Public Enum syntax
            sixfivenulltwoacme = 0
            sixfivenulltwokickass = 1
            sixfivenulltwotasm = 2
            abap = 3
            actionscript = 4
            actionscript3 = 5
            ada = 6
            algol68 = 7
            apache = 8
            applescript = 9
            apt_sources = 10
            arm = 11
            asm = 12
            asp = 13
            asymptote = 14
            autoconf = 15
            autohotkey = 16
            autoit = 17
            avisynth = 18
            awk = 19
            bascomavr = 20
            bash = 21
            basic4gl = 22
            bibtex = 23
            blitzbasic = 24
            bnf = 25
            boo = 26
            bf = 27
            c = 28
            c_mac = 29
            cil = 30
            csharp = 31
            cpp = 32
            cpp_qt = 33
            c_loadrunner = 34
            caddcl = 35
            cadlisp = 36
            cfdg = 37
            chaiscript = 38
            clojure = 39
            klonec = 40
            klonecpp = 41
            cmake = 42
            cobol = 43
            coffeescript = 44
            cfm = 45
            css = 46
            cuesheet = 47
            d = 48
            dcl = 49
            dcpu16 = 50
            dcs = 51
            delphi = 52
            oxygene = 53
            diff = 54
            div = 55
            dos = 56
            dot = 57
            e = 58
            ecmascript = 59
            eiffel = 60
            email = 61
            epc = 62
            erlang = 63
            fsharp = 64
            falcon = 65
            fo = 66
            f1 = 67
            fortran = 68
            freebasic = 69
            freeswitch = 70
            gambas = 71
            gml = 72
            gdb = 73
            genero = 74
            genie = 75
            gettext = 76
            go = 77
            groovy = 78
            gwbasic = 79
            haskell = 80
            haxe = 81
            hicest = 82
            hq9plus = 83
            html4strict = 84
            html5 = 85
            icon = 86
            idl = 87
            ini = 88
            inno = 89
            intercal = 90
            io = 91
            j = 92
            java = 93
            java5 = 94
            javascript = 95
            jquery = 96
            kixtart = 97
            latex = 98
            ldif = 99
            lb = 100
            lsl2 = 101
            lisp = 102
            llvm = 103
            locobasic = 104
            logtalk = 105
            lolcode = 106
            lotusformulas = 107
            lotusscript = 108
            lscript = 109
            lua = 110
            m68k = 111
            magiksf = 112
            make = 113
            mapbasic = 114
            matlab = 115
            mirc = 116
            mmix = 117
            modula2 = 118
            modula3 = 119
            mpasm = 121
            mxml = 122
            mysql = 123
            nagios = 124
            newlisp = 125
            text = 126
            nsis = 127
            oberon2 = 128
            objeck = 129
            objc = 130
            octave = 133
            pf = 134
            glsl = 135
            oobas = 136
            oracle11 = 137
            oracle8 = 138
            oz = 139
            parasail = 140
            parigp = 141
            pascal = 142
            pawn = 143
            pcre = 144
            per = 145
            perl = 146
            perl6 = 147
            php = 148
            php_brief = 149
            pic16 = 150
            pike = 151
            pixelbender = 152
            plsql = 153
            postgresql = 154
            povray = 155
            powershell = 156
            powerbuilder = 157
            proftpd = 158
            progress = 159
            prolog = 160
            properties = 161
            providex = 162
            purebasic = 163
            pycon = 164
            python = 165
            pys60 = 166
            q = 167
            qbasic = 168
            rsplus = 169
            rails = 170
            rebol = 171
            reg = 172
            rexx = 173
            robots = 174
            rpmspec = 175
            ruby = 176
            gnuplot = 177
            sas = 178
            scala = 179
            scheme = 180
            scilab = 181
            sdlbasic = 182
            smalltalk = 183
            smarty = 184
            spark = 185
            sparql = 186
            sql = 187
            stonescript = 188
            systemverilog = 189
            tsql = 190
            tcl = 191
            teraterm = 192
            thinbasic = 193
            typoscript = 194
            unicon = 195
            uscript = 196
            ups = 197
            urbi = 198
            vala = 199
            vbnet = 200
            vedit = 201
            verilog = 202
            vhdl = 203
            vim = 204
            visualprolog = 205
            vb = 206
            visualfoxpro = 207
            whitespace = 208
            whois = 209
            winbatch = 210
            xbasic = 211
            xml = 212
            xorg_conf = 213
            xpp = 214
            yaml = 215
            z80 = 216
            zxbasic = 217
        End Enum

        Private Shared Function getSyntax(ByVal s As syntax) As String
            Dim Syntax As String = s.ToString
            Select Case Syntax
                Case Is = "sixfivenulltwoacme"
                    Syntax = "6502acme"
                    Return Syntax
                Case Is = "sixfivenulltwokickass"
                    Syntax = "6502kickass"
                    Return Syntax
                Case Is = "sixfivenulltwotasm"
                    Syntax = "6502tasm"
                    Return Syntax
                Case Is = "cpp_qt"
                    Syntax = "cpp-qt"
                    Return Syntax
                Case Is = "php_brief"
                    Syntax = "php-brief"
                    Return Syntax
                Case Else
                    Return Syntax
            End Select
        End Function
        Public Enum PasteExpire
            Never = 0
            ten_minutes = 1
            one_hour = 2
            one_day = 3
            one_week = 4
            two_weeks = 5
            one_month = 6
        End Enum
      
        Public Enum Privacy
            PublicPaste
            UnlistedPaste
            PrivatePaste
        End Enum
        Private Shared Function getPrivacy(ByVal s As Privacy) As String
            Select Case s.ToString
                Case "PrivatePaste"
                    Return "2"
                Case "PublicPaste"
                    Return "0"
                Case "UnlistedPaste"
                    Return "1"
                Case Else
            End Select
        End Function
        ''' <summary>
        ''' Pastes a Text and returns the URL
        ''' </summary>
        ''' <param name="Title">Your title</param>
        ''' <param name="Text">Your text</param>
        ''' <param name="useLogin">UseLogin or Pasteanonymous</param>
        ''' <param name="syntax">The syntaxhighliting</param>
        ''' <param name="expire">The expiredate</param>
        ''' <param name="privacy">the privacy settings</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Paste(ByVal Title As String, ByVal Text As String, ByVal useLogin As Boolean, ByVal syntax As String, ByVal expire As PasteExpire, ByVal privacy As Privacy) As Uri
            If Not frmMain.txt_pbUsername.Text = String.Empty AndAlso Not frmMain.txt_pbPassword.Text = String.Empty Then
                Username = frmMain.txt_pbUsername.Text
                Password = frmMain.txt_pbPassword.Text
            End If
            Dim Syntax_ As String
            Dim Expire_ As String
            Dim Privacy_ As String
            Dim UserAPIKey As String = GetUserAPIKey()
            If useLogin = False AndAlso privacy = Pastebin.Privacy.PrivatePaste Then
                Return Nothing
                Exit Function
            End If
            Syntax_ = Functions.getPastebinSyntaxHighliting
            Privacy_ = getPrivacy(privacy)
            If String.IsNullOrEmpty(Text) Then
                Return Nothing
                Exit Function
            End If
            Dim Post As String = "api_option=paste&api_user_key=" & UserAPIKey & "&api_paste_private=" & Privacy_ & "&api_paste_name=" & Title & "&api_paste_expire_date=N&papi_paste_format.=" & Syntax_ & "&api_dev_key=" & dev_key & "&api_paste_code=" & Text
            Dim URL As String = "http://pastebin.com/api/api_post.php"
            Dim ReturnLink As String = CStr(Postreq(URL, Post))
            If ReturnLink.Contains("limit") Then
                MessageBox.Show("You've reached your total limit of pastes in the next 24hours! Please use an other Hoster")
                Exit Function
            End If
            Dim ReturnUri As Uri = New Uri(ReturnLink)
            If ReturnUri IsNot Nothing Then
                Return ReturnUri
            End If
        End Function
        ''' <summary>
        ''' The structure "pastes"
        ''' </summary>
        ''' <remarks></remarks>
        Public Structure Pastes
            Dim PasteKey As List(Of String)
            Dim PasteDate As List(Of Integer)
            Dim PasteTitle As List(Of String)
            Dim PasteSize As List(Of Integer)
            Dim PasteExpireDate As List(Of Integer)
            Dim PasteIsPrivate As List(Of Boolean)
            Dim PasteFormat As List(Of String)
            Dim PasteUrl As List(Of Uri)
            Dim PasteHits As List(Of Integer)
        End Structure
        ''' <summary>
        ''' Returns a List of your Pastes
        ''' </summary>
        ''' <returns>Specific List's</returns>
        ''' <remarks></remarks>
        Public Shared Function getUserPastes(ByVal Limit As Integer) As Pastes
            Dim PasteKey As New List(Of String)
            Dim PasteDate As New List(Of Integer)
            Dim PasteTitle As New List(Of String)
            Dim PasteSize As New List(Of Integer)
            Dim PasteExpireDate As New List(Of Integer)
            Dim PasteIsPrivate As New List(Of Boolean)
            Dim PasteFormat As New List(Of String)
            Dim PasteUrl As New List(Of Uri)
            Dim PasteHits As New List(Of Integer)
            Dim UserAPIKey As String = GetUserAPIKey()
            Dim post As String = "api_option=list&api_user_key=" & UserAPIKey & "&api_dev_key=" & dev_key & "&api_results_limit=" & Limit.ToString
            Dim html As String = CStr(Postreq("http://pastebin.com/api/api_post.php", post))
            For Each m As Match In New Regex("<paste_key>(.+)</paste_key>").Matches(html)
                PasteKey.Add(m.Groups.Item(1).Value)
            Next
            For Each m As Match In New Regex("<paste_date>(.+)</paste_date>").Matches(html)
                PasteDate.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_title>(.+)</paste_title>").Matches(html)
                PasteTitle.Add(m.Groups.Item(1).Value)
            Next
            For Each m As Match In New Regex("<paste_size>(.+)</paste_size>").Matches(html)
                PasteSize.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_expire_date>(.+)</paste_expire_date>").Matches(html)
                PasteExpireDate.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_private>(.+)</paste_private>").Matches(html)
                If m.Groups.Item(1).Value = "0" Then
                    PasteIsPrivate.Add(False)
                Else
                    PasteIsPrivate.Add(True)
                End If
            Next
            For Each m As Match In New Regex("<paste_format_long>(.+)</paste_format_long>").Matches(html)
                PasteFormat.Add(m.Groups.Item(1).Value)
            Next
            For Each m As Match In New Regex("<paste_url>(.+)</paste_url>").Matches(html)
                PasteUrl.Add(New Uri(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_hits>(.+)</paste_hits>").Matches(html)
                PasteHits.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            Dim Paste As New Pastes
            Paste.PasteDate = PasteDate
            Paste.PasteExpireDate = PasteExpireDate
            Paste.PasteFormat = PasteFormat
            Paste.PasteHits = PasteHits
            Paste.PasteIsPrivate = PasteIsPrivate
            Paste.PasteKey = PasteKey
            Paste.PasteSize = PasteSize
            Paste.PasteTitle = PasteTitle
            Paste.PasteUrl = PasteUrl
            Return Paste
        End Function
        ''' <summary>
        ''' Returns a List of Trending Pastes
        ''' </summary>
        ''' <returns>Specific List's</returns>
        ''' <remarks></remarks>
        Public Shared Function getTrandingPastes() As Pastes
            Dim post As String = "api_option=trends&api_dev_key=" & dev_key
            Dim PasteKey As New List(Of String)
            Dim PasteDate As New List(Of Integer)
            Dim PasteTitle As New List(Of String)
            Dim PasteSize As New List(Of Integer)
            Dim PasteExpireDate As New List(Of Integer)
            Dim PasteIsPrivate As New List(Of Boolean)
            Dim PasteFormat As New List(Of String)
            Dim PasteUrl As New List(Of Uri)
            Dim PasteHits As New List(Of Integer)
            Dim html As String = CStr(Postreq("http://pastebin.com/api/api_post.php", post))
            For Each m As Match In New Regex("<paste_key>(.+)</paste_key>").Matches(html)
                PasteKey.Add(m.Groups.Item(1).Value)
            Next
            For Each m As Match In New Regex("<paste_date>(.+)</paste_date>").Matches(html)
                PasteDate.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_title>(.+)</paste_title>").Matches(html)
                PasteTitle.Add(m.Groups.Item(1).Value)
            Next
            For Each m As Match In New Regex("<paste_size>(.+)</paste_size>").Matches(html)
                PasteSize.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_expire_date>(.+)</paste_expire_date>").Matches(html)
                PasteExpireDate.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_private>(.+)</paste_private>").Matches(html)
                If m.Groups.Item(1).Value = "0" Then
                    PasteIsPrivate.Add(False)
                Else
                    PasteIsPrivate.Add(True)
                End If
            Next
            For Each m As Match In New Regex("<paste_format_long>(.+)</paste_format_long>").Matches(html)
                PasteFormat.Add(m.Groups.Item(1).Value)
            Next
            For Each m As Match In New Regex("<paste_url>(.+)</paste_url>").Matches(html)
                PasteUrl.Add(New Uri(m.Groups.Item(1).Value))
            Next
            For Each m As Match In New Regex("<paste_hits>(.+)</paste_hits>").Matches(html)
                PasteHits.Add(Integer.Parse(m.Groups.Item(1).Value))
            Next
            Dim Paste As New Pastes
            Paste.PasteDate = PasteDate
            Paste.PasteExpireDate = PasteExpireDate
            Paste.PasteFormat = PasteFormat
            Paste.PasteHits = PasteHits
            Paste.PasteIsPrivate = PasteIsPrivate
            Paste.PasteKey = PasteKey
            Paste.PasteSize = PasteSize
            Paste.PasteTitle = PasteTitle
            Paste.PasteUrl = PasteUrl
            Return Paste
        End Function
        ''' <summary>
        ''' Deletes a paste
        ''' </summary>
        ''' <param name="PasteKey">The pastekey</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DeletePaste(ByVal PasteKey As String) As Boolean
            Dim URL As String = "http://pastebin.com/api/api_post.php"
            Dim APIUserKey As String = GetUserAPIKey()
            Dim post As String = "api_option=delete&api_user_key=" & APIUserKey & "&api_dev_key=" & dev_key & "&api_paste_key=" & PasteKey
            Dim html As String = CStr(Postreq(URL, post))
            If html.Contains("Paste Removed") Then
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' Returns the RAW data from a paste
        ''' </summary>
        ''' <param name="PasteKey">Your pastekey</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetPasteText(ByVal PasteKey As String) As String
            With New WebClient
                Dim html As String = .DownloadString("http://pastebin.com/raw.php?i=" & PasteKey)
                Return html
            End With
        End Function
    End Class
    ''' <summary>
    ''' Class to Paste at Hastebin
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Hastebin
        Public Shared Function Paste(Text As String) As Uri
            Dim post As String = Text
            Dim url As String = "http://hastebin.com/documents"
            Dim html As String = CStr(Postreq(url, post))
            Dim id As String = New Regex("{""key"":""([a-zA-Z0-9]+)""}").Match(html).Groups.Item(1).Value
            Return New Uri("http://hastebin.com/" & id)
        End Function
        Public Shared Function getPlaintext(ID As String) As String
            Dim html As String = Getreq("http://hastebin.com/raw/" & ID)
            Return html
        End Function
    End Class
    Public Class Chop
        ''' <summary>
        ''' Class to paste at Chop
        ''' </summary>
        ''' <param name="Text"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Paste(Text As String) As Uri
            Dim Highliting As String = Functions.GetChopSyntaxHighliting
            Dim post As String = "code=" & Text & "&language=" & Highliting & "&url=Drop+in+a+URL..."
            Dim url As String = "http://chopapp.com/code_snips"
            Dim html As String = CStr(Postreq(url, post))
            Dim id As String = New Regex("""token"":""([a-zA-Z0-9]+)""").Match(html).Groups.Item(1).Value
            Return New Uri("http://chopapp.com/#" & id)
        End Function
    End Class
    ''' <summary>
    ''' Class to use the Tinypaste API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Tinypast
        Public Shared Property Username As String
        Public Shared Property Password As String
        Public Shared Function Paste(Text As String, Title As String, Optional subdomain As String = "", Optional privatee As Boolean = False) As String
            If Not frmMain.txt_tpUsername.Text = String.Empty AndAlso Not frmMain.txt_tpPassword.Text = String.Empty Then
                Username = frmMain.txt_tpUsername.Text
                Password = frmMain.txt_tpPassword.Text
            End If
            Dim private_ As String
            Select Case privatee
                Case True
                    private_ = "1"
                Case False
                    private_ = "0"
            End Select
            Dim url As String = "http://tny.cz/api/create.xml?paste=" & Text & "&title=" & Title & "&subdomain=" & subdomain & "&is_private=" & private_ & "&authenticate=" & Hoster.Tinypast.Username & ":" & Functions.MD5StringHash(Hoster.Tinypast.Password).ToLower
            Dim html As String = Http.Getreq(url)
            If Not html.Contains("error") Then
                Dim tiny_url_1 As String = New Regex("<response>([a-zA-Z0-9]+)</response>").Match(html).Groups.Item(1).Value

                If Not String.IsNullOrEmpty(subdomain) Then
                    Return "http://" & subdomain & ".tny.cz/" & tiny_url_1
                Else
                    Return "http://tny.cz/" & tiny_url_1
                End If
            Else
                Select Case MessageBox.Show("An error occured. Copy to Clipboard?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error)
                    Case DialogResult.Yes
                        Clipboard.SetText(html)
                        Exit Function
                    Case DialogResult.No
                        Exit Function
                End Select
                Exit Function
            End If
        End Function
        Public Shared Function TryLogin(User As String, pass As String) As Boolean
            If Http.Getreq("http://tny.cz/api/edit.xml?id=4917c0a6&authenticate=" & User & ":" & Functions.MD5StringHash(pass).ToLower).Contains("success") Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
    ''' <summary>
    ''' Class to use the bit.ly API
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Bitly
        Private Shared Property client_id As String = "b8e0ea22c2feed70f22d2a9e280720eb56e9ae86"
        Private Shared Property client_secret As String = "2c22560d37d5146e4ada093db17df27bf95bb2af"
        Private Shared Property redirect_uri As String = "https://github.com/po-tato"
        Public Shared Sub VerifyUser()
            Process.Start("https://bitly.com/oauth/authorize?client_id=" & client_id & "&redirect_uri=" & redirect_uri)
        End Sub
        Public Shared Function EndVerify(Code As String) As Boolean
            Dim html As String = Postreq("https://api-ssl.bitly.com/oauth/access_token?client_id=" & client_id & "&client_secret=" & client_secret & "&code=" & Code & "&redirect_uri=" & redirect_uri, "").ToString

            If html.Contains("access_token") Then
                Dim AccessKey As String = New Regex("access_token=([A-Za-z0-9]+)").Match(html).Groups.Item(1).Value
                My.Settings.bit_accesskey = AccessKey
                '   MessageBox.Show("Yay, your bit.ly account was successfully verified")
                frmMain.btn_startauthorization.Enabled = False
                frmMain.btn_completeauthorization.Enabled = False
                frmMain.txt_code.Enabled = False
                Return True
            Else
                Return False
                '   MessageBox.Show("Sorry, we couldn't verify your code, please try again later", "Sorry :(", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Function
        Public Shared Function Shorten(longurl As Uri) As Uri
            If Not String.IsNullOrEmpty(My.Settings.bit_accesskey) = True Then
                Dim url_ As String = "https://api-ssl.bitly.com"
                Dim post As String = "/v3/shorten?access_token=" & My.Settings.bit_accesskey & "&longUrl=" & longurl.ToString
                Dim html As String = Http.Getreq(url_ & post)
                Dim url As String = New Regex("/([a-zA-Z0-9]+)"", ""hash"":").Match(html).Groups.Item(1).Value
                If String.IsNullOrEmpty(url) Then
                    MessageBox.Show("Error while shorten your link")
                    Return longurl
                    Exit Function
                End If
                Return New Uri("http://bit.ly/" & url)
            Else
                Return New Uri("http://bit.ly/" & New Regex("""hash"": ""([a-zA-Z0-9]+)""").Match(Http.Getreq("http://api.bit.ly/v3/shorten?login=simplepaste&apiKey=R_fd71a2b9f1b64b73bc753b3ac83d5850&longUrl=" & Uri.EscapeDataString(longurl.ToString) & "format=txt")).Groups.Item(1).Value)

            End If
        End Function
    End Class
    ''' <summary>
    ''' Class to use the Adf.ly API 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Adfly
        Public Shared Property APIKey As String
        Public Shared Property UserID As String
        Public Shared Function Login(Email As String, Password As String) As Boolean
            Dim tokenhtml As String = Http.Getreq("https://login.adf.ly/login")
            Dim token As String = New Regex("id=""token"" value=""(.+)""/>").Match(tokenhtml).Groups.Item(1).Value
            Dim post As String = "token=" & token & "&bmlUrl=&dest=&email=" & Email & "&password=" & Password
            Dim html As String = Http.Postreq("https://login.adf.ly/login", post)
            If html.Contains("That is an incorrect email / password combination.") Then
                Return False
            Else
                Dim html1 As String = Http.Getreq("https://adf.ly/publisher/tools#tools-api")
                Dim userid As String = New Regex("var THIS_USER_ID = ([0-9]+);").Match(html1).Groups.Item(1).Value
                Hoster.Adfly.UserID = userid
                Dim apikey As String = New Regex("var THIS_API_KEY = '([a-zA-Z0-9]+)';").Match(html1).Groups.Item(1).Value
                Hoster.Adfly.APIKey = apikey
                Return True
            End If
        End Function
        Public Shared Function Shorten(Url As Uri) As Uri
            If Not UserID = String.Empty AndAlso Not APIKey = String.Empty Then
                Dim _url As String = " http://api.adf.ly/api.php?key=" & APIKey & "&uid=" & UserID & "&advert_type=" & Functions.getAdflyType & "&domain=" & Functions.getAdflyDomain & "&url=" & Url.ToString
                Dim apiurl As String = Http.Getreq(_url)
                Return New Uri(apiurl)
            End If
        End Function
    End Class
    ''' <summary>
    ''' Class to use the Goo.gl API with OAuth
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Googl
        Private Shared Property client_id As String = "569470551360-ctuedv0bbvhsgs9ei2412pr5sk7v8pkb.apps.googleusercontent.com"
        Private Shared Property client_secret As String = "vWFkaotVwaoo8V9ifX5MfQss"
        Private Shared Property redirect_uri As String = "urn:ietf:wg:oauth:2.0:oob"
        Public Shared Sub OpenAuthorization()
            Dim url As String = String.Format("https://accounts.google.com/o/oauth2/auth?response_type={0}&client_id={1}&redirect_uri={2}&scope={3}",
               "code", client_id, redirect_uri, Uri.EscapeUriString("https://www.googleapis.com/auth/urlshortener"))
            Process.Start(url)
        End Sub
        Public Shared Function EndVerification(code As String) As Boolean
            Try
                Dim url As String = "https://accounts.google.com/o/oauth2/token"
                Dim post As String = "code=" & code & "&client_id=" & client_id & "&client_secret=" & client_secret & "&" & "redirect_uri=" & redirect_uri & "&" & "grant_type=authorization_code"
                Dim request As HttpWebRequest
                request = CType(HttpWebRequest.Create(url), HttpWebRequest)
                request.Method = WebRequestMethods.Http.Post
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:29.0) Gecko/20100101 Firefox/29.0"
                request.ContentType = "application/x-www-form-urlencoded"
                Dim byteArr() As Byte = Encoding.Default.GetBytes(post)
                request.ContentLength = byteArr.Length
                Dim dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArr, 0, byteArr.Length)
                Dim response As HttpWebResponse
                response = CType(request.GetResponse(), HttpWebResponse)
                Dim html As String = New StreamReader(response.GetResponseStream()).ReadToEnd()
                If html.Contains("access_token") Then
                    Dim accesstoken As String = New Regex("""access_token"" : ""(.+)"",").Match(html).Groups.Item(1).Value
                    Dim refreshtoken As String = New Regex("""refresh_token"" : ""(.+)""").Match(html).Groups.Item(1).Value
                    My.Settings.gg_accesstoken = accesstoken
                    My.Settings.gg_refreshtoken = refreshtoken
                    Return True
                Else
                    Return False
                End If
            Catch ex As WebException
                If ex.Message.Contains("400") Then
                    MessageBox.Show("Sorry, the verification was not successful", ":(", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return False
            End Try
        End Function
        Public Shared Function Shorten(Url As Uri) As Uri
            If My.Settings.gg_accesstoken = String.Empty Then
                ' MessageBox.Show("Empty")
                Dim post As String = "{""longUrl"": """ & Url.ToString & """}"
                Dim requrl As String = "https://www.googleapis.com/urlshortener/v1/url"
                Dim html As String = Http.JsonPostreq(requrl, post)
                Dim shorturl As Uri = New Uri(New Regex("""id"": ""(.+)"",").Match(html).Groups.Item(1).Value)
                If Not shorturl = Nothing Then
                    Return shorturl
                Else
                    MessageBox.Show("Error while shorting your link!")
                    Return Url
                End If
            Else
                Debug.Print(My.Settings.gg_accesstoken)
                'MessageBox.Show("NOT Empty")
                Dim post As String = "{""longUrl"": """ & Url.ToString & """}"
                Debug.Print(post)
                Dim requrl As String = "https://www.googleapis.com/urlshortener/v1/url?access_token=" & My.Settings.gg_accesstoken
                Dim html As String = Http.JsonPostreq(requrl, post)
                Debug.Print("POST=" & vbNewLine & post)
                Debug.Print("Acces=" & My.Settings.gg_accesstoken)
                Debug.Print("HTML=" & vbNewLine & html)
                Dim shorturl As Uri
                If Not html = Nothing Then
                    shorturl = New Uri(New Regex("""id"": ""(.+)"",").Match(html).Groups.Item(1).Value)
                End If
                If Not shorturl = Nothing Then
                    Return shorturl
                Else
                    MessageBox.Show("Error while shorting your link!")
                    Return Url
                End If
            End If
        End Function
        Public Shared Sub RefreshToken()
            If Not My.Settings.gg_refreshtoken = String.Empty Then

            Else
                Return
            End If
        End Sub
    End Class
End Namespace