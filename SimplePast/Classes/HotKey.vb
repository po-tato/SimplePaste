'Author: Arman Ghazanchyan
'Created date: 03/10/2008
'Last updated: 08/05/2008

Imports System.ComponentModel
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Collections.ObjectModel

#Region " Structures "

<DebuggerDisplay("Id = {Id}, Name = {Name}"), CLSCompliant(True)> _
Public Structure Hotkey
    Private _keyData As Keys
    Private _id As Integer
    Public Shared ReadOnly Empty As Hotkey

    ''' <param name="id">A unique window’s hotkey ID that is in the range 0x0000 through 0xBFFF.</param>
    ''' <param name="keyData">The Hotkey data. This parameter can contain one or combination of 
    ''' Keys.Ctrl, Keys.Alt or Keys.Shift fodifier keys and any key, combined with OR.</param>
    <DebuggerHidden()> _
    Sub New(ByVal id As Integer, ByVal keyData As Keys)
        If (keyData And Keys.Modifiers) <> Keys.None AndAlso (keyData And Not Keys.Modifiers) <> Keys.None Then
            Me._id = id
            Me._keyData = keyData
        Else
            Throw New ArgumentException("The hotkey must consist of one or more modifiers and a key.", "keyData")
        End If
    End Sub

    <DebuggerHidden()> _
    Private Sub New(ByVal keyData As Keys)
        Me._keyData = keyData
    End Sub

    ''' <summary>
    ''' Gets a Boolean indicating if the ALT key is present.
    ''' </summary>
    Public ReadOnly Property Alt() As Boolean
        <DebuggerHidden()> _
        Get
            Return (Me._keyData And Keys.Alt) = Keys.Alt
        End Get
    End Property

    ''' <summary>
    ''' Gets a Boolean indicating if the CTRL key is present.
    ''' </summary>
    Public ReadOnly Property Control() As Boolean
        <DebuggerHidden()> _
        Get
            Return (Me._keyData And Keys.Control) = Keys.Control
        End Get
    End Property

    ''' <summary>
    ''' Gets a Boolean indicating if the SHIFT key is present.
    ''' </summary>
    Public ReadOnly Property Shift() As Boolean
        <DebuggerHidden()> _
        Get
            Return (Me._keyData And Keys.Shift) = Keys.Shift
        End Get
    End Property

    ''' <summary>
    ''' Gets the modifiers of the hot key.
    ''' </summary>
    Public ReadOnly Property Modifiers() As Keys
        <DebuggerHidden()> _
        Get
            Return Me._keyData And Keys.Modifiers
        End Get
    End Property

    ''' <summary>
    ''' Gets the key of the hot key.
    ''' </summary>
    Public ReadOnly Property Key() As Keys
        <DebuggerHidden()> _
        Get
            Return Me._keyData And Not Keys.Modifiers
        End Get
    End Property

    ''' <summary>
    ''' Gets the hot key data.
    ''' </summary>
    Public ReadOnly Property KeyData() As Keys
        <DebuggerHidden()> _
        Get
            Return Me._keyData
        End Get
    End Property

    ''' <summary>
    ''' Gets the hot key id.
    ''' </summary>
    Public ReadOnly Property Id() As Integer
        <DebuggerHidden()> _
        Get
            Return Me._id
        End Get
    End Property

    ''' <summary>
    ''' Gets the hot key name.
    ''' </summary>
    Public ReadOnly Property Name() As String
        <DebuggerHidden()> _
        Get
            Return Me.ToString
        End Get
    End Property

    ''' <summary>
    ''' Sets or resets the hot key data.
    ''' </summary>
    ''' <param name="keyData">The Hotkey data. This parameter can contain one or combination of 
    ''' Keys.Ctrl, Keys.Alt or Keys.Shift fodifier keys and any key, combined with OR.</param>
    <DebuggerHidden()> _
    Public Sub [Set](ByVal keyData As Keys)
        Me._keyData = keyData
    End Sub

    ''' <summary>
    ''' Determines whether the specified System.Object is equal to the current System.Object.
    ''' </summary>
    <DebuggerHidden()> _
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is Hotkey Then
            Dim hk As Hotkey = DirectCast(obj, Hotkey)
            Return Me._id = hk._id AndAlso Me._keyData = hk._keyData
        End If
        Return False
    End Function

    <DebuggerHidden()> _
    Public Shared Operator =(ByVal a As Hotkey, ByVal b As Hotkey) As Boolean
        Return a.Equals(b)
    End Operator

    <DebuggerHidden()> _
    Public Shared Operator <>(ByVal a As Hotkey, ByVal b As Hotkey) As Boolean
        Return Not a.Equals(b)
    End Operator

    ''' <summary>
    ''' Serves as a hash function for a particular type. Hotkey.GetHashCode is 
    ''' suitable for use in hashing algorithms and data structures like a hash table.
    ''' </summary>
    <DebuggerHidden()> _
    Public Overrides Function GetHashCode() As Integer
        Return Me._id Xor ((Me._keyData << 7) Or (Me._keyData >> 25))
    End Function

    ''' <summary>
    ''' Returns a System.String that represents the current Hotkey.
    ''' </summary>
    <DebuggerHidden()> _
    Public Overrides Function ToString() As String
        Dim str As String = String.Empty
        If Me.Control Then
            str = "Ctrl+"
        End If
        If Me.Alt Then
            str &= "Alt+"
        End If
        If Me.Shift Then
            str &= "Shift+"
        End If
        str &= Me.Key.ToString
        Return str
    End Function

    ''' <summary>
    ''' Returns a System.String that represents the specified Hotkey.
    ''' </summary>
    ''' <param name="keyData">The Hotkey data. This parameter can contain one or combination of 
    ''' Keys.Ctrl, Keys.Alt or Keys.Shift fodifier keys and any key, combined with OR.</param>
    <DebuggerHidden()> _
    Public Overloads Shared Function ToString(ByVal keyData As Keys) As String
        Return New Hotkey(keyData).ToString
    End Function

End Structure

#End Region

''' <summary>
''' Registers or unregisters application hotkeys.
''' </summary>
<CLSCompliant(True)> _
Public Class HotkeyManager
    Implements IDisposable

