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

namespace BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.SolutionExtractor
{
    using BobSuperStores.Data.Logging;

    using OPTANO.Modeling.Common;
    using OPTANO.Modeling.Optimization;
    using OPTANO.Modeling.Optimization.Solver;

    /// <summary>
    /// Default class for extracting the solution information. The problem solution stores the gap, the time and the number of the solutions found, as well as if the problem could be solved.
    /// </summary>
    /// <seealso cref="ISolutionExtractor" />
    public class SolutionExtractor : ISolutionExtractor
    {
        #region Fields

        /// <summary>
        /// The optano logger.
        /// </summary>
        private readonly IOptanoLogger _optanoLogger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionExtractor"/> class.
        /// </summary>
        /// <param name="optanoLogger">The optano logger.</param>
        public SolutionExtractor(IOptanoLogger optanoLogger)
        {
            this._optanoLogger = optanoLogger;
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public ProblemSolution GetSolution(Solution solverSolution, Model model, ModelVariables modelVariables)
        {
            ProblemSolution problemSolution;
            if (solverSolution.Status != SolutionStatus.NoSolutionValues)
            {
                if (solverSolution.VariableValues.Any())
                {
                    model.VariableCollections.ForEach(vc => vc.SetVariableValues(solverSolution.VariableValues));
                }

                this._optanoLogger.Log(LogLevel.Information, "Found solution");
                problemSolution = new ProblemSolution(solverSolution, modelVariables);
            }
            else
            {
                problemSolution = new ProblemSolution(false);

                if (solverSolution.ConflictingSet != null)
                {
                    var conflictingSet = solverSolution.ConflictingSet.ToString();
                    this._optanoLogger.Log(LogLevel.Warning, "Solution is infeasible with IIS:\n {0}", conflictingSet);
                }
            }

            return problemSolution;
        }

        #endregion
    }
}