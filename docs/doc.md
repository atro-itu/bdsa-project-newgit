# Functional Requirements
## Week 1
None

## Week 2
1. GIT analyzations has to be stored in a database.
2. Has to implement resonable test.
3. doc has to be up to date with product requirements.  

## Week 3
1. Implement one or more integration tests in your test suite. 

# Non Functional Requirements
## Week 1
1. Build a small C#/.Net Core application that can be run from the command-line. As a parameter, it should receive the path to a Git repository that resides in a local directory, i.e., a directory on your computer.
2. Given that path to a repository, your application should collect all commits with respective author names and author dates. The data can be collected with the library libgit2sharp, which can be installed from NuGet.
3. Your program should be able to run in two modes, which may be indicated via command-line switches.

    1. When running GitInsight in commit frequency mode, it should produce textual output on stdout that lists the number of commits per day. For example, the output might look like the following:
        <details>
        <summary>Example</summary>

                1 2017-12-08
                6 2017-12-26
                12 2018-01-01
                13 2018-01-02
                10 2018-01-14
                7 2018-01-17
                5 2018-01-18 
        </details>

    2. When running GitInsight in commit author mode, it should produce textual output on stdout that lists the number of commits per day per author. For example, the output might look like the following:
        <details>
        <summary>Example</summary>

            Marie Beaumin
                1 2017-12-08
                6 2017-12-26
                12 2018-01-01
                13 2018-01-02
                10 2018-01-14
                7 2018-01-17
                5 2018-01-18 

            Maxime Kauta
                5 2017-12-06
                3 2017-12-07
                1 2018-01-01
                10 2018-01-02
                21 2018-01-03
                1 2018-01-04
                5 2018-01-05 
        </details>

## Week 2
1. The analyzations has to know which repo they are from.
    1. If repo is re-analyzed and analyzations are outdated it has to update the database.
    2. If repo is re-analyzed and analyzations are up to date it has to read analyzations from the database.

## Week 3
1. The REST API shall receive a repository identifier from GitHub. The form could be:
    1. <github_user>/<repository_name> 
    2. <github_organization>/<repository_name>
2. If the repository does not exist locally, then your GitInsight application shall clone the remote repository from GitHub and store it in a temporary local directory on your computer.
3. In case the repository was already cloned earlier, then the respective local repository shall be updated. That is, using libgit2sharp your application should update the local repository similar to running a git pull if you were to update a Git repository manually.
4. The analysis that your GitInsight application is performing (on that now cloned local repository) should remain the same.
5. The REST API shall return the analysis results via a JSON object.
