using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace testTask
{
    public class functions
    {
       
       
        public int view_tb_DataHard()
        {
            print_Table_and_Search(1);
            int view_main = options();

            return view_main;
        }

        public int view_nimet()
        {
            print_Table_and_Search(2);
            int view_main = options();

            return view_main;
        }

        public int options()
        {
            Console.WriteLine("Press 1 to to go to main menu\nPress 2 to exit");

            string input = Console.ReadLine();
            int n;
            int userInput = 0;
            bool isNumeric = int.TryParse(input, out n);

            if (isNumeric)
            { userInput = Convert.ToInt32(input); }

            Console.Clear();
            if (userInput == 1)
            { return 1; }
            else if (userInput == 2)
            { Environment.Exit(0); }
            else
            { Console.WriteLine("Please enter a valid option"); options(); }

            return 0;
        }

        private string print_Table_and_Search(int table_no, int search = 0, string inputDateStr = "")
        {
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            List<string> listC = new List<string>();
            List<string> listD = new List<string>();

            

            string filePath = "";
            if (table_no == 2)
            {

                //filePath = @"c:\users\adili_000\documents\visual studio 2012\Projects\testTask\testTask\csvFiles\nimet.csv";
                filePath = System.IO.Path.GetFullPath("nimet.csv");
            }
            else
            {
                //filePath = @"c:\users\adili_000\documents\visual studio 2012\Projects\testTask\testTask\csvFiles\Data_hard.csv";
                filePath = System.IO.Path.GetFullPath("Data_hard.csv");
            }



            using (var fs = File.OpenRead(filePath))

            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (table_no == 2)
                    {
                        var values = line.Split(';');
                        listA.Add(values[0]);

                        var str = values[1];
                        var values2 = str.Split(',');

                        listB.Add(values2[0]);

                        if (values2.Length > 1)
                        {
                            listC.Add(values2[1]);
                            listD.Add(values2[2]);
                        }
                        else
                        {
                            listC.Add("");
                            listD.Add("");
                        }

                    }
                    else
                    {

                        var values = line.Split(',');

                        listA.Add(values[0]);
                        listB.Add(values[1]);

                        if (values.Length > 2)
                        { listC.Add(values[2]); }
                        else
                        {
                            listC.Add("");
                        }
                    }


                }
            }

            if (search == 1)
            {
                if (listA.Contains(inputDateStr) == true)
                {
                    List<int> indexes = listA.Select((value, index) => new { value, index })
                    .Where(a => string.Equals(a.value, inputDateStr))
                    .Select(a => a.index).ToList();

                    string names = "";
                    foreach (int index in indexes)
                    {
                        if (listC[index] != "")
                        {
                            if (table_no == 2)
                            { names += "\t" + listB[index] + ",\n\t" + listC[index] + ",\n\t" + listD[index] + ",\n"; }
                            else
                            { names += "\t" + listB[index] + ",\n\t" + listC[index] + ",\n"; }
                        }
                        else
                        { names += "\t" + listB[index] + ",\n"; }
                    }

                    names = "\n" + names.Trim('\n');
                    return names.Trim(',') + "\n";
                }
            }
            else
            {
                int count = 0;
                foreach (string date in listA)
                {
                    if (table_no == 2)
                    {
                        Console.WriteLine("{0,10} {1,20} {2,20} {3,20}", date, listB[count], listC[count], listD[count]);
                    }
                    else
                    {
                        Console.WriteLine("{0,10} {1,20} {2,20}", date, listB[count], listC[count]);
                    }
                    count = count + 1;
                }
            }

            return string.Empty;
        }

        public string get_Names(DateTime inputDate)
        {

            string namesDataH = "";
            string namesNimet = "";
            string namesStr = "";
            string inputDateStr1 = inputDate.ToString("MM/dd/yyyy");
            string inputDateStr2 = inputDate.ToString("dd.MM.yyyy");

            string Date1 = GetFormated_Date(inputDateStr1, 1);
            string Date2 = GetFormated_Date(inputDateStr2, 2);

            namesDataH = print_Table_and_Search(1, 1, Date1);
            namesNimet = print_Table_and_Search(2, 1, Date2);

            namesStr = "\nCorresponding to the date the names in Data_hard.csv are:\n" + namesDataH;
            if (namesDataH == "")
            { namesStr += "\n\tNo results\n"; }
            namesStr += "\nCorresponding to the date the names in nimet.csv are:\n" + namesNimet;
            if (namesNimet == "")
            { namesStr += "\n\tNo results\n"; }
            return namesStr;
        }


        private string GetFormated_Date(string inputDateStr, int dateType)
        {

            string month = "";
            string date = "";
            string[] split;
            char separator;

            if (dateType == 1)
            { split = inputDateStr.Split('/'); separator = '/'; }
            else
            { split = inputDateStr.Split('.'); separator = '.'; }

            if (split[0][0] == '0')
            { month = split[0].TrimStart('0'); }
            if (split[1][0] == '0')
            { date = split[1].TrimStart('0'); }

            if (month != "" && date == "")
            {
                if (dateType == 1)
                { inputDateStr = month + separator + split[1] + separator + split[2]; }
                else
                { inputDateStr = month + separator + split[1] + separator; }
            }

            else if (date != "" && month == "")
            {
                if (dateType == 1)
                { inputDateStr = split[0] + separator + date + separator + split[2]; }
                else
                { inputDateStr = split[0] + separator + date + separator; }
            }

            else if (month != "" && date != "")
            {
                if (dateType == 1)
                { inputDateStr = month + separator + date + separator + split[2]; }
                else
                { inputDateStr = month + separator + date + separator; }
            }
            else if (month == "" && date == "")
            {
                if (dateType == 2)
                { inputDateStr = split[0] + separator + split[1] + separator; }
            }


            return inputDateStr;
        }


    }
}
