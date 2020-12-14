using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            int firstNum;
            int secondNum;
            int resMulti;
            string firstLine;
            string secondLine;
            string firstPath = @"D:\Study\OOP_labs\FilesForLab4\TXT_Files";
            string secondPath = @"D:\Study\OOP_labs\FilesForLab4\TXT_Files\";
            string noFilePath = @"D:\Study\OOP_labs\FilesForLab4\TXT_Files\no_file.txt";
            string badDataPath = @"D:\Study\OOP_labs\FilesForLab4\TXT_Files\bad_data.txt";
            string overflowPath = @"D:\Study\OOP_labs\FilesForLab4\TXT_Files\overflow.txt";

            DirectoryInfo directoryInfo = new DirectoryInfo(firstPath);
            int fileCount = directoryInfo.GetFiles().Length;

            try
            {
                if (File.Exists(noFilePath) || File.Exists(badDataPath) || File.Exists(overflowPath))
                    throw new Exception("Файли помилок уже створені!");

                for (int i = 10; i < fileCount + 12; i++)
                {
                    try
                    {
                        StreamReader streamReader = new StreamReader(secondPath + i + ".txt");

                        firstLine = streamReader.ReadLine();
                        firstNum = int.Parse(firstLine);
                        secondLine = streamReader.ReadLine();
                        secondNum = int.Parse(secondLine);
                        resMulti = firstNum * secondNum;
                        sum = Calculation(resMulti, sum, i);

                        streamReader.Close();
                    }
                    catch (FileNotFoundException)
                    {
                        FileStream file = new FileStream(noFilePath, FileMode.Append);
                        StreamWriter stream = new StreamWriter(file);
                        stream.WriteLine($"Файл {i}.txt - відсутній.");

                        stream.Close();
                        file.Close();
                    }
                    catch (FormatException)
                    {
                        FileStream file = new FileStream(badDataPath, FileMode.Append);
                        StreamWriter stream = new StreamWriter(file);
                        stream.WriteLine($"Файл {i}.txt - пошкоджений.");

                        stream.Close();
                        file.Close();
                    }
                    catch (ArgumentNullException)
                    {
                        using (StreamWriter stream = new StreamWriter(badDataPath, true))
                            stream.WriteLine($"Файл {i}.txt - пошкоджений.");
                    }
                    catch (OverflowException)
                    {
                        FileStream file = new FileStream(overflowPath, FileMode.Append);
                        StreamWriter stream = new StreamWriter(file);
                        stream.WriteLine($"Файл {i}.txt - числа в файлі не поміщаються в тип данних Int.");

                        stream.Close();
                        file.Close();
                    }
                }

                Console.WriteLine($"\nСума добутків - {sum}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.ReadLine();
        }

        static int Calculation(int res, int sum, int iter)
        {
            if (res > 0)
            {
                Console.WriteLine($"Добуток у файлі {iter}.txt - {res}");
                sum += res;
            }

            return sum;
        }
    }
}