using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library
{
    public interface IDataAccess
    {
        IList<PhraseEntity> GetAll();
        PhraseEntity GetSingle(int phraseId);

        // Adding
        PhraseEntity Add(string authorName, string englishPhrase);
        PhraseEntity Add(string authorName, string englishPhrase, string comment);


        bool Remove(int phraseId);
        bool Update(PhraseEntity phrase);
    }
}
