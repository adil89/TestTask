using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;



namespace testTask
{
    public class Program
    {

        functions fun = new functions();
        
        static void Main()
        {
            Console.WriteLine("\nPress 1 to view Data_Hard file \nPress 2 to view nimet file \nPress 3 to enter Date");
            string input = Console.ReadLine();
            int n;
            int userInput = 0;
            bool isNumeric = int.TryParse(input, out n);

            if (isNumeric)
            { userInput = Convert.ToInt32(input); }

            Console.Clear();
            //var date1 = userInput;
            //Console.WriteLine(date1);
            Program pg = new Program();

            if (userInput == 1)
            {
                pg.view_tb_DataHard();
            }
            else if (userInput == 2)
            {
                pg.view_nimet();
            }
            else if (userInput == 3)
            {
                Console.WriteLine("Enter a Date in format MM/dd/yyyy");
                string inputDate = Console.ReadLine();

                DateTime validDate;

                var formats = new[] { "MM/dd/yyyy"};

                if (DateTime.TryParseExact(inputDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out validDate))
                {

                    int dateLength = inputDate.Length;

                        string names = pg.get_Names(validDate);
                        //Console.WriteLine("Valid Date MM/dd/yyyy");
                        Console.WriteLine(names);
                        pg.options();   
                }
                else
                {
                    // do for invalid date
                    Console.WriteLine("Invalid Date");
                    Main();
                }
            }
            else {
                Console.WriteLine("Please enter a valid Number");
                Main();
            }
            
        }

        private void view_tb_DataHard()
        {
            int view_main = fun.view_tb_DataHard();
            if (view_main == 1)
            { Main(); }
        }

        private void view_nimet()
        {
            int view_main = fun.view_nimet();
            if (view_main == 1)
            { Main(); }
        }

        private void options()
        {
             int view_main = fun.options();
             if (view_main == 1)
             { Main(); }
            
        }

        private string get_Names(DateTime validDate)
        {
            string names = fun.get_Names(validDate);
            return names;
        }

     }
}
