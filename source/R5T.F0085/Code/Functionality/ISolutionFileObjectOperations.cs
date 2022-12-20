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

		public SolutionFile New()
		{
			var solutionFile = new SolutionFile();
			return solutionFile;
		}

        public IEnumerable<Action<SolutionFile>> SetupSolutionFile_VS2022_NoActions()
        {
            var output = new Action<SolutionFile>[]
            {
                this.SetVersionInformation_VS2022,
                this.SetSolutionProperties_Default,
                this.SetExtensibilityGlobals_Default,
            };

            return output;
        }

        public IEnumerable<Action<SolutionFile>> SetupSolutionFile_VS2022(
            IEnumerable<Action<SolutionFile>> solutionFileActions)
        {
            var output = new Action<SolutionFile>[]
            {
                this.SetVersionInformation_VS2022,
                this.SetSolutionProperties_Default,
                this.SetExtensibilityGlobals_Default,
            }
            .Append(solutionFileActions);

            return output;
        }

        public IEnumerable<Action<SolutionFile>> SetupSolutionFile_VS2022(
            params Action<SolutionFile>[] solutionFileActions)
        {
            return this.SetupSolutionFile_VS2022(
                solutionFileActions.AsEnumerable());
        }

        public IEnumerable<Action<SolutionFile>> SetupSolutionFile_Initial()
        {
            // Initially, just set all version information to unspecified.
            var output = new Action<SolutionFile>[]
            {
                this.SetVersionInformation_Unspecified
            };

            return output;
        }

        public IEnumerable<Action<SolutionFile>> SetupSolutionFile_Empty()
        {
			// Need to set *some* version information.
			var output = new Action<SolutionFile>[]
			{
				this.SetVersionInformation_Unspecified
			};

            return output;
        }

		public void SetVersionInformation_Unspecified(SolutionFile solutionFile)
		{
			solutionFile.VersionInformation = VersionInformationOperations.Instance.Create_Unspecified();
        }

        public void SetVersionInformation_VS2022(SolutionFile solutionFile)
        {
            var versionInformation = VersionInformationOperations.Instance.Create_VS2022();

            solutionFile.WithVersionInformation(versionInformation);
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
    }
}