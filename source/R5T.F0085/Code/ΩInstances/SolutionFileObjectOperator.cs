using System;


namespace R5T.F0085
{
	public class SolutionFileObjectOperator : ISolutionFileObjectOperator
	{
		#region Infrastructure

	    public static ISolutionFileObjectOperator Instance { get; } = new SolutionFileObjectOperator();

	    private SolutionFileObjectOperator()
	    {
        }

	    #endregion
	}
}