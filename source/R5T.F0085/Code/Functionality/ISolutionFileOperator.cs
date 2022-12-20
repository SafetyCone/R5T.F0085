using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.F0024;
using R5T.F0024.T001;
using R5T.T0132;


namespace R5T.F0085
{
	[FunctionalityMarker]
	public partial interface ISolutionFileOperator : IFunctionalityMarker
	{
		public async Task CreateSolutionFile(
			string solutionFilePath,
			Func<SolutionFile> solutionFileConstructor)
		{
			var solutionFile = solutionFileConstructor();

			await SolutionFileSerializer.Instance.Serialize(
				solutionFilePath,
				solutionFile);
		}

		public async Task CreateSolutionFile(
			string solutionFilePath,
			IEnumerable<Action<SolutionFile>> solutionFileActions)
		{
			var solutionFile = SolutionFileObjectOperator.Instance.CreateSolutionFile(
				solutionFileActions);

			await SolutionFileSerializer.Instance.Serialize(
				solutionFilePath,
                solutionFile);
		}

        public async Task CreateSolutionFile(
            string solutionFilePath,
            Func<IEnumerable<Action<SolutionFile>>> solutionFileActionsConstructor)
        {
			var solutionFileActions = solutionFileActionsConstructor();

			await this.CreateSolutionFile(
				solutionFilePath,
				solutionFileActions);
        }
    }
}