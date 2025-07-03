using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgettoScrum;
using ProgettoScrum.Repositories.Interfaces;
using System;
using System.Linq;

namespace TestScuola
{
    [TestClass]
    public class TestStudente
    {
        private IStudenteRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _repository = new StudenteRepositoryTest(); // Fake repository in memoria
        }

        [TestMethod]
        public void AddStudent()
        {
            var studente = new Studente
            {
                Nome = "Mario",
                Cognome = "Rossi",
                DataNascita = new DateTime(2005, 3, 15),
                IdClasse = 1
            };

            _repository.Add(studente);
            var result = _repository.GetAll();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Mario", result[0].Nome);
            Assert.AreEqual("Rossi", result[0].Cognome);
        }

        [TestMethod]
        public void ModifyStudent()
        {
            var studente = new Studente
            {
                Nome = "Luca",
                Cognome = "Verdi",
                DataNascita = new DateTime(2004, 10, 1),
                IdClasse = 2
            };

            _repository.Add(studente);
            var existing = _repository.GetAll().First();

            var modified = new Studente
            {
                Id = existing.Id,
                Nome = "Luca Modificato",
                Cognome = "Verdi",
                DataNascita = new DateTime(2004, 10, 1),
                IdClasse = 3
            };

            _repository.Modify(modified);
            var updated = _repository.GetAll().First();

            Assert.AreEqual("Luca Modificato", updated.Nome);
            Assert.AreEqual(3, updated.IdClasse);
        }

        [TestMethod]
        public void CancelStudent()
        {
            var studente = new Studente
            {
                Nome = "Elisa",
                Cognome = "Bianchi",
                DataNascita = new DateTime(2003, 2, 28),
                IdClasse = 1
            };

            _repository.Add(studente);
            var added = _repository.GetAll().First();

            _repository.Remove(added.Id);

            Assert.AreEqual(0, _repository.GetAll().Count);
        }

        [TestMethod]
        public void ViewStudent()
        {
            _repository.Add(new Studente { Nome = "Anna", Cognome = "Neri", DataNascita = new DateTime(2005, 6, 1), IdClasse = 1 });
            _repository.Add(new Studente { Nome = "Marco", Cognome = "Blu", DataNascita = new DateTime(2006, 7, 2), IdClasse = 2 });

            var all = _repository.GetAll();

            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.Any(s => s.Nome == "Anna"));
            Assert.IsTrue(all.Any(s => s.Nome == "Marco"));
        }

        [TestMethod]
        public void GetStudentById_ReturnsCorrectStudent()
        {
            var studente = new Studente
            {
                Nome = "Chiara",
                Cognome = "Gialli",
                DataNascita = new DateTime(2005, 12, 1),
                IdClasse = 2
            };

            _repository.Add(studente);
            var added = _repository.GetAll().First();

            var result = _repository.GetById(added.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(added.Id, result.Id);
            Assert.AreEqual("Chiara", result.Nome);
        }
    }
}
