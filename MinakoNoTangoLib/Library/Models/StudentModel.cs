using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;

namespace MinakoNoTangoLib.Library.Models
{
    public class StudentModel
    {
        public int NewId { get; set; }
        public string Author { get; set; }
        public string Expression { get; set; }
        public string Comment { get; set; }
        public DateTime CapturedOn { get; set; }
        public LanguageType Language { get; set; }

        private SecurityToken _token;
        private IRepository _repository;

        public StudentModel() { }
        
        public StudentModel(IRepository dataAccess, SecurityToken token)
        {
            validateRepositoryAndSecurityToken(dataAccess, token);
        }

        public void Setup(IRepository dataAccess, SecurityToken token)
        {
            validateRepositoryAndSecurityToken(dataAccess, token);
        }

        private void validateRepositoryAndSecurityToken(IRepository dataAccess, SecurityToken token)
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

        public bool SaveExpression()
        {
            this.NewId = _repository.Add(_token.Username, this.Expression, this.Language, this.Comment);
            return this.NewId > 0;
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
