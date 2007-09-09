Public Class AppWrp
    Private Declare Function CreateProcess Lib "coredll.dll" ( _
            ByVal imageName As String, _
            ByVal cmdLine As String, _
            ByVal lpProcessAttributes As IntPtr, _
            ByVal lpThreadAttributes As IntPtr, _
            ByVal boolInheritHandles As Int32, _
            ByVal dwCreationFlags As Int32, _
            ByVal lpEnvironment As IntPtr, _
            ByVal lpCurrentDir As IntPtr, _
            ByVal si() As Byte, _
            ByVal pi As ProcessInfo) As Int32

    Private Class ProcessInfo
        Public hProcess As IntPtr
        Public hThread As IntPtr
        Public ProcessID As IntPtr
        Public ThreadID As IntPtr
    End Class

    Public Sub Start(ByVal AppPath As String, ByVal IP As String, ByVal port As Integer, Optional ByVal Login As String = Nothing, Optional ByVal pass As String = Nothing)
        Dim cmdLine As String
        Const cSpace As String = " "
        cmdLine += IP
        cmdLine += cSpace & CStr(port)
        cmdLine += cSpace & Login
        cmdLine += cSpace & pass
        Me.mCreateProcess(AppPath, cmdLine, Nothing)
    End Sub

    Public Sub Run(ByVal AppPath As String)
        Me.mCreateProcess(AppPath, Nothing, Nothing)
    End Sub

    Private Function mCreateProcess(ByVal ExeName As String, ByVal CmdLine As String, ByVal pi As ProcessInfo) As Boolean
        If pi Is Nothing Then
            pi = New ProcessInfo
        End If
        Dim si(128) As Byte
        Return CreateProcess(ExeName, CmdLine, IntPtr.Zero, IntPtr.Zero, 0, 0, IntPtr.Zero, IntPtr.Zero, si, pi) <> 0
    End Function
End Class
