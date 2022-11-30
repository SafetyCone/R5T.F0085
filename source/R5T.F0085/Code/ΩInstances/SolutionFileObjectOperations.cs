using System;


namespace R5T.F0085
{
	public class SolutionFileObjectOperations : ISolutionFileObjectOperations
	{
		#region Infrastructure

	    public static ISolutionFileObjectOperations Instance { get; } = new SolutionFileObjectOperations();

	    private SolutionFileObjectOperations()
	    {
        }

	    #endregion
	}
}