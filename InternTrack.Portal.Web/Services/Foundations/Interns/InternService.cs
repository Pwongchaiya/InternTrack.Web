﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Threading.Tasks;
using InternTrack.Portal.Web.Brokers.Apis;
using InternTrack.Portal.Web.Brokers.Loggings;
using InternTrack.Portal.Web.Models.Interns;

namespace InternTrack.Portal.Web.Services.Foundations.Interns
{
    public partial class InternService : IInternService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public InternService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Intern> AddInternAsync(Intern intern) =>
        TryCatch(async () =>
        {
            return await this.apiBroker.PostInternAsync(intern);
        });
    }
}