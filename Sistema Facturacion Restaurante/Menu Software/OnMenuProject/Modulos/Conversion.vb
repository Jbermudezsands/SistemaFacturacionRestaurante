Imports System.IO
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Drawing

Module Conversion
    Function ComprimirImagen(Imagen As Image, Ancho As Integer, Alto As Integer) As Image

        '--------------------------------------VERIFICO EL ANCHO Y ALTO PARA CAMBIARLO -------------------------------
        'Dim fs As FileStream = New FileStream(RutaOrigen, FileMode.Open, FileAccess.Read, FileShare.Read)
        'Dim LaImagen As System.Drawing.Image
        'LaImagen = System.Drawing.Image.FromStream(Imagen)

        Dim Ancho2 = Ancho, Alto2 = Alto


        'este calculo es para que la foto no pierda proporciones
        'alto = Math.Floor((100 / Imagen.Width) * Imagen.Height)
        Dim NuevoBitmap As Bitmap = New Bitmap(Ancho, Alto)
        Dim Graficos As Graphics = Graphics.FromImage(NuevoBitmap)
        Graficos.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        Graficos.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Graficos.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        Dim Rectangulo As Rectangle = New Rectangle(0, 0, Ancho2, Alto2)
        If Not Imagen Is Nothing Then
            Graficos.DrawImage(Imagen, Rectangulo)
        End If

        Return NuevoBitmap
    End Function
    Function CambiarTamañoImage(Imagen As Image) As Image
        Dim ancho = 200, alto As Integer = 200
        '--------------------------------------VERIFICO EL ANCHO Y ALTO PARA CAMBIARLO -------------------------------
        'Dim fs As FileStream = New FileStream(RutaOrigen, FileMode.Open, FileAccess.Read, FileShare.Read)
        'Dim LaImagen As System.Drawing.Image
        'LaImagen = System.Drawing.Image.FromStream(Imagen)



        'este calculo es para que la foto no pierda proporciones
        'alto = Math.Floor((100 / Imagen.Width) * Imagen.Height)
        Dim NuevoBitmap As Bitmap = New Bitmap(ancho, alto)
        Dim Graficos As Graphics = Graphics.FromImage(NuevoBitmap)
        Graficos.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        Graficos.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Graficos.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        Dim Rectangulo As Rectangle = New Rectangle(0, 0, ancho, alto)
        Graficos.DrawImage(Imagen, Rectangulo)

        Return NuevoBitmap
    End Function


    Function StringtoByte(ByVal Cadena As String) As Byte()

        Dim resultado As String = Cadena
        Dim x As Integer = 0
        Dim arreglo As Byte() = Nothing
        Dim arregloTexto()


        Try
            'enunciado = New SqlCommand("select foto from Imagenes where Id_Foto='" & identificacion & "'", conexiones)
            'respuesta = enunciado.ExecuteReader()

            'While respuesta.Read
            '    resultado = respuesta.Item("foto")
            'End While

            'Llena un arreglo de Texto con los datos de la consulta separados por coma"'
            arregloTexto = resultado.Split(",")

            'Redimenciona el tamaño del arreglo de bytes'
            ReDim arreglo(arregloTexto.Length - 1)

            'Recorre el arreglo para llenar el arreglo de Bytes con el arreglo de la consulta'

            For x = 0 To arregloTexto.Length - 1
                If arregloTexto(x).Equals("") = False Then
                    arreglo(x) = arregloTexto(x)
                End If
            Next


        Catch ex As Exception

        End Try

        Return arreglo
    End Function



    'Retorna un String apartir de un arreglo de Bytes. Concatena cada elemento en la variable salida para ser almacenada en la Base de Datos'
    Function bytesToString(ByVal arreglo As Byte()) As String
        Dim salida As String = ""
        Dim x As Integer = 0
        'MsgBox("Tamaño del arreglo: " + arreglo.Length.ToString)
        Try
            For x = 0 To arreglo.Length - 1
                salida += arreglo(x).ToString + ","
            Next
        Catch ex As Exception
            MsgBox("No lo convertio a String por: " + ex.ToString)
        End Try

        Return salida
    End Function

    'Funcion que convierte de Image a Byte()
    Public Function ImagenToBytes(ByVal Imagen As Image) As Byte()
        'si hay imagen
        Dim arreglo As Byte() = Nothing
        Try
            If Not Imagen Is Nothing Then
                'variable de datos binarios en stream(flujo)
                Dim Bin As New MemoryStream
                'convertir a bytes
                Imagen.Save(Bin, Imaging.ImageFormat.Jpeg)
                'retorna binario
                arreglo = Bin.GetBuffer
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MsgBox("No convirtio a bytes por: " + ex.ToString)
        End Try
        Return arreglo
    End Function

    '--- Los PNG transparentes guardados antes como JPEG quedaron con el fondo negro en la BD.
    '--- Si el borde de la imagen es casi todo negro, pinta de blanco el negro conectado al borde (el negro interno de la imagen se conserva).
    Function QuitarFondoNegro(ByVal Origen As Image) As Image
        If Origen Is Nothing Then Return Nothing
        Try
            Dim Ancho As Integer = Origen.Width
            Dim Alto As Integer = Origen.Height
            Dim Bmp As New Bitmap(Ancho, Alto, Imaging.PixelFormat.Format32bppArgb)
            Using G As Graphics = Graphics.FromImage(Bmp)
                G.Clear(Color.White)
                G.DrawImage(Origen, 0, 0, Ancho, Alto)
            End Using

            Dim Datos As Imaging.BitmapData = Bmp.LockBits(New Rectangle(0, 0, Ancho, Alto), Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppArgb)
            Dim Bytes(Math.Abs(Datos.Stride) * Alto - 1) As Byte
            System.Runtime.InteropServices.Marshal.Copy(Datos.Scan0, Bytes, 0, Bytes.Length)
            Dim Stride As Integer = Datos.Stride
            Const Umbral As Integer = 45

            Dim EsNegro As Func(Of Integer, Integer, Boolean) = Function(X As Integer, Y As Integer) As Boolean
                              Dim P As Integer = Y * Stride + X * 4
                              Return Bytes(P) <= Umbral AndAlso Bytes(P + 1) <= Umbral AndAlso Bytes(P + 2) <= Umbral
                          End Function

            'verifico que el borde sea mayormente negro
            Dim TotalBorde As Integer = 0, NegrosBorde As Integer = 0
            For X As Integer = 0 To Ancho - 1
                TotalBorde += 2
                If EsNegro(X, 0) Then NegrosBorde += 1
                If EsNegro(X, Alto - 1) Then NegrosBorde += 1
            Next
            For Y As Integer = 0 To Alto - 1
                TotalBorde += 2
                If EsNegro(0, Y) Then NegrosBorde += 1
                If EsNegro(Ancho - 1, Y) Then NegrosBorde += 1
            Next

            If NegrosBorde < TotalBorde * 0.9 Then
                Bmp.UnlockBits(Datos)
                Bmp.Dispose()
                Return Origen
            End If

            'relleno por inundacion desde el borde
            Dim Visitado(Ancho * Alto - 1) As Boolean
            Dim Pila As New Stack(Of Integer)
            For X As Integer = 0 To Ancho - 1
                Pila.Push(X)
                Pila.Push((Alto - 1) * Ancho + X)
            Next
            For Y As Integer = 0 To Alto - 1
                Pila.Push(Y * Ancho)
                Pila.Push(Y * Ancho + Ancho - 1)
            Next
            While Pila.Count > 0
                Dim Idx As Integer = Pila.Pop
                If Visitado(Idx) Then Continue While
                Visitado(Idx) = True
                Dim X As Integer = Idx Mod Ancho
                Dim Y As Integer = Idx \ Ancho
                If Not EsNegro(X, Y) Then Continue While
                Dim P As Integer = Y * Stride + X * 4
                Bytes(P) = 255 : Bytes(P + 1) = 255 : Bytes(P + 2) = 255
                If X > 0 Then Pila.Push(Idx - 1)
                If X < Ancho - 1 Then Pila.Push(Idx + 1)
                If Y > 0 Then Pila.Push(Idx - Ancho)
                If Y < Alto - 1 Then Pila.Push(Idx + Ancho)
            End While

            System.Runtime.InteropServices.Marshal.Copy(Bytes, 0, Datos.Scan0, Bytes.Length)
            Bmp.UnlockBits(Datos)
            Return Bmp
        Catch ex As Exception
            Return Origen
        End Try
    End Function

    'Funcion que convierte de Byte() a Image
    Public Function BytesToImagen(ByVal Imagen As Byte()) As Image
        Try
            'si hay imagen
            If Not Imagen Is Nothing Then
                'caturar array con memorystream hacia Bin
                Dim Bin As New MemoryStream(Imagen)
                'con el método FroStream de Image obtenemos imagen
                Dim Resultado As Image = Image.FromStream(Bin)
                'y la retornamos
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

End Module
