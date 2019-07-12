using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;



namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {/*
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-us"));
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.LoadGrammar(new DictationGrammar());
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_recognized);
            while (true)
            {
                Console.ReadKey();
            }*/

            // Pattern to match identifier
            string[] forloop ={ "keyword","opening round bracket","datatype","identifier","Equal","number","semi colon", "identifier", "relational operator","number","semi colon","identifier",
                "Increment","closing round bracket" };
            string[] intialization = { "datatype", "identifier", "Equal", "number", "semi colon" };

            string pattern = @"^[A-Z|a-z]+[0-9|A-Z|a-z]*";
            Regex reg2 = new Regex(pattern);
            int total = 0;
            string line = string.Empty;
            string inputval = string.Empty;
        label:
            SpeechSynthesizer reader = new SpeechSynthesizer();
            line = Console.ReadLine();

            reader.SpeakAsync(line);

            if (line != string.Empty)
            {
                inputval += line;
                goto label;
            }

            string[] tokentype = new string[10000];
            string[] previous = new string[1000];
            string[] final = inputval.Split(' ');
            int number;
            bool find = false;



            for (int i = 0; i < final.Length; i++)
            {
                find = false;
                dictionary dictionary = new dictionary();
                foreach (var input in dictionary.dictionary1)
                {
                    if (input.Key == final[i])
                    {
                        tokentype[i] = input.Value;
                        total++;
                        find = true;
                        Console.WriteLine("Token # {2} < {0}, {1} >", final[i], input.Value, total);
                    }
                }

                if (find != false)
                { continue; }
                if (int.TryParse(final[i], out number))
                {
                    tokentype[i] = "number";
                    total++;
                    Console.WriteLine("Token # {1} < {0}, number >", final[i], total);
                }
                else if (reg2.IsMatch(final[i]))
                {
                    tokentype[i] = "identifier";
                    total++;
                    Console.WriteLine("Token # {1} < {0}, identifier >", final[i], total);
                }
              
            }
            Console.WriteLine("Compiled Successfully---\n");
            Program p = new Program();
            p.StructureCheck(tokentype, forloop, intialization, final);
            Console.ReadKey();

        }

        void StructureCheck(string[] tokentype, string[] forloop, string[] intialization, string[] final)
        {
           
            bool ErrorCommand = false;
            int i = 0;
            int exist = 0;


            //initialization
            if (tokentype[0] == intialization[0])
            {
                ErrorCommand = true;
                for (i = 0; i < tokentype.Length; i++)
                {
                    if (tokentype[i] == "datatype")
                    {
                    newvar:
                        i++;

                        if (tokentype[i] == "identifier")
                        {
                            i++;

                            if (tokentype[i] == "Equal")
                            {
                                i++;

                                if (tokentype[i] == "number" || tokentype[i] == "String Literal")
                                {
                                    i++;

                                    if (tokentype[i] == "comma")
                                    {
                                        goto newvar;
                                    }


                                    if (tokentype[i] == "semi colon")
                                    {
                                        ErrorCommand = false;
                                        Console.WriteLine("Sytax is correct");
                                        break;
                                    }
                                }

                            }
                            if (tokentype[i] == "semi colon")
                            {
                                ErrorCommand = false;
                                Console.WriteLine("Sytax is correct");
                                break;
                            }
                        }

                        if (tokentype[i] == "semi colon")
                        {
                            ErrorCommand = false;
                            Console.WriteLine("Sytax is correct");
                            break;
                        }
                    }

                }
                if (ErrorCommand == true)
                {
                    Console.WriteLine("Syntax Error");
                }
            }
                forloop:
                    if (tokentype[0] == forloop[0])
                    {
                        for (i = 0; i < forloop.Length; i++)
                        {
                            exist = 0;
                            foreach (string x in tokentype)
                            {
                                if (forloop[i] == x)
                                {
                                    exist = 1;
                                }
                            }
                            if (exist == 0)
                            {
                                Console.WriteLine("{0} is missing", forloop[i]);
                            }
                            if (tokentype[i] != forloop[i])
                            {
                                ErrorCommand = true;
                            }

                        }
                        if (ErrorCommand == true)
                        {
                            Console.WriteLine("Error ");
                        }
                    }   

                }
            }
            }
    

