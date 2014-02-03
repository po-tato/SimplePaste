Option Strict On
Option Explicit On
Imports System.Security.Cryptography
Imports System.Threading
'Imports IWshRuntimeLibrary
Imports System.Reflection
''' <summary>
''' SimplePaste by Thomas Schmitz - All rights reserved
''' </summary>
''' <remarks></remarks>
Public Class frmMain
#Region "Hotkey"
#Region " Methods "
    Private Sub FillKeys()
        Dim fullScreenKeys() As Keys = {Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12, Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z, Keys.PrintScreen, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9}
        For i = 0 To fullScreenKeys.Count - 1
            KeyComboBox.Items.Add(fullScreenKeys(i))
        Next
    End Sub
    Dim WithEvents hkM As HotkeyManager
    Private Function GetKeyData() As Keys
        Dim keyData As Keys = Keys.None
        If Me.cb_ctrl.Checked Then
            keyData = keyData Or Keys.Control
        End If
        If Me.cb_alt.Checked Then
            keyData = keyData Or Keys.Alt
        End If
        If Me.cb_shift.Checked Then
            keyData = keyData Or Keys.Shift
        End If
        If Me.KeyComboBox.SelectedIndex <> -1 Then
            keyData = keyData Or CType(Me.KeyComboBox.SelectedItem, Keys)
        End If
        Return keyData
    End Function
    Private Sub LoadHotkeyStuff()
        Me.hkM = New HotkeyManager(Me)
        Me.FillKeys()
    End Sub
    Private Sub LoadHotkeySettings()
        If Not My.Settings.Hotkey1 = Nothing Then
            Try
                Dim hk As New Hotkey(CInt(1), My.Settings.Hotkey1)
                Me.hkM.RegisterHotKey(hk, True)
                Me.lb_Hotkeys.Items(0) = hk.ToString & "-Upload"
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        If Not My.Settings.Hotkey2 = Nothing Then
            Try
                Dim hk As New Hotkey(CInt(2), My.Settings.Hotkey2)
                Me.hkM.RegisterHotKey(hk, True)
                Me.lb_Hotkeys.Items(1) = hk.ToString & "-Show Pastes"
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
#End Region
#Region "Events"
    Private Sub btn_saveHotkeys_Click(sender As Object, e As EventArgs) Handles btn_saveHotkeys.Click
        If lb_Hotkeys.SelectedIndex = 0 Then
            Try
                Dim hk As New Hotkey(CInt(1), Me.GetKeyData)
                Me.hkM.RegisterHotKey(hk, True)
                Me.lb_Hotkeys.Items(0) = hk.ToString & "-Upload"
                My.Settings.Hotkey1 = Me.GetKeyData
                '   MessageBox.Show(My.Settings.Hotkey1.ToString)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf lb_Hotkeys.SelectedIndex = 1 Then
            Try
                Dim hk As New Hotkey(CInt(2), Me.GetKeyData)
                Me.hkM.RegisterHotKey(hk, True)
                Me.lb_Hotkeys.Items(1) = hk.ToString & "-Show Settings"
                My.Settings.Hotkey2 = Me.GetKeyData
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show("Please select a function", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub hk_HotkeyPressed(ByVal sender As Object, ByVal e As HotkeyEventArgs) Handles hkM.HotkeyPressed
        If e.Hotkey.Id = 1 Then
            MessageBox.Show(";)")
            onPaste()
        ElseIf e.Hotkey.Id = 2 Then
            Me.Show()
            Me.Opacity = 1
            Me.ShowInTaskbar = True
            Me.BringToFront()
        End If
    End Sub
#End Region
#End Region
#Region "Loading"
    Private Sub LoadAll()
        Invoke(Sub()
                   If Not My.Settings.pb_username = String.Empty AndAlso Not My.Settings.pb_username = String.Empty Then
                       txt_pbUsername.Text = My.Settings.pb_username
                       txt_pbPassword.Text = Functions.Decrypt(My.Settings.pb_password, Environment.UserName, Environment.MachineName, "SHA1", 2, "!§$%&/()=ijsklOJ", 256)
                   End If
                   If Not My.Settings.tp_username = String.Empty AndAlso Not My.Settings.tp_password = String.Empty Then
                       txt_tpUsername.Text = My.Settings.tp_username
                       txt_tpPassword.Text = Functions.Decrypt(My.Settings.tp_password, Environment.UserName, Environment.MachineName, "SHA1", 2, "!§$%&/()=ijsklOJ", 256)
                   End If
                   cb_autoShort.Checked = My.Settings.cb_shortenLink
                   cb_Hoster.SelectedIndex = My.Settings.pref_hoster_index
                   cb_linkshortener.SelectedIndex = My.Settings.pref_shorter_index
                   cb_adfDomain.SelectedIndex = My.Settings.adf_domain
                   cb_adfType.SelectedIndex = My.Settings.adf_type
                   cb_pastePrivate.Checked = My.Settings.cb_private
                   cb_showLinkviewer.Checked = My.Settings.cb_showLink
                   cb_tpSub.Checked = My.Settings.cb_useSubdomain
                   cb_autostart.Checked = My.Settings.cb_startwithwindows
                   txt_tpSub.Text = My.Settings.txt_Subdomain
                   txt_titleFormation.Text = My.Settings.Title
                   If Not String.IsNullOrEmpty(My.Settings.bit_accesskey) Then
                       txt_code.Text = My.Settings.bit_accesskey
                       btn_completeauthorization.Enabled = False
                       btn_startauthorization.Enabled = False
                       txt_code.Enabled = False
                       lbl_bitStatus.Text &= " You've already authorized your account"
                   End If
                   If Not String.IsNullOrEmpty(My.Settings.adf_userid) AndAlso Not String.IsNullOrEmpty(My.Settings.adf_apikey) Then
                       Label18.Text = "UserID: "
                       Label17.Text = "APIKey: "
                       btn_adfLogin.Enabled = False
                       Hoster.Adfly.UserID = My.Settings.adf_userid
                       Hoster.Adfly.APIKey = My.Settings.adf_apikey
                       txt_adfUsername.Text = Hoster.Adfly.UserID
                       txt_adfPassword.Text = Hoster.Adfly.APIKey
                       txt_adfPassword.UseSystemPasswordChar = False
                       txt_adfPassword.Enabled = False
                       txt_adfUsername.Enabled = False
                       lbl_adfStatus.Text = "Status: Successfuly logged in"
                   End If
                   If Not String.IsNullOrEmpty(My.Settings.gg_accesstoken) Then
                       txt_ggCode.Text = My.Settings.gg_accesstoken
                       btn_ggStartAuth.Enabled = False
                       btn_ggComplete.Enabled = False
                       lbl_ggStatus.Text = "Status: You've already authorized your account"
                   End If
               End Sub)
    End Sub
    Private Sub adflyLogin()
        Invoke(Sub()
                   Dim email As String = txt_adfUsername.Text
                   Dim password As String = txt_adfPassword.Text
                   If Hoster.Adfly.Login(email, password) = True Then
                       Label18.Text = "UserID: "
                       Label17.Text = "APIKey: "
                       btn_adfLogin.Enabled = False
                       txt_adfUsername.Text = Hoster.Adfly.UserID
                       txt_adfPassword.Text = Hoster.Adfly.APIKey
                       txt_adfPassword.UseSystemPasswordChar = False
                       txt_adfPassword.Enabled = False
                       txt_adfUsername.Enabled = False
                       lbl_adfStatus.Text = "Status: Successfuly logged in"
                       My.Settings.adf_userid = Hoster.Adfly.UserID
                       My.Settings.adf_apikey = Hoster.Adfly.APIKey
                   Else
                       MessageBox.Show("Sorry, we couldn't verify your account. Please try again later")
                   End If
               End Sub)
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'frmLinkviewer.Open(New Uri("http://google.com"))
        Me.Hide()
        Me.Opacity = 0
        Me.ShowInTaskbar = False
        ni_main.ShowBalloonTip(1000, "SimplePaste", "SimplePaste runs now in the tray", ToolTipIcon.Info)
        Debug.Print(My.Settings.gg_refreshtoken)
        With New Thread(AddressOf LoadAll)
            .IsBackground = True
            .SetApartmentState(ApartmentState.STA)
            .Start()
        End With
        Dim parameter() As String = Environment.GetCommandLineArgs().ToArray
        If (parameter.Count - 1) >= 1 Then 'Höher als 1 weil der index 0 der Pfad zum programm ist
            For i = 1 To parameter.Count - 1
                MessageBox.Show(parameter(i))
            Next
        End If
        LoadHotkeyStuff()
        LoadHotkeySettings()
        Formation_tt.SetToolTip(txt_titleFormation, "[datetime] = actual Datetime" & vbNewLine & "[timestamp] = actual timestamp" & vbNewLine & "[usr] = your username")
        Formation_tt.ToolTipTitle = "Tags you could use"
        Formation_tt.ToolTipIcon = ToolTipIcon.Info
    End Sub

#End Region 
#Region "Login"
    Private Sub Pastebinlogin()
        Invoke(Sub()
                   Dim Username As String = txt_pbUsername.Text
                   Dim password As String = txt_pbPassword.Text
                   Dim crypto_pw As String = Functions.Encrypt(password, Environment.UserName, Environment.MachineName, "SHA1", 2, "!§$%&/()=ijsklOJ", 256)
                   My.Settings.pb_username = Username
                   My.Settings.pb_password = crypto_pw
                   My.Settings.Save()
                   Hoster.Pastebin.Username = Username
                   Hoster.Pastebin.Password = password
                   If Hoster.Pastebin.GetUserAPIKey() IsNot String.Empty Then
                       lbl_pastebinLogin.Text = "Login: successful"
                       lbl_pastebinLogin.ForeColor = Color.Green
                   Else
                       lbl_pastebinLogin.Text = "Login: not successful"
                       lbl_pastebinLogin.ForeColor = Color.Red
                   End If
               End Sub)
    End Sub
    Private Sub TinypastLogin()
        Invoke(Sub()
                   Dim Username As String = txt_tpUsername.Text
                   Dim Password As String = txt_tpPassword.Text
                   Dim crypto_pw As String = Functions.Encrypt(Password, Environment.UserName, Environment.MachineName, "SHA1", 2, "!§$%&/()=ijsklOJ", 256)
                   My.Settings.tp_username = Username
                   My.Settings.tp_password = crypto_pw
                   If Hoster.Tinypast.TryLogin(Username, Password) = True Then
                       lbl_tpStatus.Text = "Login: successful"
                       lbl_tpStatus.ForeColor = Color.Green
                   Else
                       lbl_tpStatus.Text = "Login: not successful"
                       lbl_tpStatus.ForeColor = Color.Red
                   End If
                   ' MessageBox.Show(My.Settings.tp_password)
               End Sub)
    End Sub
#End Region
#Region "Events"

    Private Sub btn_adfLogin_Click(sender As Object, e As EventArgs) Handles btn_adfLogin.Click
        With New Threading.Thread(AddressOf adflyLogin)
            .IsBackground = True
            .Start()
        End With
    End Sub

    Private Sub DeleteAll_Click(sender As Object, e As EventArgs) Handles btn_deleteAll.Click
        Select Case MessageBox.Show("Do you really want to delete all Settings? This can't be undone", "Really?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Case Windows.Forms.DialogResult.Yes
                My.Settings.Reset()
            Case Windows.Forms.DialogResult.No
                ':)
        End Select
    End Sub

    Private Sub cb_adfDomain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_adfDomain.SelectedIndexChanged
        My.Settings.adf_domain = cb_adfDomain.SelectedIndex
    End Sub

    Private Sub cb_adfType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_adfType.SelectedIndexChanged
        My.Settings.adf_type = cb_adfType.SelectedIndex
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start("http://pastebin.com/u/" & My.Settings.pb_username)
    End Sub

    Private Sub btn_ggStartAuth_Click(sender As Object, e As EventArgs) Handles btn_ggStartAuth.Click
        Hoster.Googl.OpenAuthorization()
        txt_ggCode.Enabled = True
    End Sub

    Private Sub txt_ggCode_TextChanged(sender As Object, e As EventArgs) Handles txt_ggCode.TextChanged
        btn_ggComplete.Enabled = True
    End Sub

    Private Sub btn_ggComplete_Click(sender As Object, e As EventArgs) Handles btn_ggComplete.Click
        If Hoster.Googl.EndVerification(txt_ggCode.Text) = True Then
            lbl_ggStatus.Text = "Status: Successful authenticated"
            lbl_ggStatus.ForeColor = Color.Green
        Else
            lbl_ggStatus.Text = "Status: Not successful authenticated"
            lbl_ggStatus.ForeColor = Color.Red
        End If
    End Sub

    Private Sub btn_ggOpen_Click(sender As Object, e As EventArgs) Handles btn_ggOpen.Click
        Process.Start("http://goo.gl")
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        onPaste()
    End Sub
    Private Sub Form1_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True
        Me.Hide()
        Me.Opacity = 0
        Me.ShowInTaskbar = False
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Me.Show()
        Me.Opacity = 1
        Me.ShowInTaskbar = True
        Me.BringToFront()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        My.Settings.Save()
        ni_main.Dispose()
        Application.Exit()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://github.com/po-tato")
    End Sub

    Private Sub lbl_whatsthat_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbl_whatsthat.LinkClicked
        MessageBox.Show("After verification, you'll be redirected to such a url. Please enter it in the box below")
        Process.Start("http://s14.directupload.net/images/140202/sdw9u624.png")
    End Sub

    Private Sub btn_checkforUpdates_Click(sender As Object, e As EventArgs) Handles btn_checkforUpdates.Click
        Functions.CheckForUpdates(True)
    End Sub
    Private Sub cb_autostart_CheckedChanged(sender As Object, e As EventArgs) Handles cb_autostart.CheckedChanged
        My.Settings.cb_startwithwindows = cb_autostart.Checked
        Select Case cb_autostart.Checked
            Case True
                Functions.AddToAutostart(True)
            Case False
                Functions.AddToAutostart(False)
        End Select
    End Sub
    Private Sub btn_pastebinLogin_Click(sender As Object, e As EventArgs) Handles btn_pastebinLogin.Click
        With New Threading.Thread(AddressOf Pastebinlogin)
            .IsBackground = True
            .Start()
        End With
    End Sub

    Private Sub btn_tpLogin_Click(sender As Object, e As EventArgs) Handles btn_tpLogin.Click
        With New Threading.Thread(AddressOf TinypastLogin)
            .IsBackground = True
            .Start()
        End With
    End Sub

    Private Sub StartAuthorization_Click(sender As Object, e As EventArgs) Handles btn_startauthorization.Click
        Hoster.Bitly.VerifyUser()
        txt_code.Enabled = True
    End Sub

    Private Sub CompleteAuthorization_Click(sender As Object, e As EventArgs) Handles btn_completeauthorization.Click
        If Hoster.Bitly.EndVerify(txt_code.Text) = True Then
            lbl_bitStatus.Text = "Status: Successfuly authorized your account"
        End If
    End Sub

    Private Sub Code_TextChanged(sender As Object, e As EventArgs) Handles txt_code.TextChanged
        If txt_code.Text IsNot String.Empty Then
            btn_completeauthorization.Enabled = True
        End If
    End Sub

    Private Sub cb_tpSub_CheckedChanged(sender As Object, e As EventArgs) Handles cb_tpSub.CheckedChanged
        My.Settings.cb_useSubdomain = cb_tpSub.Checked
    End Sub

    Private Sub cb_autoShort_CheckedChanged(sender As Object, e As EventArgs) Handles cb_autoShort.CheckedChanged
        My.Settings.cb_shortenLink = cb_autoShort.Checked
    End Sub

    Private Sub cb_pastePrivate_CheckedChanged(sender As Object, e As EventArgs) Handles cb_pastePrivate.CheckedChanged
        My.Settings.cb_private = cb_pastePrivate.Checked
    End Sub

    Private Sub cb_showLinkviewer_CheckedChanged(sender As Object, e As EventArgs) Handles cb_showLinkviewer.CheckedChanged
        My.Settings.cb_showLink = cb_showLinkviewer.Checked
    End Sub
    Private Sub cb_Hoster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_Hoster.SelectedIndexChanged
        My.Settings.pref_hoster_index = cb_Hoster.SelectedIndex
    End Sub

    Private Sub cb_linkshortener_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_linkshortener.SelectedIndexChanged
        My.Settings.pref_shorter_index = cb_linkshortener.SelectedIndex
    End Sub

    Private Sub txt_titleFormation_TextChanged(sender As Object, e As EventArgs) Handles txt_titleFormation.TextChanged
        My.Settings.Title = txt_titleFormation.Text
    End Sub

    Private Sub txt_tpSub_TextChanged(sender As Object, e As EventArgs) Handles txt_tpSub.TextChanged
        My.Settings.txt_Subdomain = txt_tpSub.Text
    End Sub
#End Region
#Region "Paste"
    Private Sub onPaste()
        Dim Text As String = Clipboard.GetText
        Select Case Functions.GetHoster
            Case Functions.Hoster.Pastebin
                Dim url As Uri = Hoster.Pastebin.Paste(Functions.getTitle, Text, True, Functions.GetPastebinSyntaxHighliting, Hoster.Pastebin.PasteExpire.Never, Hoster.Pastebin.Privacy.PublicPaste)
                If url = Nothing Then
                    Exit Sub
                End If
                If cb_autoShort.Checked = True Then
                    Select Case Functions.getShortener
                        Case Functions.Shorter.Bitly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Bitly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Bitly.Shorten(url).ToString)
                            End Select
                        Case Functions.Shorter.adfly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Adfly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                            End Select
                            '   Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                        Case Functions.Shorter.Googl
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Googl.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Googl.Shorten(url).ToString)
                            End Select
                    End Select
                Else
                    Select Case cb_showLinkviewer.Checked
                        Case True
                            frmLinkviewer.Open(url)
                        Case False
                            Clipboard.SetText(url.ToString)
                    End Select
                End If
            Case Functions.Hoster.Tinypaste
                Dim url As Uri = New Uri(Hoster.Tinypast.Paste(Text, Functions.getTitle, My.Settings.txt_Subdomain))
                If cb_autoShort.Checked = True Then
                    Select Case Functions.getShortener
                        Case Functions.Shorter.Bitly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Bitly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Bitly.Shorten(url).ToString)
                            End Select
                        Case Functions.Shorter.adfly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Adfly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                            End Select
                            '   Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                        Case Functions.Shorter.Googl
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Googl.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Googl.Shorten(url).ToString)
                            End Select
                    End Select
                Else
                    Select Case cb_showLinkviewer.Checked
                        Case True
                            frmLinkviewer.Open(url)
                        Case False
                            Clipboard.SetText(url.ToString)
                    End Select
                End If
            Case Functions.Hoster.Hastebin
                Dim url As Uri = Hoster.Hastebin.Paste(Text)
                If cb_autoShort.Checked = True Then
                    Select Case Functions.getShortener
                        Case Functions.Shorter.Bitly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Bitly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Bitly.Shorten(url).ToString)
                            End Select
                        Case Functions.Shorter.adfly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Adfly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                            End Select
                            '   Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                        Case Functions.Shorter.Googl
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Googl.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Googl.Shorten(url).ToString)
                            End Select
                    End Select
                Else
                    Select Case cb_showLinkviewer.Checked
                        Case True
                            frmLinkviewer.Open(url)
                        Case False
                            Clipboard.SetText(url.ToString)
                    End Select
                End If
            Case Functions.Hoster.Chop
                Dim url As Uri = Hoster.Chop.Paste(Text)
                If cb_autoShort.Checked = True Then
                    Select Case Functions.getShortener
                        Case Functions.Shorter.Bitly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Bitly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Bitly.Shorten(url).ToString)
                            End Select
                        Case Functions.Shorter.adfly
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Adfly.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                            End Select
                            '   Clipboard.SetText(Hoster.Adfly.Shorten(url).ToString)
                        Case Functions.Shorter.Googl
                            Select Case cb_showLinkviewer.Checked
                                Case True
                                    frmLinkviewer.Open(Hoster.Googl.Shorten(url))
                                Case False
                                    Clipboard.SetText(Hoster.Googl.Shorten(url).ToString)
                            End Select
                    End Select
                Else
                    Select Case cb_showLinkviewer.Checked
                        Case True
                            frmLinkviewer.Open(url)
                        Case False
                            Clipboard.SetText(url.ToString)
                    End Select
                End If
        End Select
    End Sub
#End Region
End Class
