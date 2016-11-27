﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Merq
{
	public class StaFactDiscoverer : IXunitTestCaseDiscoverer
	{
		readonly FactDiscoverer factDiscoverer;

		public StaFactDiscoverer (IMessageSink diagnosticMessageSink)
		{
			factDiscoverer = new FactDiscoverer (diagnosticMessageSink);
		}

		public IEnumerable<IXunitTestCase> Discover (ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
		{
			return factDiscoverer.Discover (discoveryOptions, testMethod, factAttribute)
								 .Select (testCase => new StaTestCase (testCase));
		}
	}

	[AttributeUsage (AttributeTargets.Method, AllowMultiple = false)]
#pragma warning disable 0436
	[XunitTestCaseDiscoverer ("Merq.StaFactDiscoverer", ThisAssembly.Project.AssemblyName)]
#pragma warning restore 0436
	public class StaFactAttribute : FactAttribute { }

	/// <summary>
	/// Wraps test cases for FactAttribute and TheoryAttribute so the test case runs on the WPF STA thread
	/// </summary>
	[DebuggerDisplay (@"\{ class = {TestMethod.TestClass.Class.Name}, method = {TestMethod.Method.Name}, display = {DisplayName}, skip = {SkipReason} \}")]
	public class StaTestCase : LongLivedMarshalByRefObject, IXunitTestCase
	{
		IXunitTestCase testCase;

		public StaTestCase (IXunitTestCase testCase)
		{
			this.testCase = testCase;
		}

		/// <summary/>
		[EditorBrowsable (EditorBrowsableState.Never)]
		[Obsolete ("Called by the de-serializer", error: true)]
		public StaTestCase () { }

		public IMethodInfo Method => testCase.Method;

		public Task<RunSummary> RunAsync (IMessageSink diagnosticMessageSink,
										 IMessageBus messageBus,
										 object[] constructorArguments,
										 ExceptionAggregator aggregator,
										 CancellationTokenSource cancellationTokenSource)
		{
			var tcs = new TaskCompletionSource<RunSummary>();
			var thread = new Thread(() =>
			{
				try
				{
                    // Set up the SynchronizationContext so that any awaits
                    // resume on the STA thread as they would in a GUI app.
                    SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());

                    // Start off the test method.
                    var testCaseTask = this.testCase.RunAsync(diagnosticMessageSink, messageBus, constructorArguments, aggregator, cancellationTokenSource);

                    // Arrange to pump messages to execute any async work associated with the test.
                    var frame = new DispatcherFrame();
					Task.Run(async delegate
					{
						try
						{
							await testCaseTask;
						}
						finally
						{
                            // The test case's execution is done. Terminate the message pump.
                            frame.Continue = false;
						}
					});
					Dispatcher.PushFrame(frame);

                    // Report the result back to the Task we returned earlier.
                    CopyTaskResultFrom(tcs, testCaseTask);
				}
				catch (Exception e)
				{
					tcs.SetException(e);
				}
			});

			thread.SetApartmentState (ApartmentState.STA);
			thread.Start ();
			return tcs.Task;
		}

		public string DisplayName => testCase.DisplayName;

		public string SkipReason => testCase.SkipReason;

		public ISourceInformation SourceInformation
		{
			get { return testCase.SourceInformation; }
			set { testCase.SourceInformation = value; }
		}

		public ITestMethod TestMethod => testCase.TestMethod;

		public object[] TestMethodArguments => testCase.TestMethodArguments;

		public Dictionary<string, List<string>> Traits => testCase.Traits;

		public string UniqueID => testCase.UniqueID;

		public void Deserialize (IXunitSerializationInfo info)
		{
			testCase = info.GetValue<IXunitTestCase> ("InnerTestCase");
		}

		public void Serialize (IXunitSerializationInfo info)
		{
			info.AddValue ("InnerTestCase", testCase);
		}

		static void CopyTaskResultFrom<T>(TaskCompletionSource<T> tcs, Task<T> template)
		{
			if (tcs == null)
				throw new ArgumentNullException (nameof (tcs));
			if (template == null)
				throw new ArgumentNullException (nameof (template));
			if (!template.IsCompleted)
				throw new ArgumentException ("Task must be completed first.", nameof (template));

			if (template.IsFaulted)
				tcs.SetException (template.Exception);
			else if (template.IsCanceled)
				tcs.SetCanceled ();
			else
				tcs.SetResult (template.Result);
		}
	}
}
