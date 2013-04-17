Constant
========

Visit the [Constant website](https://github.com/projecteon/Constant) for more information.

### What is it?
Constant enables developers to create [smart enums](http://shashankshetty.wordpress.com/2010/07/18/smart-enums/) in C# and is implemented using .NET portable class libraries.  

### Basic use

Creating constants:

<!-- {% examplecode csharp %} -->
	private class Producttype : Constant<Producttype>
	{
		[DefaultKey]
		public static readonly Producttype Default = new Producttype("default", false);
		public static readonly Producttype Box = new Producttype("box", true);

		private TestableConstant(string key)
		{
			this.Add(key, this);
		}

		public bool ValidType { get; private set; }
	}
<!-- {% endexamplecode %} -->

Using constants:
<!-- {% examplecode csharp %} -->
	public class Product
	{
		private Producttype producttype;
		public void Validate()
		{
			if(prodtuctype.IsValid)
			{
				return true;
			}

			return false;
		}	
	}
<!-- {% endexamplecode %} -->

###Motivation
The project was created because I found I had needs for this in quote a few projects and got tired of movign the code around. Making it to a NuGet package made using this much easier.

###Installation
The project can be installed easiliy via NuGet or by downloading and compiling the source yourself.

###License
See Lisence.txt