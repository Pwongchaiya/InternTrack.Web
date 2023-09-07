﻿// -------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE FOR THE WORLD
// -------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using InternTrack.Portal.Web.Models.Interns.Exceptions;
using Moq;
using RESTFulSense.Exceptions;
using Xunit;

namespace InternTrack.Portal.Web.Tests.Unit.Services.Foundations.Interns
{
    public partial class InternServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyException))]
        private async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfDependencyApiErrorOccursAndLogItAsync(
                Exception criticalDependencyException)
        {
            // given
            Guid someInternId = Guid.NewGuid();

            var failedInternDependencyException =
                new FailedInternDependencyException(
                    message: "Failed Intern dependency error occurred, contact support.",
                        innerException: criticalDependencyException);

            var expectedInternDependencyException =
                new InternDependencyException(
                    message: "Intern dependency error occurred, contact support.",
                        innerException: failedInternDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetInternByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(criticalDependencyException);

            // when
            var retrieveInternByIdTask =
                this.internService.RetrieveInternByIdAsync(someInternId);

            InternDependencyException actualInternDependencyException =
                await Assert.ThrowsAsync<InternDependencyException>(() =>
                    retrieveInternByIdTask.AsTask());

            // then
            actualInternDependencyException.Should()
                .BeEquivalentTo(expectedInternDependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.GetInternByIdAsync(It.IsAny<Guid>()),
                 Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedInternDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowDependencyExceptionOnRetrieveByIdIfDependencyApiErrorOccursAndLogItAsync()
        {
            // given
            Guid someInternId = Guid.NewGuid();
            string someMessage = GetRandomMessage();
            var someResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(
                    someResponseMessage,
                        someMessage);

            var failedInternDependencyException =
                new FailedInternDependencyException(
                    message: "Failed Intern dependency error occurred, contact support.",
                        innerException: httpResponseException);

            var expectedInternDependencyException =
                new InternDependencyException(
                    message: "Intern dependency error occurred, contact support.",
                        innerException: failedInternDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetInternByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseException);
            // when
            var retrieveInternTask =
                this.internService.RetrieveInternByIdAsync(someInternId);

            InternDependencyException actualInternDependencyException =
                await Assert.ThrowsAsync<InternDependencyException>(() =>
                    retrieveInternTask.AsTask());

            // then
            actualInternDependencyException.Should()
                .BeEquivalentTo(expectedInternDependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.GetInternByIdAsync(It.IsAny<Guid>()),
                 Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedInternDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        private async Task ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someInternId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedInternServiceException =
                new FailedInternServiceException(
                    message: "Failed Intern service error occurred, contact support.",
                        innerException: serviceException);

            var expectedInternServiceException =
                new InternServiceException(
                    message: "Intern service error occurred, contact support.",
                        innerException: failedInternServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetInternByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            var retrieveInternTask =
                this.internService.RetrieveInternByIdAsync(someInternId);

            InternServiceException actualInternServiceException =
                await Assert.ThrowsAsync<InternServiceException>(() =>
                    retrieveInternTask.AsTask());

            // then
            actualInternServiceException.Should()
                .BeEquivalentTo(expectedInternServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.GetInternByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedInternServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
