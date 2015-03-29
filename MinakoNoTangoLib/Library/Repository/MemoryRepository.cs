using MinakoNoTangoLib.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library
{
    public class MemoryRepository : IDataAccess
    {
        private int _newId;
        private IList<PhraseEntity> _data;

        public MemoryRepository() : this(1) { }
        
        public MemoryRepository(int newIdSeed)
        {
            _newId = newIdSeed;
            if(_data == null) _data = new List<PhraseEntity>();
        }

        public bool Initialize()
        {
            _newId = 1;
            _data = new List<PhraseEntity>();

            return true;
        }

        public IList<PhraseEntity> GetAll()
        {
            return _data;
        }

        public PhraseEntity GetSingle(int phraseId)
        {
            return _data.Where(x => x.Id == phraseId).FirstOrDefault();
        }

        #region Add
        public int Add(string authorName, string expression, LanguageType language)
        {
            return this.Add(authorName, expression, language, null);
        }

        public int Add(string authorName, string expression, LanguageType language, string comment)
        {
            PhraseEntity entry = new PhraseEntity()
            {
                Id = _newId++,
                AuthorName = authorName,
                Language = language,
                Comment = comment,
                Expression = expression
            };

            _data.Add(entry);

            return entry.Id;
        }
        #endregion

        public bool Remove(int phraseId)
        {
            if (phraseId == 0) throw new ArgumentException();

            PhraseEntity phrase = this.GetSingle(phraseId);
            if (phrase == null) throw new InvalidOperationException();

            return _data.Remove(phrase);
        }

        public bool Update(PhraseEntity phrase)
        {
            if (phrase == null) throw new ArgumentNullException();
            if (phrase.Id == 0) throw new ArgumentException();

            PhraseEntity originalPhrase = this.GetSingle(phrase.Id);
            if (originalPhrase == null) throw new InvalidOperationException();

            bool removed = _data.Remove(phrase);
            if (removed)
            {
                _data.Add(phrase);
            }

            bool added = _data.Contains(phrase);

            return removed && added;
        }


       
    }
}
