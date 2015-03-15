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

        PhraseEntity AddEnglishPhrase(SecurityToken _testSecurityToken, string phrase, string comment);
    }

    public class MinakoNoTangoLibrary : IMinakoNoTangoLibrary
    {
        private readonly IDataAccess _dataAcces;

        public MinakoNoTangoLibrary(IDataAccess dataAccess)
        {
            if (dataAccess != null)
            {
                _dataAcces = dataAccess;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IList<PhraseEntity> GetAllPhrases(SecurityToken token)
        {
            return _dataAcces.GetAll();
        }

        public PhraseEntity GetPhraseDetail(SecurityToken token, int phraseId)
        {
            return _dataAcces.GetSingle(phraseId);
        }

        public bool AddEnglishCorrection(SecurityToken token, int phraseId, string correction, string comment)
        {
            return true;
        }


        public bool AddJapaneseCorrection(SecurityToken token, int phraseId, string correction, string comment)
        {
            PhraseEntity storedPhrase = _dataAcces.GetSingle(phraseId);
            storedPhrase.JapansePhrase = correction;
            return true;
        }


        public PhraseEntity AddEnglishPhrase(SecurityToken token, string englishPhrase, string comment)
        {
            PhraseEntity phrase = _dataAcces.Add(token.Username, englishPhrase, comment);
            phrase.AuthorName = token.Username;
            phrase.EnglishPhrase = englishPhrase;
            phrase.Comment = comment;
            return phrase;
        }
    }
}
