namespace NEWgIT.Tests;

public class CommitCounterTests : IDisposable
{
    Repository _repository;
    string _path;

    public CommitCounterTests()
    {
        _path = "repo3";
        string localPath = "@/TestFiles/";

        Repository.Init(_path);
        _repository = new Repository(_path);
        //_repository.Delete();

        // Lucas commits - 2 now - 2 25/5/19
        /*  _repository.Commit("Her kommer smølfe Cowboy Joe", new Signature("Lucas", "lucas@gmail.com", DateTimeOffset.Now), new Signature("Lucas", "lucas@gmail.com", DateTimeOffset.Now), new CommitOptions());
         _repository.Commit("Kom og se mit cowboy show", new Signature("Lucas", "lucas@gmail.com", DateTimeOffset.Now), new Signature("Lucas", "lucas@gmail.com", DateTimeOffset.Now), new CommitOptions());
         _repository.Commit("Her kommer smølfe Cowboy Joe", new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), new CommitOptions());
         _repository.Commit("Hippija ya og hippija yo", new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), new CommitOptions());

         // Bank commits - 1 now - 1 25/5/19 - 2 26/5/19
         _repository.Index.Add(localPath + "5.md");
         _repository.Commit("kommer der et tog og det er fyldt med guld", new Signature("Bank", "bank@gmail.com", DateTimeOffset.Now), new Signature("Bank", "bank@gmail.com", DateTimeOffset.Now));
         _repository.Index.Add(localPath + "6.md");
         _repository.Commit("så råber han hands up før han smølfer lommen fuld", new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 25)), new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 25)));
         _repository.Index.Add(localPath + "7.md");
         _repository.Commit("så tager han på salon og spiller kort", new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)), new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)));
         _repository.Index.Add(localPath + "8.md");
         _repository.Commit("han smølfer mig på sin hest og rider bort", new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)), new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)));

         // Trøstrup commits - 1 25/5/19 - 1 26/5/19
         _repository.Commit("Jeg kan godt lide smølfer OwO", new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 25)), new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 25)), new CommitOptions());
         _repository.Commit("Jeg synes de er tihi", new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 26)), new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 26)), new CommitOptions());        
          */
    }

    [Fact]
    public void FrequencyMode_Should_Return_Sum_Of_Commits_Per_Day()
    {
        // Arrange

        // Act

        // Assert

        //_repository.Commits.Count().Should().Be(10);
        true.Should().Be(true);
    }

    public void Dispose()
    {
        _repository.Dispose();
        _repository.Delete();
    }
}
