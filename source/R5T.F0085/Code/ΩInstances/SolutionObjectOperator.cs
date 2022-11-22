using System;


namespace R5T.F0085
{
	public class SolutionObjectOperator : ISolutionObjectOperator
	{
		#region Infrastructure

	    public static ISolutionObjectOperator Instance { get; } = new SolutionObjectOperator();

	    private SolutionObjectOperator()
	    {
        }

	    #endregion
	}
}