﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.ApplicationInsights;
using Azure.ResourceManager.ApplicationInsights.Models;
using Azure.ResourceManager.Resources;

namespace InternTrack.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<ApplicationInsightsComponentResource> CreateApplicationInsightComponentAsync(
            string componentName,
            ResourceGroupResource resourceGroup)
        {
            var appInsightData = new ApplicationInsightsComponentData(AzureLocation.WestUS, "web")
            {
                ApplicationType = ApplicationType.Web,
                FlowType = FlowType.Bluefield,
                RequestSource = RequestSource.Rest
            };

            ArmOperation<ApplicationInsightsComponentResource> appInsightComponent =
                await resourceGroup.GetApplicationInsightsComponents()
                    .CreateOrUpdateAsync(
                        WaitUntil.Completed,
                            componentName,
                                appInsightData);

            return appInsightComponent.Value;
        }
    }
}
