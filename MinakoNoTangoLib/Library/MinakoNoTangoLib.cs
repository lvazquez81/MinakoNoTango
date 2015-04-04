using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library
{
    public interface IMinakoNoTangoLibrary
    {
        IList<PhraseEntity> GetAllPhrases(SecurityToken token);
        PhraseEntity GetPhraseDetail(SecurityToken token, int phraseId);
        bool AddEnglishCorrection(SecurityToken token, int phraseId, string correction, string comment);
        bool AddJapaneseCorrection(SecurityToken token, int phraseId, string correction, string comment);

        // Add english phrase
        PhraseEntity AddEnglishPhrase(SecurityToken token, string phrase);
        PhraseEntity AddEnglishPhrase(SecurityToken token, string phrase, string comment);
    }

    public class MinakoNoTangoLibrary : IMinakoNoTangoLibrary
    {
        private readonly IRepository _repository;

        public MinakoNoTangoLibrary(IRepository dataAccess)
        {
            if (dataAccess != null)
            {
                _repository = dataAccess;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IList<PhraseEntity> GetAllPhrases(SecurityToken token)
        {
            return _repository.GetAll();
        }

        public PhraseEntity GetPhraseDetail(SecurityToken token, int phraseId)
        {
            return _repository.GetSingle(phraseId);
        }

        public bool AddEnglishCorrection(SecurityToken token, int phraseId, string correction, string comment)
        {
            return true;
        }


        public bool AddJapaneseCorrection(SecurityToken token, int phraseId, string correction)
        {
            PhraseEntity storedPhrase = _repository.GetSingle(phraseId);
            storedPhrase.Expression = correction;
            return true;
        }

        #region Add english phrase
        public PhraseEntity AddEnglishPhrase(SecurityToken token, string englishPhrase)
        {
            //return this.AddEnglishPhrase(token, englishPhrase, null);
            throw new NotImplementedException();
        }

        public PhraseEntity AddEnglishPhrase(SecurityToken token, string englishPhrase, string comment)
        {
            //return _repository.Add(token.Username, englishPhrase, comment);
            throw new NotImplementedException();
        }
        #endregion





        public bool AddJapaneseCorrection(SecurityToken token, int phraseId, string correction, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
