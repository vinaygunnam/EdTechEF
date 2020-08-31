# Instructions

One common project for all data classes and helpers.
```
EdTech.Data.Common
```
This has the definitions for `UnitOfWork` and `GenericRepository`.

Next, we set up individual projects for each data source of interest.
```
EdTech.Data.AlphaSource
EdTech.Data.ZetaDestination
```
We use the common data classes to declare our unique sources. No need to rewrite any classes from scratch.

Finally, we have the app where we use `AutoMapper` to define mapping and ignoring navigation properties.
```
EdTech.Apps.SimpleConsole
```
