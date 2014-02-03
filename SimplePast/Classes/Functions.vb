Option Strict On
Option Explicit On

Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports IWshRuntimeLibrary
Imports System.Reflection
Imports Microsoft.Win32

Public Class Functions
#Region "SyntaxHighliting"
    ''' <summary>
    ''' Returns the Pastebin Syntax  Highliting Code
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPastebinSyntaxHighliting() As String
        Select Case frmMain.cb_syntax.SelectedIndex
            Case 0
                Return "4cs"
            Case 1
                Return "6502acme"
            Case 2
                Return "6502kickass"
            Case 3
                Return "6502tasm"
            Case 4
                Return "abap"
            Case 5
                Return "actionscript"
            Case 6
                Return "actionscript3"
            Case 7
                Return "ada"
            Case 8
                Return "algol68"
            Case 9
                Return "apache"
            Case 10
                Return "applescript"
            Case 11
                Return "apt_sources"
            Case 12
                Return "arm"
            Case 13
                Return "asm"
            Case 14
                Return "asp"
            Case 15
                Return "asymptote"
            Case 16
                Return "autoconf"
            Case 17
                Return "autohotkey"
            Case 18
                Return "autoit"
            Case 19
                Return "avisynth"
            Case 20
                Return "awk"
            Case 21
                Return "bascomavr"
            Case 22
                Return "bash"
            Case 23
                Return "basic4gl"
            Case 24
                Return "bibtex"
            Case 25
                Return "blitzbasic"
            Case 26
                Return "bnf"
            Case 27
                Return "boo"
            Case 28
                Return "bf"
            Case 29
                Return "c"
            Case 30
                Return "c_mac"
            Case 31
                Return "cil"
            Case 32
                Return "csharp"
            Case 33
                Return "cpp"
            Case 34
                Return "cpp-qt"
            Case 35
                Return "c_loadrunner"
            Case 36
                Return "caddcl"
            Case 37
                Return "cadlisp"
            Case 38
                Return "cfdg"
            Case 39
                Return "chaiscript"
            Case 40
                Return "clojure"
            Case 41
                Return "klonec"
            Case 42
                Return "klonecpp"
            Case 43
                Return "cmake"
            Case 44
                Return "cobol"
            Case 45
                Return "coffeescript"
            Case 46
                Return "cfm"
            Case 47
                Return "css"
            Case 48
                Return "cuesheet"
            Case 49
                Return "d"
            Case 50
                Return "dcl"
            Case 51
                Return "dcpu16"
            Case 52
                Return "dcs"
            Case 53
                Return "delphi"
            Case 54
                Return "oxygene"
            Case 55
                Return "diff"
            Case 56
                Return "div"
            Case 57
                Return "dos"
            Case 58
                Return "dot"
            Case 59
                Return "e"
            Case 60
                Return "ecmascript"
            Case 61
                Return "eiffel"
            Case 62
                Return "email"
            Case 63
                Return "epc"
            Case 64
                Return "erlang"
            Case 65
                Return "fsharp"
            Case 66
                Return "falcon"
            Case 67
                Return "fo"
            Case 68
                Return "f1"
            Case 69
                Return "fortran"
            Case 70
                Return "freebasic"
            Case 71
                Return "freeswitch"
            Case 72
                Return "gambas"
            Case 73
                Return "gml"
            Case 74
                Return "gdb"
            Case 75
                Return "genero"
            Case 76
                Return "genie"
            Case 77
                Return "gettext"
            Case 78
                Return "go"
            Case 79
                Return "groovy"
            Case 80
                Return "gwbasic"
            Case 81
                Return "haskell"
            Case 82
                Return "haxe"
            Case 83
                Return "hicest"
            Case 84
                Return "hq9plus"
            Case 85
                Return "html4strict"
            Case 86
                Return "html5"
            Case 87
                Return "icon"
            Case 88
                Return "idl"
            Case 89
                Return "ini"
            Case 90
                Return "inno"
            Case 91
                Return "intercal"
            Case 92
                Return "io"
            Case 93
                Return "j"
            Case 94
                Return "java"
            Case 95
                Return "java5"
            Case 96
                Return "javascript"
            Case 97
                Return "jquery"
            Case 98
                Return "kixtart"
            Case 99
                Return "latex"
            Case 100
                Return "ldif"
            Case 101
                Return "lb"
            Case 102
                Return "lsl2"
            Case 103
                Return "lisp"
            Case 104
                Return "llvm"
            Case 105
                Return "locobasic"
            Case 106
                Return "logtalk"
            Case 107
                Return "lolcode"
            Case 108
                Return "lotusformulas"
            Case 109
                Return "lotusscript"
            Case 110
                Return "lscript"
            Case 111
                Return "lua"
            Case 112
                Return "m68k"
            Case 113
                Return "magiksf"
            Case 114
                Return "make"
            Case 115
                Return "mapbasic"
            Case 116
                Return "matlab"
            Case 117
                Return "mirc"
            Case 118
                Return "mmix"
            Case 119
                Return "modula2"
            Case 120
                Return "modula3"
            Case 121
                Return "68000devpac"
            Case 122
                Return "mpasm"
            Case 123
                Return "mxml"
            Case 124
                Return "mysql"
            Case 125
                Return "nagios"
            Case 126
                Return "newlisp"
            Case 127
                Return "text"
            Case 128
                Return "nsis"
            Case 129
                Return "oberon2"
            Case 130
                Return "objeck"
            Case 131
                Return "objc"
            Case 132
                Return "ocaml-brief"
            Case 133
                Return "ocaml"
            Case 134
                Return "octave"
            Case 135
                Return "pf"
            Case 136
                Return "glsl"
            Case 137
                Return "oobas"
            Case 138
                Return "oracle11"
            Case 139
                Return "oracle8"
            Case 140
                Return "oz"
            Case 141
                Return "parasail"
            Case 142
                Return "parigp"
            Case 143
                Return "pascal"
            Case 144
                Return "pawn"
            Case 145
                Return "pcre"
            Case 146
                Return "per"
            Case 147
                Return "perl"
            Case 148
                Return "perl6"
            Case 149
                Return "php"
            Case 150
                Return "php-brief"
            Case 151
                Return "pic16"
            Case 152
                Return "pike"
            Case 153
                Return "pixelbender"
            Case 154
                Return "plsql"
            Case 155
                Return "postgresql"
            Case 156
                Return "povray"
            Case 157
                Return "powershell"
            Case 158
                Return "powerbuilder"
            Case 159
                Return "proftpd"
            Case 160
                Return "progress"
            Case 161
                Return "prolog"
            Case 162
                Return "properties"
            Case 163
                Return "providex"
            Case 164
                Return "purebasic"
            Case 165
                Return "pycon"
            Case 166
                Return "python"
            Case 167
                Return "pys60"
            Case 168
                Return "q"
            Case 169
                Return "qbasic"
            Case 170
                Return "rsplus"
            Case 171
                Return "rails"
            Case 172
                Return "rebol"
            Case 173
                Return "reg"
            Case 174
                Return "rexx"
            Case 175
                Return "robots"
            Case 176
                Return "rpmspec"
            Case 177
                Return "ruby"
            Case 178
                Return "gnuplot"
            Case 179
                Return "sas"
            Case 180
                Return "scala"
            Case 181
                Return "scheme"
            Case 182
                Return "scilab"
            Case 183
                Return "sdlbasic"
            Case 184
                Return "smalltalk"
            Case 185
                Return "smarty"
            Case 186
                Return "spark"
            Case 187
                Return "sparql"
            Case 188
                Return "sql"
            Case 189
                Return "stonescript"
            Case 190
                Return "systemverilog"
            Case 191
                Return "tsql"
            Case 192
                Return "tcl"
            Case 193
                Return "teraterm"
            Case 194
                Return "thinbasic"
            Case 195
                Return "typoscript"
            Case 196
                Return "unicon"
            Case 197
                Return "uscript"
            Case 198
                Return "ups"
            Case 199
                Return "urbi"
            Case 200
                Return "vala"
            Case 201
                Return "vbnet"
            Case 202
                Return "vedit"
            Case 203
                Return "verilog"
            Case 204
                Return "vhdl"
            Case 205
                Return "vim"
            Case 206
                Return "visualprolog"
            Case 207
                Return "vb"
            Case 208
                Return "visualfoxpro"
            Case 209
                Return "whitespace"
            Case 210
                Return "whois"
            Case 211
                Return "winbatch"
            Case 212
                Return "xbasic"
            Case 213
                Return "xml"
            Case 214
                Return "xorg_conf"
            Case 215
                Return "xpp"
            Case 216
                Return "yaml"
            Case 217
                Return "z80"
            Case 218
                Return "zxbasic"
        End Select
    End Function
    ''' <summary>
    ''' Returns the Chop Syntax Highliting Code
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetChopSyntaxHighliting() As String
        Select Case frmMain.cb_syntax.SelectedIndex
            Case 33
                Return "c"
            Case 74
                Return "css"
            Case 94
                Return "java"
            Case 96
                Return "javascript"
            Case 149
                Return "php"
            Case 166
                Return "python"
            Case 177
                Return "ruby"
            Case Else
                Return "html"
        End Select
    End Function
