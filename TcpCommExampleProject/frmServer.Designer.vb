<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Connected Clients", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Disconnected Clients", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServer))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btSendText = New System.Windows.Forms.Button()
        Me.gbTextInput = New System.Windows.Forms.GroupBox()
        Me.lbTextInput = New System.Windows.Forms.ListBox()
        Me.tbSendText = New System.Windows.Forms.TextBox()
        Me.btStartServer = New System.Windows.Forms.Button()
        Me.gbSentText = New System.Windows.Forms.GroupBox()
        Me.btStartNewClient = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lvClients = New System.Windows.Forms.ListView()
        Me.Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SessionId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MachineId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.sessionRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendTextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendAFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestLargeArrayTransferHelperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisconnectSessionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lvImages = New System.Windows.Forms.ImageList(Me.components)
        Me.ofdSendFileToClient = New System.Windows.Forms.OpenFileDialog()
        Me.cbUniqueIds = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lvFileTransfers = New System.Windows.Forms.ListView()
        Me.Empty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FileName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Direction = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PercentComplete = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cbIpAddresses = New System.Windows.Forms.ComboBox()
        Me.gbIpAddresses = New System.Windows.Forms.GroupBox()
        Me.gbUniqueMachineIds = New System.Windows.Forms.GroupBox()
        Me.StatusStrip1.SuspendLayout()
        Me.gbTextInput.SuspendLayout()
        Me.gbSentText.SuspendLayout()
        Me.sessionRightClickMenu.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.gbIpAddresses.SuspendLayout()
        Me.gbUniqueMachineIds.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 575)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(354, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(29, 17)
        Me.tsslStatus.Text = "Idle."
        '
        'btSendText
        '
        Me.btSendText.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSendText.Location = New System.Drawing.Point(268, 14)
        Me.btSendText.Margin = New System.Windows.Forms.Padding(2)
        Me.btSendText.Name = "btSendText"
        Me.btSendText.Size = New System.Drawing.Size(64, 24)
        Me.btSendText.TabIndex = 1
        Me.btSendText.Text = "Send Text"
        Me.btSendText.UseVisualStyleBackColor = True
        '
        'gbTextInput
        '
        Me.gbTextInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTextInput.Controls.Add(Me.lbTextInput)
        Me.gbTextInput.Location = New System.Drawing.Point(7, 249)
        Me.gbTextInput.Margin = New System.Windows.Forms.Padding(2)
        Me.gbTextInput.Name = "gbTextInput"
        Me.gbTextInput.Padding = New System.Windows.Forms.Padding(2)
        Me.gbTextInput.Size = New System.Drawing.Size(336, 195)
        Me.gbTextInput.TabIndex = 2
        Me.gbTextInput.TabStop = False
        Me.gbTextInput.Text = "Text in:"
        '
        'lbTextInput
        '
        Me.lbTextInput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTextInput.FormattingEnabled = True
        Me.lbTextInput.HorizontalScrollbar = True
        Me.lbTextInput.Location = New System.Drawing.Point(4, 17)
        Me.lbTextInput.Margin = New System.Windows.Forms.Padding(2)
        Me.lbTextInput.Name = "lbTextInput"
        Me.lbTextInput.Size = New System.Drawing.Size(328, 173)
        Me.lbTextInput.TabIndex = 0
        '
        'tbSendText
        '
        Me.tbSendText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSendText.Location = New System.Drawing.Point(4, 16)
        Me.tbSendText.Margin = New System.Windows.Forms.Padding(2)
        Me.tbSendText.Multiline = True
        Me.tbSendText.Name = "tbSendText"
        Me.tbSendText.Size = New System.Drawing.Size(260, 24)
        Me.tbSendText.TabIndex = 3
        '
        'btStartServer
        '
        Me.btStartServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btStartServer.Location = New System.Drawing.Point(277, 542)
        Me.btStartServer.Margin = New System.Windows.Forms.Padding(2)
        Me.btStartServer.Name = "btStartServer"
        Me.btStartServer.Size = New System.Drawing.Size(69, 24)
        Me.btStartServer.TabIndex = 1
        Me.btStartServer.Text = "Start Server"
        Me.btStartServer.UseVisualStyleBackColor = True
        '
        'gbSentText
        '
        Me.gbSentText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbSentText.Controls.Add(Me.tbSendText)
        Me.gbSentText.Controls.Add(Me.btSendText)
        Me.gbSentText.Location = New System.Drawing.Point(7, 493)
        Me.gbSentText.Margin = New System.Windows.Forms.Padding(2)
        Me.gbSentText.Name = "gbSentText"
        Me.gbSentText.Padding = New System.Windows.Forms.Padding(2)
        Me.gbSentText.Size = New System.Drawing.Size(336, 45)
        Me.gbSentText.TabIndex = 1
        Me.gbSentText.TabStop = False
        Me.gbSentText.Text = "Broadcast text (send to all connected clients)"
        '
        'btStartNewClient
        '
        Me.btStartNewClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btStartNewClient.Location = New System.Drawing.Point(180, 542)
        Me.btStartNewClient.Margin = New System.Windows.Forms.Padding(2)
        Me.btStartNewClient.Name = "btStartNewClient"
        Me.btStartNewClient.Size = New System.Drawing.Size(93, 24)
        Me.btStartNewClient.TabIndex = 3
        Me.btStartNewClient.Text = "Start New Client"
        Me.btStartNewClient.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(100, 543)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Clear Listbox"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lvClients
        '
        Me.lvClients.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvClients.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvClients.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Status, Me.SessionId, Me.MachineId})
        Me.lvClients.ContextMenuStrip = Me.sessionRightClickMenu
        Me.lvClients.FullRowSelect = True
        ListViewGroup1.Header = "Connected Clients"
        ListViewGroup1.Name = "ConnectedClients"
        ListViewGroup2.Header = "Disconnected Clients"
        ListViewGroup2.Name = "DisconnectedClients"
        Me.lvClients.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvClients.Location = New System.Drawing.Point(1, 4)
        Me.lvClients.Name = "lvClients"
        Me.lvClients.Size = New System.Drawing.Size(327, 199)
        Me.lvClients.SmallImageList = Me.lvImages
        Me.lvClients.TabIndex = 0
        Me.lvClients.UseCompatibleStateImageBehavior = False
        Me.lvClients.View = System.Windows.Forms.View.Details
        '
        'Status
        '
        Me.Status.Text = "Status"
        Me.Status.Width = 121
        '
        'SessionId
        '
        Me.SessionId.Text = "SessionId"
        Me.SessionId.Width = 63
        '
        'MachineId
        '
        Me.MachineId.Text = "MachineId"
        Me.MachineId.Width = 128
        '
        'sessionRightClickMenu
        '
        Me.sessionRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendTextToolStripMenuItem, Me.SendAFileToolStripMenuItem, Me.TestLargeArrayTransferHelperToolStripMenuItem, Me.DisconnectSessionToolStripMenuItem})
        Me.sessionRightClickMenu.Name = "sessionRightClickMenu"
        Me.sessionRightClickMenu.Size = New System.Drawing.Size(176, 92)
        '
        'SendTextToolStripMenuItem
        '
        Me.SendTextToolStripMenuItem.Name = "SendTextToolStripMenuItem"
        Me.SendTextToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.SendTextToolStripMenuItem.Text = "Send Text"
        '
        'SendAFileToolStripMenuItem
        '
        Me.SendAFileToolStripMenuItem.Name = "SendAFileToolStripMenuItem"
        Me.SendAFileToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.SendAFileToolStripMenuItem.Text = "Send A file"
        '
        'TestLargeArrayTransferHelperToolStripMenuItem
        '
        Me.TestLargeArrayTransferHelperToolStripMenuItem.Name = "TestLargeArrayTransferHelperToolStripMenuItem"
        Me.TestLargeArrayTransferHelperToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.TestLargeArrayTransferHelperToolStripMenuItem.Text = "Send a large array"
        '
        'DisconnectSessionToolStripMenuItem
        '
        Me.DisconnectSessionToolStripMenuItem.Name = "DisconnectSessionToolStripMenuItem"
        Me.DisconnectSessionToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.DisconnectSessionToolStripMenuItem.Text = "Disconnect Session"
        '
        'lvImages
        '
        Me.lvImages.ImageStream = CType(resources.GetObject("lvImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.lvImages.TransparentColor = System.Drawing.Color.Transparent
        Me.lvImages.Images.SetKeyName(0, "user-available.ico")
        Me.lvImages.Images.SetKeyName(1, "user-invisible.ico")
        '
        'ofdSendFileToClient
        '
        Me.ofdSendFileToClient.FileName = "OpenFileDialog1"
        '
        'cbUniqueIds
        '
        Me.cbUniqueIds.AutoSize = True
        Me.cbUniqueIds.Checked = True
        Me.cbUniqueIds.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbUniqueIds.Location = New System.Drawing.Point(7, 18)
        Me.cbUniqueIds.Name = "cbUniqueIds"
        Me.cbUniqueIds.Size = New System.Drawing.Size(117, 17)
        Me.cbUniqueIds.TabIndex = 6
        Me.cbUniqueIds.Text = "Enforce Unique Ids"
        Me.cbUniqueIds.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(7, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(339, 232)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lvClients)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(331, 206)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Connected Clients"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lvFileTransfers)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(331, 206)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Active File Transfers"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lvFileTransfers
        '
        Me.lvFileTransfers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvFileTransfers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Empty, Me.FileName, Me.Direction, Me.PercentComplete})
        Me.lvFileTransfers.Location = New System.Drawing.Point(1, 3)
        Me.lvFileTransfers.Name = "lvFileTransfers"
        Me.lvFileTransfers.Size = New System.Drawing.Size(331, 200)
        Me.lvFileTransfers.TabIndex = 0
        Me.lvFileTransfers.UseCompatibleStateImageBehavior = False
        Me.lvFileTransfers.View = System.Windows.Forms.View.Details
        '
        'Empty
        '
        Me.Empty.Text = "ID"
        Me.Empty.Width = 30
        '
        'FileName
        '
        Me.FileName.Text = "File Name"
        Me.FileName.Width = 150
        '
        'Direction
        '
        Me.Direction.Text = "Direction"
        Me.Direction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PercentComplete
        '
        Me.PercentComplete.Text = "% Complete"
        Me.PercentComplete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.PercentComplete.Width = 80
        '
        'cbIpAddresses
        '
        Me.cbIpAddresses.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbIpAddresses.FormattingEnabled = True
        Me.cbIpAddresses.Location = New System.Drawing.Point(6, 18)
        Me.cbIpAddresses.Name = "cbIpAddresses"
        Me.cbIpAddresses.Size = New System.Drawing.Size(178, 21)
        Me.cbIpAddresses.TabIndex = 8
        '
        'gbIpAddresses
        '
        Me.gbIpAddresses.Controls.Add(Me.cbIpAddresses)
        Me.gbIpAddresses.Location = New System.Drawing.Point(153, 448)
        Me.gbIpAddresses.Name = "gbIpAddresses"
        Me.gbIpAddresses.Size = New System.Drawing.Size(190, 43)
        Me.gbIpAddresses.TabIndex = 9
        Me.gbIpAddresses.TabStop = False
        Me.gbIpAddresses.Text = "Listen on: (0.0.0.0 = IPAddress.Any)"
        '
        'gbUniqueMachineIds
        '
        Me.gbUniqueMachineIds.Controls.Add(Me.cbUniqueIds)
        Me.gbUniqueMachineIds.Location = New System.Drawing.Point(7, 448)
        Me.gbUniqueMachineIds.Name = "gbUniqueMachineIds"
        Me.gbUniqueMachineIds.Size = New System.Drawing.Size(140, 43)
        Me.gbUniqueMachineIds.TabIndex = 10
        Me.gbUniqueMachineIds.TabStop = False
        Me.gbUniqueMachineIds.Text = "Machine Ids"
        '
        'frmServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 597)
        Me.Controls.Add(Me.gbUniqueMachineIds)
        Me.Controls.Add(Me.gbIpAddresses)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.gbTextInput)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btStartNewClient)
        Me.Controls.Add(Me.gbSentText)
        Me.Controls.Add(Me.btStartServer)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmServer"
        Me.Text = "TcpComm Server"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.gbTextInput.ResumeLayout(False)
        Me.gbSentText.ResumeLayout(False)
        Me.gbSentText.PerformLayout()
        Me.sessionRightClickMenu.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.gbIpAddresses.ResumeLayout(False)
        Me.gbUniqueMachineIds.ResumeLayout(False)
        Me.gbUniqueMachineIds.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btSendText As System.Windows.Forms.Button
    Friend WithEvents gbTextInput As System.Windows.Forms.GroupBox
    Friend WithEvents lbTextInput As System.Windows.Forms.ListBox
    Friend WithEvents tbSendText As System.Windows.Forms.TextBox
    Friend WithEvents btStartServer As System.Windows.Forms.Button
    Friend WithEvents gbSentText As System.Windows.Forms.GroupBox
    Friend WithEvents btStartNewClient As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lvClients As System.Windows.Forms.ListView
    Friend WithEvents Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents SessionId As System.Windows.Forms.ColumnHeader
    Friend WithEvents MachineId As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvImages As System.Windows.Forms.ImageList
    Friend WithEvents sessionRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SendTextToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendAFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ofdSendFileToClient As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DisconnectSessionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestLargeArrayTransferHelperToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbUniqueIds As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lvFileTransfers As System.Windows.Forms.ListView
    Friend WithEvents Empty As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Direction As System.Windows.Forms.ColumnHeader
    Friend WithEvents PercentComplete As System.Windows.Forms.ColumnHeader
    Friend WithEvents cbIpAddresses As System.Windows.Forms.ComboBox
    Friend WithEvents gbIpAddresses As System.Windows.Forms.GroupBox
    Friend WithEvents gbUniqueMachineIds As System.Windows.Forms.GroupBox

End Class
