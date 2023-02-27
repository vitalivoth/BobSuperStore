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

    using BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding;
    using BobSuperStores.Data.Optimization.OptanoModeling.ResultBuilding.ResultCreators;

    using JetBrains.Annotations;

    /// <summary>
    /// Autofac registration module for the result building parts.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    [UsedImplicitly]
    public class ResultBuildingRegistration : Module
    {
        #region Methods

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ResultBuilder>().As<IResultBuilder>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ResultBuildingRegistration).Assembly)
                .Where(t => t.ImplementsInterface<ICreateResults>())
                .As<ICreateResults>()
                .InstancePerLifetimeScope();
        }

        #endregion
    }
}