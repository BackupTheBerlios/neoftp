Option Strict Off
Option Explicit On
Namespace p7.Crypto
    Public Module KryptoMod
        Const Seperator As String = ", "

        Function Encrypt(ByRef Str As String, ByRef En As Boolean) As String
            Dim StrLen As Integer
            Dim StrRet As String
            Dim Var2, i, Var, Zahl As Short
            Dim LastI, ii As Short
            Dim ENDE As Boolean
            Dim codeB As String

            ENDE = False
            LastI = 1
            StrRet = ""
            i = 0
            StrLen = Len(Str)

            If Not En Then
                StrLen = StrLen / 3
            End If

            If StrLen = 0 Then GoTo RETUR
            Var = (Int((StrLen / 11) * 9) Mod 5) + System.Math.Sin(StrLen) * 9
            For i = 0 To StrLen - 1
                Var2 = System.Math.Cos(i) * 9 + System.Math.Sin(i * 1.1) * 3
                If En Then
                    Zahl = Asc(Mid(Str, i + 1, 1))
                Else
                    Zahl = Val(Mid(Str, i * 3 + 1, 3))
                End If
                If En Then
                    codeB = CStr(System.Math.Abs(Zahl - Var + Var2))
                    StrRet = StrRet & Left("000", 3 - Len(codeB)) & codeB
                Else
                    StrRet = StrRet & Chr(System.Math.Abs(Zahl - Var2 + Var))
                End If
                'Debug.Print Var, Var2
            Next

RETUR:
            Encrypt = StrRet
        End Function
    End Module
End Namespace