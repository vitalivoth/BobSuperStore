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
    using OPTANO.Modeling.Optimization;

    /// <summary>
    /// Contains method to set the values for the <see cref="SolverConfiguration"/>.
    /// </summary>
    public static class SolverConfigurationSetter
    {
        #region Public Methods and Operators

        /// <summary>
        /// Sets the configuration values.
        /// </summary>
        /// <param name="solverConfiguration">The solver configuration.</param>
        /// <param name="solvingOptions">The solving options.</param>
        public static void SetConfigurationValues(SolverConfiguration solverConfiguration, SolvingOptions solvingOptions)
        {
            if (solvingOptions.MaximalRunTime.HasValue)
            {
                solverConfiguration.TimeLimit = solvingOptions.MaximalRunTime.Value.TotalSeconds;
            }

            if (solvingOptions.Gap.HasValue)
            {
                solverConfiguration.MIPGap = solvingOptions.Gap.Value;
            }
        }

        #endregion
    }
}