using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;
using System;

namespace TestScuola
{
    [TestClass]
    public class TestVoto
    {
        private IVotoRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _repository = new VotoRepositoryTest();
        }

        [TestMethod]
        public void AddVote()
        {
            var voto = new Voto { Valore = 7.5f, StudenteId = 10, MateriaId = 2, Data = DateTime.Today };
            _repository.Add(voto);

            var result = _repository.GetAll();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(voto.Id, result[0].Id);
        }

        [TestMethod]
        public void ModifyVote()
        {
            var voto = new Voto { Valore = 6, StudenteId = 1, MateriaId = 3, Data = DateTime.Today };
            _repository.Add(voto);

            var updated = new Voto { Id = voto.Id, Valore = 8, StudenteId = 1, MateriaId = 3, Data = DateTime.Today };
            _repository.Modify(updated);

            var result = _repository.GetAll()[0];
            Assert.AreEqual(8, result.Valore);
        }

        [TestMethod]
        public void CancelVote()
        {
            var voto = new Voto { Valore = 5, StudenteId = 2, MateriaId = 1, Data = DateTime.Today };
            _repository.Add(voto);

            _repository.Remove(voto.Id);
            var result = _repository.GetAll();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ViewVote()
        {
            var voto1 = new Voto { Valore = 6.5f, StudenteId = 2, MateriaId = 1, Data = DateTime.Today };
            var voto2 = new Voto { Valore = 7.2f, StudenteId = 3, MateriaId = 2, Data = DateTime.Today };
            _repository.Add(voto1);
            _repository.Add(voto2);

            var result = _repository.GetAll();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetVotiConMateriaPerStudenteTest()
        {
            var voto1 = new Voto { Valore = 7, StudenteId = 1, MateriaId = 1, Data = DateTime.Today };
            var voto2 = new Voto { Valore = 8, StudenteId = 1, MateriaId = 2, Data = DateTime.Today };
            var voto3 = new Voto { Valore = 6, StudenteId = 2, MateriaId = 1, Data = DateTime.Today };

            _repository.Add(voto1);
            _repository.Add(voto2);
            _repository.Add(voto3);

            var votiStudente1 = _repository.GetVotiConMateriaPerStudente(1);

            Assert.AreEqual(2, votiStudente1.Count);
            Assert.IsTrue(votiStudente1.All(v => v.StudenteId == 1));
            Assert.AreEqual("Materia1", votiStudente1[0].NomeMateria);
            Assert.AreEqual("Materia2", votiStudente1[1].NomeMateria);
        }
    }
}
