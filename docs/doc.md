# Functional Requirements
## Week 1
None

## Week 2
1. GIT analyzations has to be stored in a database.
2. Has to implement resonable test.
3. doc.md has to be up to date with product requirements.  

## Week 3
1. Implement one or more integration tests in your test suite. 

## Week 4
1. The front-end application interacts with your `GitInsight` back-end application via the REST API that you implemented last week.
2. The front-end application should be able to receive the identifier of a GitHub repository `<github_user>/<repository_name>` or `<github_organization>/<repository_name>` as described for last week, see above) via a suitable input field.
3. Implement visualizations to your .Net Blazor front-ends that look similar to those in the illustrations on top of the [docs](https://github.com/duckth/bdsa-project-newgit/edit/main/docs/project-description.md). These visualizations should present the results of the two analyses that you already implemented in your `GitInsight` applications, i.e., the commit frequencies over time and the and the commit frequencies over time per author. You might want to use bar charts or another suitable chart type to present the analyses results.
4. Implement a visualization of the forks of a GitHub repository as a third visualization. It might just be a list view similar as illustrated on top. In case you find a more suitable visualization, you are free to choose that instead.
5. Now that your application changed, update the documentation of your `GitInsight` applications to reflect the current state of the application. For sure, update the architecture illustration from last week. Likely it is a good idea to illustrate how the front-end and the back-end interact when a new analysis is triggered via a sequence diagram. In this case the it will be a sub-system sequence diagram where the blocks on top of swim lanes represent the front-end, back-end, and other systems as sub-systems instead of objects as shown multiple times in lectures, see for example [here](https://www.lucidchart.com/pages/uml-system-sequence-diagram).

## Week 5
1. Implement an analysis and visualization of your choice in your `GitInsight` application.

## Week 6
1. Polish `GitInsight` application.
2. Implements missing requirements.
3. Complete documentatin.
4. Prepare 15 minute presentation about the application and answer the questions written in [week 6](https://github.com/duckth/bdsa-project-newgit/blob/main/docs/project-description.md).
5. Presentation has to reside in the docs directory [here](https://github.com/duckth/bdsa-project-newgit/blob/main/docs).

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

## Week 4
1. Add a front-end web-application that you write with .Net Blazor (WebAssembly) to your already existing applications
2. To connect to the GitHub REST API, you need an Access Token. Read [this documentation](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) on how to receive an Access Token for the GitHub REST API. Remember and double check on how Rasmus demonstrated to handle secrets like access tokens in .Net projects. That is, do not store the access token directly in your source code. It should never end up in your source code repository that is publicly shared with the world.

## Week 5
1. Extend your `GitInsight` application with a feature that restricts access only to authorized users. Users have to authenticate themselves before they are able to analyze a GitHub repository.
2. The authentication should hold for the front-end web-application as well as for the back-end REST API.

## Week 6
1. Complete test suite.
