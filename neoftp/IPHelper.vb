Imports System.Net

Public Class IPHelper

    Private mIPList As New ArrayList
    Private mStartIP As IPAddress
    Private mEndIP As IPAddress
    Private mPortList As New ArrayList

    Public Property StartIP() As String
        Get
            Return mStartIP.Address.ToString
        End Get
        Set(ByVal Value As String)
            If Me.isValidIP(Value) Then
                mStartIP = IPAddress.Parse(Value)
            Else
                Throw New Exception("IP Not Valid")
            End If
        End Set
    End Property
    Public Property EndIP() As String
        Get
            Return mEndIP.Address.ToString
        End Get
        Set(ByVal Value As String)
            If Me.isValidIP(Value) Then
                mEndIP = IPAddress.Parse(Value)
            Else
                Throw New Exception("IP Not Valid")
            End If
        End Set
    End Property
    Public ReadOnly Property IPlist() As ArrayList
        Get
            calcIPRange()
            Return Me.mIPList
        End Get
    End Property
    Private Sub calcIPRange()
        Dim tempIP As IPAddress
        Dim EndIP As IPAddress = IPAddress.Parse(Me.Increment(mEndIP.ToString))

        Me.mIPList.Clear()
        tempIP = mStartIP

        Do
            Me.mIPList.Add(tempIP)
            'tempIP = Me.Increment2(tempIP)
            'System.Diagnostics.Debug.WriteLine(tempIP.ToString)

            tempIP = IPAddress.Parse(Me.Increment(tempIP.ToString))
        Loop While Not EndIP.Equals(tempIP)
    End Sub
    Public Shared Function isValidIP(ByVal IPstr As String) As Boolean
        Dim rx As New System.Text.RegularExpressions.Regex("^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$")
        '"\b((25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)\.){3}(25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)\b"
        Return rx.IsMatch(IPstr)
    End Function

    Public Shared Function isIP(ByVal IPstr As String) As Boolean
        IPstr = IPstr.Replace(".", "")
        Return IsNumeric(IPstr)
    End Function

    Public Shared Function isBigger(ByVal IP1 As String, ByVal IP2 As String) As Boolean
        Dim A1, B1, C1, D1, A2, B2, C2, D2 As Int16
        Dim s() As String
        Const DOT = "."

        s = IP1.Split(DOT)
        A1 = CInt(s(0))
        B1 = CInt(s(1))
        C1 = CInt(s(2))
        D1 = CInt(s(3))

        s = IP2.Split(DOT)
        A2 = CInt(s(0))
        B2 = CInt(s(1))
        C2 = CInt(s(2))
        D2 = CInt(s(3))

        If CInt(A1) > CInt(A2) Then
            Return True
        Else
            If CInt(B1) > CInt(B2) Then
                Return True
            Else
                If CInt(C1) > CInt(C2) Then
                    Return True
                Else
                    If CInt(D1) > CInt(D2) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        End If
    End Function
    Public Shared Function Increment2(ByVal ip As IPAddress) As IPAddress
        Dim retIP As New IPAddress(ip.HostToNetworkOrder(ip.Address) + 1)
        Return retIP
        'Dim B() As Byte
        'B = ip.get
    End Function
    Public Shared Function Increment(ByVal IP As String) As String
        Dim A, B, C, D As Int16
        Dim s() As String
        Const DOT = "."

        s = IP.Split(DOT)
        A = CInt(s(0))
        B = CInt(s(1))
        C = CInt(s(2))
        D = CInt(s(3))

        D += 1
        If D > 255 Then
            D = 0
            C += 1
            If C > 255 Then
                C = 0
                B += 1
                If B > 255 Then
                    B = 0
                    A += 1
                    If A > 255 Then
                        A = 0 : B = 0 : C = 0 : D = 0
                    End If
                End If
            End If
        End If
        Return A & DOT & B & DOT & C & DOT & D
    End Function
    Public Shared Function getPortList(ByVal StartPort As Integer, ByVal EndPort As Integer) As ArrayList
        Dim I As Integer = StartPort
        Dim ret As New ArrayList
        Do
            ret.Add(I)
            I += 1
        Loop While I <= EndPort
        Return ret
    End Function
    'Public Enum IPComp
    '    isBigger = 1
    '    isSmaller = -1
    '    isEqual = 0
    'End Enum


End Class
