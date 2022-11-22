using System;


namespace R5T.F0085
{
	public class SolutionFileOperations : ISolutionFileOperations
	{
		#region Infrastructure

	    public static ISolutionFileOperations Instance { get; } = new SolutionFileOperations();

	    private SolutionFileOperations()
	    {
        }

	    #endregion
	}
}