using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Entities
{
    public class PhraseEntity
    {
        public int Id { get; set; }
        public string EnglishPhrase { get; set; }
        public string SpanishPhrase { get; set; }
        public string JapansePhrase { get; set; }

        // Author 
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        // Comments
        public string Comment { get; set; }
        public IList<CommentEntity> Comments { get; set; }

        
    }
}
