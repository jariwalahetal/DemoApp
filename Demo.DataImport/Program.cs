using Demo.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting data import");
            foreach (string file in Directory.EnumerateFiles(@"..\Debug\Data", "*.csv"))
            {
                Console.WriteLine("Processing file:" + file);
                System.IO.StreamReader actualFile = new System.IO.StreamReader(file);
                int counter = 0;
                string line;
                while ((line = actualFile.ReadLine()) != null)
                {
                    if(counter != 0)
                    {
                        var elements = line.Split(',');
                        var card = new Card()
                        {
                            UnitId = elements[0],
                            OpeId = elements[1],
                            OpeId6 = elements[2],
                            InstNM = elements[3],
                            City = elements[4],
                            State = elements[5],
                            Pincode = elements[6]
                        };
                        Task.Delay(5000);
                        Console.WriteLine("Adding data:" + card.UnitId);
                        CardContext.AddCard(card);
                        
                    }
                     counter++;
                }

                actualFile.Close();
            }
            //for testing only
            Console.WriteLine("End data import");
            Console.ReadLine();
        }
    }
}
