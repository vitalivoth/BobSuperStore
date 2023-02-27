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

namespace BobSuperStores.Data.Optimization.OptanoModeling.Solver.SolverCallback
{
    using BobSuperStores.Data.Logging;

    using OPTANO.Modeling.Optimization.Solver.Interfaces;

    /// <summary>
    /// Solver callback which logs all status messages from. 
    /// </summary>
    /// <seealso cref="ISolverCallback" />
    public class LogSolverMessagesCallback : ISolverCallback
    {
        #region Fields

        private readonly IOptanoLogger _logger;

        private readonly LogLevel _logLevelToUse;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogSolverMessagesCallback" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="logLevelToUse">The log level to use for logging the solver messages.</param>
        public LogSolverMessagesCallback(IOptanoLogger logger, LogLevel logLevelToUse = LogLevel.Information)
        {
            this._logger = logger;
            this._logLevelToUse = logLevelToUse;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Callbacks the specified status information.
        /// </summary>
        /// <param name="statusInfo">The status information.</param>
        public void Callback(StatusInfo statusInfo)
        {
            if (statusInfo != null && !string.IsNullOrWhiteSpace(statusInfo?.LogMessage))
            {
                var message = statusInfo.LogMessage.TrimEnd();
                this._logger.Log(this._logLevelToUse, message);
            }
        }

        #endregion
    }
}