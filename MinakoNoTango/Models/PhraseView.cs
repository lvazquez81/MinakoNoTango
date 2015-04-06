using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinakoNoTango.Models
{
    public class PhraseView
    {
        public string NewPhrase { get; set; }
        public List<string> Phrases { get; set; }

        public string Expression { get; set; }
        public string Comment { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }

        public PhraseView()
        {
            this.Phrases = new List<string>();
        }
    }
}