// ***********************************************************************
// Copyright (c) 2010 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
#if CLR_2_0 || CLR_4_0
using System.Collections.Generic;
#endif
using NUnit.Framework.Api;

namespace NUnit.Framework.Internal.Commands
{
    /// <summary>
    /// TestCommand is the base class for all test commands
    /// in the framework.
    /// </summary>
    public abstract class TestCommand
    {
        private Test test;
#if CLR_2_0 || CLR_4_0
        private IList<TestCommand> children;
#else
        private System.Collections.IList children;
#endif

        /// <summary>
        /// TODO: Documentation needed for constructor
        /// </summary>
        /// <param name="test"></param>
        public TestCommand(Test test)
        {
            this.test = test;
        }

        #region ITestCommandMembers

        /// <summary>
        /// TODO: Documentation needed for property
        /// </summary>
        public Test Test
        {
            get { return test; }
        }

        /// <summary>
        /// Gets any child TestCommands of this command
        /// </summary>
        /// <value>A list of child TestCommands</value>
#if CLR_2_0 || CLR_4_0
        public IList<TestCommand> Children
#else
        public System.Collections.IList Children
#endif
        {
            get 
            {
                if (children == null)
#if CLR_2_0 || CLR_4_0
                    children = new List<TestCommand>();
#else
                    children = new System.Collections.ArrayList();
#endif

                return children;
            }
        }

        /// <summary>
        /// Runs the test, returning a TestResult.
        /// </summary>
        /// <param name="context">The TestExecutionContext to be used for running the test.</param>
        /// <returns>A TestResult</returns>
        public abstract TestResult Execute(TestExecutionContext context);

        #endregion
    }
}
