internal class Program
{
    private static void Main(string[] args)
    {
        /*
        // Declaramos un flujo de escritura para el archivo de texto
        StreamWriter file = new StreamWriter("Log_Usuario.txt");

        // Bucle infinito que se repetirá hasta que el usuario introduzca la frase "END"
        while (true)
        {
            // Pedimos al usuario que introduzca una frase
            Console.WriteLine("Introduzca una frase:");
            string frase = Console.ReadLine();

            // Si la frase es "END", terminamos el bucle
            if (frase == "END")
                break;

            // Escribimos la frase en el archivo de texto
            file.WriteLine(frase);
        }
        // Cerramos el flujo de escritura
        file.Close();
        */

        StreamReader fichero = new StreamReader("Log_Usuario.txt");
        for (int i = 0; i < 3; i++)
        {
            
            Console.WriteLine(fichero.ReadLine());

        }
        fichero.Close();

    }
}