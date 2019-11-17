Option Strict On
Option Explicit On

Imports System.Net

Public Class frmServer

    Private server As TcpComm.Server
    Private lat As TcpComm.Utilities.LargeArrayTransferHelper

    Private Sub UI(ByVal uiUpdate As Action)
            If Me.InvokeRequired Then
                Try
                    Me.Invoke(DirectCast(Sub() uiUpdate(), MethodInvoker))
                Catch ex As Exception
                    ' The try catch block here is here because the end user (you) may decide to close the form without closing the server first, 
                    ' and that may cause an ObjectDesposed access exception. I think we'd all rather not see that.
                End Try
            Else
                Try
                    uiUpdate()
                Catch ex As Exception
                    ' The try catch block here is here because the end user (you) may decide to close the form without closing the server first, 
                    ' and that may cause an ObjectDesposed access exception. I think we'd all rather not see that.
                End Try
            End If
    End Sub

    Public Sub UpdateUI(ByVal bytes() As Byte, ByVal sessionID As Int32, ByVal dataChannel As Byte)

        ' Use TcpComm.Utilities.LargeArrayTransferHelper to make it easier to send and receive 
        ' large arrays sent via lat.SendArray()
        ' The LargeArrayTransferHelperb will assemble any number of incoming large arrays
        ' on any channel or from any sessionId, and pass them back to this callback
        ' when they are complete. Returns True if it has handled this incomming packet,
        ' so we exit the callback when it returns true.
        If lat.HandleIncomingBytes(bytes, dataChannel, sessionID, { 100, 100 }) then Return

        If dataChannel = 100 then
            UI(Sub()
                       Dim viewer As frmLatViewer = New frmLatViewer
                       viewer.PassBytes(bytes)
                       viewer.Show()
                   End Sub)
            Return
        End If

        If dataChannel < 251 Then
                UI(Sub() Me.lbTextInput.Items.Add("Session " & sessionID.ToString & ": " & TcpComm.Utilities.BytesToString(bytes)))
        ElseIf dataChannel = 255 Then
            Dim tmp = ""
            Dim msg As String = TcpComm.Utilities.BytesToString(bytes)
            Dim dontReport As Boolean = False

            ' server has finished sending the bytes you put into sendBytes()
            If msg.Length > 3 Then tmp = msg.Substring(0, 3)
            If tmp = "UBS" Then ' User Bytes Sent.
                Dim parts() As String = Split(msg, "UBS:")
                msg = "Data sent to session: " & parts(1)
            End If

            If msg = "Connected." Then UI(Sub() UpdateClientsList())
            If msg.Contains(" MachineID:") Then UI(Sub() UpdateClientsList())
            If msg.Contains("Session Stopped. (") Then UI(Sub() UpdateClientsList())

            If Not dontReport Then UI(Sub() Me.tsslStatus.Text = msg)
        End If

    End Sub

    Private Sub UpdateClientsList()

        Dim sessionList As List(Of TcpComm.Server.SessionCommunications) = server.GetSessionCollection()
        Dim lvi As ListViewItem

        Me.lvClients.Items.Clear

        For Each session As TcpComm.Server.SessionCommunications In sessionList
            If session.IsRunning Then
                lvi = New ListViewItem(" Connected", 0, lvClients.Groups.Item(0))
            Else
                lvi = New ListViewItem(" Disconnected", 1, lvClients.Groups.Item(1))
            End If

            lvi.SubItems.Add(session.sessionID.ToString())
            lvi.SubItems.Add(session.machineId)
            Me.lvClients.Items.Add(lvi)
        Next
    End Sub

    Private Sub frmServer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If server IsNot Nothing Then server.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim addresses As List(Of System.Net.IPAddress) = TcpComm.Server.GetLocalIpAddresses()
        Me.tsslStatus.Text = "Idle."

        addresses.Add(System.Net.IPAddress.Any)

        For Each address As System.Net.IPAddress In addresses
            cbIpAddresses.Items.Add(address)
        Next

        cbIpAddresses.SelectedItem = cbIpAddresses.Items(0)

        frmClient.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btStartServer.Click

        If btStartServer.Text = "Start Server" Then
            Dim errMsg As String     = ""
            Dim address As IPAddress = System.Net.IPAddress.Parse(cbIpAddresses.SelectedItem.ToString())

            server                   = New TcpComm.Server(AddressOf UpdateUI, , cbUniqueIds.Checked)
            lat                      = New TcpComm.Utilities.LargeArrayTransferHelper(server)

            If Not server.Start(22490, address, errMsg) Then
                tsslStatus.Text = errMsg
                Return
            End If

            Dim ftm As New Threading.Thread(AddressOf FileTransferMonitor)
            ftm.Start()

            btStartServer.Text = "Stop Server"
        Else
            If server IsNot Nothing Then server.Close()
            lat = Nothing
            Me.lvClients.Items.Clear
            btStartServer.Text = "Start Server"
        End If

    End Sub

    Private Sub UpdateFileProgress(session As TcpComm.Server.SessionCommunications)
        Dim moveOn As Boolean = False
        Dim tmpId As Integer = -1

        moveOn = False

        ' If we're sending a file from this session...
        If session.SendingFile Then
            ' We need to run some code on the UI thread:
            UI(Sub()
                   tmpId = -1

                   ' Is there already an entry for this session in the listview? If so, update the progress:
                   For Each tlvi As ListViewItem In lvFileTransfers.Items
                       Integer.TryParse(tlvi.SubItems(0).Text, tmpId)
                       If tmpId = session.sessionID And tlvi.SubItems(2).Text.Equals("Out") Then

                           tlvi.SubItems(3).Text = session.GetPercentOfFileSent().ToString() & "%"
                           Exit For
                       Else
                           tmpId = -1
                       End If
                   Next

                   ' If not, create one:
                   If tmpId = -1 Then
                       Dim lvi As New ListViewItem
                       lvi.Text = session.sessionID.ToString()
                       lvi.SubItems.Add(System.IO.Path.GetFileName(session.OutgingFileName))
                       lvi.SubItems.Add("Out")
                       lvi.SubItems.Add(session.GetPercentOfFileSent().ToString())
                       lvFileTransfers.Items.Add(lvi)
                   End If

                   moveOn = True
               End Sub)

            ' Wait for the UI update to complete...
            While Not moveOn
                Threading.Thread.Sleep(1)
            End While
        Else
            ' If this session WAS sending a file, but it's not now, remove it from the listview:
            ' So we need to run some more code on the UI thread:
            UI(Sub()
                   For Each tlvi As ListViewItem In lvFileTransfers.Items
                       Integer.TryParse(tlvi.SubItems(0).Text, tmpId)
                       If tmpId = session.sessionID And tlvi.SubItems(2).Text.Equals("Out") Then
                           lvFileTransfers.Items.Remove(tlvi)
                           Exit For
                       End If
                   Next
               End Sub)
        End If
        
        ' If we're receiving a file from this session...
        If session.ReceivingFile Then
            moveOn = False

            ' Aaannndd more code on the UI thread...
            UI(Sub()
                    tmpId = -1

                   ' Is there already an entry for this session in the listview? If so, update the progress:
                    For Each tlvi As ListViewItem In lvFileTransfers.Items
                        Integer.TryParse(tlvi.Text, tmpId)
                        If tmpId = session.sessionId And tlvi.SubItems(2).Text.Equals("In") Then 

                            tlvi.SubItems(3).Text = session.GetPercentOfFileReceived.ToString() & "%"
                            Exit For
                        Else
                            tmpId = -1
                        End If
                    Next

                    Dim lvi As New ListViewItem

                    ' And of not, create one here too:
                    If tmpId = -1 then
                        lvi.Text = session.sessionID.ToString()
                        lvi.SubItems.Add(System.IO.Path.GetFileName(session.IncomingFileName))
                        lvi.SubItems.Add("In")
                        lvi.SubItems.Add(session.GetPercentOfFileReceived.ToString())
                        lvFileTransfers.Items.Add(lvi)
                    End If

                    moveOn = True
                End Sub)

            ' Wait for the UI update to complete...
            While Not moveOn
                Threading.Thread.Sleep(1)
            End While
        Else
            ' If this session WAS receiving a file, but it's not now, remove it from the listview:
            ' Even more code on the UI thread:
            UI(Sub()
                   For Each tlvi As ListViewItem In lvFileTransfers.Items
                       Integer.TryParse(tlvi.SubItems(0).Text, tmpId)
                       If tmpId = session.sessionID And tlvi.SubItems(2).Text.Equals("In") Then
                           lvFileTransfers.Items.Remove(tlvi)
                           Exit For
                       End If
                   Next
               End Sub)
        End If

    End Sub


    Private Sub FileTransferMonitor()
        
        Dim waitTimeout As Date
        Dim sessions As List(Of TcpComm.Server.SessionCommunications)
        Dim moveOn As Boolean = False

        While server.IsRunning

            waitTimeout = Now.AddMilliseconds(100)
            While Now < waitTimeout
                Threading.Thread.Sleep(1)
            End While

            sessions = server.GetSessionCollection()

            For Each session As TcpComm.Server.SessionCommunications In sessions
                UpdateFileProgress(session)
            Next

        End While

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSendText.Click
        If Me.tbSendText.Text.Trim.Length > 0 Then
            ' Send a text message to all connected sessions on channel 1.
            server.SendText(Me.tbSendText.Text.Trim)
        End If
    End Sub

    Private Sub btStartNewClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btStartNewClient.Click
        Dim newClient As New frmClient
        Static uniqueMachineIDNumber As Int32 = 1
        newClient.SetMachineIDNumber(uniqueMachineIDNumber)
        newClient.Show()
        uniqueMachineIDNumber +=1
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.lbTextInput.Items.Clear()
    End Sub

    Private Sub SendAFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SendAFileToolStripMenuItem.Click
        If lvClients.SelectedItems.Count > 0 then
            Dim lvi As ListViewItem = lvClients.SelectedItems.Item(0)
            ' Get the session using the sessionID pulled from the selected listview item
            Dim session As TcpComm.Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))
            Dim message As String
            Dim fileName As String

            If session is Nothing then
                MsgBox("This session is disconnected.", MsgBoxStyle.Critical, "TcpDemoApp")
                Return
            End If

            message = "Select a file to send to " & lvi.SubItems(2).Text

            ofdSendFileToClient.Title       = message
            ofdSendFileToClient.FileName    = ""
            ofdSendFileToClient.ShowDialog
            fileName = ofdSendFileToClient.FileName

            If fileName.Trim().Equals("") then Exit Sub

            If Not server.SendFile(fileName, session.sessionID) Then
                MsgBox("This session is disconnected.", MsgBoxStyle.Critical, "TcpDemoApp")
            End If
        End If
    End Sub

    Private Sub SendTextToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SendTextToolStripMenuItem.Click
        If lvClients.SelectedItems.Count > 0 then
            Dim lvi As ListViewItem = lvClients.SelectedItems.Item(0)
            ' Get the session using the sessionID pulled from the selected listview item
            Dim session As TcpComm.Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))
            Dim message, title, defaultValue As String
            Dim retValue As Object

            If session is Nothing then
                MsgBox("This session is disconnected.", MsgBoxStyle.Critical, "TcpDemoApp")
                Return
            End If

            message = "Type some text to send to " & lvi.SubItems(2).Text
            title = "TcpComm Demo App"
            defaultValue = "Test text"
            retValue = InputBox(message, title, defaultValue)
            If retValue Is "" Then Return

            If session IsNot Nothing Then server.SendText(retValue.ToString(), 1, session.sessionID)
        End If
    End Sub

    Private Sub DisconnectSessionToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DisconnectSessionToolStripMenuItem.Click
        If lvClients.SelectedItems.Count > 0 then
            Dim lvi As ListViewItem = lvClients.SelectedItems.Item(0)
            ' Get the session using the sessionID pulled from the selected listview item
            Dim session As TcpComm.Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))

            If session IsNot Nothing Then session.Close()
        End If
    End Sub

    Private Sub TestLargeArrayTransferHelperToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestLargeArrayTransferHelperToolStripMenuItem.Click
        If lvClients.SelectedItems.Count = 0 then 
            MsgBox("You must select a connected client to send an array.", MsgBoxStyle.Critical, "TcpComm Demo App")
            Return
        End If

        Dim lvi As ListViewItem = lvClients.SelectedItems.Item(0)
        ' Get the session using the sessionID pulled from the selected listview item
        Dim session As TcpComm.Server.SessionCommunications = server.GetSession(Convert.ToInt32(lvi.SubItems(1).Text))

        If session is Nothing then
            MsgBox("You can't send a large array to a disconnected session!", MsgBoxStyle.Critical, "TcpComm Demo App")
            Return
        End If

        Dim msg = "This version if the library includes a helper function for people attempting to send arrays larger then the maximum packetsize. " & _
            "In those cases, the array will be broken up into multiple packets, and delivered one by one. This helper class can be used to send the large arrays and " & _
            "have LAT (the TcpComm.Utilities.LargeArrayTransferHelper tool) assemble them for you in the remote machine. " & vbCrLf & vbCrLf & _
            "This test will read about 500k of a large text file into a byte array, and send it to the client you selected (this would normally arrive in about 8 pieces). When it arrives, it will be " & _
            "displayed in the 'Lat Viewer', a form with a multiline textbox on it that you can use to verify that all the text has been delivered and assembled " & _
            "properly, and verify the message size."

        Dim retVal As MsgBoxResult = MsgBox(msg, MsgBoxStyle.Information Or MsgBoxStyle.OkCancel, "TcpComm Demo App")
        If retVal = MsgBoxResult.Ok Then
            If session IsNot Nothing Then
                Dim fileBytes() As Byte = System.IO.File.ReadAllBytes("big.txt")
                Dim errMsg As String = ""
                If Not lat.SendArray(fileBytes, 100, session.sessionID, errMsg) Then MsgBox(errMsg, MsgBoxStyle.Critical, "TcpComm Demo App")
            End If
        End If
    End Sub
End Class