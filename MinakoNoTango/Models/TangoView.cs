using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinakoNoTango.Models
{
    public class TangoView
    {
        public string NewVocabulary { get; set; }
        public List<string> Vocabulary { get; set; }

        public TangoView()
        {
            this.Vocabulary = new List<string>();
        }
    }
}