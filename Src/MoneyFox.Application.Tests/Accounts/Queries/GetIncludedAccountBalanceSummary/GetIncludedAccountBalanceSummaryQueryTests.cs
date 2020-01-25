﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MoneyFox.Application.Accounts.Queries.GetExcludedAccount;
using MoneyFox.Application.Common.Interfaces;
using MoneyFox.Application.Tests.Infrastructure;
using MoneyFox.Domain.Entities;
using MoneyFox.Persistence;
using Moq;
using Xunit;

namespace MoneyFox.Application.Tests.Accounts.Queries.GetIncludedAccountBalanceSummary
{
    [ExcludeFromCodeCoverage]
    public class GetIncludedAccountBalanceSummaryQueryTests
    {
        private readonly EfCoreContext context;
        private readonly Mock<IContextAdapter> contextAdapterMock;

        public GetIncludedAccountBalanceSummaryQueryTests()
        {
            context = InMemoryEfCoreContextFactory.Create();

            contextAdapterMock = new Mock<IContextAdapter>();
            contextAdapterMock.SetupGet(x => x.Context).Returns(context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            InMemoryEfCoreContextFactory.Destroy(context);
        }

        [Fact]
        public async Task GetSummary()
        {
            // Arrange
            var accountExcluded = new Account("test", 80, isExcluded: true);
            var accountIncluded = new Account("test", 80);
            await context.AddAsync(accountExcluded);
            await context.AddAsync(accountIncluded);
            await context.SaveChangesAsync();
            
            // Act
            var result =
                await new GetExcludedAccountQuery.Handler(contextAdapterMock.Object).Handle(new GetExcludedAccountQuery(), default);

            // Assert
        }
    }
}
