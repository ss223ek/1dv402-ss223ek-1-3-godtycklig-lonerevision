using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ss223ek_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                int numberOfSalaries;
                numberOfSalaries = ReadInt("Ange antal löner att mata in:");
                if (numberOfSalaries < 2)
                {
                    //sätt färg
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Du måste mata in minst två löner för att kunna göra en beräkning!");
                    // ta bort färg
                    Console.ResetColor();
                }
                // Nu har vi fått giltigt antal löner att mata in
                else
                {
                    ProcessSalaries(numberOfSalaries);
                }

                //sätt färg
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Tryck tangent för ny beräkning - Esc avslutar.");
                Console.ResetColor();
            }
            while (ConsoleKey.Escape != Console.ReadKey(true).Key);
        }


        // Generell inmatning. Ta emot en sträng, om denna inte går att tolka som positivt heltal visas det tydligt
        private static int ReadInt(string prompt)
        {
            int intNumber;
            string input;
            while (true)
            {
                Console.Write("{0} ", prompt);
                input = Console.ReadLine();
                try
                {
                    intNumber = int.Parse(input);   //här kan det bli fel med bokatäver eller för stora tal
                    if (intNumber < 0)
                    {
                        throw new Exception();      // här väljer jag att ta bort negativa tal
                    }
                    break;  // inget fel har inträffat, då går vi vidare
                }
                catch
                {
                    //sätt färg
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("FEL! '{0}' kan inte tolkas som ett positivt heltal", input);
                    // ta bort färg
                    Console.ResetColor();
                }
            }
            return intNumber;
        }

        private static void ProcessSalaries(int count)
        {
            int[] salaries = new int[count];
            int median, medium, spread;

            for (int i = 0; i < count; i++)                  //jobbar med index för att inte få en tom position i array
            {
                salaries[i] = ReadInt(String.Format("Ange lön nummer {0}:", (i + 1)));    //omvandlar till en sträng innan argument
            }

            //medel
            // det finns redan färdiga metoder (sid 609)
            medium = (int)salaries.Average();

            //Lönespridning
            // det finns max och min redan
            spread = (salaries.Max()) - (salaries.Min());

            //medianberäkning
            //sortera, använd heltalsdivision för att hitta det enda eller de två mittvärden
            // udda eller jämt avgörs med modulo2
            // eftersom lönernas ordning ska finnas kvar måste de kopieras innan sortering
            int[] salariesBackup = new int[count];
            Array.Copy(salaries, salariesBackup, count);
            Array.Sort(salaries);
            //om antallöner mod2 har rest är det udda antal löner
            if (0 != count % 2)
            {
                median = salaries[count / 2];
            }
            // annars är det jämt antal, dela cont på två och ta även det förgående 
            else
            {
                median = (int)(salaries[count / 2] + salaries[count / 2 - 1]) / 2;
            }

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Medianlön      :{0,12:c0}", median);
            Console.WriteLine("Medellön       :{0,12:c0}", medium);
            Console.WriteLine("Lönespridning  :{0,12:c0}", spread);
            Console.WriteLine("-------------------------------");

            for (int i = 0; i < count; i++)
            {
                Console.Write("{0,9}", salariesBackup[i]);
                if (2 == i % 3)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}
