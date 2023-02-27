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

using BobSuperStores.Data.Model;
using BobSuperStores.Data.Model.Interfaces;

/// <summary>
/// The parameter calculator.
/// </summary>
public class ParameterCalculator : IParameterCalculator
{
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterCalculator"/> class.
    /// </summary>
    /// <param name="costPerMeter">The cost per meter.</param>
    public ParameterCalculator(double costPerMeter = .3)
    {
        this.CostPerMeter = costPerMeter;
    }

    #endregion

    #region Properties

    private double CostPerMeter { get; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public double GetOpeningCostForFacility(ILocation location)
    {
        return (location as Warehouse)?.OpeningCosts ?? 0d;
    }

    /// <inheritdoc />
    public double GetConnectionCost(IDistance distance)
    {
        return this.CostPerMeter * distance.DistanceInMeter;
    }

    #endregion
}