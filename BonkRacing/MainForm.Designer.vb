﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
		Me.components = New System.ComponentModel.Container
		Me.renderTimer = New System.Windows.Forms.Timer(Me.components)
		Me.startButton = New System.Windows.Forms.Button
		Me.stopButton = New System.Windows.Forms.Button
		Me.clearButton = New System.Windows.Forms.Button
		Me.createButton = New System.Windows.Forms.Button
		Me.selectButton = New System.Windows.Forms.Button
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.BrushToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editButton = New System.Windows.Forms.Button
		Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.ContextMenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'renderTimer
		'
		Me.renderTimer.Enabled = True
		Me.renderTimer.Interval = 20
		'
		'startButton
		'
		Me.startButton.Location = New System.Drawing.Point(154, 12)
		Me.startButton.Name = "startButton"
		Me.startButton.Size = New System.Drawing.Size(75, 23)
		Me.startButton.TabIndex = 0
		Me.startButton.Text = "Start"
		'
		'stopButton
		'
		Me.stopButton.Location = New System.Drawing.Point(235, 12)
		Me.stopButton.Name = "stopButton"
		Me.stopButton.Size = New System.Drawing.Size(75, 23)
		Me.stopButton.TabIndex = 0
		Me.stopButton.Text = "Stop"
		'
		'clearButton
		'
		Me.clearButton.Location = New System.Drawing.Point(316, 12)
		Me.clearButton.Name = "clearButton"
		Me.clearButton.Size = New System.Drawing.Size(75, 23)
		Me.clearButton.TabIndex = 0
		Me.clearButton.Text = "Clear"
		'
		'createButton
		'
		Me.createButton.Location = New System.Drawing.Point(397, 12)
		Me.createButton.Name = "createButton"
		Me.createButton.Size = New System.Drawing.Size(75, 23)
		Me.createButton.TabIndex = 0
		Me.createButton.Text = "Create"
		'
		'selectButton
		'
		Me.selectButton.Location = New System.Drawing.Point(478, 12)
		Me.selectButton.Name = "selectButton"
		Me.selectButton.Size = New System.Drawing.Size(75, 23)
		Me.selectButton.TabIndex = 0
		Me.selectButton.Text = "Select"
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImageToolStripMenuItem, Me.BrushToolStripMenuItem, Me.PropertiesToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 92)
		'
		'ImageToolStripMenuItem
		'
		Me.ImageToolStripMenuItem.Name = "ImageToolStripMenuItem"
		Me.ImageToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
		Me.ImageToolStripMenuItem.Text = "Image..."
		'
		'BrushToolStripMenuItem
		'
		Me.BrushToolStripMenuItem.Name = "BrushToolStripMenuItem"
		Me.BrushToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.BrushToolStripMenuItem.Text = "Brush..."
		'
		'editButton
		'
		Me.editButton.ContextMenuStrip = Me.ContextMenuStrip1
		Me.editButton.Location = New System.Drawing.Point(559, 12)
		Me.editButton.Name = "editButton"
		Me.editButton.Size = New System.Drawing.Size(75, 23)
		Me.editButton.TabIndex = 0
		Me.editButton.Text = "Edit"
		'
		'PropertiesToolStripMenuItem
		'
		Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
		Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.PropertiesToolStripMenuItem.Text = "Properties..."
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(984, 562)
		Me.Controls.Add(Me.startButton)
		Me.Controls.Add(Me.selectButton)
		Me.Controls.Add(Me.createButton)
		Me.Controls.Add(Me.editButton)
		Me.Controls.Add(Me.clearButton)
		Me.Controls.Add(Me.stopButton)
		Me.DoubleBuffered = True
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Bonk Racing"
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents renderTimer As System.Windows.Forms.Timer
	Private WithEvents startButton As System.Windows.Forms.Button
	Private WithEvents stopButton As System.Windows.Forms.Button
	Private WithEvents clearButton As System.Windows.Forms.Button
	Private WithEvents createButton As System.Windows.Forms.Button
	Private WithEvents selectButton As System.Windows.Forms.Button
	Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents ImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents BrushToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents editButton As System.Windows.Forms.Button
	Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
