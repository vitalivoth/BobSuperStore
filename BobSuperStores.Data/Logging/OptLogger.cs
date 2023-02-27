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

namespace BobSuperStores.Data.Logging
{
    using Serilog;

    /// <summary>
    /// Logger used for the optimization, delegates all calls to the already given external logger.
    /// </summary>
    public class OptLogger : IOptanoLogger
    {
        #region Fields

        private readonly ILogger _logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OptLogger"/> class.
        /// </summary>
        /// <param name="logger">the logger coming from the external program call.</param>
        public OptLogger(ILogger logger)
        {
            this._logger = logger;
        }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public void Log(LogLevel logLevel, string format, params object[] parameter)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    this._logger.Information(format, parameter);
                    break;
                case LogLevel.Debug:
                    this._logger.Debug(format, parameter);
                    break;
                case LogLevel.Warning:
                    this._logger.Warning(format, parameter);
                    break;
                case LogLevel.Error:
                    this._logger.Error(format, parameter);
                    break;
                default:
                    this._logger.Information(format, parameter);
                    break;
            }
        }

        /// <inheritdoc />
        public void LogException(Exception exception)
        {
            this._logger.Error(exception.ToString());
        }

        #endregion
    }
}