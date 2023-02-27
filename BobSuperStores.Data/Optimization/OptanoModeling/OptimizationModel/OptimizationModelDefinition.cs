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
    using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ConstraintCreators;
    using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ObjectiveCreators;
    using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.StartValueCalculation;
    using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.VariableCreators;
    using BobSuperStores.Data.Optimization.OptanoModeling.Solver;

    using OPTANO.Modeling.Optimization.Interfaces;

    /// <summary>
    /// All information needed to create the optimization problem with modeling.
    /// </summary>
    /// <seealso cref="IOptimizationModelDefinition" />
    public class OptimizationModelDefinition : IOptimizationModelDefinition
    {
        #region Fields

        private readonly ISolverProvider _solverProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimizationModelDefinition" /> class.
        /// </summary>
        /// <param name="solverProvider">The solver provider.</param>
        /// <param name="variableCreators">The variable creators.</param>
        /// <param name="constraintCreators">The constraint creators.</param>
        /// <param name="objectiveCreators">The objective creators.</param>
        /// <param name="startValueCalculator">The start value calculator.</param>
        public OptimizationModelDefinition(
            ISolverProvider solverProvider,
            IEnumerable<IVariableInitializer> variableCreators,
            IEnumerable<ICreateConstraints> constraintCreators,
            IEnumerable<ICreateObjectives> objectiveCreators,
            IStartValueCalculator startValueCalculator)
        {
            this._solverProvider = solverProvider;
            this.StartValueCalculator = startValueCalculator;
            this.VariableCreators = variableCreators;
            this.ObjectiveFunctionCreators = objectiveCreators;
            this.ConstraintCreators = constraintCreators;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the start value calculator.
        /// </summary>
        public IStartValueCalculator StartValueCalculator { get; }

        /// <summary>
        /// Gets the variable creators.
        /// </summary>
        public IEnumerable<IVariableInitializer> VariableCreators { get; }

        /// <summary>
        /// Gets the objective function creators.
        /// </summary>
        public IEnumerable<ICreateObjectives> ObjectiveFunctionCreators { get; }

        /// <summary>
        /// Gets the constraint creators.
        /// </summary>
        public IEnumerable<ICreateConstraints> ConstraintCreators { get; }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public ISolver SolverCreationFunction()
        {
            return this._solverProvider.GetSolver();
        }

        #endregion
    }
}