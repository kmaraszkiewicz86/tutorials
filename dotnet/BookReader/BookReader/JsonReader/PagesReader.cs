using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using BookReader.Model;
using Newtonsoft.Json;

namespace BookReader.JsonReader
{
    public class PagesReader
    {
        /// <summary>
        /// Gets the pages readers.
        /// </summary>
        /// <value>
        /// The pages readers.
        /// </value>
        public LinkedList<PageModel> PagesModels { get; private set; }

        private string _path;

        private int _page = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagesReader"/> class.
        /// </summary>
        /// <exception cref="Exception">Folder locatization {_path}</exception>
        public PagesReader()
        {
            _path = Path.Combine(Environment.CurrentDirectory, "Pages");
            PagesModels = new LinkedList<PageModel>();

            if (!Directory.Exists(_path))
            {
                throw new Exception($"Folder locatization {_path} doesnt exist");
            }
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        public void Read()
        {
            var files = Directory.GetFiles(_path, "*.json");

            foreach (var file in files)
            {
                var page = JsonConvert.DeserializeObject<PageModel>(File.ReadAllText(file));
                PagesModels.AddLast(page);
            }
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public PageModel GetPage(int page)
        {
            return PagesModels.FirstOrDefault(p => p.PageNumber == page);
        }

        /// <summary>
        /// Gets the next page number.
        /// </summary>
        /// <returns></returns>
        public int GetNextPageNumber()
        {
            if (PagesModels.Select(p => p.PageNumber).Max() <= _page)
                _page = 1;
            else
            {
                _page++;
            }

            return _page;
        }

        /// <summary>
        /// Gets the previous page number.
        /// </summary>
        /// <returns></returns>
        public int GetPrevPageNumber()
        {
            if (_page <= 1)
                _page = 1;
            else
            {
                _page--;
            }

            return _page;
        }
    }
}