using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.F0024;
using R5T.F0024.T001;
using R5T.T0132;


namespace R5T.F0085
{
	[FunctionalityMarker]
	public partial interface ISolutionFileOperations : IFunctionalityMarker
	{
        public async Task NewSolutionFile_VS2022_NoActions(string solutionFilePath)
        {
            await SolutionFileOperator.Instance.CreateSolutionFile(
                solutionFilePath,
                SolutionFileObjectOperations.Instance.SetupSolutionFile_VS2022_NoActions);
        }

        public async Task NewSolutionFile_VS2022(
			string solutionFilePath,
			IEnumerable<Action<SolutionFile>> solutionFileActions)
		{
            await SolutionFileOperator.Instance.CreateSolutionFile(
                solutionFilePath,
                SolutionFileObjectOperations.Instance.SetupSolutionFile_VS2022(solutionFileActions));
        }

        public async Task NewSolutionFile_VS2022(
			string solutionFilePath,
            params Action<SolutionFile>[] solutionFileActions)
		{
			await this.NewSolutionFile_VS2022(
				solutionFilePath,
				solutionFileActions.AsEnumerable());
		}

        /// <summary>
        /// Creates a new <see cref="SolutionFile"/> object in its initial state (with unspecified version information), runs the provided action on it, then saves the object to the provided path.
        /// </summary>
        public async Task NewSolutionFile_Initial(string solutionFilePath)
		{
            await SolutionFileOperator.Instance.CreateSolutionFile(
                solutionFilePath,
                SolutionFileObjectOperations.Instance.SetupSolutionFile_Initial);
        }

		public VisualStudioVersion Get_VisualStudioVersion(string solutionFilePath)
		{
			var visualStudioVersion = Instances.SolutionFileOperator.InQueryContext_Synchronous(
				solutionFilePath,
				this.Get_VisualStudioVersion);

			return visualStudioVersion;
		}

        public VisualStudioVersion Get_VisualStudioVersion_OrUnknown(string solutionFilePath)
        {
			try
			{
				var visualStudioVersion = Instances.SolutionFileOperator.InQueryContext_Synchronous(
					solutionFilePath,
					this.Get_VisualStudioVersion);

				return visualStudioVersion;
			}
			catch (Exception)
			{
				return VisualStudioVersion.Unknown;
			}
        }

        public VisualStudioVersion Get_VisualStudioVersion(SolutionFile solutionFile)
        {
            var versionDescription = solutionFile.VersionInformation.VersionDescription;

            var versionString = versionDescription.Split(
                Z0000.Characters.Instance.Space)
                .Last();

            var visualStudioVersion = versionString switch
            {
                IVisualStudioVersionStrings.Version_15_Constant => VisualStudioVersion.Version_2017,
                IVisualStudioVersionStrings.Version_16_Constant => VisualStudioVersion.Version_2019,
                IVisualStudioVersionStrings.Version_17_Constant => VisualStudioVersion.Version_2022,
                _ => throw F0000.SwitchOperator.Instance.GetUnrecognizedSwitchValueException(versionString, "Visual Studio Version String"),
            };

            return visualStudioVersion;
        }

		public bool Is_VisualStudio2022(string solutionFilePath)
		{
			var visualStudioVersion = this.Get_VisualStudioVersion(solutionFilePath);

			var output = VisualStudioVersion.Version_2022 == visualStudioVersion;
			return output;
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

		public async Task NewSolutionFile_Empty(string solutionFilePath)
		{
			await SolutionFileOperator.Instance.CreateSolutionFile(
				solutionFilePath,
				SolutionFileObjectOperations.Instance.SetupSolutionFile_Empty);
		}

        public void UpgradeSolutionFile_ToVS2022(SolutionFile solutionFile)
		{
			solutionFile.VersionInformation = VersionInformationOperations.Instance.Create_VS2022();
		}

        public async Task UpgradeSolutionFile_ToVS2022(
			string solutionFilePath,
			bool createBackupFile = false)
		{
			if(createBackupFile)
			{
				F0002.FileSystemOperator.Instance.CreateBackupFile(
					solutionFilePath);
			}

			// Now upgrade.
            await this.ModifySolutionFile(
				solutionFilePath,
				this.UpgradeSolutionFile_ToVS2022);
		}

    }
}