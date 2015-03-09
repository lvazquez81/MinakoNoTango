using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinakoNoTangoLib.Entities;
using MinakoNoTangoLib.Library;
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
        private IMinakoNoTangoLibrary _lib;
        private SecurityToken _testSecurityToken;

        [TestInitialize]
        public void TestSetup()
        {
            IDataAccess testRepository = initiateFakeDatabase();
            _lib = new MinakoNoTangoLibrary(testRepository);
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
            mockDataAccess.Setup(x => x.GetAll()).Returns(new List<PhraseEntity>()
            {
                new PhraseEntity(){ Id = 1, EnglishPhrase = "Good morning!", AuthorName = "Minako" },
                new PhraseEntity(){ Id = 2, JapansePhrase = "Konnichi wa!", AuthorName = "Chibi" },
                new PhraseEntity(){ Id = 3, JapansePhrase = "Dokka e onsen e ikimashouka", AuthorName = "Chibi" },
                new PhraseEntity(){ Id = 4, EnglishPhrase = "I love to take photos", AuthorName = "Minako" }
            });

            mockDataAccess.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(
                new PhraseEntity()
                {
                    Id = It.IsAny<int>(),
                    EnglishPhrase = It.IsAny<string>(),
                    SpanishPhrase = It.IsAny<string>(),
                    JapansePhrase = It.IsAny<string>()
                });


            return mockDataAccess.Object;
        }

        [TestMethod]
        public void Dulce_QuieroHacerLogin_MeDejaEntrar()
        {
            string userName = "minako";
            string password = "jesuiminako";
            IMinakoNoTangoAuthentication auth = new MinakoNoTangoAuthentication();

            SecurityToken token = auth.GetAuthenticationToken(userName, password);

            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrWhiteSpace(token.Token));
            Assert.AreEqual(userName, token.Username);
            Assert.IsTrue(token.ExpirationDate > DateTime.Now);

            // TODO: Validate view 
        }

        [TestMethod]
        public void Dulce_QuieroVerLasFrases_MuestraListaDeFrases()
        {
            List<PhraseEntity> phrases = _lib.GetAllPhrases(_testSecurityToken).ToList();

            Assert.IsNotNull(phrases);
            CollectionAssert.AllItemsAreNotNull(phrases);
        }

        [TestMethod]
        public void Dulce_QuieroVerUnaFraseDeLuis_MuestraDetalleDeFrase()
        {
            PhraseEntity testPhrase = new PhraseEntity()
            {
                Id = 1,
                JapansePhrase = "Machigai ga aru mono",
                AuthorName = "Chibi"
            };

            var dataAccessWithTestPhrase = new Mock<IDataAccess>();
            dataAccessWithTestPhrase.Setup(x => x.GetSingle(It.IsAny<int>())).Returns(testPhrase);
            IMinakoNoTangoLibrary lib = new MinakoNoTangoLibrary(dataAccessWithTestPhrase.Object);

            PhraseEntity phrase = lib.GetPhraseDetail(_testSecurityToken, testPhrase.Id);

            Assert.IsNotNull(phrase);
            Assert.AreEqual(testPhrase.Id, phrase.Id);
        }

        [TestMethod]
        public void Dulce_QuieroCorregirUnaFraseDeLuis_CapturaDeCorreccion()
        {
            PhraseEntity testPhrase = new PhraseEntity()
            {
                Id = 1,
                JapansePhrase = "Machigai ga aru mono",
                AuthorName = "Chibi"
            };

            var dataAccessWithTestPhrase = new Mock<IDataAccess>();
            dataAccessWithTestPhrase.Setup(x => 
                x.GetSingle(It.IsAny<int>())).Returns(testPhrase);
            dataAccessWithTestPhrase.Setup(x =>
               x.Update(It.IsAny<int>(), It.IsAny<PhraseEntity>()))
               .Returns(true);
         
            IMinakoNoTangoLibrary libWithTestData = 
                new MinakoNoTangoLibrary(dataAccessWithTestPhrase.Object);

            string correction = "Machigai wa kore desu";
            string comment = "El error esta en la particula.";

            bool result = libWithTestData.AddJapaneseCorrection(_testSecurityToken, testPhrase.Id, correction, comment);
            Assert.IsTrue(result);

            PhraseEntity correctedPhrase = libWithTestData.GetPhraseDetail(_testSecurityToken, testPhrase.Id);
            Assert.IsNotNull(correctedPhrase);
            Assert.AreEqual(testPhrase.Id, correctedPhrase.Id);
            Assert.AreEqual(correction, correctedPhrase.JapansePhrase);
        }

        [TestMethod]
        public void Dulce_QuieroAgregarMiFrase_CapturaDeFraseNueva()
        {
            string author = "Minako";
            string phrase = "This is my new phrase.";
            string comment = "Luis, me ayudas con esta?";

            _testSecurityToken.Username = author;
            PhraseEntity newPhrase = _lib.AddEnglishPhrase(_testSecurityToken, phrase, comment);
            
            Assert.IsNotNull(newPhrase);
            Assert.AreEqual(author, newPhrase.AuthorName);
            Assert.AreEqual(phrase, newPhrase.EnglishPhrase);
        }

        [TestMethod]
        public void Dulce_QuieroComentarSobreUnaFrase_CapturaDeComentario()
        {
        }
    }
}
