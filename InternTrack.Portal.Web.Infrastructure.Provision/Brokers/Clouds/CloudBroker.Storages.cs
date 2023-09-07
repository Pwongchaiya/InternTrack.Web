﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Sql;
using Azure.ResourceManager.Sql.Models;
using InternTrack.Portal.Web.Infrastructure.Provision.Models.Storages;

namespace InternTrack.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<SqlDatabaseResource> CreateSqlDataBaseAsync(
            string sqlDatabaseName,
            SqlServerResource sqlServer)
        {
            var sqlDbData =
                new SqlDatabaseData(AzureLocation.WestUS3)
                {
                    Sku = new SqlSku("S0")
                    {
                        Tier = "Standard",
                    },
                    CreateMode = SqlDatabaseCreateMode.Default,
                };

            ArmOperation<SqlDatabaseResource> database = await sqlServer
                .GetSqlDatabases()
                .CreateOrUpdateAsync(
                    WaitUntil.Completed,
                    sqlDatabaseName,
                    sqlDbData);

            return database.Value;
        }

        public async ValueTask<SqlServerResource> CreateSqlServerAsync(
            string sqlServerName,
            ResourceGroupResource resourceGroup)
        {
            var sqlServerData =
                new SqlServerData(AzureLocation.WestUS3)
                {
                    AdministratorLogin = GetAdminAccess().AdminName,
                    AdministratorLoginPassword = GetAdminAccess().AdminAccess,
                };

            ArmOperation<SqlServerResource> server = await resourceGroup
                .GetSqlServers()
                .CreateOrUpdateAsync(
                    WaitUntil.Completed,
                    sqlServerName,
                    sqlServerData);

            return server.Value;
        }

        public SqlDatabaseAccess GetAdminAccess()
        {
            return new SqlDatabaseAccess
            {
                AdminName = adminName,
                AdminAccess = adminPassword
            };
        }
    }
}
