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

namespace BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding
{
    using BobSuperStores.Data.Logging;
    using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel;
    using BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding.ResultCreators;

    /// <summary>
    /// Postprocessing of the data after the optimization is run.
    /// </summary>
    /// <seealso cref="IResultBuilder" />
    public class ResultBuilder : IResultBuilder
    {
        #region Fields

        private readonly IEnumerable<ICreateResults> _resultCreators;

        private readonly IOptanoLogger _logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultBuilder" /> class.
        /// </summary>
        /// <param name="resultCreators">The result creators.</param>
        /// <param name="logger">the logger to use.</param>
        public ResultBuilder(IEnumerable<ICreateResults> resultCreators, IOptanoLogger logger)
        {
            this._resultCreators = resultCreators;
            this._logger = logger;
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public bool BuildResultData(IModelData modelData, ProblemSolution solution)
        {
            var success = true;
            if (solution.IsSolvingSuccessful)
            {
                this._logger.Log(LogLevel.Information, "Start writing results");
                success = this.CreateResultsInScenario(modelData, solution);
            }

            return success;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the results in scenario.
        /// </summary>
        /// <param name="modelData">The model data.</param>
        /// <param name="solution">The solution.</param>
        /// <returns>if the creation of the results was successful.</returns>
        private bool CreateResultsInScenario(IModelData modelData, ProblemSolution solution)
        {
            var success = this._resultCreators.All(rc => rc.CreateResults(modelData, solution));
            return success;
        }

        #endregion
    }
}