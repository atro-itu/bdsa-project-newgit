# UML Diagrams code

This documents holds the code for the different UML diagrams.
The reason for making this documents is that PlantUML is not visualy supported by markdown.
Therefore another (TODO link to location) document will hold the png's.

## Package Diagram

```plantuml
@startuml packageDiagram

namespace NEWgIT.Core {
  namespace Services{
    class CommitFetcherService{}
    class ForkFetcherService{}
    interface ICommitFetcherService{}
    interface IForkFetcherService{}
  }
  namespace Data {
    class AnalysisDTO {}
    class CommitDTO{}
    class AuthorsDTO{}
    class ForksDTO{}
    class FrequenciesDTO{}
  }
  class CommitCounter{}
  interface IAnalysisRepository{}
  'class RepositoryExtensions{}'
  class Response{}
  class Stringify{}
}

namespace NEWgIT.Infrastructure{
  class Analysis{}
  class AnalysisRepository{}
  class CommitInfo{}
  class DbExtensions{}
  class GitContext{}
}

namespace NEWgIT{
  namespace Controller{
    class AnalysisController{}
  }
  class Program{}
}

'Outgoing NEWgIT.Infrastructure.AnalysisRepository dependencies'
NEWgIT.Core <|-- NEWgIT.Infrastructure

'Internal NEWgIT.Core.Services dependencies'
NEWgIT.Core.Services.ICommitFetcherService <|-- NEWgIT.Core.Services.CommitFetcherService
NEWgIT.Core.Services.IForkFetcherService <|-- NEWgIT.Core.Services.ForkFetcherService

'Internal NEWgIT.Infrastructure.denpendencies
NEWgIT.Infrastructure.GitContext <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Infrastructure.Analysis <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Infrastructure.CommitInfo <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Infrastructure.DbExtensions <|-- NEWgIT.Infrastructure.Analysis
NEWgIT.Infrastructure.Analysis <|-- NEWgIT.Infrastructure.GitContext
NEWgIT.Infrastructure.CommitInfo <|-- NEWgIT.Infrastructure.GitContext

'Outgoing NEWgIT.Controller.AnalysisController dependencies'
NEWgIT.Core <|-- NEWgIT

@enduml
```

## Activity Diagram

```plantuml
@startuml activityDiagram
|Controller|
start
note right: GET analysis/{owner}/{name}/{mode}

|DatabaseLayer|
if (analysis exists in db?) is (yes) then
  :fetch analysis by identifier;
else (no)
  :Find repository by identifier;

  |Services|
  switch (mode)
  case ( Frequency)
    :CommitCounter frequency mode;
  case ( Author) 
    :CommitCounter author mode;
  case ( Forks )
    :ForkFetcherService get forks;
  endswitch
    
  |DatabaseLayer|
  :Persist data;
endif

|Controller|
  :Serialize analysis object;
end

@enduml
```

## Sequence Diagram

```plantuml
@startuml sequenceDiagram

actor User
participant NEWgIT.Client
participant NEWgIT.Api
participant NEWgIT.Core
participant Azure

'Log in

User -> Azure : Acquire token
User <- Azure : Reponse

'Find repo

User -> NEWgIT.Client : input repo owner and repo name
NEWgIT.Client -> NEWgIT.Client : Log repo owner and repo name
NEWgIT.Client -> NEWgIT.Api : Post
NEWgIT.Api -> NEWgIT.Core : FindByIdentifier
NEWgIT.Api <- NEWgIT.Core : Response
NEWgIT.Api -> NEWgIT.Core : GetRepoCommits
NEWgIT.Api <- NEWgIT.Core : Response
NEWgIT.Api -> NEWgIT.Core : Create
NEWgIT.Api <- NEWgIT.Core : Response
NEWgIT.Client <- NEWgIT.Api : Response
activate NEWgIT.Client
NEWgIT.Client -> NEWgIT.Api : Put
note left
  if received 409
end note
NEWgIT.Api -> NEWgIT.Core : GetRepoCommits
NEWgIT.Api <-NEWgIT.Core : Response
NEWgIT.Api -> NEWgIT.Core : Update
NEWgIT.Api <- NEWgIT.Core : Response
User <- NEWgIT.Client : Return
note right
  if received 404
end note
deactivate NEWgIT.Client
NEWgIT.Client <- NEWgIT.Api : Response

'Get frequency

NEWgIT.Client -> NEWgIT.Api : Get (frequency)
NEWgIT.Api -> NEWgIT.Core : FindByIdentifier
NEWgIT.Core -> NEWgIT.Core : FrequencyMode
NEWgIT.Core -> NEWgIT.Core : Response
NEWgIT.Api <- NEWgIT.Core : Response
NEWgIT.Client <- NEWgIT.Api : Response
activate NEWgIT.Client
User <- NEWgIT.Client : Show not found
note right
  if commits not found
end note
User <- NEWgIT.Client : Show frequency commits
note right
  if commits found
end note
deactivate NEWgIT.Client

'Get author

NEWgIT.Client -> NEWgIT.Api : Get (author)
NEWgIT.Api -> NEWgIT.Core : FindByIdentifier
NEWgIT.Core -> NEWgIT.Core : AuthorMode
NEWgIT.Core -> NEWgIT.Core : Response
NEWgIT.Api <- NEWgIT.Core : Response
NEWgIT.Client <- NEWgIT.Api : Response
activate NEWgIT.Client
User <- NEWgIT.Client : Show not found
note right
  if commits not found
end note
User <- NEWgIT.Client : Show author commits
note right
  if commits found
end note
deactivate NEWgIT.Client

'Get forks

NEWgIT.Client -> NEWgIT.Api : Get (forks)
NEWgIT.Api -> NEWgIT.Core : FetchForks
NEWgIT.Api <- NEWgIT.Core : Response
NEWgIT.Client <- NEWgIT.Api : Response
User <- NEWgIT.Client : Show Ok

@enduml
```
