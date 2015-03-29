using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinakoNoTangoLib.Entities;
using MinakoNoTangoLib.Library;
using MinakoNoTangoLib.Library.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinakoNoTango.Tests.LibraryTests
{
    [TestClass]
    public class DulceTests
    {
        private StudentModel _lib;
        private SecurityToken _testSecurityToken;
        private IDataAccess _repository;

        #region Test setup
        [TestInitialize]
        public void TestSetup()
        {
            _repository = initiateFakeDatabase();
            _testSecurityToken = getTestSecurityToken();
        }

        private SecurityToken getTestSecurityToken()
        {
            string testUsername = "Dulce";
            string token = "123abc";
            DateTime expiration = DateTime.Now.AddHours(1);

            return new SecurityToken()
            {
                Username = testUsername,
                Token = token,
                ExpirationDate = expiration
            };
        }

        private IDataAccess initiateFakeDatabase()
        {
            var mockDataAccess = new Mock<IDataAccess>();

            // Mock GetAll
            mockDataAccess.Setup(x => x.GetAll()).Returns(new List<PhraseEntity>()
            {
                new PhraseEntity(){ Id = 1, Expression = "Good morning!", AuthorName = "Minako" },
                new PhraseEntity(){ Id = 2, Expression = "Konnichi wa!", AuthorName = "Chibi" },
                new PhraseEntity(){ Id = 3, Expression = "Dokka e onsen e ikimashouka", AuthorName = "Chibi" },
                new PhraseEntity(){ Id = 4, Expression = "I love to take photos", AuthorName = "Minako" }
            });

            // Mock GetSingle
            mockDataAccess.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(
                new PhraseEntity()
                {
                    Id = 1,
                    Expression = Faker.TextFaker.Sentence()
                });

            // Mock Add
            mockDataAccess
                .Setup(x => x.Add(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<LanguageType>(),
                    It.IsAny<string>()))
                .Returns(1);


            return mockDataAccess.Object;
        }
        #endregion

        [TestMethod]
        public void Dulce_QuieroHacerLogin_MeDejaEntrar()
        {
            LoginModel login = new LoginModel();
            login.Username = "minako";
            login.Password = "jesusminako";

            SecurityToken token = login.GetAuthenticationToken();

            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.Token));
            Assert.AreEqual(login.Username, token.Username);
            Assert.IsTrue(token.ExpirationDate > DateTime.Now);
        }

        [TestMethod]
        public void Dulce_QuieroAgregarMiFrase_CapturaDeFraseNueva()
        {
            StudentModel student = new StudentModel(_repository, _testSecurityToken);
            student.Expression = "Machigai ga aru mono";
            student.Author = _testSecurityToken.Username;
            student.Language = LanguageType.Japanese;

            PhraseEntity phrase = student.SaveExpression();

            Assert.IsNotNull(phrase);
            Assert.AreEqual(student.Expression, phrase.Expression);
            Assert.AreEqual(student.Author, phrase.AuthorName);
            Assert.AreEqual(student.Language, phrase.Language);
        }

        [TestMethod]
        public void Dulce_QuieroVerLasFrases_MuestraListaDeFrases()
        {
            StudentModel student = new StudentModel(_repository, _testSecurityToken);
            List<PhraseEntity> phrases = student.GetAllPhrases().ToList();

            Assert.IsNotNull(phrases);
            CollectionAssert.AllItemsAreNotNull(phrases);
        }

        [TestMethod]
        public void Dulce_QuieroVerUnaFraseDeLuis_MuestraDetalleDeFrase()
        {
            string author = "Luis";
            
            // Repository contains an expression
            var repository = new Mock<IDataAccess>();
            repository.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(new PhraseEntity()
            {
                Id = Faker.NumberFaker.Number(),
                Expression = Faker.TextFaker.Sentence(),
                AuthorName = author
            });

            TeacherModel student = new TeacherModel(repository.Object, _testSecurityToken);
            PhraseEntity phrase = student.GetExpression(1);

            Assert.IsNotNull(phrase);
            Assert.AreEqual(author, phrase.AuthorName);
        }

        [TestMethod]
        public void Dulce_QuieroCorregirUnaFraseDeLuis_CapturaDeCorreccion()
        {
            string author = "Luis";
            string sampleExpression = "Machigai aru toko";
            string originalComment = "Como se usa el toko";
            
            // Repository contains an expression
            var memRepository = new MemoryRepository(1);
            int id = memRepository.Add(author, sampleExpression, LanguageType.English, originalComment);

            TeacherModel teacher = new TeacherModel(memRepository, _testSecurityToken);
            PhraseEntity expression = teacher.GetExpression(id);
            teacher.ViewedExpression = expression;
            teacher.Comment = "Se usa con particulas.";
            teacher.Correction = "Machigai nai toko";

            bool result = teacher.AddCorrection();
            Assert.IsTrue(result);

            PhraseEntity corretedPhrase = teacher.GetExpression(id);
            Assert.IsNotNull(corretedPhrase);
            Assert.AreEqual(id, corretedPhrase.Id);
            Assert.AreEqual(sampleExpression, corretedPhrase.Expression);
            Assert.IsTrue(corretedPhrase.Corrections.Count == 1);
            Assert.AreEqual(teacher.Correction, corretedPhrase.Corrections[0].Expression);
        }

        [TestMethod]
        public void Dulce_QuieroComentarSobreUnaFrase_CapturaDeComentario()
        {
            string author = "Luis";
            string sampleExpression = "Machigai aru toko";
            string originalComment = "Como se usa el toko";

            // Repository contains an expression
            var memRepository = new MemoryRepository(1);
            int id = memRepository.Add(author, sampleExpression, LanguageType.English, originalComment);

            TeacherModel teacher = new TeacherModel(memRepository, _testSecurityToken);
            PhraseEntity expression = teacher.GetExpression(id);
            teacher.ViewedExpression = expression;
            teacher.Comment = "Se usa con particulas.";
            teacher.Correction = "Machigai nai toko";

            bool result = teacher.AddCorrection();
            Assert.IsTrue(result);

            PhraseEntity corretedPhrase = teacher.GetExpression(id);
            Assert.IsNotNull(corretedPhrase);
            Assert.AreEqual(id, corretedPhrase.Id);
            Assert.IsTrue(corretedPhrase.Comments.Count == 1);
            Assert.AreEqual(teacher.Comment, corretedPhrase.Comments[0].Comment);
        }
    }
}
