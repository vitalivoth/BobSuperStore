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

namespace BobSuperStores.Data.Optimization.OptanoModeling.Solver
{
    using JetBrains.Annotations;

    using OPTANO.Modeling.Optimization.Interfaces;
    using OPTANO.Modeling.Optimization.Solver.Gurobi;
    using OPTANO.Modeling.Optimization.Solver.Gurobi1000;

    /// <summary>
    /// Provider for the gurobi solver.
    /// </summary>
    /// <seealso cref="ISolverProvider" />
    public class GurobiSolverProvider : ISolverProvider
    {
        #region Fields

        [CanBeNull]
        private readonly ISolverCallback _solverCallback;

        [NotNull]
        private readonly SolvingOptions _solvingOptions;

        [NotNull]
        private readonly GurobiSolverConfiguration _solverConfig;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GurobiSolverProvider"/> class.
        /// </summary>
        /// <param name="solvingOptions">The solving options.</param>
        /// <param name="solverConfig">The solver config to use for the solver, if none is given, the default values are used.</param>
        /// <param name="solverCallback">The solver callback.</param>
        public GurobiSolverProvider(
            [NotNull] SolvingOptions solvingOptions,
            [CanBeNull] GurobiSolverConfiguration solverConfig = null,
            [CanBeNull] ISolverCallback solverCallback = null)
        {
            this._solverCallback = solverCallback;
            this._solvingOptions = solvingOptions;
            this._solverConfig = solverConfig ?? new GurobiSolverConfiguration()
                                                     {
                                                         ComputeIIS = true,
                                                         LogToConsole = false,
                                                         LimitedLicenseUsage = LimitedLicenseUsage.Fallback,
                                                     };
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public ISolver GetSolver()
        {
            SolverConfigurationSetter.SetConfigurationValues(this._solverConfig, this._solvingOptions);

            var solver = new GurobiSolver(this._solverConfig);
            var solverCallback = this._solverCallback;
            if (solverCallback != null)
            {
                solver.Status += solverCallback.Callback;
            }

            return solver;
        }

        #endregion
    }
}