#End Region
#Region "MD5"
    ''' <summary>
    ''' MD5 Function, which returns a MD5 hash from a string
    ''' </summary>
    ''' <param name="strString">The String</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MD5StringHash(ByVal strString As String) As String
        Dim MD5 As New MD5CryptoServiceProvider
        Dim Data As Byte()
        Dim Result As Byte()
        Dim Res As String = String.Empty
        Dim Tmp As String = String.Empty

        Data = Encoding.ASCII.GetBytes(strString)
        Result = MD5.ComputeHash(Data)
        For i As Integer = 0 To Result.Length - 1
            Tmp = Hex(Result(i))
            If Len(Tmp) = 1 Then Tmp = "0" & Tmp
            Res += Tmp
        Next
        Return Res
    End Function
#End Region
#Region "AES"
    ''' <summary>
    ''' Both functions by http://www.obviex.com/samples/Encryption.aspx
    ''' </summary>
    ''' <param name="plainText"></param>
    ''' <param name="passPhrase"></param>
    ''' <param name="saltValue"></param>
    ''' <param name="hashAlgorithm"></param>
    ''' <param name="passwordIterations"></param>
    ''' <param name="initVector"></param>
    ''' <param name="keySize"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Encrypt _
           ( _
               ByVal plainText As String, _
               ByVal passPhrase As String, _
               ByVal saltValue As String, _
               ByVal hashAlgorithm As String, _
               ByVal passwordIterations As Integer, _
               ByVal initVector As String, _
               ByVal keySize As Integer _
           ) _
           As String
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(plainText)
        Dim password As PasswordDeriveBytes
        password = New PasswordDeriveBytes _
        ( _
            passPhrase, _
            saltValueBytes, _
            hashAlgorithm, _
            passwordIterations _
        )
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(CInt(keySize / 8))
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim encryptor As ICryptoTransform
        encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream()
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream _
        ( _
            memoryStream, _
            encryptor, _
            CryptoStreamMode.Write _
        )
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        cryptoStream.FlushFinalBlock()
        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()
        memoryStream.Close()
        cryptoStream.Close()
        Dim cipherText As String
        cipherText = Convert.ToBase64String(cipherTextBytes)
        Encrypt = cipherText
    End Function
    Public Shared Function Decrypt _
    ( _
        ByVal cipherText As String, _
        ByVal passPhrase As String, _
        ByVal saltValue As String, _
        ByVal hashAlgorithm As String, _
        ByVal passwordIterations As Integer, _
        ByVal initVector As String, _
        ByVal keySize As Integer _
    ) _
    As String
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)
        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)
        Dim cipherTextBytes As Byte()
        cipherTextBytes = Convert.FromBase64String(cipherText)
        Dim password As PasswordDeriveBytes
        password = New PasswordDeriveBytes _
        ( _
            passPhrase, _
            saltValueBytes, _
            hashAlgorithm, _
            passwordIterations _
        )
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(CInt(keySize / 8))
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()
        symmetricKey.Mode = CipherMode.CBC
        Dim decryptor As ICryptoTransform
        decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream(cipherTextBytes)
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream _
        ( _
            memoryStream, _
            decryptor, _
            CryptoStreamMode.Read _
        )
        Dim plainTextBytes As Byte()
        ReDim plainTextBytes(cipherTextBytes.Length)
        Dim decryptedByteCount As Integer
        decryptedByteCount = cryptoStream.Read _
        ( _
            plainTextBytes, _
            0, _
            plainTextBytes.Length _
        )

        memoryStream.Close()
        cryptoStream.Close()
        Dim plainText As String
        plainText = Encoding.UTF8.GetString _
        ( _
            plainTextBytes, _
            0, _
            decryptedByteCount _
        )
        Decrypt = plainText
    End Function
