using System;
using System.Collections.Generic;

using R5T.F0000;
using R5T.F0024.T001;
using R5T.T0132;


namespace R5T.F0085
{
	[FunctionalityMarker]
	public partial interface ISolutionFileObjectOperator : IFunctionalityMarker
	{
        public Guid AddProject(
            SolutionFile solutionFile,
            string solutionFilePath,
            string projectFilePath)
        {
            var projectIdentity = F0024.SolutionFileOperator.Instance.AddProject(
                solutionFile,
                solutionFilePath,
                projectFilePath);

            return projectIdentity;
        }

        public SolutionFile CreateSolutionFile(
            IEnumerable<Action<SolutionFile>> modifiers)
        {
            var solutionFile = ConstructionOperator.Instance.Create(
                SolutionFileObjectOperations.Instance.New,
                modifiers);

            return solutionFile;
        }
    }
}