using System;

namespace FinalizeExceptionsDemoNetCore
{
	public class ProgramNetCore
	{
		public static void Main(string[] args)
		{
			AllocateFinalizableObject();

			Console.WriteLine("Let's do the GC!");

			GC.Collect();
			GC.WaitForPendingFinalizers();

			Console.WriteLine("Still alive!");
			Console.Read();
		}

		private static void AllocateFinalizableObject()	// needed for .NET Core where the instance is still held in regs in debug
		{
			var x = new MyClass(1);
			x = new MyClass(2);
		}
	}

	public class MyClass
	{
		private readonly int id;

		public MyClass(int id)
		{
			this.id = id;
		}

		~MyClass()
		{
			Console.WriteLine($"Hello from Finalize:{id}...");

			MethodWithException();

			//throw new Exception($"ExceptionFromFinalize:{id}");
		}

		private void MethodWithException()
		{
			throw new Exception($"ExceptionFromMethodCalledFromFinalize:{id}");
		}
	}
}
