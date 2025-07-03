using ProgettoScrum.Repositories.Interfaces;
using ProgettoScrum;

[TestClass]
public sealed class TestClasse
{
    private IClasseRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _repository = new ClasseRepositoryTest(); // il fake implementa IClasseRepository
    }

    [TestMethod]
    public void AddClasse()
    {
        var classe = new Classe { Anno = 2025, Sezione = "A" };
        _repository.Add(classe);

        var all = _repository.GetAll();
        Assert.AreEqual(1, all.Count);

        var first = all[0];
        Assert.AreEqual(2025, first.Anno);
        Assert.AreEqual("A", first.Sezione);

        var byId = _repository.GetById(first.IdClasse);
        Assert.IsNotNull(byId);
        Assert.AreEqual(first.IdClasse, byId!.IdClasse);
    }

    [TestMethod]
    public void ModifyClasse()
    {
        var classe = new Classe { IdClasse = 2, Anno = 2024, Sezione = "C" };
        _repository.Add(classe);

        var updated = new Classe { IdClasse = 2, Anno = 2025, Sezione = "D" };
        _repository.Modify(updated);

        var result = _repository.GetById(2);
        Assert.IsNotNull(result);
        Assert.AreEqual(2025, result!.Anno);
        Assert.AreEqual("D", result.Sezione);
    }

    [TestMethod]
    public void CancelClasse()
    {
        var classe = new Classe { IdClasse = 3, Anno = 2025, Sezione = "B" };
        _repository.Add(classe);

        _repository.Remove(3);

        var all = _repository.GetAll();
        Assert.AreEqual(0, all.Count);
    }

    [TestMethod]
    public void ViewClasse()
    {
        var classe1 = new Classe { IdClasse = 4, Anno = 2023, Sezione = "A" };
        var classe2 = new Classe { IdClasse = 5, Anno = 2024, Sezione = "B" };
        _repository.Add(classe1);
        _repository.Add(classe2);

        var all = _repository.GetAll();
        Assert.AreEqual(2, all.Count);
    }
}
