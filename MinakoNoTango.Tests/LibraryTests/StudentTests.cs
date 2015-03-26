using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinakoNoTangoLib.Entities;
using MinakoNoTangoLib.Library;
using MinakoNoTangoLib.Library.Models;
using Moq;
using System;
using System.Collections.Generic;

namespace MinakoNoTango.Tests.LibraryTests
{
    [TestClass]
    public class StudentTests
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
                    Id = It.IsAny<int>(),
                    Expression = It.IsAny<string>()
                });

            // Mock Add
            mockDataAccess
                .Setup(x => x.Add(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<LanguageType>(),
                    It.IsAny<string>()))
                .Returns(
                   It.IsAny<int>());


            return mockDataAccess.Object;
        }
        
     
    }
}
