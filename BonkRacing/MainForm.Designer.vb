<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.ImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.BrushToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.CreateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.MoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.ResizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
		Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editButton = New System.Windows.Forms.Button
		Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
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
		Me.startButton.Location = New System.Drawing.Point(434, 8)
		Me.startButton.Name = "startButton"
		Me.startButton.Size = New System.Drawing.Size(75, 23)
		Me.startButton.TabIndex = 0
		Me.startButton.Text = "Start"
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImageToolStripMenuItem, Me.BrushToolStripMenuItem, Me.PropertiesToolStripMenuItem, Me.CreateToolStripMenuItem, Me.MoveToolStripMenuItem, Me.ResizeToolStripMenuItem, Me.ToolStripMenuItem3, Me.ToolStripMenuItem2, Me.LoadToolStripMenuItem, Me.SaveToolStripMenuItem, Me.ClearToolStripMenuItem})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 252)
		'
		'ImageToolStripMenuItem
		'
		Me.ImageToolStripMenuItem.Name = "ImageToolStripMenuItem"
		Me.ImageToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.ImageToolStripMenuItem.Text = "Image..."
		'
		'BrushToolStripMenuItem
		'
		Me.BrushToolStripMenuItem.Name = "BrushToolStripMenuItem"
		Me.BrushToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.BrushToolStripMenuItem.Text = "Brush..."
		'
		'PropertiesToolStripMenuItem
		'
		Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
		Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.PropertiesToolStripMenuItem.Text = "Properties..."
		'
		'CreateToolStripMenuItem
		'
		Me.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem"
		Me.CreateToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.CreateToolStripMenuItem.Text = "Create"
		'
		'MoveToolStripMenuItem
		'
		Me.MoveToolStripMenuItem.Name = "MoveToolStripMenuItem"
		Me.MoveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.MoveToolStripMenuItem.Text = "Move"
		'
		'ResizeToolStripMenuItem
		'
		Me.ResizeToolStripMenuItem.Name = "ResizeToolStripMenuItem"
		Me.ResizeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.ResizeToolStripMenuItem.Text = "Resize"
		'
		'ToolStripMenuItem2
		'
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 6)
		'
		'LoadToolStripMenuItem
		'
		Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
		Me.LoadToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.LoadToolStripMenuItem.Text = "Load..."
		'
		'SaveToolStripMenuItem
		'
		Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
		Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.SaveToolStripMenuItem.Text = "Save..."
		'
		'ClearToolStripMenuItem
		'
		Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
		Me.ClearToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.ClearToolStripMenuItem.Text = "Clear..."
		'
		'editButton
		'
		Me.editButton.ContextMenuStrip = Me.ContextMenuStrip1
		Me.editButton.Location = New System.Drawing.Point(515, 8)
		Me.editButton.Name = "editButton"
		Me.editButton.Size = New System.Drawing.Size(75, 23)
		Me.editButton.TabIndex = 0
		Me.editButton.Text = "Edit"
		'
		'ToolStripMenuItem3
		'
		Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
		Me.ToolStripMenuItem3.Size = New System.Drawing.Size(152, 22)
		Me.ToolStripMenuItem3.Text = "Adjust texture"
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1024, 600)
		Me.Controls.Add(Me.startButton)
		Me.Controls.Add(Me.editButton)
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
	Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents ImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents BrushToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents editButton As System.Windows.Forms.Button
	Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents CreateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents MoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ResizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents LoadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ClearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem

End Class
