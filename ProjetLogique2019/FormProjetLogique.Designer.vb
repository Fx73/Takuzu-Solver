<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProjetLogique2019
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProjetLogique2019))
        Me.LabelBienvenue = New System.Windows.Forms.Label()
        Me.ButtonPreRap = New System.Windows.Forms.Button()
        Me.Buttonregles = New System.Windows.Forms.Button()
        Me.ButtonRegleConj = New System.Windows.Forms.Button()
        Me.ButtonTakuzuCreate = New System.Windows.Forms.Button()
        Me.Buttoncreerconjonctive = New System.Windows.Forms.Button()
        Me.Buttondimacs = New System.Windows.Forms.Button()
        Me.Button3sat = New System.Windows.Forms.Button()
        Me.ButtonDelAll = New System.Windows.Forms.Button()
        Me.ButtonQuit = New System.Windows.Forms.Button()
        Me.TextBoxMain = New System.Windows.Forms.TextBox()
        Me.ButtonSatSolveOnline = New System.Windows.Forms.Button()
        Me.ButtonSatSolvePerso = New System.Windows.Forms.Button()
        Me.ButtonCompleteTakuzu = New System.Windows.Forms.Button()
        Me.ButtonOCo = New System.Windows.Forms.Button()
        Me.ButtonODi = New System.Windows.Forms.Button()
        Me.ButtonO3s = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckReglesMix = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'LabelBienvenue
        '
        resources.ApplyResources(Me.LabelBienvenue, "LabelBienvenue")
        Me.LabelBienvenue.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.LabelBienvenue.Name = "LabelBienvenue"
        '
        'ButtonPreRap
        '
        resources.ApplyResources(Me.ButtonPreRap, "ButtonPreRap")
        Me.ButtonPreRap.Name = "ButtonPreRap"
        Me.ButtonPreRap.UseVisualStyleBackColor = True
        '
        'Buttonregles
        '
        resources.ApplyResources(Me.Buttonregles, "Buttonregles")
        Me.Buttonregles.Name = "Buttonregles"
        Me.Buttonregles.UseVisualStyleBackColor = True
        '
        'ButtonRegleConj
        '
        resources.ApplyResources(Me.ButtonRegleConj, "ButtonRegleConj")
        Me.ButtonRegleConj.Name = "ButtonRegleConj"
        Me.ButtonRegleConj.UseVisualStyleBackColor = True
        '
        'ButtonTakuzuCreate
        '
        resources.ApplyResources(Me.ButtonTakuzuCreate, "ButtonTakuzuCreate")
        Me.ButtonTakuzuCreate.ForeColor = System.Drawing.Color.Brown
        Me.ButtonTakuzuCreate.Name = "ButtonTakuzuCreate"
        Me.ButtonTakuzuCreate.UseVisualStyleBackColor = True
        '
        'Buttoncreerconjonctive
        '
        resources.ApplyResources(Me.Buttoncreerconjonctive, "Buttoncreerconjonctive")
        Me.Buttoncreerconjonctive.ForeColor = System.Drawing.Color.Brown
        Me.Buttoncreerconjonctive.Name = "Buttoncreerconjonctive"
        Me.Buttoncreerconjonctive.UseVisualStyleBackColor = True
        '
        'Buttondimacs
        '
        resources.ApplyResources(Me.Buttondimacs, "Buttondimacs")
        Me.Buttondimacs.ForeColor = System.Drawing.Color.Brown
        Me.Buttondimacs.Name = "Buttondimacs"
        Me.Buttondimacs.UseVisualStyleBackColor = True
        '
        'Button3sat
        '
        Me.Button3sat.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.Button3sat, "Button3sat")
        Me.Button3sat.ForeColor = System.Drawing.Color.Brown
        Me.Button3sat.Name = "Button3sat"
        Me.Button3sat.UseVisualStyleBackColor = False
        '
        'ButtonDelAll
        '
        resources.ApplyResources(Me.ButtonDelAll, "ButtonDelAll")
        Me.ButtonDelAll.BackColor = System.Drawing.Color.Transparent
        Me.ButtonDelAll.Name = "ButtonDelAll"
        Me.ButtonDelAll.UseVisualStyleBackColor = False
        '
        'ButtonQuit
        '
        resources.ApplyResources(Me.ButtonQuit, "ButtonQuit")
        Me.ButtonQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonQuit.Name = "ButtonQuit"
        Me.ButtonQuit.UseVisualStyleBackColor = True
        '
        'TextBoxMain
        '
        resources.ApplyResources(Me.TextBoxMain, "TextBoxMain")
        Me.TextBoxMain.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TextBoxMain.Name = "TextBoxMain"
        Me.TextBoxMain.ReadOnly = True
        '
        'ButtonSatSolveOnline
        '
        Me.ButtonSatSolveOnline.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.ButtonSatSolveOnline, "ButtonSatSolveOnline")
        Me.ButtonSatSolveOnline.ForeColor = System.Drawing.Color.Brown
        Me.ButtonSatSolveOnline.Name = "ButtonSatSolveOnline"
        Me.ButtonSatSolveOnline.UseVisualStyleBackColor = False
        '
        'ButtonSatSolvePerso
        '
        Me.ButtonSatSolvePerso.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.ButtonSatSolvePerso, "ButtonSatSolvePerso")
        Me.ButtonSatSolvePerso.ForeColor = System.Drawing.Color.Brown
        Me.ButtonSatSolvePerso.Name = "ButtonSatSolvePerso"
        Me.ButtonSatSolvePerso.UseVisualStyleBackColor = False
        '
        'ButtonCompleteTakuzu
        '
        Me.ButtonCompleteTakuzu.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.ButtonCompleteTakuzu, "ButtonCompleteTakuzu")
        Me.ButtonCompleteTakuzu.ForeColor = System.Drawing.Color.Brown
        Me.ButtonCompleteTakuzu.Name = "ButtonCompleteTakuzu"
        Me.ButtonCompleteTakuzu.UseVisualStyleBackColor = False
        '
        'ButtonOCo
        '
        resources.ApplyResources(Me.ButtonOCo, "ButtonOCo")
        Me.ButtonOCo.ForeColor = System.Drawing.Color.Brown
        Me.ButtonOCo.Name = "ButtonOCo"
        Me.ButtonOCo.UseVisualStyleBackColor = True
        '
        'ButtonODi
        '
        resources.ApplyResources(Me.ButtonODi, "ButtonODi")
        Me.ButtonODi.ForeColor = System.Drawing.Color.Brown
        Me.ButtonODi.Name = "ButtonODi"
        Me.ButtonODi.UseVisualStyleBackColor = True
        '
        'ButtonO3s
        '
        resources.ApplyResources(Me.ButtonO3s, "ButtonO3s")
        Me.ButtonO3s.ForeColor = System.Drawing.Color.Brown
        Me.ButtonO3s.Name = "ButtonO3s"
        Me.ButtonO3s.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'CheckReglesMix
        '
        Me.CheckReglesMix.Checked = True
        Me.CheckReglesMix.CheckState = System.Windows.Forms.CheckState.Checked
        resources.ApplyResources(Me.CheckReglesMix, "CheckReglesMix")
        Me.CheckReglesMix.Name = "CheckReglesMix"
        Me.CheckReglesMix.UseVisualStyleBackColor = True
        '
        'ProjetLogique2019
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonQuit
        Me.Controls.Add(Me.CheckReglesMix)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonO3s)
        Me.Controls.Add(Me.ButtonODi)
        Me.Controls.Add(Me.ButtonOCo)
        Me.Controls.Add(Me.ButtonCompleteTakuzu)
        Me.Controls.Add(Me.ButtonSatSolvePerso)
        Me.Controls.Add(Me.ButtonSatSolveOnline)
        Me.Controls.Add(Me.TextBoxMain)
        Me.Controls.Add(Me.ButtonQuit)
        Me.Controls.Add(Me.ButtonDelAll)
        Me.Controls.Add(Me.Button3sat)
        Me.Controls.Add(Me.Buttondimacs)
        Me.Controls.Add(Me.Buttoncreerconjonctive)
        Me.Controls.Add(Me.ButtonTakuzuCreate)
        Me.Controls.Add(Me.ButtonRegleConj)
        Me.Controls.Add(Me.Buttonregles)
        Me.Controls.Add(Me.ButtonPreRap)
        Me.Controls.Add(Me.LabelBienvenue)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "ProjetLogique2019"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelBienvenue As Label
    Friend WithEvents ButtonPreRap As Button
    Friend WithEvents Buttonregles As Button
    Friend WithEvents ButtonRegleConj As Button
    Friend WithEvents ButtonTakuzuCreate As Button
    Friend WithEvents Buttoncreerconjonctive As Button
    Friend WithEvents Buttondimacs As Button
    Friend WithEvents Button3sat As Button
    Friend WithEvents ButtonDelAll As Button
    Friend WithEvents ButtonQuit As Button
    Friend WithEvents TextBoxMain As TextBox
    Friend WithEvents ButtonSatSolveOnline As Button
    Friend WithEvents ButtonSatSolvePerso As Button
    Friend WithEvents ButtonCompleteTakuzu As Button
    Friend WithEvents ButtonOCo As Button
    Friend WithEvents ButtonODi As Button
    Friend WithEvents ButtonO3s As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents CheckReglesMix As CheckBox
End Class
