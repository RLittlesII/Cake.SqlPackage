﻿using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.SqlPackage
{
    /// <summary>
    /// SqlPackage tool execution for extract action.
    /// </summary>
    internal class SqlPackageExtractRunner : SqlPackageRunner<SqlPackageExtractSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageExtractRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tools.</param>
        public SqlPackageExtractRunner(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        protected override ProcessArgumentBuilder BuildArguments(SqlPackageExtractSettings settings)
        {
            var builder = CreateBuilder(settings);

            if (settings.OutputPath != null)
            {
                builder.Append($"/{nameof(settings.OutputPath)}:\"{settings.OutputPath.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }

            if (settings.OverwriteFiles.HasValue)
            {
                builder.Append($"/{nameof(settings.OverwriteFiles)}:{settings.OverwriteFiles}");
            }

            if (settings.Quiet.HasValue)
            {
                builder.Append($"/{nameof(settings.Quiet)}:{settings.Quiet}");
            }

            if (!string.IsNullOrEmpty(settings.SourceConnectionString))
            {
                builder.Append($"/{nameof(settings.SourceConnectionString)}:\"{settings.SourceConnectionString}\"");
            }
            else
            {
                if (!string.IsNullOrEmpty(settings.SourceDatabaseName))
                {
                    builder.Append($"/{nameof(settings.SourceDatabaseName)}:{settings.SourceDatabaseName}");
                }

                if (settings.SourceEncryptConnection.HasValue)
                {
                    builder.Append($"/{nameof(settings.SourceEncryptConnection)}:{settings.SourceEncryptConnection}");
                }

                if (!string.IsNullOrEmpty(settings.SourcePassword))
                {
                    builder.Append($"/{nameof(settings.SourcePassword)}:{settings.SourcePassword}");
                }

                if (!string.IsNullOrEmpty(settings.SourceServerName))
                {
                    builder.Append($"/{nameof(settings.SourceServerName)}:{settings.SourceServerName}");
                }

                if (settings.SourceTimeout > 0)
                {
                    builder.Append($"/{nameof(settings.SourceTimeout)}:{settings.SourceTimeout}");
                }

                if (settings.SourceTrustServerCertificate.HasValue)
                {
                    builder.Append($"/{nameof(settings.SourceTrustServerCertificate)}:{settings.SourceTrustServerCertificate}");
                }

                if (!string.IsNullOrEmpty(settings.SourceUser))
                {
                    builder.Append($"/{nameof(settings.SourceUser)}:{settings.SourceUser}");
                }
            }

            if (settings.TargetFile != null)
            {
                builder.Append($"/{nameof(settings.TargetFile)}:\"{settings.TargetFile.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }

            if (!string.IsNullOrEmpty(settings.TenantId))
            {
                builder.Append($"/{nameof(settings.TenantId)}:{settings.TenantId}");
            }

            if (settings.UniversalAuthentication.HasValue)
            {
                builder.Append($"/{nameof(settings.UniversalAuthentication)}:{settings.UniversalAuthentication}");
            }

            // Copy common settings to builder
            var commonBuilder = BuildSqlPackageArguments(settings);

            return CopyArgumentsTo(commonBuilder, builder);
        }
    }
}
