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
        
        private readonly SecurityToken _token;
        private readonly IDataAccess _repository;

        public StudentModel(IDataAccess dataAccess, SecurityToken token)
        {
            if (token != null)
            {
                _token = token;
            }
            else
            {
                throw new ArgumentException();
            }

            if (dataAccess != null)
            {
                _repository = dataAccess;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public PhraseEntity SaveExpression()
        {
            return _repository.Add(_token.Username, this.Expression, this.Comment);
        }

        public IList<PhraseEntity> GetAllPhrases()
        {
            return _repository.GetAll();
        }

        public PhraseEntity GetExpression(int phraseId)
        {
            return _repository.GetSingle(phraseId);
        }
    }
}
