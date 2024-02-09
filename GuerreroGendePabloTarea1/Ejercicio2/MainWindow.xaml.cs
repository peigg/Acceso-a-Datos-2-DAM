using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Tarea02
{
   
    public partial class MainWindow : Window
    {
        // variable para la ruta del archivo seleccionado

        private string archivoOrigenPath = "";


        public MainWindow()
        {
            InitializeComponent();
        }

        // Evento para seleccionar un archivo de nuestro sistema usando OpenFileDialog
        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                archivoOrigenPath = openFileDialog.FileName;
                MessageBox.Show("Archivo seleccionado: " + archivoOrigenPath);
            }
        }

        /* Evento para copiar el archivo seleccionado a una nueva ubicación con el 
         * nombre de archivo original + "_Copia"
         * Usamos SaveFileDialog para seleccionar la ubicación y el nombre del archivo
         * Usamos StreamReader para leer los caracteres del archivo y StreamWriter para escribirlos
         * Usamos el método peek para saber si el archivo tiene más caracteres y así ir copiando
         */
        private void btnCopiar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(archivoOrigenPath))
            {
                MessageBox.Show("Por favor, selecciona un archivo de origen primero.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(archivoOrigenPath) + "_Copia" + Path.GetExtension(archivoOrigenPath);
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(archivoOrigenPath))
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            while (reader.Peek() >= 0)
                            {
                                char c = (char)reader.Read();
                                writer.Write(c);
                            }
                        }
                    }
                    MessageBox.Show("Archivo copiado con éxito.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al copiar el archivo: {ex.Message}");
                }
            }
        }
    }
}