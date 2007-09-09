Public Class cMain
    Public Shared Sub Main(ByVal args() As String)
        Dim login As New p7.NetWorking.p7FTP.LoginStruct

        Try
            login.mAddress = args(0)
            login.mPort = args(1)
            'wenn bis hierher alles da -> dann gleich connecten
            login.mLogin = args(2)
            login.mPassword = args(3)
            'wenn ale werte übergebenb sind --> dann auch autologin
        Catch ex As Exception

        End Try

        Application.Run(New frmFTP(login))
    End Sub
End Class