#Region " Enumarations "

    Private Enum Modifier As Integer
        None = &H0
        Alt = &H1
        Control = &H2
        Shift = &H4
    End Enum

#End Region

#Region " Event Handlers "

    ''' <summary>
    ''' Occurs when a registered hotkey by the HotkeyManager is pressed.
    ''' </summary>
    <Description("Occurs when a registered hotkey by the HotkeyManager is pressed.")> _
    Event HotkeyPressed As EventHandler(Of HotkeyEventArgs)

#End Region

    Private _hotkeys As New Dictionary(Of Integer, Keys)
    Private _hotkeyProc As New HotkeyPorc(Me)
    Private disposedValue As Boolean ' To detect redundant calls

#Region " Properties "

    ''' <summary>
    ''' Gets a collection of hot keys that have been registered by the HotkeyManager.
    ''' </summary>
    Public ReadOnly Property Hotkeys() As HotkeyCollection
        <DebuggerHidden()> _
        Get
            Dim hCollection As New Collection(Of Hotkey)
            For Each key As Integer In Me._hotkeys.Keys
                hCollection.Add(New Hotkey(key, Me._hotkeys.Item(key)))
            Next
            Return New HotkeyCollection(hCollection)
        End Get
    End Property

    ''' <summary>
    ''' Gets the handle to the window associated with the HotkeyManager.
    ''' </summary>
    Public ReadOnly Property Handle() As IntPtr
        <DebuggerHidden()> _
        Get
            Return Me._hotkeyProc.Handle
        End Get
    End Property

#End Region

#Region " Methods "

    ''' <param name="window">
    ''' Handle to the window associated with the hot key.
    ''' </param>
    <DebuggerHidden()> _
    Sub New(ByVal window As Form)
        If window IsNot Nothing Then
            Me._hotkeyProc.AssignHandle(window.Handle)
        Else
            Throw New HotkeyException("The hot key window (Form) cannot be Null (Nothing in VB).")
        End If
    End Sub

    ''' <summary>
    ''' Registers an application hotkey.
    ''' </summary>
    ''' <param name="hk">The HotkeyManager.Hotkey to registerd.</param>
    ''' <param name="throwException">Specifies whether an exception should be thrown after the method fails.</param>
    <DebuggerHidden()> _
    Public Overloads Function RegisterHotKey(ByVal hk As Hotkey, ByVal throwException As Boolean) As Boolean
        Dim ex As HotkeyException
        If hk <> Hotkey.Empty Then
            If Not Me._hotkeys.ContainsKey(hk.Id) Then
                If Not NativeMethods.RegisterHotKey( _
                Me._hotkeyProc.Handle, hk.Id, HotkeyManager.ConvertTo(hk.Modifiers), hk.Key) Then
                    Dim eCode As Integer = Marshal.GetLastWin32Error
                    ex = New HotkeyException(New Win32Exception(eCode).Message)
                Else
                    Me._hotkeys.Add(hk.Id, hk.KeyData)
                    Return True
                End If
            Else
                ex = New HotkeyException("A hot key with the same id is already registered.")
            End If
        Else
            ex = New HotkeyException("The hot key cannot be empty or null (Nothing in VB).")
        End If
        If throwException Then
            Throw ex
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Unregisters an application hot key that was previously registered.
    ''' </summary>
    ''' <param name="hk">The HotkeyManager.Hotkey to unregister. 
    ''' If the function succeeds, the return value is the unregistered hot key.</param>
    ''' <param name="throwException">Specifies whether an exception should be thrown after the method fails.</param>
    <DebuggerHidden()> _
    Public Function UnregisterHotKey(ByVal hk As Hotkey, ByVal throwException As Boolean) As Hotkey
        If hk <> Hotkey.Empty Then
            Return Me.UnregisterHotKey(hk.Id, throwException)
        Else
            If throwException Then
                Throw New HotkeyException("The hot key cannot be empty or null (Nothing in VB).")
            End If
        End If
    End Function

    ''' <param name="id">The identifier (Id) of the hot key to unregister. 
    ''' If the function succeeds, the return value is the unregistered hot key.</param>
    ''' <param name="throwException">Specifies whether an exception should be thrown after the method fails.</param>
    <DebuggerHidden()> _
    Public Function UnregisterHotKey(ByVal id As Integer, ByVal throwException As Boolean) As HotKey
        Dim hk As Hotkey = Nothing
        Dim ex As HotkeyException
        If Me._hotkeys.ContainsKey(id) Then
            If Not NativeMethods.UnregisterHotKey(Me._hotkeyProc.Handle, id) Then
                Dim eCode As Integer = Marshal.GetLastWin32Error
                ex = New HotkeyException(New Win32Exception(eCode).Message)
            Else
                hk = New Hotkey(id, Me._hotkeys.Item(id))
                Me._hotkeys.Remove(id)
                Return hk
            End If
        Else
            ex = New HotkeyException("The hot key id is not registered.")
        End If
        If throwException Then
            Throw ex
        End If
        Return hk
    End Function

    ''' <summary>
    ''' Replaces the hotkey data for the same hotkey id.
    ''' </summary>
    ''' <param name="hk">The Hotkey whose data should be replace.</param>
    <DebuggerHidden()> _
    Public Function Replace(ByVal hk As Hotkey, ByVal throwException As Boolean) As Hotkey
        Dim hk1 As Hotkey = Nothing
        Dim ex As HotkeyException
        If hk <> Hotkey.Empty Then
            If Me._hotkeys.ContainsKey(hk.Id) Then
                If Not NativeMethods.RegisterHotKey(Me._hotkeyProc.Handle, hk.Id, HotkeyManager.ConvertTo(hk.Modifiers), hk.Key) Then
                    Dim eCode As Integer = Marshal.GetLastWin32Error
                    ex = New HotkeyException(New Win32Exception(eCode).Message)
                Else
                    hk1 = New Hotkey(hk.Id, Me._hotkeys.Item(hk.Id))
                    Me._hotkeys.Item(hk.Id) = hk.KeyData
                    Return hk1
                End If
            Else
                ex = New HotkeyException("The hot key id is not registered.")
            End If
        Else
            ex = New HotkeyException("The hot key cannot be empty or null (Nothing in VB).")
        End If
        If throwException Then
            Throw ex
        End If
        Return hk1
    End Function

    ''' <summary>
    ''' Determines whether a hotkey is available.
    ''' </summary>
    ''' <param name="keyData">The System.Windows.Form.Keys that represents the hot key data to be checked.
    ''' This parameter can contain one or combination of Keys.Ctrl, 
    ''' Keys.Alt or Keys.Shift fodifier keys and any key, combined with OR.</param>
    <DebuggerHidden()> _
    Public Function IsAvailable(ByVal keyData As Keys) As Boolean
        Dim i As Integer = 1
        While Me._hotkeys.ContainsKey(i)
            i += 1
        End While
        Dim hk As New Hotkey(i, keyData)
        Dim helper As Boolean = Me.RegisterHotKey(hk, False)
        Me.UnregisterHotKey(hk, False)
        Return helper
    End Function

    ''' <summary>
    ''' Determines whether a hot key is registered by the HotkeyManager.
    ''' </summary>
    ''' <param name="hk">The Hotkey to check.</param>
    <DebuggerHidden()> _
    Public Function IsRegistered(ByVal hk As Hotkey) As Boolean
        Return Me._hotkeys.ContainsKey(hk.Id) OrElse Me._hotkeys.ContainsValue(hk.KeyData)
    End Function

    ''' <summary>
    ''' Converts a HotkeyManager.Modifier to System.Windows.Forms.Keys.
    ''' </summary>
    ''' <param name="key">The HotkeyManager.Modifier to be converted to System.Windows.Forms.Keys.</param>
    <DebuggerHidden()> _
    Private Shared Function ConvertTo(ByVal key As Modifier) As Keys
        Dim myKeys As Keys = Keys.None
        If (key And Modifier.Alt) = Modifier.Alt Then
            myKeys = myKeys Or Keys.Alt
        End If
        If (key And Modifier.Control) = Modifier.Control Then
            myKeys = myKeys Or Keys.Control
        End If
        If (key And Modifier.Shift) = Modifier.Shift Then
            myKeys = myKeys Or Keys.Shift
        End If
        Return myKeys
    End Function

    ''' <summary>
    ''' Converts a System.Windows.Forms.Keys to HotkeyManager.Modifier.
    ''' </summary>
    ''' <param name="key">The System.Windows.Forms.Keys to be converted to HotkeyManager.Modifier.</param>
    <DebuggerHidden()> _
    Private Shared Function ConvertTo(ByVal key As Keys) As Modifier
        Dim myKeys As Modifier = Modifier.None
        If (key And Keys.Alt) = Keys.Alt Then
            myKeys = myKeys Or Modifier.Alt
        End If
        If (key And Keys.Control) = Keys.Control Then
            myKeys = myKeys Or Modifier.Control
        End If
        If (key And Keys.Shift) = Keys.Shift Then
            myKeys = myKeys Or Modifier.Shift
        End If
        Return myKeys
    End Function

    ' IDisposable
    <DebuggerHidden()> _
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                For Each key As Integer In Me._hotkeys.Keys
                    If Not NativeMethods.UnregisterHotKey(Me._hotkeyProc.Handle, key) Then
                        Dim eCode As Integer = Marshal.GetLastWin32Error
                        My.Application.Log.WriteException(New HotkeyException(New Win32Exception(eCode).Message))
                        My.Application.Log.WriteException(New HotkeyException(New Win32Exception(eCode).Message), _
                        TraceEventType.Warning, "  Hotkey.Dispose")
                    End If
                Next
                Me._hotkeys.Clear()
                Me._hotkeyProc.ReleaseHandle()
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "

    ''' <summary>
    ''' Unregisters all hotkeys and releases all resources used by the HotkeyManager.
    ''' </summary>
    <DebuggerHidden()> _
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

#Region " On Event "

    <DebuggerHidden()> _
    Protected Overridable Sub OnHotkeyPressed(ByVal e As HotkeyEventArgs)
        RaiseEvent HotkeyPressed(Me, e)
    End Sub

#End Region

#Region " NativeMethods "

    ''' <summary>
    ''' Represents win32 Api shared methods, structures, and constants.
    ''' </summary>
    <CLSCompliant(True)> _
    Private NotInheritable Class NativeMethods

