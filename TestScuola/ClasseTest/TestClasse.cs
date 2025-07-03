using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;

namespace TestScuola.ClasseTest
{
    [TestClass]
    public sealed class TestClasse
    {
        private IClasseRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _repository = new ClasseRepositoryTest();
        }

        [TestMethod]
        public void AddClasse()
        {
            var classe = new Classe { IdClasse = 1, Anno = 2025, Sezione = "A" };
            _repository.Add(classe);

            var result = _repository.GetAll();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(classe.IdClasse, result[0].IdClasse);
            Assert.AreEqual(classe.Anno, result[0].Anno);
            Assert.AreEqual(classe.Sezione, result[0].Sezione);
        }

        [TestMethod]
        public void ModifyClasse()
        {
            var classe = new Classe { IdClasse = 2, Anno = 2024, Sezione = "C" };
            _repository.Add(classe);

            var updated = new Classe { IdClasse = 2, Anno = 2025, Sezione = "D" };
            _repository.Modify(updated);

            var result = _repository.GetAll()[0];

            Assert.AreEqual(2025, result.Anno);
            Assert.AreEqual("D", result.Sezione);
        }

        [TestMethod]
        public void CancelClasse()
        {
            var classe = new Classe { IdClasse = 3, Anno = 2025, Sezione = "B" };
            _repository.Add(classe);
            _repository.Remove(36);

            var result = _repository.GetAll();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ViewClasse()
        {
            var classe1 = new Classe { IdClasse = 4, Anno = 2023, Sezione = "A" };
            var classe2 = new Classe { IdClasse = 5, Anno = 2024, Sezione = "B" };
            _repository.Add(classe1);
            _repository.Add(classe2);

            var result = _repository.GetAll();

            Assert.AreEqual(2, result.Count);
        }
    }
}
