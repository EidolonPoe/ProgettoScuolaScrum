using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;
using System.Linq;

namespace TestScuola
{
    [TestClass]
    public class TestMateria
    {
        private IMateriaRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _repository = new MateriaRepositoryTest();
        }

        [TestMethod]
        public void AddMateria()
        {
            var materia = new Materia { Nome = "Matematica" };
            var id = _repository.Add(materia);

            var result = _repository.GetAll();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Matematica", result[0].Nome);
            Assert.AreEqual(id, result[0].IdMateria);
        }

        [TestMethod]
        public void ModifyMateria()
        {
            var materia = new Materia { Nome = "Matematica" };
            var id = _repository.Add(materia);

            var updatedMateria = new Materia { IdMateria = id, Nome = "Fisica" };
            _repository.Modify(updatedMateria);

            var result = _repository.GetAll().FirstOrDefault(m => m.IdMateria == id);
            Assert.IsNotNull(result);
            Assert.AreEqual("Fisica", result.Nome);
        }

        [TestMethod]
        public void CancelMateria()
        {
            var materia = new Materia { Nome = "Matematica" };
            var id = _repository.Add(materia);
            _repository.Remove(id);

            var result = _repository.GetAll();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ViewMateria()
        {
            var materia1 = new Materia { Nome = "Matematica" };
            var materia2 = new Materia { Nome = "Fisica" };
            _repository.Add(materia1);
            _repository.Add(materia2);

            var result = _repository.GetAll();
            Assert.AreEqual(2, result.Count);
        }
    }
}
