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
    /// <summary>
    /// Epsilon handling for the values returned from the optimization.
    /// </summary>
    public class EpsilonHandling
    {
        #region Fields

        /// <summary>
        /// The solving options.
        /// </summary>
        private readonly SolvingOptions _solvingOptions;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EpsilonHandling"/> class.
        /// </summary>
        /// <param name="solvingOptions">The solving options.</param>
        public EpsilonHandling(SolvingOptions solvingOptions)
        {
            this._solvingOptions = solvingOptions;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Checks whether the <paramref name="variableValue"/> is not equal to zero considering the epsilon tolerance
        /// defined by <see cref="SolvingOptions.Epsilon"/>.
        /// </summary>
        /// <param name="variableValue">
        /// The variable value.
        /// </param>
        /// <returns>
        /// true in case the value is not equal to zero considering the epsilon tolerance.
        /// </returns>
        public bool EpsilonNotZero(double variableValue)
        {
            return Math.Abs(variableValue) > this._solvingOptions.Epsilon;
        }

        #endregion
    }
}