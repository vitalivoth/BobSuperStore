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

namespace BobSuperStores.Data.Optimization.OptanoModeling
{
    using BobSuperStores.Data.Logging;
    using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel;
    using BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding;

    /// <summary>
    /// Solver for a problem started with the scheduler, containing an optimization problem to be solved.
    /// </summary>
    /// <seealso cref="IProblemSolver" />
    public class ProblemSolver : IProblemSolver
    {
        #region Fields

        private readonly IModelData _modelData;

        private readonly IOptanoLogger _logger;

        private readonly IOptimizationProblemSolver _optimizationProblemSolver;

        private readonly IResultBuilder _resultBuilder;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemSolver" /> class.
        /// </summary>
        /// <param name="modelData">the model data.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="optimizationProblemSolver">The optimization problem solver.</param>
        /// <param name="resultBuilder">The postprocessor.</param>
        public ProblemSolver(
            IModelData modelData,
            IOptanoLogger logger,
            IOptimizationProblemSolver optimizationProblemSolver,
            IResultBuilder resultBuilder)
        {
            this._modelData = modelData;
            this._logger = logger;
            this._optimizationProblemSolver = optimizationProblemSolver;
            this._resultBuilder = resultBuilder;
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public bool HandleProblem()
        {
            var success = true;
            try
            {
                this._logger.Log(LogLevel.Information, "Start solving");
                // solve optimization problem
                this._logger.Log(LogLevel.Information, "Starting Optimization");
                var solution = this._optimizationProblemSolver.SolveProblem();
                success = solution.IsSolvingSuccessful;

                // storing of results
                this._logger.Log(LogLevel.Information, "Starting deleting old results and building new results");
                success &= this._resultBuilder.BuildResultData(this._modelData, solution);
            }
            catch (Exception exception)
            {
                success = false;
                this._logger.LogException(exception);
            }

            this._logger.Log(LogLevel.Information, $"Optimization Problem solved with {(success ? "" : "no ")} success.");
            return success;
        }

        #endregion
    }
}