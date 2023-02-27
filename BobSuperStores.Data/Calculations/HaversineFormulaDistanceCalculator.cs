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

using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Model.Interfaces;

using JetBrains.Annotations;

/// <summary>
/// Calculates the distance between two geo locations by using the Haversine-Formula:
/// https://en.wikipedia.org/wiki/Haversine_formula.
/// </summary>
public class HaversineFormulaDistanceCalculator : IDistanceCalculator
{
    #region Constants

    private const int EarthRadiusInKm = 6371;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="HaversineFormulaDistanceCalculator"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public HaversineFormulaDistanceCalculator([NotNull] IOptanoLogger logger)
    {
        this.Logger = logger;
    }

    #endregion

    #region Properties

    [NotNull]
    private IOptanoLogger Logger { get; }

    #endregion

    #region Public Methods and Operators

    /// <inheritdoc />
    public double CalculateDistance(IGeoCoordinate start, IGeoCoordinate destination)
    {
        this.Logger.Log(LogLevel.Debug, "Calculating distance between '{0}' and '{1}'", start, destination);
        var radiansOverDegrees = Math.PI / 180.0;

        var startLatRadians = start.Latitude * radiansOverDegrees;
        var startLongRadians = start.Longitude * radiansOverDegrees;
        var destLatRadians = destination.Latitude * radiansOverDegrees;
        var destLongRadians = destination.Longitude * radiansOverDegrees;

        var diffLongitude = startLongRadians - destLongRadians;
        var diffLatitude = startLatRadians - destLatRadians;

        var result1 = Math.Pow(Math.Sin(diffLatitude / 2.0), 2.0) +
                      (Math.Cos(startLatRadians) * Math.Cos(destLatRadians) *
                       Math.Pow(Math.Sin(diffLongitude / 2.0), 2.0));

        var distance = Math.Round(
            HaversineFormulaDistanceCalculator.EarthRadiusInKm * 2.0 *
            Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1)),
            2);

        this.Logger.Log(
            LogLevel.Debug,
            "Result of distance calculation ({0}, {1}), ({2}, {3}): {4}",
            start.Latitude,
            start.Longitude,
            destination.Latitude,
            destination.Longitude,
            distance);

        return distance;
    }

    #endregion
}