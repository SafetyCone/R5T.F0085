using System;
using System.Threading.Tasks;

using R5T.F0024;
using R5T.F0024.T001;
using R5T.T0132;


namespace R5T.F0085
{
	[FunctionalityMarker]
	public partial interface ISolutionFileOperations : IFunctionalityMarker
	{
		public async Task CreateNew_VS2022(
			string solutionFilePath,
			Func<SolutionFile, Task> solutionFileAction = default)
		{
			await this.CreateNew_Initial(
				solutionFilePath,
				Instances.ActionOperator.Get_RunMultiple(
					SolutionFileObjectOperations.Instance.Setup_VS2022_New,
					solutionFileAction));
		}

		/// <summary>
		/// Creates a new <see cref="SolutionFile"/> object in its initial state (with unspecified version information), runs the provided action on it, then saves the object to the provided path.
		/// </summary>
		public async Task CreateNew_Initial(
			string solutionFilePath,
			Func<SolutionFile, Task> solutionFileAction = default)
		{
			var solutionFile = new SolutionFile()
				.WithVersionInformation(VersionInformationOperations.Instance.Create_Unspecified())
				;

			await Instances.ActionOperator.Run(
				solutionFileAction,
				solutionFile);

			SolutionFileSerializer.Instance.Serialize(
				solutionFilePath,
				solutionFile);
		}

		public async Task ModifySolutionFile(
			string solutionFilePath,
			Func<SolutionFile, Task> solutionFileAction = default)
		{
			await F0024.SolutionFileOperator.Instance.InModifyContext(
				solutionFilePath,
				// Ignore the solution file path.
				async (solutionFile, _) =>
				{
					await F0000.ActionOperator.Instance.Run(
						solutionFileAction,
						solutionFile);
				});
		}

        public async Task ModifySolutionFile(
            string solutionFilePath,
            Action<SolutionFile> solutionFileAction = default)
        {
            await F0024.SolutionFileOperator.Instance.InModifyContext(
                solutionFilePath,
                // Ignore the solution file path.
                (solutionFile, _) =>
                {
                    F0000.ActionOperator.Instance.Run(
                        solutionFileAction,
                        solutionFile);

					return Task.CompletedTask;
                });
        }

        public void UpgradeSolutionFile_ToVS2022(SolutionFile solutionFile)
		{
			solutionFile.VersionInformation = VersionInformationOperations.Instance.Create_VS2022();
		}

        public async Task UpgradeSolutionFile_ToVS2022(string solutionFilePath)
		{
			await this.ModifySolutionFile(
				solutionFilePath,
				this.UpgradeSolutionFile_ToVS2022);
		}

    }
}