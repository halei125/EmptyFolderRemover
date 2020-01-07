using System;
using System.IO;
using System.Threading;

namespace Geeky.VanityRemover.Core
{
	public class Cleaner : ICleaner, IContextAware
	{
		private readonly object padLock = new object();

		private Thread cleaningThread;

		private SynchronizationContext context;

		private volatile bool isCleaning;

		private volatile bool cancel;

		private volatile uint totalDeleted;

		private volatile uint totalScanned;

		public SynchronizationContext Context
		{
			get
			{
				return context;
			}
			set
			{
				context = (value ?? new SynchronizationContext());
			}
		}

		private event EventHandler<CleaningDoneEventArgs> CleaningDone = delegate
		{
		};

		event EventHandler<CleaningDoneEventArgs> ICleaner.CleaningDone
		{
			add
			{
				lock (padLock)
				{
					this.CleaningDone = (EventHandler<CleaningDoneEventArgs>)Delegate.Combine(this.CleaningDone, value);
				}
			}
			remove
			{
				lock (padLock)
				{
					this.CleaningDone = (EventHandler<CleaningDoneEventArgs>)Delegate.Remove(this.CleaningDone, value);
				}
			}
		}

		public Cleaner()
		{
			context = new SynchronizationContext();
		}

		bool ICleaner.Clean(DirectoryInfo directory)
		{
			lock (padLock)
			{
				if (isCleaning)
				{
					return false;
				}
				isCleaning = true;
			}
			StartCleaningThread(directory);
			return true;
		}

		void ICleaner.Cancel()
		{
			lock (padLock)
			{
				cancel = true;
			}
		}

		private void StartCleaningThread(DirectoryInfo directory)
		{
			cleaningThread = new Thread(DoCleaning)
			{
				Name = "Cleaning thread",
				IsBackground = false
			};
			cleaningThread.Start(directory);
		}

		private void DoCleaning(object directory)
		{
			totalScanned = 0u;
			totalDeleted = 0u;
			cancel = false;
			DeleteEmptyDirectories(directory as DirectoryInfo);
			DoneCleaning();
		}

		private void DeleteEmptyDirectories(DirectoryInfo directory)
		{
			totalScanned++;
			if (!cancel && directory.Exists)
			{
				try
				{
					DirectoryInfo[] directories = directory.GetDirectories();
					foreach (DirectoryInfo directory2 in directories)
					{
						DeleteEmptyDirectories(directory2);
					}
				}
				catch (Exception)
				{
					return;
				}
				if (!directory.IsNotEmpty())
				{
					try
					{
						directory.Delete();
						totalDeleted++;
					}
					catch (Exception)
					{
					}
				}
			}
		}

		private void DoneCleaning()
		{
			InvokeCleaningDone();
			lock (padLock)
			{
				isCleaning = false;
			}
		}

		private void InvokeCleaningDone()
		{
			CleaningDoneEventArgs state = new CleaningDoneEventArgs(totalScanned, totalDeleted);
			context.Post(delegate(object e)
			{
				this.CleaningDone(this, e as CleaningDoneEventArgs);
			}, state);
		}
	}
}
