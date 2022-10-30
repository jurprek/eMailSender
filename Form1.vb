Imports System.Net.Mail
Imports System.Drawing.Imaging
Imports System.IO.File

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim MyMailMessage As New MailMessage()

        Dim time As String
        ' While (1)

        time = DateTime.Now.ToString("dd.MM.yyyy.(HH:mm") + "h)"
        Dim imeSlike As String
            imeSlike = "ScreenShot" + ".jpg" '+ time
        'Do
        'If (DateTime.Now.Hour = 8 Or 9 Or 10 Or 11 Or 12 Or 13 Or 14 Or
        '15 Or 16 Or 17 Or 18 Or 19 Or 20 Or 21) And DateTime.Now.Minute = 51 Then 'vrijeme u koje ce slati email (sa attachment-om: screenshot-om trenutne radne povrsine)

        Using Bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
            Using G As Graphics = Graphics.FromImage(Bmp)
                G.CopyFromScreen(0, 0, 0, 0, Bmp.Size)
                Bmp.Save(imeSlike, ImageFormat.Jpeg) 'ime pod kojim sprema skreenshot
            End Using
        End Using

        Try
            MyMailMessage.From = New MailAddress("FROM@gmail.com") 'adresa s koje se salje email, mora imati ukljucene "manje sigurne aplikacije u postavkama gmaila
            MyMailMessage.To.Add("TO@gmail.com") 'adresa na koju salje email
            MyMailMessage.Subject = "something"
            MyMailMessage.Body = "something else" + time
            Dim SMTP As New SmtpClient("smtp.gmail.com") With {
                            .Port = 587,
                            .EnableSsl = True,
                            .Credentials = New System.Net.NetworkCredential("FROM@gmail.com", "passwordC0de") ' adresa s koje salje email i password
            }

            Dim attach As New Net.Mail.Attachment(imeSlike) 'ime attachmenta mora odgovarati imenu screenshota
            MyMailMessage.Attachments.Add(attach)


            SMTP.Send(MyMailMessage)
            'MessageBox.Show("EA: email sent...")

        Catch ex As Exception
            'MessageBox.Show(ex.ToString)
            'IO.File.Delete(imeSlike) 'ne radi, tj. ne briše file!
        End Try
        'End If
        'Threading.Thread.Sleep(60 * 1000) ' Sleep 1 minute and check again
        'Loop
        'End While
        System.Windows.Forms.Application.Exit() 'ovaj izađe iz programa u verziji bez petlji.... tj. kad pozivam iz ExpertAdvisoraradi otvaranja/zatvaranja pozicije
        'Close()
    End Sub

End Class