# TypeCobol

Prototype of an incremental Cobol compiler front-end for IBM Enterprise Cobol 5.1 for zOS syntax.

This prototype is a work in progress and is currently written in C#.

# Open Source License

The code of this repository is published under the CeCILL-C v1 Open Source Licence, which is very similar to the **LGPL** but was adapted to be compatible with the french law.

http://www.cecill.info/licences/Licence_CeCILL-C_V1-en.html

Summary :

- The Licensee is authorized to use the Software, without any limitation as to its fields of application.

- The Licensee acknowledges that the Software is supplied "as is" by the Licensor without any other express or tacit warranty.

- DISTRIBUTION OF SOFTWARE WITHOUT MODIFICATION : 
  The Licensee is authorized to distribute true copies of the Software in Source Code or Object Code form, provided that said distribution complies with all the provisions of the Agreement and is accompanied by:
    1.a copy of the Agreement,
    2.a notice relating to the limitation of both the Licensor's warranty and liability as set forth in Articles 8 and 9, 
    and 3.the Licensee allows effective access to the full Source Code of the Software.
    
- DISTRIBUTION OF MODIFIED SOFTWARE : 
  The Licensee is authorized to distribute the Modified Software, in source code or object code form, provided that said distribution complies with all the provisions of the Agreement and is accompanied by: 
    1.a copy of the Agreement,
    2.a notice relating to the limitation of both the Licensor's warranty and liability as set forth in Articles 8 and 9, 
    and 3.the Licensee allows effective access to the full source code of the Modified Software.
    
- DISTRIBUTION OF DERIVATIVE SOFTWARE :
  When the Licensee creates Derivative Software, this Derivative Software may be distributed under a license agreement other than this Agreement, subject to compliance with the requirement to include a notice concerning the rights over the Software.


# Architecture overview

## Visual Studio solution and projects

The solution was uploaded to Github using [Visual Studio 2015 Community RC](http://go.microsoft.com/fwlink/?LinkId=524433) and the [GitHub Extension for Visual Studio](https://visualstudiogallery.msdn.microsoft.com/75be44fb-0794-4391-8865-c3279527e97d).

The best way to test this project is to download and install both tools (for free) on your local machine, [login to Github from Visual Studio](http://channel9.msdn.com/Series/ConnectOn-Demand/217), then refresh this page and click on the *Open in Visual Studio* button which should appear on the right of the repository : this action will clone the solution in your local Git repository and open it in Visual Studio.

The solution contains 4 projects :
- TypeCobol.Grammar uses Antlr4 to generate C# parsers for Cobol compiler directives and Cobol statements
- TypeCobol is the main project, it implements a complete Cobol compiler front-end
- TypeCobol.Test provides unit tests which can be launched from the *Test Explorer* in Visual Studio
- TypeCobolStudio is a sample code editor used for visual demos
