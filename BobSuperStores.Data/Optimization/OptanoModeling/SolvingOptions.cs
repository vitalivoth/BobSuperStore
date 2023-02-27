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
    /// Options for solving the optimization model. This central class contains all options for modifying the solving settings.
    /// </summary>
    public class SolvingOptions
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SolvingOptions"/> class.
        /// </summary>
        /// <param name="gap">the gap to use.</param>
        /// <param name="maxRunTime">the max run time to use for solving the problem.</param>
        public SolvingOptions(double gap = .02, TimeSpan maxRunTime = default)
        {
            this.Gap = gap;
            this.MaximalRunTime = maxRunTime == TimeSpan.Zero ? TimeSpan.FromHours(1) : maxRunTime;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the gap.
        /// </summary>
        public double? Gap { get; }

        /// <summary>
        /// Gets the maximal run time.
        /// </summary>
        public TimeSpan? MaximalRunTime { get; }

        /// <summary>
        /// Gets the epsilon value which is used within <see cref="EpsilonHandling.EpsilonNotZero"/> to check whether floating point values of the solver are not equal to zero.
        /// Values that differ less than epsilon from zero are considered as zero. Does NOT influence the solver tolerance itself.
        /// </summary>
        public double Epsilon => 0.0000001;

        #endregion
    }
}