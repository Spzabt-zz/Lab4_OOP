using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab4_OOP_PartTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            string fileName;
            string path = @"D:\Study\OOP_labs\FilesForLab4\Pictures";
            string pathForPicture = @"D:\Study\OOP_labs\FilesForLab4\Pictures\";

            string[] pictures = Directory.GetFiles(path);
            Regex regexForImage = new Regex("^((.bmp)|(.gif)|(.tiff?)|(.jpe?g)|(.png))$", RegexOptions.IgnoreCase);
            
            try
            {
                foreach (var picture in pictures)
                    if (Path.GetFileNameWithoutExtension(picture).Contains("-mirrored"))
                        throw new Exception($"Файли вже створені!");

                foreach (var picture in pictures)
                {
                    try
                    {
                        if (regexForImage.IsMatch(Path.GetExtension(picture)))
                        {
                            Bitmap bitmap = new Bitmap(picture);
                            bitmap.RotateFlip(RotateFlipType.Rotate180FlipY);
                            fileName = Path.GetFileNameWithoutExtension(picture);
                            bitmap.Save(pathForPicture + fileName + "-mirrored.gif");
                            bitmap.Dispose();
                            count++;
                        }
                        else
                            throw new Exception($"Файл [{Path.GetFileName(picture)}] не являється картинкою!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.WriteLine($"Змінено файлів - {count}.");
            Console.ReadLine();
        }
    }
}
