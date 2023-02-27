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

namespace BobSuperStores.Data.Optimization.OptanoModeling.Registration
{
    using Autofac;

    using BobSuperStores.Data.Optimization.OptanoModeling.Solver;
    using BobSuperStores.Data.Optimization.OptanoModeling.Solver.SolverCallback;

    using JetBrains.Annotations;

    /// <summary>
    /// Autofac module to register the mathematical programming solvers.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    [UsedImplicitly]
    public class SolverRegistration : Module
    {
        #region Methods

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<GurobiSolverProvider>().As<ISolverProvider>().InstancePerLifetimeScope();
            builder.RegisterType<LogSolverMessagesCallback>().As<ISolverCallback>().InstancePerLifetimeScope();
        }

        #endregion
    }
}