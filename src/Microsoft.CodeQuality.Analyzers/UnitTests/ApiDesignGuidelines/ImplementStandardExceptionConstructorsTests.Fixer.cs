// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeQuality.CSharp.Analyzers.ApiDesignGuidelines;
using Microsoft.CodeQuality.VisualBasic.Analyzers.ApiDesignGuidelines;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Test.Utilities;
using Xunit;

namespace Microsoft.CodeQuality.Analyzers.ApiDesignGuidelines.UnitTests
{
    public class ImplementStandardExceptionConstructorsFixerTests : CodeFixTestBase
    {
        protected override DiagnosticAnalyzer GetBasicDiagnosticAnalyzer()
        {
            return new BasicImplementStandardExceptionConstructorsAnalyzer();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new CSharpImplementStandardExceptionConstructorsAnalyzer();
        }

        protected override CodeFixProvider GetBasicCodeFixProvider()
        {
            return new ImplementStandardExceptionConstructorsFixer();
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new ImplementStandardExceptionConstructorsFixer();
        }

        #region CSharpUnitTests
        //Note: In the test cases you won't see a missing all 3 constructors scenario, because that will never occur since default constructor is always generated by system in that case

        [Fact]
        public void TestCSFixMissingTwoCtors_Case1()
        {
            var code = @"
using System;
public class Foo : Exception
{
    public Foo()
    {
    }
}
";
            var fix = @"
using System;
public class Foo : Exception
{
    public Foo()
    {
    }

    public Foo(string message) : base(message)
    {
    }

    public Foo(string message, Exception innerException) : base(message, innerException)
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact]
        public void TestCSFixMissingTwoCtors_Case2()
        {
            var code = @"
using System;
public class Foo : Exception
{
    public Foo(string message)
    {
    }
}
";
            var fix = @"
using System;
public class Foo : Exception
{
    public Foo(string message)
    {
    }

    public Foo()
    {
    }

    public Foo(string message, Exception innerException) : base(message, innerException)
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact]
        public void TestCSFixMissingTwoCtors_Case3()
        {
            var code = @"
using System;
public class Foo : Exception
{
    public Foo(string message, Exception innerException)
    {
    }
}
";
            var fix = @"
using System;
public class Foo : Exception
{
    public Foo(string message, Exception innerException)
    {
    }

    public Foo()
    {
    }

    public Foo(string message) : base(message)
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact]
        public void TestCSFixMissingOneCtor_Case1()
        {
            var code = @"
using System;
public class Foo : Exception
{
    public Foo(string message): base(message)
    {
    }

    public Foo(string message, Exception innerException) : base(message, innerException)
    {
    }

}
";
            var fix = @"
using System;
public class Foo : Exception
{
    public Foo(string message): base(message)
    {
    }

    public Foo(string message, Exception innerException) : base(message, innerException)
    {
    }

    public Foo()
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact]
        public void TestCSFixMissingOneCtor_Case2()
        {
            var code = @"
using System;
public class Foo : Exception
{
    public Foo()
    {
    }

    public Foo(string message)
    {
    }

}
";
            var fix = @"
using System;
public class Foo : Exception
{
    public Foo()
    {
    }

    public Foo(string message)
    {
    }

    public Foo(string message, Exception innerException) : base(message, innerException)
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        [Fact]
        public void TestCSFixMissingOneCtor_Case3()
        {
            var code = @"
using System;
public class Foo : Exception
{
    public Foo()
    {
    }

    public Foo(string message, Exception innerException)
    {
    }

}
";
            var fix = @"
using System;
public class Foo : Exception
{
    public Foo()
    {
    }

    public Foo(string message, Exception innerException)
    {
    }

    public Foo(string message) : base(message)
    {
    }
}
";
            VerifyCSharpFix(code, fix);
        }

        #endregion

        #region BasicUnitTests
        //Note: In the test cases you won't see a missing all 3 constructors scenario, because that will never occur since default constructor is always generated by system in that case

        [Fact]
        public void TestVBFixMissingTwoCtors_Case1()
        {
            var code = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New()
    End Sub
End Class
";
            var fix = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact]
        public void TestVBFixMissingTwoCtors_Case2()
        {
            var code = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New(message As String)
    End Sub
End Class
";
            var fix = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New(message As String)
    End Sub

    Public Sub New()
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact]
        public void TestVBFixMissingTwoCtors_Case3()
        {
            var code = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New(message As String, innerException As Exception)
    End Sub
End Class
";
            var fix = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New(message As String, innerException As Exception)
    End Sub

    Public Sub New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact]
        public void TestVBFixMissingOneCtor_Case1()
        {
            var code = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New()
    End Sub

    Public Sub New(message As String)
    End Sub
End Class
";
            var fix = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New()
    End Sub

    Public Sub New(message As String)
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact]
        public void TestVBFixMissingOneCtor_Case2()
        {
            var code = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New()
    End Sub

    Public Sub New(message As String, innerException As Exception)
    End Sub
End Class
";
            var fix = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New()
    End Sub

    Public Sub New(message As String, innerException As Exception)
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }

        [Fact]
        public void TestVBFixMissingOneCtor_Case3()
        {
            var code = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New(message As String)
    End Sub

    Public Sub New(message As String, innerException As Exception)
    End Sub
End Class
";
            var fix = @"
Imports System
Public Class Foo : Inherits Exception
    Public Sub New(message As String)
    End Sub

    Public Sub New(message As String, innerException As Exception)
    End Sub

    Public Sub New()
    End Sub
End Class
";
            VerifyBasicFix(code, fix);
        }
        #endregion
    }
}