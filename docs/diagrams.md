# Diagrams
This document holds the UML diagrams for the project.

## Package Diagram

This is the package diagram for the project. It shows the different packages and their dependencies.

![Package Diagram](../images/package_diagram.png)

The reason that `NEWgIT` is not shown to depend on `AnalysisRepository` or `NEWgIT.Infrastructure` is because it depends on `IAnalysisRepository`. Later in the runtime `IAnalysisRepository` will be implemented by the concrete class.

## Activity Diagram
