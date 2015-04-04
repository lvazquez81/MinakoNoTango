using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinakoNoTangoLib.Entities;
using MinakoNoTangoLib.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTango.Tests.LibraryTests
{
    [TestClass]
    public class RepositoryTests
    {
        #region Test helpers
        private PhraseEntity addPhraseToRepository()
        {
            string authorName = Faker.NameFaker.Name();
            string englishPhrase = Faker.TextFaker.Sentence();
            string comment = Faker.TextFaker.Sentences(3);

            IRepository repo = new MemoryRepository();
            int phraseId = repo.Add(authorName, englishPhrase, LanguageType.English, comment);

            PhraseEntity phrase = new PhraseEntity()
            {
                Id = phraseId,
                AuthorName = authorName,
                Expression = englishPhrase,
                Comment = comment
            };

            return phrase;
        }
        #endregion

        [TestMethod]
        public void MemRepository_AddPhrase()
        {
            string authorName = Faker.NameFaker.Name();
            string expression = Faker.TextFaker.Sentence();
            LanguageType language = LanguageType.English;
            
            IRepository repo = new MemoryRepository();
            //PhraseEntity phrase = repo.Add(authorName, englishPhrase);
            int phraseId = repo.Add(authorName, expression, language);

            Assert.IsTrue(phraseId > 0);

            //Assert.IsNotNull(phrase);
            //Assert.IsTrue(phrase.Id > 0);
            //Assert.AreEqual(authorName, phrase.AuthorName);
            //Assert.AreEqual(englishPhrase, phrase.Expression);
        }

        [TestMethod]
        public void MemRepository_AddPhraseWithComment()
        {
            string authorName = Faker.NameFaker.Name();
            string expression = Faker.TextFaker.Sentence();
            string comment = Faker.TextFaker.Sentences(3);
            LanguageType language = LanguageType.English;

            IRepository repo = new MemoryRepository();
            //PhraseEntity phrase = repo.Add(authorName, englishPhrase, comment);
            int phraseId = repo.Add(authorName, expression, language, comment);

            Assert.IsTrue(phraseId > 0);

            //Assert.IsNotNull(phrase);
            //Assert.IsTrue(phrase.Id > 0);
            //Assert.AreEqual(authorName, phrase.AuthorName);
            //Assert.AreEqual(englishPhrase, phrase.Expression);
            //Assert.AreEqual(comment, phrase.Comment);
        }

        [TestMethod]
        public void MemRepository_RemovePhrase()
        {
            string authorName = Faker.NameFaker.Name();
            string expression = Faker.TextFaker.Sentence();
            string comment = Faker.TextFaker.Sentences(3);
            LanguageType language = LanguageType.English;

            IRepository repo = new MemoryRepository();
            //PhraseEntity phrase = repo.Add(authorName, englishPhrase, comment);
            int phraseId = repo.Add(authorName, expression, language, comment);
            Assert.IsTrue(phraseId > 0);
            //Assert.IsNotNull(phrase);

            bool hasBeenDeleted = repo.Remove(phraseId);
            PhraseEntity deletedPhrase = repo.GetSingle(phraseId);

            Assert.IsTrue(hasBeenDeleted);
            Assert.IsNull(deletedPhrase);
        }

        [TestMethod]
        public void MemRepository_ModifyPhrase()
        {
            IRepository repo = new MemoryRepository();
            string authorName = Faker.NameFaker.Name();
            string englishPhrase = Faker.TextFaker.Sentence();
            string comment = Faker.TextFaker.Sentences(3);

            int phraseId = repo.Add(authorName, englishPhrase, LanguageType.English, comment);

            string modification = Faker.TextFaker.Sentence();
            PhraseEntity updatedPhrase = new PhraseEntity()
            {
                Id = phraseId,
                AuthorName = authorName,
                Expression = modification,
                Language = LanguageType.English,
                Comment = comment
            };

            bool updated = repo.Update(updatedPhrase);
            Assert.IsTrue(updated);

            PhraseEntity storedPhrase = repo.GetSingle(updatedPhrase.Id);
            Assert.IsNotNull(storedPhrase);
            Assert.AreEqual(phraseId, storedPhrase.Id);
            Assert.AreEqual(modification, storedPhrase.Expression);
        }
    }
}
