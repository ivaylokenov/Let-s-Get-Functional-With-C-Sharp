namespace Concepts.Expressions
{
    public class ExpressionsExample
    {
		public static string GetSalutation(int hour)
		{
			string salutation; // placeholder value

			if (hour < 12)
			{
				salutation = "Good Morning";
			}
            else
            {
				salutation = "Good Afternoon";
			}

			return salutation; // return mutated variable
		}
	
		public static string GetSalutationExpression(int hour)
			=> hour < 12 ? "Good Morning" : "Good Afternoon";
	}
}
