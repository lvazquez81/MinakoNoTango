using MinakoNoTangoLib.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MinakoNoTangoLib.Library.Repository
{
    public class XmlRepository : IRepository
    {
        private XDocument _xml;
        private int _newId = 1;
        private string _filePath;
        private List<PhraseEntity> _data;

        public XmlRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                FileStream stream = File.Create(_filePath);
                stream.Close();
            }
            loadFromXmlFile();
        }

        private void loadFromXmlFile()
        {
            FileStream file = File.Open(_filePath, FileMode.Open);
            _xml = XDocument.Load(file);
            IEnumerable<XElement> phraseNodes = _xml.Elements("Phrase");

            if (_data == null) _data = new List<PhraseEntity>();
            _data.Clear();
            foreach (XElement node in phraseNodes)
            {
                var phrase = new PhraseEntity()
                {
                     Id = Convert.ToInt32(node.Attribute("Id").Value),
                     AuthorName = node.Attribute("AuthorName").Value,
                     Expression = node.Value,
                };
                _data.Add(phrase);
            }
        }

        public bool Initialize()
        {
            _data.Clear();
            return true;
        }

        public IList<PhraseEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public PhraseEntity GetSingle(int phraseId)
        {
            throw new NotImplementedException();
        }

        public int Add(string authorName, string expression, LanguageType language)
        {
            var phrase = new PhraseEntity()
            {
                Id = _newId,
                AuthorName = authorName,
                Expression = expression,
                Language = language
            };

            _data.Add(phrase);

            _xml.Add(new XElement("Phrase"));

            saveToXmlFile();

            return _newId++;
        }

        private bool saveToXmlFile()
        {
            try
            {
                _xml.Save(_filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Add(string authorName, string expression, LanguageType language, string comment)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int phraseId)
        {
            throw new NotImplementedException();
        }

        public bool Update(PhraseEntity phrase)
        {
            throw new NotImplementedException();
        }


    }
}
