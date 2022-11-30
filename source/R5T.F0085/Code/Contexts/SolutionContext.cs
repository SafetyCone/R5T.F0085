using System;

using R5T.T0142;


namespace R5T.F0085.T001
{
    [DataTypeMarker]
    public class SolutionContext : SolutionFileContext
    {
        public string SolutionName { get; set; }
        public string SolutionDescription { get; set; }
    }
}
