﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PropertiesForm
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
		Me.Label1 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.x = New System.Windows.Forms.TextBox
		Me.y = New System.Windows.Forms.TextBox
		Me.width = New System.Windows.Forms.TextBox
		Me.height = New System.Windows.Forms.TextBox
		Me.mass = New System.Windows.Forms.TextBox
		Me.restitution = New System.Windows.Forms.TextBox
		Me.locked = New System.Windows.Forms.CheckBox
		Me.solid = New System.Windows.Forms.CheckBox
		Me.Button1 = New System.Windows.Forms.Button
		Me.reloadButton = New System.Windows.Forms.Button
		Me.zorder = New System.Windows.Forms.TextBox
		Me.Label7 = New System.Windows.Forms.Label
		Me.tname = New System.Windows.Forms.TextBox
		Me.Label8 = New System.Windows.Forms.Label
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(55, 41)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(14, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "X"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(55, 67)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(14, 13)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "Y"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(34, 93)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(35, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Width"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(31, 119)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(38, 13)
		Me.Label4.TabIndex = 0
		Me.Label4.Text = "Height"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(37, 145)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(32, 13)
		Me.Label5.TabIndex = 0
		Me.Label5.Text = "Mass"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(12, 171)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(57, 13)
		Me.Label6.TabIndex = 0
		Me.Label6.Text = "Restitution"
		'
		'x
		'
		Me.x.Location = New System.Drawing.Point(75, 38)
		Me.x.Name = "x"
		Me.x.Size = New System.Drawing.Size(97, 20)
		Me.x.TabIndex = 1
		'
		'y
		'
		Me.y.Location = New System.Drawing.Point(75, 64)
		Me.y.Name = "y"
		Me.y.Size = New System.Drawing.Size(97, 20)
		Me.y.TabIndex = 2
		'
		'width
		'
		Me.width.Location = New System.Drawing.Point(75, 90)
		Me.width.Name = "width"
		Me.width.Size = New System.Drawing.Size(97, 20)
		Me.width.TabIndex = 3
		'
		'height
		'
		Me.height.Location = New System.Drawing.Point(75, 116)
		Me.height.Name = "height"
		Me.height.Size = New System.Drawing.Size(97, 20)
		Me.height.TabIndex = 4
		'
		'mass
		'
		Me.mass.Location = New System.Drawing.Point(75, 142)
		Me.mass.Name = "mass"
		Me.mass.Size = New System.Drawing.Size(97, 20)
		Me.mass.TabIndex = 5
		'
		'restitution
		'
		Me.restitution.Location = New System.Drawing.Point(75, 168)
		Me.restitution.Name = "restitution"
		Me.restitution.Size = New System.Drawing.Size(97, 20)
		Me.restitution.TabIndex = 6
		'
		'locked
		'
		Me.locked.AutoSize = True
		Me.locked.Location = New System.Drawing.Point(75, 220)
		Me.locked.Name = "locked"
		Me.locked.Size = New System.Drawing.Size(62, 17)
		Me.locked.TabIndex = 8
		Me.locked.Text = "Locked"
		Me.locked.UseVisualStyleBackColor = True
		'
		'solid
		'
		Me.solid.AutoSize = True
		Me.solid.Location = New System.Drawing.Point(75, 243)
		Me.solid.Name = "solid"
		Me.solid.Size = New System.Drawing.Size(49, 17)
		Me.solid.TabIndex = 9
		Me.solid.Text = "Solid"
		Me.solid.UseVisualStyleBackColor = True
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(97, 271)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(75, 23)
		Me.Button1.TabIndex = 10
		Me.Button1.Text = "&Apply"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'reloadButton
		'
		Me.reloadButton.Location = New System.Drawing.Point(16, 271)
		Me.reloadButton.Name = "reloadButton"
		Me.reloadButton.Size = New System.Drawing.Size(75, 23)
		Me.reloadButton.TabIndex = 11
		Me.reloadButton.Text = "&Reload"
		Me.reloadButton.UseVisualStyleBackColor = True
		'
		'zorder
		'
		Me.zorder.Location = New System.Drawing.Point(75, 194)
		Me.zorder.Name = "zorder"
		Me.zorder.Size = New System.Drawing.Size(97, 20)
		Me.zorder.TabIndex = 7
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(26, 197)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(43, 13)
		Me.Label7.TabIndex = 0
		Me.Label7.Text = "Z Order"
		'
		'tname
		'
		Me.tname.Location = New System.Drawing.Point(75, 12)
		Me.tname.Name = "tname"
		Me.tname.Size = New System.Drawing.Size(97, 20)
		Me.tname.TabIndex = 0
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(34, 15)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(35, 13)
		Me.Label8.TabIndex = 0
		Me.Label8.Text = "Name"
		'
		'PropertiesForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(184, 312)
		Me.Controls.Add(Me.reloadButton)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.solid)
		Me.Controls.Add(Me.locked)
		Me.Controls.Add(Me.zorder)
		Me.Controls.Add(Me.restitution)
		Me.Controls.Add(Me.mass)
		Me.Controls.Add(Me.height)
		Me.Controls.Add(Me.width)
		Me.Controls.Add(Me.y)
		Me.Controls.Add(Me.tname)
		Me.Controls.Add(Me.x)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.Label1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "PropertiesForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Properties"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents x As System.Windows.Forms.TextBox
	Friend WithEvents y As System.Windows.Forms.TextBox
	Friend WithEvents width As System.Windows.Forms.TextBox
	Friend WithEvents height As System.Windows.Forms.TextBox
	Friend WithEvents mass As System.Windows.Forms.TextBox
	Friend WithEvents restitution As System.Windows.Forms.TextBox
	Friend WithEvents locked As System.Windows.Forms.CheckBox
	Friend WithEvents solid As System.Windows.Forms.CheckBox
	Friend WithEvents reloadButton As System.Windows.Forms.Button
	Friend WithEvents zorder As System.Windows.Forms.TextBox
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents tname As System.Windows.Forms.TextBox
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Private WithEvents Button1 As System.Windows.Forms.Button
End Class
