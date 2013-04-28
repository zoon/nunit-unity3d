// ***********************************************************************
// Copyright (c) 2007 Charlie Poole
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

// NOTE: this is heavily modified version of TextUI.cs

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using NUnit.Framework.Api;
using NUnit.Framework.Internal;

namespace NUnitLite.Runner
{
    /// <summary>
    /// TextUI is a general purpose class that runs tests and
    /// outputs to a TextWriter.
    /// 
    /// Call it from your Main like this:
    ///   new TextUI(textWriter).Execute(args);
    ///     OR
    ///   new TextUI().Execute(args);
    /// The provided TextWriter is used by default, unless the
    /// arguments to Execute override it using -out. The second
    /// form uses the Console, provided it exists on the platform.
    /// </summary>
    public class NUnitStreamUI
    {
        private int reportCount = 0;

        // private NUnit.ObjectList assemblies = new NUnit.ObjectList();

        private TextWriter writer;

        private ITestAssemblyRunner runner;

        public ResultSummary Summary { get; private set; }

        #region Constructors

       
        /// <summary>
        /// Initializes a new instance of the <see cref="TextUI"/> class.
        /// </summary>
        /// <param name="writer">The TextWriter to use.</param>
        public NUnitStreamUI(TextWriter writer)
        {
            // Set the default writer - may be overridden by the args specified
            this.writer = writer;
            this.runner = new NUnitLiteTestAssemblyRunner(new NUnitLiteTestAssemblyBuilder());
        }

        

        #endregion

        #region Public Methods
        /// <summary>
        /// Execute a test run based on the aruments passed
        /// from Main.
        /// </summary>
        /// <param name="args">An array of arguments</param>
        public void Execute(Assembly assembly)
        {
            try
            {
                IDictionary loadOptions = new Hashtable();
                if (!runner.Load(assembly, loadOptions))
                {
                    writer.WriteLine("No tests found in assembly {0}", assembly.GetName().Name);
                    return;
                }
                writer.Write(assembly.GetName().Name + ": ");
                RunTests();
            }
#pragma warning disable 168
            catch (NullReferenceException ex)
#pragma warning restore 168
            {
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
            }
            finally
            {
                writer.Close();
            }

        }

        private void RunTests()
        {
            ITestResult result = runner.Run(TestListener.NULL, TestFilter.Empty);
            ReportResults(result);
        }

        /// <summary>
        /// Reports the results.
        /// </summary>
        /// <param name="result">The result.</param>
        private void ReportResults( ITestResult result )
        {
            Summary = new ResultSummary(result);

            writer.WriteLine("{0} Tests : {1} Failures, {2} Errors, {3} Not Run",
                Summary.TestCount, Summary.FailureCount, Summary.ErrorCount, Summary.NotRunCount);

            if (Summary.FailureCount > 0 || Summary.ErrorCount > 0)
                PrintErrorReport(result);

            if (Summary.NotRunCount > 0)
                PrintNotRunReport(result);
        }
        #endregion

        #region Helper Methods

        private void PrintErrorReport(ITestResult result)
        {
            reportCount = 0;
            writer.WriteLine();
            writer.WriteLine("Errors and Failures:");
            PrintErrorResults(result);
        }

        private void PrintErrorResults(ITestResult result)
        {
            if (result.HasChildren)
                foreach (ITestResult r in result.Children)
                    PrintErrorResults(r);
            else if (result.ResultState == ResultState.Error || result.ResultState == ResultState.Failure)
            {
                writer.WriteLine();
                writer.WriteLine("{0}) {1} ({2})", ++reportCount, result.Name, result.FullName);
                writer.WriteLine(result.Message);
                writer.WriteLine(result.StackTrace);
            }
        }

        private void PrintNotRunReport(ITestResult result)
        {
            reportCount = 0;
            writer.WriteLine();
            writer.WriteLine("Tests Not Run:");
            PrintNotRunResults(result);
        }

        private void PrintNotRunResults(ITestResult result)
        {
            if (result.HasChildren)
                foreach (ITestResult r in result.Children)
                    PrintNotRunResults(r);
            else if (result.ResultState == ResultState.Ignored || result.ResultState == ResultState.NotRunnable || result.ResultState == ResultState.Skipped)
            {
                writer.WriteLine();
                writer.WriteLine("{0}) {1} ({2}) : {3}", ++reportCount, result.Name, result.FullName, result.Message);
            }
        }

        private void PrintFullReport(ITestResult result)
        {
            writer.WriteLine();
            writer.WriteLine("All Test Results:");
            PrintAllResults(result, " ");
        }

        private void PrintAllResults(ITestResult result, string indent)
        {
            string status = null;
            switch (result.ResultState.Status)
            {
                case TestStatus.Failed:
                    status = "FAIL";
                    break;
                case TestStatus.Skipped:
                    status = "SKIP";
                    break;
                case TestStatus.Inconclusive:
                    status = "INC ";
                    break;
                case TestStatus.Passed:
                    status = "OK  ";
                    break;
            }

            writer.Write(status);
            writer.Write(indent);
            writer.WriteLine(result.Name);

            if (result.HasChildren)
                foreach (ITestResult r in result.Children)
                    PrintAllResults(r, indent + "  ");
        }
        #endregion
    }
}
