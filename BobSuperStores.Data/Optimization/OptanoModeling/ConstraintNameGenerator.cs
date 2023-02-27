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
    using OPTANO.Modeling.Optimization;

    /// <summary>
    /// Class contains methods to generate the names for constraints.
    /// </summary>
    public class ConstraintNameGenerator
    {
        #region Public Methods and Operators

        /// <summary>
        /// Adds the name to the <paramref name="existingConstraint"/>.
        /// </summary>
        /// <param name="existingConstraint">The existing constraint.</param>
        /// <param name="name">The name.</param>
        /// <returns>The new constraint with the correct name set.</returns>
        public Constraint AddNameToConstraint(Constraint existingConstraint, string name)
        {
            return new Constraint(
                existingConstraint.Expression,
                name,
                existingConstraint.LowerBound,
                existingConstraint.UpperBound,
                existingConstraint.LazyConstraintLevel);
        }

        #endregion
    }
}