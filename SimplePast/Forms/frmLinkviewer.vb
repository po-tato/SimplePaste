Public Class frmLinkviewer
    Public Shared Sub Open(Url As Uri)
        '  frmLinkviewer.Show()
        frmLinkviewer.TextBox1.Text = Url.ToString
        '   frmLinkviewer.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width - frmLinkviewer.Size.Width, Screen.PrimaryScreen.WorkingArea.Height - frmLinkviewer.Size.Height)
        frmLinkviewer.Show()
        frmLinkviewer.BringToFront()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start(TextBox1.Text)
        Me.Hide()
        Me.Opacity = 0
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clipboard.SetText(TextBox1.Text)
        Me.Hide()
        Me.Opacity = 0
    End Sub
    Private Sub Me_Shown(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(Screen.PrimaryScreen.WorkingArea.Width - Me.Size.Width, Screen.PrimaryScreen.WorkingArea.Height - Me.Size.Height)
    End Sub
End Class