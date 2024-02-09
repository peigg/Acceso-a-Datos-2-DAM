using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace Ejercicio3
{
   
    public partial class MainWindow : Window
    {
        // Ruta del archivo de estructura que debemos cargar

        private string archivoEstructuraPath = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        // Evento para cargar archivo usando OpenFileDialog

        private void btnCargarArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                archivoEstructuraPath = openFileDialog.FileName;
                txtMensaje.Text = $"Archivo cargado: {archivoEstructuraPath}";
            }

        }

        // Evento para crear la estructura de directorios y archivos, usamos el archivo
        // cargado en el evento anterior
        private void btnCrearEstructura_Click(object sender, RoutedEventArgs e)
        {
            // Si no se ha cargado un archivo, mostramos mensaje y salimos
            if (string.IsNullOrEmpty(archivoEstructuraPath))
            {
                MessageBox.Show("Por favor, carga un archivo de estructura primero.");
                return;
            }
            
            
            /* Leemos el archivo de estructura y creamos la estructura de directorios
             y archivos usando las rutas que leemos del archivo
            */
            try
            {
                string[] lineas = File.ReadAllLines(archivoEstructuraPath);
                string directorioActual = "";

                // Recorremos las líneas del archivo usando un foreach
                foreach (var linea in lineas)
                {
                    // Si la línea no empieza con "----" es una carpeta, si no, es un archivo
                    if (!linea.StartsWith("----")) // Es una carpeta
                    {
                        directorioActual = linea.Replace(":", ":/");
                        CrearDirectorioSiNoExiste(directorioActual);
                    }
                    else // Es un archivo
                    {
                        string nombreArchivo = linea.Trim('-').Trim();
                        CrearArchivoSiNoExiste(Path.Combine(directorioActual, nombreArchivo));
                    }
                }
                txtMensaje.Text = "Estructura creada con éxito.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la estructura: {ex.Message}");
            }
        }

        // Método para crear directorio si no existe
        private void CrearDirectorioSiNoExiste(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        // Método para crear archivo si no existe
        private void CrearArchivoSiNoExiste(string path)
        {
            if (!File.Exists(path))
            {
                // Crear el archivo vacío
                using (var fs = File.Create(path)) { }
            }
        }
    }
}