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

namespace BobSuperStores.Tests.Optimization.SetUp;

using Autofac;

using BobSuperStores.Data.Csv;
using BobSuperStores.Data.Optimization;
using BobSuperStores.Data.Optimization.OptanoModeling;

using OPTANO.Modeling.Optimization.Solver.Gurobi;
using OPTANO.Modeling.Optimization.Solver.Gurobi952;

using Serilog;
using Serilog.Core;

/// <summary>
/// The class containing the methods to start an optimization for test cases.
/// </summary>
public static class OptimizationTestRunner
{
    #region Public Methods and Operators

    /// <summary>
    /// Runs the optimization for the given model data and returns if it was successful.
    /// </summary>
    /// <param name="modelData">The model data to use for optimization.</param>
    /// <param name="additionalRegistrations">Optional additional registrations.</param>
    /// <returns>If solving the model data with the optimization model was successful.</returns>
    public static bool RunOptimizationForModelData(ModelData modelData, Action<ContainerBuilder> additionalRegistrations = null)
    {
        var builder = OptimizationTestRunner.DoRegistrations(modelData, additionalRegistrations);

        return OptimizationTestRunner.RunOptimization(builder);
    }

    /// <summary>
    /// Runs the optimization for model data imported from csv files in the given folder.
    /// </summary>
    /// <param name="csvImportPath">The path to import from.</param>
    /// <param name="additionalRegistrations">Optional additional registrations.</param>
    /// <returns>If solving the model data with the optimization model was successful.</returns>
    public static bool RunOptimizationForModelDataFromCsvImport(
        string csvImportPath = @"..\..\..\..\CsvData",
        Action<ContainerBuilder> additionalRegistrations = null)
    {
        var registrationsToAdd = new Action<ContainerBuilder>(
            cb =>
                {
                    additionalRegistrations?.Invoke(cb);
                    cb.RegisterInstance(new CsvCommandLineOptions() { SourceFileDirectory = csvImportPath }).AsSelf();
                });
        var builder = OptimizationTestRunner.DoRegistrations(null, registrationsToAdd);

        return OptimizationTestRunner.RunOptimization(builder);
    }

    #endregion

    #region Methods

    private static bool RunOptimization(ContainerBuilder builder)
    {
        // solve the facility location problem
        using var scope = builder.Build().BeginLifetimeScope();
        var solver = scope.Resolve<IProblemSolver>();
        var success = solver.HandleProblem();

        return success;
    }

    private static ContainerBuilder DoRegistrations(ModelData modelData = null, Action<ContainerBuilder> additionalRegistrations = null)
    {
        var builder = new ContainerBuilder();

        builder.RegisterAssemblyModules(typeof(IProblemSolver).Assembly);
        if (modelData != null)
        {
            builder.RegisterInstance(modelData).As<IModelData>().SingleInstance().ExternallyOwned();
        }

        builder.RegisterInstance(Logger.None).As<ILogger>().SingleInstance();
        builder.RegisterInstance(OptimizationTestRunner.GetGurobiSolverConfigurationForTests()).AsSelf().SingleInstance();

        additionalRegistrations?.Invoke(builder);

        return builder;
    }

    private static GurobiSolverConfiguration GetGurobiSolverConfigurationForTests()
    {
        // we want to use the limited licence only for test purposes
        return new GurobiSolverConfiguration()
                   {
                       ComputeIIS = true,
                       LogToConsole = false,
                       LimitedLicenseUsage = LimitedLicenseUsage.Always,
                   };
    }

    #endregion
}