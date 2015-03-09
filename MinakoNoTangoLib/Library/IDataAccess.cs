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
        bool Add(PhraseEntity phrase);
        bool Update(int phraseId, PhraseEntity phrase);
    }
}
