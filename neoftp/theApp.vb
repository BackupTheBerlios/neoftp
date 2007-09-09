Imports System.Runtime.InteropServices


Public Module theApp


#If Compact Then
    Private Declare Function RegOpenKeyExW Lib "Coredll" (ByVal hKey As Integer, ByVal lpSubKey As String, ByVal ulOptions As Integer, ByVal samDesired As Integer, ByRef phkResult As Integer) As Integer
    Private Declare Function RegQueryValueExW Lib "Coredll" (ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByVal lpData As String, ByRef lpcbData As Integer) As Integer
    Private Declare Function RegCloseKey Lib "Coredll" (ByVal hKey As Integer) As Integer
    Private Declare Function RegSetValueEx Lib "Coredll" (ByVal hKey As Integer, ByVal lpValueName As String, ByVal Reserved As Integer, ByVal dwType As Integer, ByVal lpData As String, ByVal cbData As Integer) As Integer
    Private Declare Function RegCreateKeyEx Lib "Coredll" (ByVal hKey As Integer, ByVal lpSubKey As String, ByVal Reserved As Integer, ByVal lpClass As String, ByVal dwOptions As Integer, ByVal samDesired As Integer, ByVal lpSecurityAttributes As Integer, ByVal phkResult As Integer, ByVal lpdwDisposition As Integer) As Integer
    ' Note that if you declare the lpData parameter as String, you must pass it By Value.
    Public Enum RegRootKey
        HKEY_CLASSES_ROOT = &H80000000
        HKEY_CURRENT_USER = &H80000001
        HKEY_LOCAL_MACHINE = &H80000002
        HKEY_USERS = &H80000003
    End Enum
    Public Const REG_OPTION_NON_VOLATILE = 0       ' Key is preserved when system is rebooted
    Public Const KEY_SET_VALUE = &H2
    Public Const REG_SZ = 1                         ' Unicode nul terminated string


    Private Declare Function KernelIoControl Lib "CoreDll.dll" (ByVal dwIoControlCode As Int32, ByVal lpInBuf As IntPtr, ByVal nInBufSize As Int32, ByVal lpOutBuf() As Byte, ByVal nOutBufSize As Int32, ByRef lpBytesReturned As Int32) As Boolean

    Private Const ERROR_NOT_SUPPORTED As Int32 = &H32
    Private Const ERROR_INSUFFICIENT_BUFFER As Int32 = &H7A
    Private METHOD_BUFFERED As Int32 = 0
    Private FILE_ANY_ACCESS As Int32 = 0
    Private FILE_DEVICE_HAL As Int32 = &H101
    Private IOCTL_HAL_GET_DEVICEID As Int32 = (&H10000 * FILE_DEVICE_HAL) Or (&H4000 * FILE_ANY_ACCESS) Or (&H4 * 21) Or METHOD_BUFFERED
#End If

    Const ERROR_SUCCESS As Integer = &H0
    Private SettingsPath As String = "Software\p7\" & theApp.AppExeName

    Public ReadOnly Property AppPath() As String
        Get

#If Compact Then
            AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
#Else
            AppPath = System.AppDomain.CurrentDomain.BaseDirectory() 'Windows.Forms.Application.ExecutablePath() '
#End If

        End Get
    End Property

    Public ReadOnly Property myAssembly() As System.Reflection.Assembly
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly()
        End Get
    End Property


    Public ReadOnly Property Version() As String
        Get
#If Not COMPACT Then
            Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileMajorPart & _
                System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileMinorPart 
#Else
            Version = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString
#End If

        End Get
    End Property
    Public ReadOnly Property Build() As String
        Get
#If Compact Then
            Throw New Exception("Build Property not suppported now")
#Else
            Build = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileBuildPart()
#End If
        End Get
    End Property

    Public ReadOnly Property AppExeName() As String
        Get
            AppExeName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
        End Get
    End Property

    Public ReadOnly Property UserName() As String
        Get
#If Compact Then
            Return ReadRegistryKey(RegRootKey.HKEY_CURRENT_USER, "\ControlPanel\Owner", "Owner")
#Else
            Return System.Security.Principal.WindowsIdentity.GetCurrent.Name
#End If
        End Get
    End Property
