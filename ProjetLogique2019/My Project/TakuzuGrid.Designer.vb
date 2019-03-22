<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TakuzuGrid
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ComboBoxN = New System.Windows.Forms.ComboBox()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'ComboBoxN
        '
        Me.ComboBoxN.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBoxN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ComboBoxN.DisplayMember = "2"
        Me.ComboBoxN.FormattingEnabled = True
        Me.ComboBoxN.Items.AddRange(New Object() {"2x2", "4x4", "6x6", "8x8", "10x10", "12x12", "14x14", "16x16"})
        Me.ComboBoxN.Location = New System.Drawing.Point(33, 3)
        Me.ComboBoxN.MaxLength = 5
        Me.ComboBoxN.Name = "ComboBoxN"
        Me.ComboBoxN.Size = New System.Drawing.Size(75, 21)
        Me.ComboBoxN.TabIndex = 12
        Me.ComboBoxN.Text = "10x10"
        '
        'ButtonOk
        '
        Me.ButtonOk.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonOk.Location = New System.Drawing.Point(0, 147)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(10)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(134, 31)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Ok"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'PanelButtons
        '
        Me.PanelButtons.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelButtons.AutoSize = True
        Me.PanelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.PanelButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PanelButtons.Location = New System.Drawing.Point(7, 26)
        Me.PanelButtons.MinimumSize = New System.Drawing.Size(120, 120)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(120, 120)
        Me.PanelButtons.TabIndex = 14
        '
        'TakuzuGrid
        '
        Me.AcceptButton = Me.ButtonOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(134, 178)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.ComboBoxN)
        Me.MinimizeBox = False
        Me.Name = "TakuzuGrid"
        Me.ShowInTaskbar = False
        Me.Text = "TakuzuGrid"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBoxN As ComboBox
    Friend WithEvents ButtonOk As Button
    Friend WithEvents PanelButtons As Panel
End Class
