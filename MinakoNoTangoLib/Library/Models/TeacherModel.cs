﻿using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTangoLib.Library.Models
{
    public class TeacherModel
    {
        private readonly SecurityToken _token;
        private readonly IRepository _repository;

        public IList<PhraseEntity> Expressions { get; set; }
        public PhraseEntity ViewedExpression { get; set; }
        public string Comment { get; set; }
        public string Correction { get; set; }

        public TeacherModel(IRepository dataAccess, SecurityToken token)
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

            this.Expressions = _repository.GetAll();
        }

        public PhraseEntity GetExpression(int phraseId)
        {
            return _repository.GetSingle(phraseId);
        }

        public bool AddCorrection()
        {
            PhraseEntity originalPhrase = this.ViewedExpression;

            // Corretions
            if (originalPhrase.Corrections == null)
                originalPhrase.Corrections = new List<CorrectionEntity>();

            originalPhrase.Corrections.Add(new CorrectionEntity()
            {
                Expression = this.Correction,
                CorrecctedOn = DateTime.Now
            });

            // Comments
            if (originalPhrase.Comments == null)
                originalPhrase.Comments = new List<CommentEntity>();

            originalPhrase.Comments.Add(new CommentEntity()
            {
                AuthorName = _token.Username,
                Comment = this.Comment
            });

            return _repository.Update(originalPhrase);
        }

        public void LoadViewedExpression(int Id)
        {
            if (Id > 0)
            {
                this.ViewedExpression = _repository.GetSingle(Id);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
