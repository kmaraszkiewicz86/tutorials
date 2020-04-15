using System;
using System.Collections.Generic;

namespace EntityFrameworkExample.Services
{
    public abstract class BaseTableWriter<IModel> where IModel: class
    {
        public abstract IEnumerable<IModel> Models
        {
            get;
            set;
        }

        protected IEnumerable<IModel> models;


        private const int tableWidth = 73;

        public abstract void Print();

        protected void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        protected void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        protected string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
