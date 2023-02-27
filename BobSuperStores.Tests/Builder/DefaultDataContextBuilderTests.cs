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

namespace BobSuperStores.Tests.Builder;

using Autofac;

using BobSuperStores.Data;
using BobSuperStores.Data.Builder;
using BobSuperStores.Data.Csv;
using BobSuperStores.Data.Logging;
using BobSuperStores.Data.Modules;

using Serilog;
using Serilog.Core;

using Shouldly;

/// <summary>
/// Tests the <see cref="DefaultDataContextBuilder"/>.
/// </summary>
[TestFixture]
public class DefaultDataContextBuilderTests
{
    #region Public Methods and Operators

    /// <summary>
    /// Test that the data context is correctly build from CSV files.
    /// </summary>
    /// <returns>>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Test]
    public async Task TestCorrectBuild()
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(new CsvCommandLineOptions { SourceFileDirectory = @"..\..\..\..\CsvData" }).AsSelf();
        builder.RegisterModule(new FromCsvModule(Logger.None));
        builder.RegisterInstance(Logger.None).As<ILogger>().SingleInstance();
        builder.RegisterType<OptLogger>().As<IOptanoLogger>().SingleInstance();
        var container = builder.Build();

        await using var scope = container.BeginLifetimeScope();
        var contextBuilder = scope.Resolve<IDataContextBuilder>();
        contextBuilder.GetType().ShouldBe(typeof(DefaultDataContextBuilder));

        await Verifier.Verify(contextBuilder.Build());
    }

    #endregion
}