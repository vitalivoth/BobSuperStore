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

    using JetBrains.Annotations;

    using OPTANO.Modeling.Optimization.Interfaces;

    /// <summary>
    /// The definition containing all information for creating the optimization model.
    /// </summary>
    public interface IOptimizationModelDefinition
    {
        #region Public properties

        /// <summary>
        /// Gets the start value calculator.
        /// </summary>
        [NotNull]
        IStartValueCalculator StartValueCalculator { get; }

        /// <summary>
        /// Gets the variable creators.
        /// </summary>
        [NotNull, ItemNotNull]
        IEnumerable<IVariableInitializer> VariableCreators { get; }

        /// <summary>
        /// Gets the objective function creators.
        /// </summary>
        [NotNull, ItemNotNull]
        IEnumerable<ICreateObjectives> ObjectiveFunctionCreators { get; }

        /// <summary>
        /// Gets the constraint creators.
        /// </summary>
        [NotNull, ItemNotNull]
        IEnumerable<ICreateConstraints> ConstraintCreators { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Solvers the creation function.
        /// </summary>
        /// <returns>the solver to use to solve the optimization problem.</returns>
        [NotNull]
        ISolver SolverCreationFunction();

        #endregion
    }
}