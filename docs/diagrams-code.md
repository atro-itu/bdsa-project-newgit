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
  class AnalysisDTO {}
  class CommitCounter{}
  class CommitDTO{}
  interface IAnalysisRepository{}
  class RepositoryExtensions{}
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

NEWgIT.Core.IAnalysisRepository <|-- NEWgIT.Infrastructure.AnalysisRepsository
NEWgIT.Core.Services.ICommitFetcherService <|-- NEWgIT.Core.Services.CommitFetcherService
NEWgIT.Core.Services.IForkFetcherService <|-- NEWgIT.Core.Services.ForkFetcherService

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
