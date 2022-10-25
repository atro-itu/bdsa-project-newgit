# Functional Requirements

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
