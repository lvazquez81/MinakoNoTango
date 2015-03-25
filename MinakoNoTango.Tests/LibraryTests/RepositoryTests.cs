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
        private PhraseEntity addPhraseToRepository()
        {
            string authorName = Faker.NameFaker.Name();
            string englishPhrase = Faker.TextFaker.Sentence();
            string comment = Faker.TextFaker.Sentences(3);

            IDataAccess repo = new MemoryRepository();
            PhraseEntity phrase = repo.Add(authorName, englishPhrase, comment);

            return phrase;
        }

        [TestInitialize]
        public void SetupTest()
        {
            MemoryRepository.Initialize();
        }
        
        [TestMethod]
        public void MemRepository_AddPhrase()
        {
            string authorName = Faker.NameFaker.Name();
            string englishPhrase = Faker.TextFaker.Sentence();
            
            IDataAccess repo = new MemoryRepository();
            PhraseEntity phrase = repo.Add(authorName, englishPhrase);

            Assert.IsNotNull(phrase);
            Assert.IsTrue(phrase.Id > 0);
            Assert.AreEqual(authorName, phrase.AuthorName);
            Assert.AreEqual(englishPhrase, phrase.Expression);
        }

        [TestMethod]
        public void MemRepository_AddPhraseWithComment()
        {
            string authorName = Faker.NameFaker.Name();
            string englishPhrase = Faker.TextFaker.Sentence();
            string comment = Faker.TextFaker.Sentences(3);

            IDataAccess repo = new MemoryRepository();
            PhraseEntity phrase = repo.Add(authorName, englishPhrase, comment);

            Assert.IsNotNull(phrase);
            Assert.IsTrue(phrase.Id > 0);
            Assert.AreEqual(authorName, phrase.AuthorName);
            Assert.AreEqual(englishPhrase, phrase.Expression);
            Assert.AreEqual(comment, phrase.Comment);
        }

        [TestMethod]
        public void MemRepository_RemovePhrase()
        {
            string authorName = Faker.NameFaker.Name();
            string englishPhrase = Faker.TextFaker.Sentence();
            string comment = Faker.TextFaker.Sentences(3);

            IDataAccess repo = new MemoryRepository();
            PhraseEntity phrase = repo.Add(authorName, englishPhrase, comment);
            Assert.IsNotNull(phrase);

            bool hasBeenDeleted = repo.Remove(phrase.Id);
            PhraseEntity deletedPhrase = repo.GetSingle(phrase.Id);

            Assert.IsTrue(hasBeenDeleted);
            Assert.IsNull(deletedPhrase);
        }

        [TestMethod]
        public void MemRepository_ModifyPhrase()
        {
            PhraseEntity storedPhrase = addPhraseToRepository();
            
            int storedPhraseId = storedPhrase.Id;
            string modification = Faker.TextFaker.Sentence();

            storedPhrase.Expression = modification;

            IDataAccess repo = new MemoryRepository();
            bool updated = repo.Update(storedPhrase);
            Assert.IsTrue(updated);

            storedPhrase = repo.GetSingle(storedPhrase.Id);
            Assert.IsNotNull(storedPhrase);
            Assert.AreEqual(storedPhraseId, storedPhrase.Id);
            Assert.AreEqual(modification, storedPhrase.Expression);
        }
    }
}
