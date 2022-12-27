using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cam
{
    public class TitleContents
    {
        public TitleContents(string title, string contents)
        {
            this.title = title;
            this.contents = contents;
        }

        public string title { get; set; } = "";
        public string contents { get; set; } = "";
    }
}
