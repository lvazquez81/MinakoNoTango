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
        int Add(string authorName, string expression, LanguageType language);
        int Add(string authorName, string expression, LanguageType language, string comment);


        bool Remove(int phraseId);
        bool Update(PhraseEntity phrase);

        bool Initialize();
    }
}
