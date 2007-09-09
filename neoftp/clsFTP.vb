Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic
Imports System.Threading

'Imports System.Runtime.Remoting.Messaging
Namespace p7.Networking
    Public Class p7FTP

        Private mTcpClient As TcpClient
        Private mNetStream As NetworkStream
        Private mBytes() As Byte
        Private intBytesRec As Int64
        Private mDataStream As NetworkStream
        Private mTcpData As New TcpClient

        Private Const DEAFULTSLEEPTIME As Integer = 10
        Private Const DEFAULTRESPONSECYCLES As Integer = 1000


        'Private lastResponce As String

        'FTP Server IP
        Private mServerAddress As IPAddress
        'events
        Friend Event FTP_ConnectionSuccess()
        Friend Event FTP_DisConnected()
        Friend Event FTP_CommandSend(ByVal command As String)
        Friend Event FTP_gotResponce(ByVal responce As String)
        Friend Event FTP_BytesReceived(ByVal bytesCount As Long)
        Friend Event FTP_BytesSend(ByVal bytesCount As Long)
        'Friend Event FTP_DownloadFinished()
        Public CancelOP As Boolean = False

        Public Structure LoginStruct
            Public mAddress As String
            Public mPort As Integer
            Public mLogin As String
            Public mPassword As String
            Public Overrides Function ToString() As String
                Return Me.mAddress
            End Function
        End Structure
        ReadOnly Property ServerAdd() As IPAddress
            Get
                ServerAdd = mServerAddress
            End Get
        End Property

        'FTP Port
        Private mFtpPort As Int32 = 21
        ReadOnly Property FtpPort() As Int32
            Get
                FtpPort = mFtpPort
            End Get
        End Property

        'Connection State
        Private mConnected As Boolean = False
        ReadOnly Property Connected() As Boolean
            Get
                Connected = mConnected
            End Get
        End Property

        'FTP Server return info
        Private mFtpResponce As String
        ReadOnly Property FtpResponce() As String
            Get
                FtpResponce = mFtpResponce
                'mFtpResponce = ""
            End Get
        End Property
       
        Public Sub New()

        End Sub
        Public Sub New(ByVal ServerAdd As String, ByVal FtpPort As Int32)
            Try
                Me.mFtpPort = FtpPort
                If IPHelper.isValidIP(ServerAdd) Then
                    Me.mServerAddress = IPAddress.Parse(ServerAdd)
                Else
                    Me.mServerAddress = Dns.Resolve(ServerAdd).AddressList(0)
                End If

            Catch err As Exception
                MsgBox(err.ToString())
                Me.Dispose()
            End Try
        End Sub

        Public Delegate Sub DnsCallback(ByVal ar As IAsyncResult)

        '
        Protected Sub Dispose()
            If Not mConnected Then
                Call Close()
            End If
        End Sub

        Public Function Connect() As Boolean
            Return BuildConnection(Me.mServerAddress, Me.mFtpPort)
        End Function
        'Build FTP connection
        Private Function BuildConnection(ByVal ServerAdd As IPAddress, ByVal FtpPort As Int32) As Boolean
            Dim strTemp As String
            Dim msg As String

            If FtpPort <= 0 Or FtpPort > 65535 Then
                msg = "Port out of range: 1 ~ 65535 !"
                Debug.WriteLine(msg)
                MsgBox(msg)
                Exit Function
            End If
            '
            Try
                mTcpClient = New TcpClient
                mTcpClient.Connect(ServerAdd, FtpPort)

                msg = "Connected to " + ServerAdd.ToString + ":" + FtpPort.ToString
                Debug.WriteLine(msg)

                strTemp = GetResponce()

                msg = ServerAdd.ToString + " answers " + strTemp
                Debug.WriteLine(msg)

                If strTemp.StartsWith("220") Then
                    mConnected = True
                    RaiseEvent FTP_ConnectionSuccess()
                Else
                    MsgBox("No Connection could be made. " + msg, MsgBoxStyle.Exclamation)
                    Me.Close()
                End If

            Catch err As Exception
                MsgBox("Error while building Connection. Please Check your Network ! " + vbCrLf + _
                    err.ToString(), MsgBoxStyle.Exclamation)
            End Try

            Return mConnected
        End Function

        '
        Public Sub Close()
            If mConnected Then
                Try
                    Me.FtpCommand("QUIT")
                    mTcpClient.Close()
                Catch err As Exception
                    MsgBox(err.ToString())
                Finally
                    mConnected = False
                    mTcpClient = Nothing
                End Try
            End If
            RaiseEvent FTP_DisConnected()
        End Sub

        'Get FTP server responce
        Private Function GetResponce() As String
            Dim i As Integer


            mFtpResponce = String.Empty

            If mNetStream Is Nothing Then
                mNetStream = mTcpClient.GetStream()
            End If


            'cycle around to give more time to answer
            Do

                Application.DoEvents()
                Dim buffer As String
                buffer = String.Empty
                Thread.Sleep(DEAFULTSLEEPTIME)

                Do While mNetStream.DataAvailable

                    ReDim mBytes(mTcpClient.ReceiveBufferSize)
                    intBytesRec = mNetStream.Read(mBytes, 0, CInt(mTcpClient.ReceiveBufferSize))
                    buffer = buffer & Encoding.ASCII.GetString(mBytes, 0, intBytesRec)

                Loop
                mFtpResponce = mFtpResponce & buffer
                i += 1

            Loop While i < DEFAULTRESPONSECYCLES And (mFtpResponce = String.Empty Or Not mFtpResponce.EndsWith(vbLf))

            Debug.WriteLine("Server responds :" + mFtpResponce)

            If i > 1 Then
                Debug.WriteLine("Loops = " + i.ToString)
            End If

            'an event is raised to write mFtpResponce to log
            RaiseEvent FTP_gotResponce(mFtpResponce)
            GetResponce = mFtpResponce

        End Function

        'Login 
        Public Function IdVerify(ByVal strID As String, ByVal strPW As String) As Boolean
            Dim strTemp As String

            If mConnected Then
                'ID
                mBytes = Encoding.ASCII.GetBytes("USER " & strID & vbCrLf)
                mNetStream.Write(mBytes, 0, mBytes.Length)
                mNetStream.Flush()
                Application.DoEvents()

                strTemp = GetResponce()
                If Not strTemp.StartsWith("331") Then
                    MsgBox(strTemp)
                    Return False
                    Exit Function
                End If

                'password
                mBytes = Encoding.ASCII.GetBytes("PASS " & strPW & vbCrLf)
                mNetStream.Write(mBytes, 0, mBytes.Length)
                mNetStream.Flush()
                Application.DoEvents()


                strTemp = GetResponce()

                If Not strTemp.StartsWith("230") Then
                    MsgBox(strTemp)
                    Return False
                    Exit Function
                End If



                'If mNetStream.DataAvailable Then
                '    Call GetResponce()
                'End If
                Return True
            End If
        End Function

        'Directory list
        Public Function DirList() As String
            Dim intPort As Int32
            Dim MS As New MemoryStream
            Dim strTemp As String

            If mConnected Then
                'Try
                intPort = cmdPasv2Port()
                FtpCommand("TYPE A")

                strTemp = Me.FtpCommand("LIST -aL") '-aL
                If Not strTemp.StartsWith("125") And Not strTemp.StartsWith("150") Then
                    Throw New p7_FTPException("Could not start the transfer" & vbCrLf & strTemp)
                    Exit Function
                End If
                MS = OtherPortGet(intPort, DataPortMode.Other)

                DirList = Encoding.UTF7.GetString(MS.ToArray, 0, MS.Length)
                If DirList.EndsWith(vbCrLf) Then
                    DirList = DirList.Substring(0, DirList.Length - Len(vbCrLf))
                End If

                If strTemp.IndexOf("226") = -1 Then
                    strTemp = GetResponce()
                    If Not strTemp.StartsWith("226") Then
                        Throw New p7_FTPException("Could not complete the transfer" & vbCrLf & strTemp)
                    End If
                End If
                'Catch err As Exception
                '    MsgBox(err.ToString())
                'End Try
            End If
        End Function

        '
        Public Function cmdPasv2Port() As Int32
            Dim i, j As Int32
            Dim strTemp As String
            Dim cnt As Integer
            If mConnected Then
                '
                Do While cnt < 100 And strTemp = Nothing
                    Application.DoEvents()
                    strTemp = Me.FtpCommand("PASV")
                    If Not strTemp.StartsWith("227") Then
                        strTemp = GetResponce()
                    End If
                    cnt += 1
                Loop
                If strTemp Is Nothing OrElse strTemp.IndexOf("(") = -1 Then
                    Throw New p7_FTPException("Cold not get the other port")
                    Exit Function
                End If
                'strTemp = mFtpResponce
                i = strTemp.LastIndexOf(",")
                j = strTemp.LastIndexOf(")")
                cmdPasv2Port = CInt(strTemp.Substring(i + 1, j - i - 1))
                strTemp = strTemp.Substring(1, i - 1)
                j = i
                i = strTemp.LastIndexOf(",")
                cmdPasv2Port = 256 * CInt(strTemp.Substring(i + 1, j - i - 2)) + cmdPasv2Port
                'mTcpData.ReceiveBufferSize = 8192
                mTcpData = New TcpClient
                mTcpData.Connect(Me.mServerAddress, cmdPasv2Port)
                mDataStream = mTcpData.GetStream()
                'Catch err As Exception
                '    MsgBox(err.ToString())
                'End Try
            End If
        End Function

        Private Enum DataPortMode
            Download
            [Resume]
            Other
        End Enum

        'Get other port's data
        Private Function OtherPortGet(ByVal intDataPort As Int32, ByVal mode As DataPortMode, Optional ByVal FilePath As String = "", Optional ByVal WillRecieveBytes As Long = 0) As MemoryStream
            Dim strTemp As String
            Dim MS As New MemoryStream
            Dim FStr As System.IO.FileStream
            Dim BytesReceived As Long

            Try

                Select Case mode
                    Case DataPortMode.Resume
                        FStr = New System.IO.FileStream(FilePath, IO.FileMode.Append, FileAccess.Write)
                    Case DataPortMode.Download
                        FStr = New System.IO.FileStream(FilePath, IO.FileMode.Create, FileAccess.Write)
                End Select

                Dim i As Integer = 0
                Do

                    If BytesReceived = 0 Then Thread.Sleep(DEAFULTSLEEPTIME)

                    Do While mDataStream.DataAvailable Or BytesReceived < WillRecieveBytes

                        ReDim mBytes(mTcpData.ReceiveBufferSize)
                        intBytesRec = mDataStream.Read(mBytes, 0, mTcpData.ReceiveBufferSize)
                        BytesReceived += intBytesRec

                        If mode = DataPortMode.Other Then

                            MS.Write(mBytes, 0, intBytesRec)

                        ElseIf mode = DataPortMode.Resume Or mode = DataPortMode.Download Then

                            FStr.Write(mBytes, 0, intBytesRec)
                            RaiseEvent FTP_BytesReceived(intBytesRec)
                            Application.DoEvents()
                            If Me.CancelOP Then Exit Do

                        End If

                    Loop
                    i += 1

                Loop While BytesReceived = 0 And i < DEFAULTRESPONSECYCLES

            Catch err As Exception

                Throw err

            Finally

                Me.CancelOP = False
                If mode <> DataPortMode.Other Then
                    FStr.Close()
                Else
                    OtherPortGet = MS
                End If

                mTcpData.Close()
            End Try

        End Function


        'ChDir
        Public Function Chdir(ByVal newDir As String) As Boolean
            Return FtpCommand("CWD " & newDir).StartsWith("250")
        End Function

        Public Function getCurDir() As String
            Dim strTemp As String = FtpCommand("PWD")

            If strTemp.StartsWith("257") Then
                Dim StartIndex As Integer = strTemp.IndexOf("""") + 1
                Return strTemp.Substring(StartIndex, strTemp.LastIndexOf("""") - StartIndex)
            Else
                Throw New p7_FTPException("Could not get the Current directory" & strTemp)
            End If

        End Function

        'general command
        Public Function FtpCommand(ByVal strCommand As String) As String
            If mConnected Then
                Try
                    Erase mBytes
                    mBytes = Encoding.UTF8.GetBytes(strCommand & vbCrLf)

                    mNetStream.Write(mBytes, 0, mBytes.Length)
                    mNetStream.Flush()
                    Application.DoEvents()

                    RaiseEvent FTP_CommandSend(strCommand)
                    FtpCommand = String.Empty & GetResponce()
                Catch err As Exception
                    MsgBox(err.Message)
                End Try
            Else
                Return String.Empty
            End If
        End Function

        'FTP Upload
        Public Function FtpSendFile(ByVal Path2File As String, ByVal Size As Long)
            Dim intPort As Int32

            If mConnected Then

                'set the transfer mode to binary
                Dim resp As String = FtpCommand("TYPE I")
                If resp Is Nothing Then
                    MsgBox("No Response from Server")
                    mConnected = False
                    Exit Function
                End If

                '// get a list of IP addresses for this machine
                Dim ipThis As IPHostEntry = Dns.GetHostByName(Dns.GetHostName())
                Dim r As Random = New Random
                Dim port As Integer = 0
                Dim bIPFound As Boolean = False
                Dim sOut As String
                '// we will try all IP addresses assigned to this machine
                '// the first one that the remote machine likes will be chosen
                Dim i As Integer
                For i = 0 To ipThis.AddressList.Length - 1

                    Dim ip As String = ipThis.AddressList(i).ToString().Replace(".", ",")
                    Dim p1 As Integer = r.Next(100)
                    Dim p2 As Integer = r.Next(100)
                    port = 256 * p1 + p2
                    Dim sPortCom As String = "PORT " + ip + "," + p1.ToString() + "," + p2.ToString()
                    '// Port command now looks like PORT 61,45,6,34,xx,yy where
                    '// first 4 values is your IP address and xx and yy are random numbers
                    '// 256*xx+yy will be the port number where the remote machine will connect
                    '// and send data
                    sOut = FtpCommand(sPortCom)

                    If sOut.StartsWith("200") Then
                        '// PORT command accepted
                        bIPFound = True
                        Exit For
                    End If

                Next

                If Not bIPFound Then
                    Throw New p7_FTPException("Server did not accept IP for sending" & vbCrLf & sOut)
                End If


                '// issue the upload command
                Dim strTemp As String = FtpCommand("STOR " + System.IO.Path.GetFileName(Path2File))
                ' we will get either a confirmation of the download(150) or an error message
                If strTemp.StartsWith("550") Then
                    Throw New p7_FTPException("Could not create remote file" & strTemp)
                    Exit Function
                End If

                Const WRITEBUFFER As Integer = 1024 * 10
                ' start the download
                Dim bData(WRITEBUFFER) As Byte
                Dim bytesRead As Integer = 0
                Dim Sender

                If Size > 0 Then
                    Dim fStream As FileStream = New FileStream(Path2File, FileMode.Open, FileAccess.Read, FileShare.Read, WRITEBUFFER, False)

                    ' now we are ready for transfer
                    ' set up a listener and start listening
                    Dim conn As TcpListener = New TcpListener(port)
                    conn.Start()
                    Dim xfer As Socket

                    xfer = conn.AcceptSocket()
                    Do
                        bytesRead = fStream.Read(bData, 0, WRITEBUFFER)
                        xfer.Send(bData, 0, bytesRead, SocketFlags.None)
                        If Me.CancelOP Then Exit Do
                        RaiseEvent FTP_BytesSend(bytesRead)
                        Application.DoEvents()
                    Loop While (bytesRead > 0)

                    'CleanUP
                    Me.CancelOP = False
                    fStream.Close()
                    fStream = Nothing
                    xfer.Shutdown(SocketShutdown.Both)
                    xfer.Close()
                    xfer = Nothing
                    conn.Stop()
                    conn = Nothing
                End If


                'consume the "226 Transfer Complete" response
                strTemp = Me.GetResponce
                If Size > 0 AndAlso Not strTemp.StartsWith("226") Then
                    Throw New p7_FTPException(strTemp)
                End If

            End If

        End Function

        'FTP Download
        Public Function FtpGetFile(ByVal strFile As String, ByVal DownloadToFile As String, ByVal Size As Long, ByVal Offset As Long) As MemoryStream
            Dim intPort As Int32
            Dim MS As New MemoryStream
            Dim strTemp As String
            Dim mode As DataPortMode
            Dim Bytes2Receive As Long
            If mConnected Then

                FtpCommand("TYPE I")
                intPort = cmdPasv2Port()
                If Offset > 0 Then
                    mode = DataPortMode.Resume
                    Bytes2Receive = Size - Offset
                    strTemp = FtpCommand("REST " & Offset)
                    If strTemp.StartsWith("350") Then
                        'alles ok
                    ElseIf strTemp.StartsWith("504") Then
                        Dim antw As DialogResult = MsgBox("Server does not support Resume. Overwrite ?", MsgBoxStyle.YesNo, "Resume not supported")
                        If antw = DialogResult.No Then
                            Exit Function
                        Else
                            mode = DataPortMode.Download
                            Bytes2Receive = Size
                        End If
                    Else
                        MsgBox(strTemp)
                    End If
                Else
                    mode = DataPortMode.Download
                    Bytes2Receive = Size
                End If

                strTemp = FtpCommand("RETR " & strFile)
                If Not strTemp.StartsWith("150") And Not strTemp.StartsWith("125") Then
                    Throw New p7_FTPException("Could not get File." & vbCrLf & strTemp)
                    Exit Function
                End If

                System.Threading.Thread.CurrentThread.Sleep(200)

                FtpGetFile = OtherPortGet(intPort, mode, DownloadToFile, Bytes2Receive)

                System.Threading.Thread.CurrentThread.Sleep(200)

                strTemp = GetResponce()
                If Not strTemp.StartsWith("226") Then
                    Throw New p7_FTPException("Could not get File." & vbCrLf & strTemp)
                End If

            End If
        End Function

        Public Function MkDir(ByVal DirName As String) As Boolean
            If Me.FtpCommand("MKD " & DirName).StartsWith("257") Then Return True
        End Function

        Public Function RmDir(ByVal DirName As String) As Boolean
            If Me.FtpCommand("RMD " & DirName).StartsWith("250") Then Return True
        End Function

        Public Function DelFile(ByVal FileName As String) As Boolean
            If Me.FtpCommand("DELE " & FileName).StartsWith("250") Then Return True
        End Function

        Public Function Rename(ByVal oldName As String, ByVal newName As String) As Boolean
            If Me.FtpCommand("RNFR " & oldName).StartsWith("350") Then
                If Me.FtpCommand("RNTO " & newName).StartsWith("250") Then
                    Return True
                End If
            End If
        End Function

        Public Class p7_FTPException
            Inherits Exception
            Public Sub New(ByVal msg As String)
                MyBase.New(msg)
            End Sub
        End Class

    End Class

End Namespace