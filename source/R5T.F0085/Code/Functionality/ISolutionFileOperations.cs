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
	}
}