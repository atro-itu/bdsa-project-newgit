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

NEWgIT.Core.IAnalysisRepository <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Core.Services.ICommitFetcherService <|-- NEWgIT.Core.Services.CommitFetcherService
NEWgIT.Core.Services.IForkFetcherService <|-- NEWgIT.Core.Services.ForkFetcherService
NEWgIT.Infrastructure.GitContext <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Core.Response <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Infrastructure.Analysis <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Core.Data.AnalysisDTO <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Core.Data.CommitDTO <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Infrastructure.CommitInfo <|-- NEWgIT.Infrastructure.AnalysisRepository
NEWgIT.Infrastructure.DbExtensions <|-- NEWgIT.Infrastructure.Analysis
NEWgIT.Infrastructure.Analysis <|-- NEWgIT.Infrastructure.GitContext
NEWgIT.Infrastructure.CommitInfo <|-- NEWgIT.Infrastructure.GitContext
NEWgIT.Core.IAnalysisRepository <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.Services.ICommitFetcherService <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.Services.IForkFetcherService <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.Data.AuthorsDTO <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.Data.FrequenciesDTO <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.CommitCounter <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.Data.ForksDTO <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.Data.AnalysisDTO <|-- NEWgIT.Controller.AnalysisController
NEWgIT.Core.Response <|-- NEWgIT.Controller.AnalysisController

@enduml
```

## Activity Diagram

```plantuml
@startuml
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
