using System.Drawing; // Namespace for working with images
using System.IO; // Namespace for file I/O operations
using System; // Basic system functionality

namespace Prog6221_Part1
{
    // This class is responsible for displaying an ASCII art version of the logo image.
    internal class Logo
    {
        // Constructor that handles loading and rendering the logo as ASCII art
        public Logo()
        {
            // Get the base directory of the project (i.e., where the application is running from)
            string path_project = AppDomain.CurrentDomain.BaseDirectory;

            // Adjust the path to point to the project directory (remove the 'bin\\Debug' part)
            string new_path_projectn = path_project.Replace("bin\\Debug", "");

            // Combine the project directory path with the logo image file name to get the full path
            string full_path = Path.Combine(new_path_projectn, "LOGO.jpg");

            // Load the logo image from the specified path
            Bitmap image = new Bitmap(full_path);

            // Resize the image to a smaller size (210x200) for better representation in the console
            image = new Bitmap(image, new Size(210, 200));

            // Loop through every pixel of the image to create an ASCII representation
            for (int height = 0; height < image.Height; height++) // Outer loop for height (rows of pixels)
            {
                for (int width = 0; width < image.Width; width++) // Inner loop for width (columns of pixels)
                {
                    // Get the color of the pixel at the current width and height
                    Color pixelColor = image.GetPixel(width, height);

                    // Calculate the average color value (grayscale equivalent) by averaging the RGB values
                    int color = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    // Convert the grayscale value to an ASCII character based on its intensity
                    // Darker colors will be represented by more complex characters, lighter colors by simpler ones
                    char ascii_design = color > 200 ? '.' :
                                        color > 150 ? '*' :
                                        color > 100 ? '0' :
                                        color > 50 ? '#' : '@';

                    // Print the ASCII character to the console to form the "image"
                    Console.Write(ascii_design);
                }

                // Move to the next line to continue rendering the next row of pixels
                Console.WriteLine();
            } // End of the loops for rendering the image
        }
    }
}
