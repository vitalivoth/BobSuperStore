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

namespace BobSuperStores.Data.Calculations;

using BobSuperStores.Data.Model;
using BobSuperStores.Data.Model.Interfaces;

using JetBrains.Annotations;

/// <summary>
/// Defines a simple distance calculator that calculates a distance between two <see cref="Location"/>.
/// </summary>
public interface IDistanceCalculator
{
    #region Public Methods and Operators

    /// <summary>
    /// Calculates the distance between the <paramref name="start"/> and <paramref name="destination"/>.
    /// </summary>
    /// <param name="start">The start location.</param>
    /// <param name="destination">The destination.</param>
    /// <returns>The distance between both <see cref="Location"/>.</returns>
    double CalculateDistance([NotNull] IGeoCoordinate start, [NotNull] IGeoCoordinate destination);

    #endregion
}