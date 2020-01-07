using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Geeky.VanityRemover.Properties
{
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = resourceMan = new ResourceManager("Geeky.VanityRemover.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static Bitmap browse
		{
			get
			{
				object @object = ResourceManager.GetObject("browse", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap go
		{
			get
			{
				object @object = ResourceManager.GetObject("go", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap stop
		{
			get
			{
				object @object = ResourceManager.GetObject("stop", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal Resources()
		{
		}
	}
}
