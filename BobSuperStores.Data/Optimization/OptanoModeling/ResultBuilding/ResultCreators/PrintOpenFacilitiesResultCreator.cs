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

namespace BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding.ResultCreators;

using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel;
using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel.ParameterCalculation;

using JetBrains.Annotations;

/// <summary>
/// Output for the results for opening a facility.
/// </summary>
[UsedImplicitly]
public class PrintOpenFacilitiesResultCreator : ICreateResults
{
    #region Fields

    private readonly IOptanoLogger _logger;

    private readonly EpsilonHandling _epsilonHandling;

    private readonly IParameterCalculator _parameterCalculator;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PrintOpenFacilitiesResultCreator"/> class.
    /// </summary>
    /// <param name="logger">the logger.</param>
    /// <param name="epsilonHandling">class for handling epsilon checks.</param>
    /// <param name="parameterCalculator">the parameter calculator.</param>
    public PrintOpenFacilitiesResultCreator(IOptanoLogger logger, EpsilonHandling epsilonHandling, IParameterCalculator parameterCalculator)
    {
        this._logger = logger;
        this._epsilonHandling = epsilonHandling;
        this._parameterCalculator = parameterCalculator;
    }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public bool CreateResults(IModelData modelData, ProblemSolution solution)
    {
        this._logger.Log(LogLevel.Information, "The following facilities are opened:");
        var overallCost = 0d;
        var openFacilities = modelData.Facilities
            .Where(f => this._epsilonHandling.EpsilonNotZero(solution.ModelVariables.OpenFacility[f].Value));

        foreach (var openFacility in openFacilities)
        {
            var openingCostForFacility = this._parameterCalculator.GetOpeningCostForFacility(openFacility);
            this._logger.Log(LogLevel.Information, $"Open facility '{openFacility}' with cost {openingCostForFacility:N2}");
            overallCost += openingCostForFacility;
        }

        this._logger.Log(LogLevel.Information, $"Overall cost for opening: {overallCost:N2}\n");

        return true;
    }

    #endregion
}