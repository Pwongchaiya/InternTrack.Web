﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using Azure.ResourceManager.AppService;
using Azure.ResourceManager.Resources;
using System.Threading.Tasks;

namespace InternTrack.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial interface ICloudBroker
    {
        ValueTask<WebSiteResource> CreateWebAppAsync(
            string webAppName,
            string databaseConnectionString,
            AppServicePlanResource plan,
            ResourceGroupResource resourceGroup);
    }
}
