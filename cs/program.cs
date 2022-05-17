using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utf82Cp1212
{
    class Program
    {
        static int debugCount = 0;

        static List<int> reported = new List<int>();

        static int Main(string[] args)
        {
            bool skip = false;
            string infile, outfile;
            bool warned = false;
            switch (args.Length)
            {
                case 0:
                {
                    Console.WriteLine("No input file specified...");
                    return -1;
                }
                case 1:
                {
                    infile = GetInFile(args);
                    if (infile == null)
                    {
                        return -2;
                    }
                    outfile = BuildOutFileName(infile);
                    OutputFilenames(infile, outfile);
                    break;
                }
                case 2:
                {
                    infile = GetInFile(args);
                    if (infile == null)
                    {
                        return -2;
                    }
                    if (args[1] == "/s")
                    {
                        skip = true;
                        Console.WriteLine("Skip 1 row active (/s)");
                        outfile = BuildOutFileName(infile);
                    }
                    else
                    {
                        outfile = args[1];
                    }
                    OutputFilenames(infile, outfile);
                    break;
                }
                case 3:
                {
                    infile = GetInFile(args);
                    if (infile == null)
                    {
                        return -2;
                    }
                    outfile = args[1];
                    if (args[2] == "/s")
                    {
                        skip = true;
                        Console.WriteLine("Skip 1 row active (/s)");
                    }
                    else
                    {
                        Console.WriteLine("Invalid argument 3: {0}", args[2]);
                        return -3;
                    }
                    OutputFilenames(infile, outfile);
                    break;
                }
                default:
                {
                    Console.WriteLine("To much arguments specified: {0}", args.Length);
                    return -3;
                }
            }

            FileInfo fi = new FileInfo(infile);
            long bytesRead = 0;
            int lastPercent = 0;
            int line = 0;
            int? length = null;
            using (StreamReader sr = new StreamReader(infile, Encoding.UTF8))
            {
                using (StreamWriter sw = new StreamWriter(outfile, false, Encoding.GetEncoding(1252)))
                {
                    if (skip)
                    {
                        if (!sr.EndOfStream)
                        {
                            string skipline = sr.ReadLine();
                            Console.WriteLine("Skipping line: '{0}'", skipline);
                        }
                    }
                    Console.Write("00%");
                    while (!sr.EndOfStream)
                    {
                        string str = sr.ReadLine();
                        if (ConvertString(str, out string conv, true))
                        {
                            if (length == null)
                            {
                                length = conv.Length;
                                Console.WriteLine("Setting line length to {0}", length);
                            }
                            else
                            {
                                if (conv.Length != length)
                                {
                                    if (conv.Length == length + 2 && conv.StartsWith("\"") && conv.EndsWith("\""))
                                    {
                                      conv = conv.Substring(1, conv.Length - 2);   
                                    }
                                    else
                                    {
                                        if (!warned)
                                        {
                                            Console.WriteLine("At least 1 line difference... [{0}] L={1} instead of {2}", line, str.Length, length);
                                            warned = true;
                                        }
                                    }
                                }
                            }
                            bytesRead += str.Length;
                            int newPercent = (int)((bytesRead * 100) / fi.Length);
                            if (newPercent > lastPercent)
                            {
                                Console.Write("\b\b\b{0:00}%", newPercent);
                                lastPercent = newPercent;
                            }
                            sw.WriteLine(conv);
                        }
                        else
                        {
                            Console.WriteLine("Conversion problem: {0}", conv);
                            // Console.ReadLine();
                        }
                        line++;
                    }
                    sw.Close();
                }
                sr.Close();
            }

            Console.WriteLine("ok");
            //Console.ReadLine();
            return 0;
        }

        private static string GetInFile(string[] args)
        {
            string infile;
            infile = args[0];
            if (!File.Exists(infile))
            {
                Console.WriteLine("Input file does not exist: {0}", infile);
                return null;
            }
            return infile;
        }

        private static void OutputFilenames(string infile, string outfile)
        {
            Console.WriteLine("in file is {0}", infile);
            Console.WriteLine("outfile is {0}", outfile);
        }

        private static string BuildOutFileName(string infile)
        {
            string outfile;
            outfile = Path.Combine(Path.GetDirectoryName(infile),
                Path.GetFileNameWithoutExtension(infile) + "_out" + Path.GetExtension(infile));
            return outfile;
        }

        private static bool ConvertString(string readString, out string convertedString, bool processWhitelist)
        {
            if (!processWhitelist)
            {
                convertedString = readString;
                return true;
            }
            else
            {
                // white list conversion
                StringBuilder sb = new StringBuilder();
                foreach (char c in readString)
                {
                    sb.Append(ConvertChar(c));
                }
                convertedString = sb.ToString();
                return true;
            }
        }

        private static char ConvertChar(char inChar)
        {
            if (inChar >= 'a' && inChar <= 'z')
            {
                return inChar;
            }
            if (inChar >= 'A' && inChar <= 'Z')
            {
                return inChar;
            }
            if (inChar >= '1' && inChar <= '9')
            {
                return inChar;
            }
            switch (inChar)
            {
                case 'Ê': // 202 
                case '£': // 163 
                case '@': // 64 
                case 'È': // 200
                case '¬': // 172
                case '\'': // 39
                case ',': // 44
                case '=': // 61
                case '<': // 60
                case '>': // 62

                case (char)8:   //8
                case (char)11:  //11 
                case '}':  //125
                case '~':  //126
                case 'Á':  //193
                case 'Ç':  //199


                case '0': case '?': case '°':
                case ' ': case '*': case '^':
                case 'ä': case 'ü': case 'ö':
                case 'Ä': case 'Ü': case 'Ö':
                case 'Ã':
                case '.': case '+': case '"':
                case 'é': case 'è': case 'ê': case 'ë':
                case 'à': case 'á': case 'â': case 'ã':
                case 'ù': case 'û': case 'ú':
                case 'Â': case 'À':
                case 'ô': case 'ò':  case 'ó': case 'õ':
                case '`': case '-': case '/': case ';': case '_':
                case '´': case '§': case '¨':
                case '(': case ')':
                case 'ï': case 'î': case 'ì': case 'í':
                case 'ç': case 'ñ': case '\\':
                case 'ø':
                case 'ý':
                case 'É':
                case '#':
                case '&':
                case '%':
                case ':':
                case '!':
                case 'Ë':
                case 'Ô':
                case '|':
                case (char)223:
                case 'Ï':
                {
                    return inChar;
                }
                case '\t':
                {
                    return ' ';
                }
                case '¶':
                {
                    return ' ';
                }
                case (char)26:
                {
                    return ' ';
                }
                case (char)188:
                    {
                        return ' ';
                    }
                case (char)65533:
                {
                    debugCount++;
                    return ' ';
                }
                case (char)160:
                    {
                        return ' ';
                    }
                default:
                {
                    if (!reported.Contains((int)inChar))
                    {
                        Console.WriteLine("ERR: cannot translate {0}", (int)inChar);
                        reported.Add((int)inChar);
                    }
                    return ' ';
                }
            }
        }
    }
}
