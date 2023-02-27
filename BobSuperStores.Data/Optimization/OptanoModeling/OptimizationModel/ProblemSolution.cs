#region Copyright (c) OPTANO GmbH

// ////////////////////////////////////////////////////////////////////////////////
// 
//        OPTANO GmbH Source Code
//        Copyright (c) 2010-2023 OPTANO GmbH
//        ALL RIGHTS RESERVED.
// 
//    The entire contents of this file is protected by German and
//    International Copyright Laws. Unauthorized reproduction,
//    reverse-engineering, and distribution of all or any portion of
//    the code contained in this file is strictly prohibited and may
//    result in severe civil and criminal penalties and will be
//    prosecuted to the maximum extent possible under the law.
// 
//    RESTRICTIONS
// 
//    THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES
//    ARE CONFIDENTIAL AND PROPRIETARY TRADE SECRETS OF
//    OPTANO GMBH.
// 
//    THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED
//    FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE
//    COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE
//    AVAILABLE TO OTHER INDIVIDUALS WITHOUT WRITTEN CONSENT
//    AND PERMISSION FROM OPTANO GMBH.
// 
// ////////////////////////////////////////////////////////////////////////////////

#endregion

namespace BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel
{
    using OPTANO.Modeling.Optimization;
    using OPTANO.Modeling.Optimization.Solver;

    /// <summary>
    /// All information from solving the optimization problem are stored here.
    /// </summary>
    public class ProblemSolution
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemSolution"/> class.
        /// </summary>
        /// <param name="success">If solving the problem was successful.</param>
        public ProblemSolution(bool success = false)
        {
            this.IsSolvingSuccessful = success;
            this.Gap = null;
            this.Runtime = null;
            this.NumberOfSolutionsFound = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemSolution" /> class.
        /// </summary>
        /// <param name="solution">The solution.</param>
        /// <param name="modelVariables">The model variables.</param>
        public ProblemSolution(Solution solution, ModelVariables modelVariables)
            : this()
        {
            this.ModelVariables = modelVariables;
            this.IsSolvingSuccessful = solution.Status != SolutionStatus.NoSolutionValues;
            this.Gap = solution.Gap;
            this.Runtime = solution.OverallWallTime;
            this.NumberOfSolutionsFound = solution.NumberOfExploredNodes;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets a value indicating whether solving was successful.
        /// </summary>
        public bool IsSolvingSuccessful { get; }

        /// <summary>
        /// Gets the gap.
        /// </summary>
        public double? Gap { get; }

        /// <summary>
        /// Gets the runtime.
        /// </summary>
        public TimeSpan? Runtime { get; }

        /// <summary>
        /// Gets the number of solutions found.
        /// </summary>
        public long? NumberOfSolutionsFound { get; }

        /// <summary>
        /// Gets the model variables.
        /// </summary>
        public ModelVariables ModelVariables { get; }

        #endregion
    }
}