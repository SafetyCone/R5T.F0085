using System;
using System.Threading.Tasks;

using R5T.F0024;
using R5T.F0024.T001;
using R5T.T0132;


namespace R5T.F0085
{
	[FunctionalityMarker]
	public partial interface ISolutionFileObjectOperations : IFunctionalityMarker
	{
        public Task Setup_VS2022_New(SolutionFile solutionFile)
        {
			this.Setup_VS2022_New_Synchronous(solutionFile);

			return Task.CompletedTask;
        }

        public void Setup_VS2022_New_Synchronous(SolutionFile solutionFile)
		{
			this.SetVersionInformation_VS2022(solutionFile);
			this.SetSolutionProperties_Default(solutionFile);
			this.SetExtensibilityGlobals_Default(solutionFile);
		}

        public void SetExtensibilityGlobals_Default(SolutionFile solutionFile)
        {
            var extensibilityGlobals = GlobalSectionGenerator.Instance.ExtensibilityGlobals_GetDefault();

            solutionFile.AddGlobalSection(extensibilityGlobals);
        }

        public void SetSolutionProperties_Default(SolutionFile solutionFile)
		{
			var solutionProperties = GlobalSectionGenerator.Instance.SolutionProperties_GetDefault();

			solutionFile.AddGlobalSection(solutionProperties);
		}

        public void SetVersionInformation_VS2022(SolutionFile solutionFile)
		{
			var versionInformation = VersionInformationOperations.Instance.Create_VS2022();

			solutionFile.WithVersionInformation(versionInformation);
		}
    }
}