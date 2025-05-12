using AutoMapper;
using FluentAssertions;
using System.Globalization;
using System.Security.Claims;
using Ae.Sample.Identity.Data;
using Ae.Sample.Identity.DbEntities;
using Ae.Sample.Identity.Profiles;
using Ae.Sample.Identity.Security;
using Ae.Sample.Identity.Utils;

namespace Ae.Sample.Identity.Unittests.Profiles
{
    public class IdentityStorageProfileTests
    {
        private readonly IMapper _mapper;

        public IdentityStorageProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IdentityStorageProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_DbAppClaim_To_AppClaim_MapsCorrectly()
        {
            // Arrange
            var dbClaim = new DbAppClaim
            {
                Id = Guid.NewGuid(),
                Type = "role",
                Value = "admin",
                ValueType = ClaimValueTypes.String

            };
            var dbClaims = new List<DbAppClaim> { dbClaim };

            // Act
            var result = _mapper.Map<IEnumerable<AppClaim>>(dbClaims);

            // Assert
            var appClaim = Assert.Single(result);
            appClaim.Id.Should().Be(dbClaim.Id);
            appClaim.Type.Should().Be(dbClaim.Type);
            appClaim.Value.Should().Be(dbClaim.Value);
            appClaim.ValueType.Should().Be(dbClaim.ValueType);
        }

        [Fact]
        public void Map_EmptyDbAppClaimList_ReturnsEmptyAppClaimList()
        {
            // Arrange
            var dbClaims = new List<DbAppClaim>();

            // Act
            var result = _mapper.Map<IEnumerable<AppClaim>>(dbClaims);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Map_NullDbAppClaimList_ReturnsEmptyAppClaimList()
        {
            // Act
            var result = _mapper.Map<IEnumerable<AppClaim>>(null as List<DbAppClaim>);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Map_NullDbAppClaim_ReturnsNullAppClaim()
        {
            // Act
            var result = _mapper.Map<AppClaim>(null as DbAppClaim);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Map_DbAppClaimWithProperties_To_AppClaim_MapsCorrectly()
        {
            // Arrange
            Claim claim = new(AppClaimTypes.AccountBalance, 1500.00d.ToString("R", CultureInfo.InvariantCulture), ClaimValueTypes.Double);
            claim.Properties.Add("currency", "DEMOPOINT");
            claim.Properties.Add("description", "Account balance in DEMOPOINT");

            var dbClaim = new DbAppClaim
            {
                Id = Guid.NewGuid(),
                Type = claim.Type,
                Value = claim.Value,
                ValueType = claim.ValueType,
                DisplayText = "Account Balance",
                PropertiesJson = ClaimUtils.SerializePropertiesToJson(claim.Properties),
                Description = "Account Balance"
            };
            var dbClaims = new List<DbAppClaim> { dbClaim };

            // Act
            var result = _mapper.Map<IEnumerable<AppClaim>>(dbClaims);

            // Assert
            var appClaim = Assert.Single(result);
            appClaim.Id.Should().Be(dbClaim.Id);
            appClaim.Type.Should().Be(dbClaim.Type);
            appClaim.Value.Should().Be(dbClaim.Value);
            appClaim.ValueType.Should().Be(dbClaim.ValueType);
            appClaim.DisplayText.Should().Be(dbClaim.DisplayText);
            appClaim.Description.Should().Be(dbClaim.Description);
            appClaim.Properties.Should().BeEquivalentTo(claim.Properties);
        }

        [Fact]
        public void Map_AppClaim_To_DbAppClaim_MapsCorrectly()
        {
            // Arrange
            var appClaim = new AppClaim
            {
                Id = Guid.NewGuid(),
                Type = "role",
                Value = "admin",
                ValueType = ClaimValueTypes.String,
                DisplayText = "Role",
                Description = "User role",
                Properties = new Dictionary<string, string>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                }
            };

            // Act
            var result = _mapper.Map<DbAppClaim>(appClaim);

            // Assert
            result.Id.Should().Be(appClaim.Id);
            result.Type.Should().Be(appClaim.Type);
            result.Value.Should().Be(appClaim.Value);
            result.ValueType.Should().Be(appClaim.ValueType);
            result.DisplayText.Should().Be(appClaim.DisplayText);
            result.Description.Should().Be(appClaim.Description);

            var deserializedProperties = ClaimUtils.DeserializePropertiesFromJson(result.PropertiesJson);
            deserializedProperties.Should().BeEquivalentTo(appClaim.Properties);
        }

        [Fact]
        public void Map_NullAppClaim_ReturnsNullDbAppClaim()
        {
            // Act
            var result = _mapper.Map<DbAppClaim>(null as AppClaim);

            // Assert
            result.Should().BeNull();
        }

    }
}
