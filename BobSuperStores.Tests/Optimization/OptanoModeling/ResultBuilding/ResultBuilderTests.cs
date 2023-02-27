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

namespace BobSuperStores.Tests.Optimization.OptanoModeling.ResultBuilding;

using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Model.Interfaces;
using BobSuperStores.Data.Optimization;
using BobSuperStores.Data.Optimization.OptanoModeling.OptimizationModel;
using BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding;
using BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding.ResultCreators;

using Moq;

using Serilog.Core;

/// <summary>
/// Tests for the result builder class.
/// </summary>
[TestFixture]
public class ResultBuilderTests
{
    #region Public Methods and Operators

    /// <summary>
    /// Tests that if solving was successful, each result creator is called exactly once.
    /// </summary>
    [Test]
    public void IfSolvingWasSuccessfulEveryResultCreatorShouldBeCalled()
    {
        ResultBuilderTests.CheckAmountCalls(true, Times.Once);
    }

    /// <summary>
    /// Tests that if solving was not successful, no result creator is called.
    /// </summary>
    [Test]
    public void IfSolvingWasNotSuccessfulEveryResultCreatorShouldNotBeCalled()
    {
        ResultBuilderTests.CheckAmountCalls(false, Times.Never);
    }

    #endregion

    #region Methods

    private static void CheckAmountCalls(bool success, Func<Times> times)
    {
        var solution = new ProblemSolution(success);
        var modelData = new ModelData(Enumerable.Empty<IDistance>());

        var resultCreatorMocks = Enumerable.Range(0, 5).Select(
                i =>
                    {
                        var resultCreatorMock = new Mock<ICreateResults>();
                        resultCreatorMock.Setup(m => m.CreateResults(modelData, solution))
                            .Returns(true);
                        return resultCreatorMock;
                    })
            .ToList();

        var builder = new ResultBuilder(resultCreatorMocks.Select(x => x.Object), new OptLogger(Logger.None));

        builder.BuildResultData(modelData, solution);

        foreach (var resultCreatorMock in resultCreatorMocks)
        {
            resultCreatorMock.Verify(m => m.CreateResults(modelData, solution), times);
        }
    }

    #endregion
}