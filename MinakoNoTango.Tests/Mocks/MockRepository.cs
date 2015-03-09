using MinakoNoTangoLib.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTango.Tests.Mocks
{
    public class MockRepository : IDataAccess
    {
        public IList<MinakoNoTangoLib.Entities.PhraseEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public MinakoNoTangoLib.Entities.PhraseEntity GetSingle(int phraseId)
        {
            throw new NotImplementedException();
        }

        public bool Add(MinakoNoTangoLib.Entities.PhraseEntity phrase)
        {
            throw new NotImplementedException();
        }
    }
}
