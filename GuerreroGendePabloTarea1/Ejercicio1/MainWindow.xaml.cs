using System;
using System.IO;
using System.Timers;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Windows.Controls;

namespace GuerreroGendePabloTarea01
{
    public partial class MainWindow : Window
    {
        // Timer that will be used to show the modify window after 10 seconds
        private static System.Timers.Timer timer;
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
         * When the user clicks the "Save" button, the content of the TextBox is saved in a file called "MyFile.txt" in the project directory.
        */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // gets the content of the TextBox
            string content = txtContent.Text;

            // verifies if the TextBox has content
            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("The textbox is empty, nothing will be saved.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // gets the project directory path
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;

                // combines the project directory path with the name of the file you want to create
                string fileName = "MyFile.txt";
                string filePath = Path.Combine(projectPath, fileName);

                // saves the content of the TextBox in the file created in the project folder
                File.WriteAllText(filePath, content);

                MessageBox.Show("The file wass succesfuly saved in the route GuerreroGendePabloTarea01\\bin\\Debug\\net7.0-windows.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // hides the save canvas and shows the read canvas

                canvasSave.Visibility = Visibility.Hidden;
                canvasSave.IsEnabled = false;

                canvasRead.IsEnabled = true;
                canvasRead.Visibility = Visibility.Visible;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saving Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // gets the project directory path
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;

                // combines the project directory path with the name of the file you want to create
                string fileName = "MyFile.txt";
                string filePath = Path.Combine(projectPath, fileName);

                // verifies if the file exists
                if (File.Exists(filePath))
                {
                    // reads the content of the file
                    string content = File.ReadAllText(filePath);
                    lblRead.Content = content;
                    txtModify.Text = content;

                    MessageBox.Show("When you close this message you will have 10 seconds to read it, after that the program will show the modify window.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    timer = new System.Timers.Timer(10000);
                    timer.Elapsed += TimerElapsed;
                    timer.AutoReset = false; // So the timer only runs once
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Couldn´t find the file.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File reading error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            
            
          
            string content = txtModify.Text;


            // verifies if the TextBox has content
            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("The textbox is empty, nothing will be saved.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // obtains the project directory path
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;

                // combines the project directory path with the name of the file you want to create
                string fileName = "MyFile.txt";
                string filePath = Path.Combine(projectPath, fileName);

                // saves the content of the TextBox in the file created in the project folder
                File.WriteAllText(filePath, content);

                MessageBox.Show("The file wass succesfuly modified.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                

              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Saving Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        // Method that will be called when the timer ends
        private void TimerElapsed(Object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
               
                canvasRead.IsEnabled = false;
                canvasRead.Visibility = Visibility.Collapsed;

                canvasModify.IsEnabled = true;
                canvasModify.Visibility = Visibility.Visible;
            });
        }
    }
}
