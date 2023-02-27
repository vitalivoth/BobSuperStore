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

namespace BobSuperStores.Tests.Optimization;

using Autofac;

using BobSuperStores.Data.Model.Interfaces;
using BobSuperStores.Data.Optimization;
using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel;
using BobSuperStores.Tests.Optimization.SetUp;

using Moq;

using Shouldly;

/// <summary>
/// The overall optimization integration tests.
/// </summary>
public class OverallOptimizationTests
{
    #region Public Methods and Operators

    /// <summary>
    /// An empty optimization model should be solvable.
    /// </summary>
    [Test]
    public void EmptyModelDataShouldBeSolvable()
    {
        var modelData = new ModelData(Enumerable.Empty<IDistance>());

        var success = OptimizationTestRunner.RunOptimizationForModelData(modelData);

        success.ShouldBeTrue();
    }

    /// <summary>
    /// If an exception is thrown in solving the model, it should be catch and solving should not be successful.
    /// </summary>
    [Test]
    public void OnExceptionThrownSolvingModelShouldReturnFalse()
    {
        var modelData = new ModelData(Enumerable.Empty<IDistance>());

        var success = OptimizationTestRunner.RunOptimizationForModelData(
            modelData,
            cb =>
                {
                    var solverMock = new Mock<IOptimizationProblemSolver>();
                    solverMock.Setup(m => m.SolveProblem()).Throws<ArgumentException>();
                    cb.RegisterInstance(solverMock.Object).As<IOptimizationProblemSolver>().SingleInstance();
                });

        success.ShouldBeFalse();
    }

    /// <summary>
    /// The optimization model given in den example data should be solvable.
    /// </summary>
    [Test]
    public void DefaultOptimizationModelShouldBeSolvable()
    {
        var success = OptimizationTestRunner.RunOptimizationForModelDataFromCsvImport();

        success.ShouldBeTrue();
    }

    /// <summary>
    /// If there are no connections between clients and facilities, the optimization problem is not feasible and should return no success.
    /// </summary>
    [Test]
    public void OptimizationModelWithoutFeasibleConnectionsShouldBeInfeasible()
    {
        var distances = TestDataFactory.GetDistances(0, 0).ToList();
        var facilities = TestDataFactory.GetFacilities(1).ToList();
        var clients = TestDataFactory.GetLocations(1).ToList();

        var modelData = new ModelData(distances, clients, facilities);

        var success = OptimizationTestRunner.RunOptimizationForModelData(modelData);

        success.ShouldBeFalse();
    }

    #endregion
}