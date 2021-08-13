using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using rvezy.Data;
using rvezy.Models;
using rvezy.Services;
using Xunit;

namespace rvezy.Tests
{
    public class ListingServiceTests
    {
        private readonly ICsvProvider _provider;
        private readonly DataContext _dataContext;
        private readonly IListingService _subject;

        public ListingServiceTests()
        {
            _provider = Substitute.For<ICsvProvider>();
            _dataContext = Substitute.For<DataContext>();
            _subject = new ListingService(_provider, _dataContext);
        }

        [Fact]
        public async Task GivenAFileWeGetAList()
        {
            var data = new List<Listing>();
            _provider.GetListingsFromFile().Returns(data);
            
            var result = await _subject.GetAllListings(null);
            result.Should().BeEmpty();
        }
        
    }

}