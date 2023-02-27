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

namespace BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ParameterCalculation;

using BobSuperStores.Data.Model.Interfaces;

/// <summary>
/// The parameter calculator needed to obtain all detail values needed in the optimization model. 
/// </summary>
public interface IParameterCalculator
{
    #region Public Methods and Operators

    /// <summary>
    /// Gets the cost for opening a location.
    /// </summary>
    /// <param name="location">the location to open.</param>
    /// <returns>the cost for opening the location.</returns>
    double GetOpeningCostForFacility(ILocation location);

    /// <summary>
    /// Gets the cost for opening the given connection.
    /// </summary>
    /// <param name="distance">the connection to use.</param>
    /// <returns>the cost for the connection.</returns>
    double GetConnectionCost(IDistance distance);

    #endregion
}