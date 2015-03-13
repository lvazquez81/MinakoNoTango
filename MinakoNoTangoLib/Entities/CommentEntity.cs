using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Entities
{
    public class CommentEntity
    {
        public string AuthorName { get; set; }
        public string Comment { get; set; }
        public DateTime EditedOn { get; set; }
        public bool ShowComment { get; set; }

    }
}
