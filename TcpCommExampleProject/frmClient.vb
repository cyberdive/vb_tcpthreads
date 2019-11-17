Option Strict On
Option Explicit On

Public Class frmClient

    Public IsClosing As Boolean = False
    Public client As New TcpComm.Client(AddressOf UpdateUI, True, 30)
    Public lat As TcpComm.Utilities.LargeArrayTransferHelper
    Private uiUpdateLocker As New Object()

    ' User interface update helper function
    Private Sub UI(ByVal uiUpdate As Action)
        SyncLock uiUpdateLocker
            If Me.InvokeRequired Then
                Me.Invoke(DirectCast(Sub() uiUpdate(), MethodInvoker))
            Else
                uiUpdate()
            End If
        End SyncLock
    End Sub

    Public Sub UpdateUI(ByVal bytes() As Byte, ByVal dataChannel As Byte)

        ' Use TcpComm.Utilities.LargeArrayTransferHelper to make it easier to send and receive 
        ' large arrays sent via lat.SendArray()
        ' The LargeArrayTransferHelperb will assemble an incoming large array
        ' on any channel we choose to evaluate, and pass it back to this callback
        ' when it is complete. Returns True if it has handled this incomming packet,
        ' so we exit the callback when it returns true.
        If Not lat Is Nothing AndAlso lat.HandleIncomingBytes(bytes, dataChannel, , {100, 100}) Then Return

        Dim dontReport As Boolean = False

        If dataChannel < 251 Then
            If bytes.Length < 65535 Then
                UI(Sub() Me.ListBox1.Items.Add(TcpComm.Utilities.BytesToString(bytes)))
            Else
                ' This is a large array delivered by LAT. Display it in the 
                ' large transfer viewer form.
                UI(Sub()
                       Me.ListBox1.Items.Add("Received LAT packet containing " & bytes.Length.ToString & " bytes.")
                       Dim viewer As frmLatViewer = New frmLatViewer
                       viewer.PassBytes(bytes)
                       viewer.Show()
                   End Sub)

            End If

        ElseIf dataChannel = 255 Then
            Dim msg As String = TcpComm.Utilities.BytesToString(bytes)
            Dim tmp As String = ""

            ' Display info about the incoming file:
            If msg.Length > 15 Then tmp = msg.Substring(0, 15)
            If tmp = "Receiving file:" Then
                UI(Sub() 
                       If btGetFile.Text = "Get File" Then btGetFile.Text = "Cancel"
                       gbGetFilePregress.Text = "Receiving: " & client.GetIncomingFileName
                   End Sub)
                dontReport = True
            End If

            ' Display info about the outgoing file:
            If msg.Length > 13 Then tmp = msg.Substring(0, 13)
            If tmp = "Sending file:" Then
                UI(Sub() gbSendFileProgress.Text = "Sending: " & client.GetOutgoingFileName)
                dontReport = True
            End If

            ' The file being sent to the client is complete.
            If msg = "->Done" Then
                UI(Sub()
                       gbGetFilePregress.Text = "File->Client: Transfer complete."
                       btGetFile.Text = "Get File"
                   End Sub)
                dontReport = True
            End If

            ' The file being sent to the server is complete.
            If msg = "<-Done" Then
                UI(Sub()
                       gbSendFileProgress.Text = "File->Server: Transfer complete."
                       btSendFile.Text = "Send File"
                   End Sub)
                dontReport = True
            End If

            ' The file transfer to the client has been aborted.
            If msg = "->Aborted." Then
                UI(Sub() gbGetFilePregress.Text = "File->Client: Transfer aborted.")
                UI(Sub() pbIncomingFile.Value = 0)
                dontReport = True
            End If

            ' The file transfer to the server has been aborted.
            If msg = "<-Aborted." Then
                UI(Sub() gbSendFileProgress.Text = "File->Server: Transfer aborted.")
                UI(Sub() pbOutgoingFile.Value = 0)
                dontReport = True
            End If

            ' _Client as finished sending the bytes you put into sendBytes()
            If msg.Length > 4 Then tmp = msg.Substring(0, 4)
            If tmp = "UBS:" Then ' User Bytes Sent on channel:???.
                UI(Sub() btSendText.Enabled = True)
                dontReport = True
            End If

            ' We have an error message. Could be local, or from the server.
            If msg.Length > 4 Then tmp = msg.Substring(0, 5)
            If tmp = "ERR: " Then
                UI(Sub() MsgBox("" & Split(msg, ": ")(1), MsgBoxStyle.Critical, "Test Tcp Communications App"))
                dontReport = True
            End If

            If msg.Equals("Disconnected.") Then UI(Sub() Me.Button2.Text = "Connect")

            ' Display all other messages in the status strip.
            If Not dontReport Then UI(Sub() Me.ToolStripStatusLabel1.Text = TcpComm.Utilities.BytesToString(bytes))
        End If

    End Sub

    Private Sub frmClient_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        tmrPoll.Enabled = False
        IsClosing = True
        If client IsNot Nothing AndAlso client.isClientRunning Then
            client.Close(True)
            While client.isClientRunning
                Application.DoEvents()
            End While
        End If
    End Sub

    Private Sub frmClient_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        client.Close(True)
    End Sub

    Private Sub frmClient_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox2.Text = client.GetLocalIpAddress.ToString
        client.SetReceivedFilesFolder(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\ClientReceivedFiles")
        tmrPoll.Start()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If Me.Button2.Text = "Connect" Then
            Dim s As String
            Dim errMsg As String = ""
            s = Me.TextBox3.Text.Trim
            Me.Button2.Enabled = False
            Me.Button2.Text = "Disconnect"
            client = New TcpComm.Client(AddressOf UpdateUI, cbReconnect.Checked, 30)
            If Not client.Connect(Me.TextBox2.Text.Trim, Convert.ToUInt16(s), Me.tbMachineID.Text, errMsg) Then
                Me.Button2.Text = "Connect"
                If errMsg.Trim <> "" Then MsgBox(errMsg, MsgBoxStyle.Critical, "Test Tcp Communications App")
            End If

            lat = New TcpComm.Utilities.LargeArrayTransferHelper(client)

            client.incomingFileProgress = Sub(percentComplete)
                                              UI(Sub()
                                                     Me.pbIncomingFile.Value = percentComplete
                                                 End Sub)
                                          End Sub
            client.outgoingFileProgress = Sub(percentComplete)
                                              UI(Sub()
                                                     Me.pbOutgoingFile.Value = percentComplete
                                                 End Sub)
                                          End Sub
            client.incomingFileComplete = Sub()
                                              UI(Sub()
                                                     Me.pbIncomingFile.Value = 0
                                                 End Sub)
                                          End Sub
            client.outgoingFileComplete = Sub()
                                              UI(Sub()
                                                     Me.pbOutgoingFile.Value = 0
                                                 End Sub)
                                          End Sub

            Me.Button2.Enabled = True
        Else
            client.Close()
            Me.Button2.Text = "Connect"
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGetFileBrowse.Click
        Dim ofd As New OpenFileDialog
        Dim _path As String
        ofd.ShowDialog()
        _path = ofd.FileName
        tbGetFileReq.Text = _path
    End Sub

    Private Sub btGetFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGetFile.Click
        If btGetFile.Text = "Get File" Then
            If tbGetFileReq.Text.Trim <> "" Then
                If client.isClientRunning Then client.GetFile(tbGetFileReq.Text.Trim)
                btGetFile.Text = "Cancel"
            End If
        Else
            client.CancelIncomingFileTransfer()
            btGetFile.Text = "Get File"
        End If
    End Sub

    Private Sub tmrPoll_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrPoll.Tick
        '' Update Mbps
        Me.Text = "Test Client (" & client.GetMbps & ")"
    End Sub

    Private Sub btSendFileBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSendFileBrowse.Click
        Dim ofd As New OpenFileDialog
        ofd.ShowDialog()
        tbSendFile.Text = ofd.FileName
    End Sub

    Private Sub btSendFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSendFile.Click
        If btSendFile.Text = "Send File" Then
            If tbSendFile.Text.Trim <> "" Then
                If client.isClientRunning Then client.SendFile(tbSendFile.Text.Trim)
                btSendFile.Text = "Cancel"
            End If
        Else
            client.CancelOutgoingFileTransfer()
            btSendFile.Text = "Send File"
        End If
    End Sub

    Private Sub btSendText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSendText.Click
        If Me.tbSendText.Text.Trim.Length > 0 Then
            Dim errorMessage As String = ""
            If Not client.SendText(Me.tbSendText.Text.Trim, , errorMessage) Then MsgBox(errorMessage, MsgBoxStyle.Critical)
        Else
            Dim reslt As DialogResult = MessageBox.Show("You clicked 'Send' when the text box was empty. Click OK to send the example large array to the server.", "Send a large array?", 
                                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

            If reslt = DialogResult.OK Then
                Dim fileBytes() As Byte = System.IO.File.ReadAllBytes("big.txt")
                Dim errMsg As String = ""
                If Not lat.SendArray(fileBytes, 100, errMsg) Then MsgBox(errMsg, MsgBoxStyle.Critical, "TcpComm Demo App")
            End If
            
        End If
    End Sub

    Public Function SetMachineIDNumber(ByVal idNumber As Int32) As Boolean
        Me.tbMachineID.Text = "Unique Machine ID" & idNumber.ToString()
        Return True
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        client.SetMachineID(Me.tbMachineID.Text)
    End Sub

End Class