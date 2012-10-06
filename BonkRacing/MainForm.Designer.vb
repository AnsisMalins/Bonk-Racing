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
		Me.stopButton = New System.Windows.Forms.Button
		Me.clearButton = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'renderTimer
		'
		Me.renderTimer.Enabled = True
		Me.renderTimer.Interval = 20
		'
		'startButton
		'
		Me.startButton.Location = New System.Drawing.Point(12, 12)
		Me.startButton.Name = "startButton"
		Me.startButton.Size = New System.Drawing.Size(75, 23)
		Me.startButton.TabIndex = 0
		Me.startButton.Text = "Start"
		'
		'stopButton
		'
		Me.stopButton.Location = New System.Drawing.Point(93, 12)
		Me.stopButton.Name = "stopButton"
		Me.stopButton.Size = New System.Drawing.Size(75, 23)
		Me.stopButton.TabIndex = 0
		Me.stopButton.Text = "Stop"
		'
		'clearButton
		'
		Me.clearButton.Location = New System.Drawing.Point(174, 12)
		Me.clearButton.Name = "clearButton"
		Me.clearButton.Size = New System.Drawing.Size(75, 23)
		Me.clearButton.TabIndex = 0
		Me.clearButton.Text = "Clear"
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(984, 562)
		Me.Controls.Add(Me.clearButton)
		Me.Controls.Add(Me.stopButton)
		Me.Controls.Add(Me.startButton)
		Me.DoubleBuffered = True
		Me.Name = "MainForm"
		Me.ResumeLayout(False)

	End Sub
	Private WithEvents renderTimer As System.Windows.Forms.Timer
	Private WithEvents startButton As System.Windows.Forms.Button
	Private WithEvents stopButton As System.Windows.Forms.Button
	Private WithEvents clearButton As System.Windows.Forms.Button

End Class
