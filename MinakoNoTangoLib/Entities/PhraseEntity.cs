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
        public string Expression { get; set; }
        public LanguageType Language { get; set; }

        // Author 
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        // Comments
        public string Comment { get; set; }
        public IList<CommentEntity> Comments { get; set; }

        public IList<CorrectionEntity> Corrections { get; set; }

        public override bool Equals(object obj)
        {
            PhraseEntity x = obj as PhraseEntity;
            if (x != null)
            {
                return x.Id == this.Id;
            }
            else
            {
                return false;
            }
        }
    }

    public class CorrectionEntity
    {
        public string Expression { get; set; }
        public DateTime CorrecctedOn { get; set; }
    }
}
