# Clean on Close

[![Build status](https://ci.appveyor.com/api/projects/status/ukp6on6aji932y07?svg=true)](https://ci.appveyor.com/project/madskristensen/closealltabs)

<!-- Update the VS Gallery link after you upload the VSIX-->
Download this extension from the [VS Gallery](https://visualstudiogallery.msdn.microsoft.com/[GuidFromGallery])
or get the [CI build](http://vsixgallery.com/extension/55640f47-34bc-436b-8820-e7f64fbb31fc/).

---------------------------------------

Closes all open documents when the solution is closing or when
Visual Studio is closing.

See the [change log](CHANGELOG.md) for changes and road map.

## Features
When Visual Studio closes or the current solution is being manually closed,
this extension will perform clean up.

- Closes all open documents
- Collapses nodes in Solution Explorer
- Super fast - you won't even notice it

### Close open documents
All open documents will be closed when the solution closes. This makes
solution load faster for the next time you open it.

### Collapse nodes in Solution Explorer
Projects and solutions can quickly become noisy to look at when folders
and nested files are expanded in Solution Explorer. 

This extension automatically collapses all expanded nodes except for
project nodes. 

![Before and after](art\before-after.png)

## Contribute
Check out the [contribution guidelines](.github./CONTRIBUTING.md)
if you want to contribute to this project.

For cloning and building this project yourself, make sure
to install the
[Extensibility Tools 2015](https://visualstudiogallery.msdn.microsoft.com/ab39a092-1343-46e2-b0f1-6a3f91155aa6)
extension for Visual Studio which enables some features
used by this project.

## License
[Apache 2.0](LICENSE)