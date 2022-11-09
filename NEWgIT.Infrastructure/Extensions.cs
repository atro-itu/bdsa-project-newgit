namespace NEWgIT;

public static class Extensions
{
    public static Repository Seed(this Repository repo)
    {

        CommitOptions opts = new CommitOptions { AllowEmptyCommit = true };

        // Lucas commits - 2 29/02/08 - 2 25/5/19
        repo.Commit("Her kommer smølfe Cowboy Joe", new Signature("Lucas", "lucas@gmail.com", new DateTime(2008, 02, 29)), new Signature("Lucas", "lucas@gmail.com", new DateTime(2008, 02, 29)), options: opts);
        repo.Commit("Kom og se mit cowboy show", new Signature("Lucas", "lucas@gmail.com", new DateTime(2008, 02, 29)), new Signature("Lucas", "lucas@gmail.com", new DateTime(2008, 02, 29)), options: opts);
        repo.Commit("Her kommer smølfe Cowboy Joe", new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), options: opts);
        repo.Commit("Hippija ya og hippija yo", new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), new Signature("Lucas", "lucas@gmail.com", new DateTime(2019, 05, 25)), options: opts);

        // Bank commits - 1 29/02/08 - 1 25/5/19 - 2 26/5/19
        repo.Commit("kommer der et tog og det er fyldt med guld", new Signature("Bank", "bank@gmail.com", new DateTime(2008, 02, 29)), new Signature("Bank", "bank@gmail.com", new DateTime(2008, 02, 29)), options: opts);
        repo.Commit("så råber han hands up før han smølfer lommen fuld", new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 25)), new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 25)), options: opts);
        repo.Commit("så tager han på salon og spiller kort", new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)), new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)), options: opts);
        repo.Commit("han smølfer mig på sin hest og rider bort", new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)), new Signature("Bank", "bank@gmail.com", new DateTime(2019, 05, 26)), options: opts);

        // Trøstrup commits - 1 25/5/19 - 1 26/5/19
        repo.Commit("Jeg kan godt lide smølfer OwO", new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 25)), new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 25)), options: opts);
        repo.Commit("Jeg synes de er tihi", new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 26)), new Signature("Trøstrup", "frepe@gmail.com", new DateTime(2010, 05, 26)), options: opts);
        return repo;
    }
}