#If Compact Then
    Public Function ReadRegistryKey(ByVal RootKey As RegRootKey, ByVal Path As String, ByVal SubKey As String)

        Dim lngSize As Integer, hlngSubKey As Integer
        Dim lngType As Integer, lngResult As Integer
        Dim RegData As String
        'Const ERROR_NO_MORE_ITEMS As Integer = &H103
        'Const KEY_READ As Integer = &H120019

        lngSize = 256
        RegData = New String(" ".Chars(0), lngSize)
        'Handle (hlngSubKey) to the "\ControlPanel\Owner" key
        lngResult = RegOpenKeyExW(RootKey, Path, 0, 0, hlngSubKey)

        'Read the "Owner" info frm open registry key
        lngResult = RegQueryValueExW(hlngSubKey, SubKey, 0, lngType, RegData, lngSize)
        'Release the key handle
        lngResult = RegCloseKey(hlngSubKey)
        Return RegData.Trim
    End Function
    Public Function WriteRegistryKey(ByVal RootKey As RegRootKey, ByVal Path As String, ByVal SubKey As String, ByVal Value As String) As Boolean
        Dim lngType As Integer, lngResult As Integer
        Dim lngSize As Integer, hlngSubKey As Integer

        lngResult = RegOpenKeyExW(RootKey, Path, 0, 0, hlngSubKey)
        If lngResult <> ERROR_SUCCESS Then
            'Key does not exist --> so creat it
            lngResult = RegCreateKeyEx(RootKey, Path, 0, String.Empty, REG_OPTION_NON_VOLATILE, KEY_SET_VALUE, 0, SubKey, hlngSubKey)
            If lngResult <> ERROR_SUCCESS Then Return False
        End If
        'write the value
        lngResult = RegSetValueEx(hlngSubKey, SubKey, 0, REG_SZ, Value, 2 * Len(Value))
        If lngResult <> ERROR_SUCCESS Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function GetSetting(ByVal Section As String, ByVal Setting As String) As String
        Return ReadRegistryKey(RegRootKey.HKEY_CURRENT_USER, SettingsPath, Setting)
    End Function
    Public Function SaveSetting(ByVal Section As String, ByVal Setting As String, ByVal Value As String) As Boolean
        Return WriteRegistryKey(RegRootKey.HKEY_CURRENT_USER, SettingsPath, Section, Value)
    End Function
#End If
    Public ReadOnly Property PrevInstance() As Boolean
        Get

        End Get
    End Property

#If Compact Then
    Public ReadOnly Property DeviceID() As String
        Get

            ' Initialize the output buffer to the size of a Win32 DEVICE_ID structure
            '
            Dim outbuff(19) As Byte
            Dim dwOutBytes As Int32
            Dim done As Boolean = False

            Dim nBuffSize As Int32 = outbuff.Length
            ' Set DEVICEID.dwSize to size of buffer.  Some platforms look at
            ' this field rather than the nOutBufSize param of KernelIoControl
            ' when determining if the buffer is large enough.
            '
            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0)
            dwOutBytes = 0


            ' Loop until the device ID is retrieved or an error occurs
            '
            While Not done
                If KernelIoControl(IOCTL_HAL_GET_DEVICEID, IntPtr.Zero, 0, outbuff, nBuffSize, dwOutBytes) Then
                    done = True
                Else
                    Dim [error] As Integer = Marshal.GetLastWin32Error()
                    Select Case [error]
                        Case ERROR_NOT_SUPPORTED
                            Throw New NotSupportedException("IOCTL_HAL_GET_DEVICEID is not supported on this device", New System.ComponentModel.Win32Exception([error]))

                        Case ERROR_INSUFFICIENT_BUFFER
                            ' The buffer is not big enough for the data.  The required size 
                            ' is in the first 4 bytes of the output buffer (DEVICE_ID.dwSize).
                            nBuffSize = BitConverter.ToInt32(outbuff, 0)
                            outbuff = New Byte(nBuffSize) {}
                            ' Set DEVICEID.dwSize to size of buffer.  Some
                            ' platforms look at this field rather than the
                            ' nOutBufSize param of KernelIoControl when
                            ' determining if the buffer is large enough.
                            '
                            BitConverter.GetBytes(nBuffSize).CopyTo(outbuff, 0)

                        Case Else
                            Throw New System.ComponentModel.Win32Exception([error], "Unexpected error")
                    End Select
                End If
            End While

            Dim dwPresetIDOffset As Int32 = BitConverter.ToInt32(outbuff, &H4)  ' DEVICE_ID.dwPresetIDOffset
            Dim dwPresetIDSize As Int32 = BitConverter.ToInt32(outbuff, &H8)    ' DEVICE_ID.dwPresetIDSize
            Dim dwPlatformIDOffset As Int32 = BitConverter.ToInt32(outbuff, &HC) ' DEVICE_ID.dwPlatformIDOffset
            Dim dwPlatformIDSize As Int32 = BitConverter.ToInt32(outbuff, &H10) ' DEVICE_ID.dwPlatformIDBytes
            Dim sb As New System.text.StringBuilder
            Dim i As Integer

            For i = dwPresetIDOffset To (dwPresetIDOffset + dwPresetIDSize) - 1
                sb.Append(String.Format("{0:X2}", outbuff(i)))
            Next i
            sb.Append("-")
            For i = dwPlatformIDOffset To (dwPlatformIDOffset + dwPlatformIDSize) - 1
                sb.Append(String.Format("{0:X2}", outbuff(i)))
            Next i
            DeviceID = sb.ToString()
        End Get
    End Property

    'Friend Class DetectPrevInstance
    '    Declare Function FindWindow Lib "CoreDll.dll" (ByVal className() As Char, ByVal WindowsName() As Char) As Integer
    '    Declare Function SetForegroundWindow Lib "CoreDll.dll" (ByVal hWnd As Integer) As Boolean

    '    Public Function IsRunning(ByVal WindowName As String) As Boolean
    '        Dim hWnd As Integer
    '        hWnd = FindWindow(Nothing, WindowName.ToCharArray())
    '        If hWnd <> 0 Then
    '            SetForegroundWindow(hWnd)
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Function
    'End Class
#End If

End Module
