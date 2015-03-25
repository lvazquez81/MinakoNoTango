using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library
{
    public class MemoryRepository : IDataAccess
    {
        private static int _Id;
        private static IList<PhraseEntity> _data;
        public MemoryRepository()
        {
            _Id = 1;
            if(_data == null) _data = new List<PhraseEntity>();
        }

        public static void Initialize()
        {
            MemoryRepository._Id = 1;
            MemoryRepository._data = new List<PhraseEntity>();
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
        public PhraseEntity Add(string authorName, string englishPhrase)
        {
            return this.Add(authorName, englishPhrase, null);
        }

        public PhraseEntity Add(string authorName, string englishPhrase, string comment)
        {
            PhraseEntity entry = new PhraseEntity()
            {
                Id = MemoryRepository._Id++,
                AuthorName = authorName,
                Comment = comment,
                Expression = englishPhrase
            };

            _data.Add(entry);

            return entry;
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
