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
    using OPTANO.Modeling.Optimization;

    /// <summary>
    /// Class for extracting all information from the solver solution.
    /// </summary>
    public interface ISolutionExtractor
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the problem solution from the solver solution.
        /// </summary>
        /// <param name="solverSolution">The solver solution.</param>
        /// <param name="model">The model.</param>
        /// <param name="modelVariables">The model variables.</param>
        /// <returns>
        /// the solution for the optimization problem.
        /// </returns>
        ProblemSolution GetSolution(Solution solverSolution, Model model, ModelVariables modelVariables);

        #endregion
    }
}