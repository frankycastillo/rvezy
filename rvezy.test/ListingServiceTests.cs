using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using rvezy.Core.Logger;
using rvezy.Data;
using rvezy.Models;
using rvezy.Services;
using Xunit;

namespace rvezy.Tests
{
    public class ListingServiceTests
    {
        private readonly ICsvProvider _provider;
        private readonly IRepository<Listing> _repository;
        private readonly IListingService _subject;

        public ListingServiceTests()
        {
            var logger = Substitute.For<ILogger>();
            var configuration = Substitute.For<IConfiguration>();
            var contextAccessor = Substitute.For<IHttpContextAccessor>();

            _provider = Substitute.For<ICsvProvider>();
            _repository = Substitute.For<IRepository<Listing>>();
            _subject = new ListingService(configuration, logger, contextAccessor, _provider, _repository);
        }

        [Fact]
        public async Task GivenAFileWeGetAList()
        {
            var data = new List<Listing>();
            _provider.GetListingsFromFile().Returns(data);
            
            var result = await _subject.GetAllListingsCsv(null);
            result.Should().BeEmpty();
        }
        
    }

}