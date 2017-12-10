using System;
using System.IO;
using System.Text.RegularExpressions;
using WindowsService.BLL.Interfaces;


namespace WindowsService.BLL
{
    public class ParserCSV: IParser
    {
       private string fileName { get; set; }
       private string managerName { get; set; }
       private DateTime date { get; set; }
       private string[] substrings { get; set; }

        public bool ParseFile(string filePath)
        {
            FileInfo fileInf = new FileInfo(filePath);

            if (fileInf.Exists)
            {
                fileName = fileInf.Name;
       
                int n1 = 0;
                int n2 = 0;

                for (int i = 1; i < fileName.Length; i++)
                {
                    if (fileName[i] == '_')
                    {
                        n1 = i;
                    }
                    else if (fileName[i] == '.')
                    {
                        n2 = i;
                    }
                }

                managerName = fileName.Substring(1, n1 - 1);
                date = DateTime.Parse(fileName.Substring(n1 + 1, n2 - n1 - 1));

                using (StreamReader file = new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    SalesService salesService = new SalesService();
                    string line = null;

                    while ((line = file.ReadLine()) != null)
                    {
                        substrings = Regex.Split(line, ",");
                        salesService.AddSales(managerName, date, substrings);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