#Region " Constants "

        Public Const WM_HOTKEY As Int32 = &H312
        Public Const WM_NCDESTROY As Integer = &H82

#End Region

#Region " Methods "

        <DebuggerHidden()> _
        Private Sub New()
        End Sub

        <DllImport("user32", SetLastError:=True)> _
        Public Shared Function RegisterHotKey( _
        ByVal hwnd As IntPtr, _
        ByVal id As Int32, _
        ByVal fsModifiers As Int32, _
        ByVal vk As Keys) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32", SetLastError:=True)> _
        Public Shared Function UnregisterHotKey( _
        ByVal hwnd As IntPtr, _
        ByVal id As Int32) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

    End Class 'NativeMethods

#End Region

#Region " HotkeyProc Class "

    <CLSCompliant(True)> _
    Private Class HotkeyPorc
        Inherits NativeWindow

        Private _owner As HotkeyManager

        <DebuggerHidden()> _
        Sub New(ByVal owner As HotkeyManager)
            Me._owner = owner
        End Sub

        <DebuggerHidden()> _
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            If m.Msg = NativeMethods.WM_NCDESTROY Then
                Me._owner.Dispose()
            ElseIf m.Msg = NativeMethods.WM_HOTKEY Then
                Dim wParam As UInteger = CUInt(m.LParam) >> 16
                Dim lParam As UInteger = (CUInt(m.LParam) << 16) >> 16
                Dim modifiers As Keys = HotkeyManager.ConvertTo(CType(lParam, Modifier))
                Dim hk As New Hotkey(CInt(m.WParam), CType(wParam Or modifiers, Keys))
                Me._owner.OnHotkeyPressed(New HotkeyEventArgs(hk, m.HWnd))
            End If
            MyBase.WndProc(m)
        End Sub

    End Class

