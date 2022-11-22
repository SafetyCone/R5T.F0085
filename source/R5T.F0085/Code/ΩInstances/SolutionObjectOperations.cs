using System;


namespace R5T.F0085
{
	public class SolutionObjectOperations : ISolutionObjectOperations
	{
		#region Infrastructure

	    public static ISolutionObjectOperations Instance { get; } = new SolutionObjectOperations();

	    private SolutionObjectOperations()
	    {
        }

	    #endregion
	}
}