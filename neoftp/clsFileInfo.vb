'###############################################################################
'################################# FILEINFO ####################################
'###############################################################################
#Region "FileInfo"

Public Class FileINFO
    Private infoToken() As String
    Private isUNIX_Format As Boolean


    Public Sub New(ByVal DirLine)
        Dim i, ii As Integer
        Dim tokens() As String = Split(DirLine, " ")

        isUNIX_Format = tokens(0).StartsWith("-") Or tokens(0).StartsWith("d")

        'cut aout the additionell empty tokens through the spaces
        'If Not isUNIX_Format Then
        For i = LBound(tokens) To UBound(tokens)
            If Not tokens(i) = "" Then
                ReDim Preserve infoToken(ii)
                infoToken(ii) = tokens(i)
                ii += 1
            End If
        Next
        'End If

    End Sub

    Public ReadOnly Property Attribs() As String
        Get
            If isUNIX_Format Then
                Return infoToken(0)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property isDirectory() As Boolean
        Get
            If isUNIX_Format Then
                Return infoToken(0).StartsWith("d")
            Else
                Return infoToken(2).Equals("<DIR>")
            End If
        End Get
    End Property

    Public ReadOnly Property LinkCounter() As Integer
        Get
            If isUNIX_Format Then
                Return CInt(infoToken(1))
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property Owner() As String
        Get
            If isUNIX_Format Then
                Return infoToken(2)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property Group() As String
        Get
            If isUNIX_Format Then
                Return infoToken(3)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public ReadOnly Property Size() As Long
        Get
            If isUNIX_Format Then
                Return CLng(infoToken(4))
            Else
                If Me.isDirectory Then
                    Return 0
                Else
                    Return CLng(infoToken(2))
                End If

            End If
        End Get
    End Property

    Public ReadOnly Property [Date]() As Date
        Get
            If isUNIX_Format Then
                Return (CDate(infoToken(5) & "/" & infoToken(6)))

            Else

                Dim datum1, datum2 As Date

                Try
                    datum1 = CDate(infoToken(0))
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)
                End Try
                Try
                    datum1 = CDate(infoToken(0))
                Catch ex As Exception
                    Debug.WriteLine(ex.ToString)
                End Try

                Return datum1 + datum2

            End If

        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Dim s As Integer

            If isUNIX_Format Then
                s = 8
            Else
                s = 3 '+ IIf(Me.isDirectory, 0, 1)
            End If

            Dim i As Integer
            Dim retV As String
            For i = s To UBound(infoToken)
                retV += infoToken(i) & " "
            Next

            'cut the line feed
            Return retV.Substring(0, retV.Length - 1)
        End Get
    End Property
End Class

#End Region