#End Region

End Class

#Region " HotkeyEventArgs Class "

''' <summary>
''' Provides data for HotkeyManager events.
''' </summary>
<CLSCompliant(True)> _
Public Class HotkeyEventArgs
    Inherits EventArgs

    Private ReadOnly _hk As Hotkey
    Private ReadOnly _hwnd As IntPtr

    ''' <param name="hk">The HotkeyManager.Hotkey that contains the hot key information.</param>
    ''' <param name="hwnd">The window handle of the message.</param>
    <DebuggerHidden()> _
    Sub New(ByVal hk As Hotkey, ByVal hwnd As IntPtr)
        Me._hwnd = hwnd
        Me._hk = hk
    End Sub

    ''' <summary>
    ''' Gets the window handle of the message.
    ''' </summary>
    Public ReadOnly Property HWnd() As IntPtr
        <DebuggerHidden()> _
        Get
            Return Me._hwnd
        End Get
    End Property

    ''' <summary>
    ''' Gets HotkeyManager.Hotkey that contains the pressed hot key information.
    ''' </summary>
    Public ReadOnly Property Hotkey() As Hotkey
        <DebuggerHidden()> _
        Get
            Return Me._hk
        End Get
    End Property

End Class

#End Region

#Region " HotkeyException Class "

''' <summary>
''' Represents errors that occur in the HotkeyManager.
''' </summary>
<Serializable(), CLSCompliant(True)> _
Public Class HotkeyException
    Inherits Exception

    <DebuggerHidden()> _
    Sub New()
    End Sub

    <DebuggerHidden()> _
    Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    <DebuggerHidden()> _
    Sub New(ByVal message As String, ByVal ex As Exception)
        MyBase.New(message, ex)
    End Sub

    <DebuggerHidden()> _
    Protected Sub New(ByVal info As Runtime.Serialization.SerializationInfo, ByVal context As Runtime.Serialization.StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class

#End Region

#Region " HotkeyCollection "

''' <summary>
''' Represents read only collection of HotkeyManager.Hotkey.
''' </summary>
Public Class HotkeyCollection
    Inherits ReadOnlyCollection(Of Hotkey)

    <DebuggerHidden()> _
    Sub New(ByVal hCollection As Collection(Of Hotkey))
        MyBase.New(hCollection)
    End Sub

End Class

#End Region