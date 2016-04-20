<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.btnRunFeed = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnRunFeed
        '
        Me.btnRunFeed.Location = New System.Drawing.Point(90, 107)
        Me.btnRunFeed.Name = "btnRunFeed"
        Me.btnRunFeed.Size = New System.Drawing.Size(195, 71)
        Me.btnRunFeed.TabIndex = 0
        Me.btnRunFeed.Text = "Run Betfair Feed"
        Me.btnRunFeed.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 299)
        Me.Controls.Add(Me.btnRunFeed)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Harness for BetfairFeedService"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnRunFeed As Button
End Class