#End Region
#Region "Normal Functions"
    ''' <summary>
    ''' Checks for Updates with the Updatesystem.NET
    ''' </summary>
    ''' <param name="async"></param>
    ''' <remarks></remarks>
    Public Shared Sub CheckForUpdates(async As Boolean)
        Dim updController As New updateSystemDotNet.updateController()
        updController.updateUrl = "http://simplepaste.si.funpic.de/Update/"
        updController.projectId = "5dd1008a-5ddb-46b4-8241-48ce6847d1e2"
        updController.publicKey = "<RSAKeyValue><Modulus>1cedft+97N7g4p36XijybR+v5+stwvJ1E1gXvLhNk4DTUmRbVEjrxc8UhDbFahjZ4agJLeh+KNjn1aQiTflaA1a5Sn8f70Jvbb6Bs53nMyeUtgUySGmU9LdMbAuPKc3333+j8dW9GtUv7w/Kt+wAFchUW99yQ+LVoP7KXtbhQjDxqM++5WCxXcmYVzE/4GKOhG/iDBD2CDzvkZYG66ZVWBqIMWp6GAqIHPPA7BgDBHPIE4k+jaupUyCBj3px8aQJ8UreOo6/0evoGPTTEJ05KFzC8LUcul0irioMyjIZoS3mxhPqKevVKOsYayHP10hJ64sRpe9hOA2J7iIqujLcLOQV/PyEYP1mJsrA6YmiwdWcSJtesKXnQvgm/LpbK4yHEY3T2sIRxHEg9Okeg9A68Fh9iUd8gwddV8VZ1BBhV/+n0GNU1MtpfHs2yiar5U4cVpebUELzMGdgAwpaq8eCLHgiIK8aJswKNiC0E0hqCAZF4fYhKI2CMa0jJ1N6GK1H3g6YYFB8nEAWXZJTgj2QXiXuuYHwh5ol0VUtSGUByllcxDrt8xBk87vlOukVejFx/Mu/ZmFGPfX4gIWGts663B5Mo0sFdTIKKo7+t0JRM3wyqTfV1GQBFPK3QXyGH4rwH0o97o46/ONwWOxYU3Z88UMaMda/crQZe+Q6/w+OLe0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
        updController.releaseFilter.checkForFinal = True
        updController.releaseFilter.checkForBeta = True
        updController.releaseFilter.checkForAlpha = True
        updController.restartApplication = True
        updController.retrieveHostVersion = True
        If async = False Then
            updController.checkForUpdatesAsync()
        Else
            updController.updateInteractive()
        End If
    End Sub
    ''' <summary>
    ''' Returns the Title of the paste
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getTitle() As String
        Dim originalTitle As String = frmMain.txt_titleFormation.Text
        Dim a As String = originalTitle
        If a.Contains("[datetime]") Then
            a = a.Replace("[datetime]", DateTime.Now.ToString)
        End If
        If a.Contains("[timestamp]") Then
            a = a.Replace("[timestamp]", DateTime.Now.Millisecond.ToString)
        End If
        If a.Contains("[username]") Then
            a = a.Replace("[username]", Environment.UserName)
        End If
        Dim txt As String = a.Split("&"c)(1).Split(Chr(34))(1).Replace(Chr(34), "")
        a = a.Replace(Chr(34), "").Replace("&", "")
        Return a
    End Function
    Public Enum Hoster
        Pastebin
        Tinypaste
        Hastebin
        Chop
    End Enum
    Public Enum Shorter
        Bitly
        Googl
        adfly
    End Enum
    ''' <summary>
    ''' Returns the favourite Hoster
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHoster() As Hoster
        Select Case frmMain.cb_Hoster.SelectedIndex
            Case 0
                Return Hoster.Pastebin
            Case 1
                Return Hoster.Tinypaste
            Case 2
                Return Hoster.Hastebin
            Case 3
                Return Hoster.Chop
        End Select
    End Function
    ''' <summary>
    ''' Returns the favourite URL Shortener
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getShortener() As Shorter
        Select Case frmMain.cb_linkshortener.SelectedIndex
            Case 0
                Return Shorter.Bitly
            Case 1
                Return Shorter.Googl
            Case 2
                Return Shorter.adfly
        End Select
    End Function
    ''' <summary>
    ''' Adds the program to the Autostart
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Shared Sub AddToAutostart(value As Boolean)
        Select Case value
            Case True
                Dim WshShell As New IWshShell_Class
                Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
                Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup)
                MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\SimplePaste.lnk"), IWshRuntimeLibrary.IWshShortcut)
                MyShortcut.TargetPath = (New System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath 'Specify target app full path
                MyShortcut.Save()
            Case False
                If IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "/SimplePaste.lnk") Then
                    IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "/SimplePaste.lnk")
                Else
                    MessageBox.Show("Nope")
                End If
        End Select
    End Sub
#End Region
#Region "Adfly"

    Public Shared Function getAdflyDomain() As String
        Select Case frmMain.cb_adfDomain.SelectedIndex
            Case 0
                Return "adf.ly"
            Case 1
                Return "q.gs"
        End Select
    End Function
    Public Shared Function getAdflyType() As String
        Select Case frmMain.cb_adfType.SelectedIndex
            Case 0
                Return "int"
            Case 1
                Return "banner"
        End Select
    End Function
#End Region
End Class
