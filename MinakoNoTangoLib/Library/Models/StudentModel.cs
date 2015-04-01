using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library.Models
{
    public class StudentModel
    {
        public string Author { get; set; }
        public string Expression { get; set; }
        public string Comment { get; set; }
        public DateTime CapturedOn { get; set; }
        public LanguageType Language { get; set; }

        public PhraseEntity NewExpession { get; set; }
        
        private readonly SecurityToken _token;
        private readonly IDataAccess _repository;

        public StudentModel() { }

        public StudentModel(IDataAccess dataAccess, SecurityToken token)
        {
            if (token != null)
            {
                _token = token;
            }
            else
            {
                throw new ArgumentNullException();
            }

            if (dataAccess != null)
            {
                _repository = dataAccess;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public PhraseEntity SaveExpression()
        {
            PhraseEntity phrase = null;
            int phraseId = _repository.Add(_token.Username, this.Expression, this.Language, this.Comment);

            if (phraseId > 0)
            {
                phrase = new PhraseEntity()
                {
                    Id = phraseId,
                    AuthorName = _token.Username,
                    Expression = this.Expression,
                    Language = this.Language,
                    Comment = this.Comment
                };
            }

            return phrase;
        }

        public IList<PhraseEntity> GetAllPhrases()
        {
            return _repository.GetAll();
        }

        public PhraseEntity GetExpression(int phraseId)
        {
            return _repository.GetSingle(phraseId);
        }

        public bool SaveExpression(PhraseEntity phrase)
        {
            throw new NotImplementedException();
        }
    }
}
