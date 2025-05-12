using FluentAssertions;
using System.Security.Claims;
using System.Globalization;
using Ae.Sample.Identity.Utils;
using Ae.Sample.Identity.Security;

namespace Ae.Sample.Identity.Unittests.Utils
{
    public class ClaimUtilsTests
    {
        [Fact]
        public void SerializeToJson_ShouldReturnValidJson_WhenClaimsAreProvided()
        {
            // Arrange
            var claims = new List<Claim>
                {
                    new (ClaimTypes.Name, "John Doe"),
                    new (ClaimTypes.Email, "john.doe@example.com")
                };

            // Act
            var result = ClaimUtils.SerializeToJson(claims);

            // Assert
            result.Should().NotBeNullOrEmpty();

            var claimsCheck = ClaimUtils.DeserializeFromJson(result);
            claimsCheck.Should().BeEquivalentTo(claims);
        }

        [Fact]
        public void SerializeToJson_ShouldReturnEmptyJson_WhenNoClaims()
        {
            // Arrange
            var claims = new List<Claim>();

            // Act
            var result = ClaimUtils.SerializeToJson(claims);

            // Assert
            "[]".Should().Be(result);

            var claimsCheck = ClaimUtils.DeserializeFromJson(result);
            claimsCheck.Should().BeEquivalentTo(claims);
        }

        [Fact]
        public void SerializeToJson_ShouldHandleClaimValueTypes()
        {
            // Arrange
            var claimDouble = new Claim(AppClaimTypes.AccountBalance, 1500.00d.ToString("R", CultureInfo.InvariantCulture), ClaimValueTypes.Double);
            claimDouble.Properties.Add("currency", "DEMOPOINT");
            claimDouble.Properties.Add("description", "Account balance in DEMOPOINT");

            var claims = new List<Claim>
                {
                    claimDouble,
                    new (AppClaimTypes.Manager, false.ToString().ToLowerInvariant(), ClaimValueTypes.Boolean),
                    new (ClaimTypes.Role, "Administrator", ClaimValueTypes.String),
                    new ("Level", 1.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer),
                    new (ClaimTypes.DateOfBirth, (new DateTime(2025, 3, 25)).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), ClaimValueTypes.Date)
                };

            // Act
            var result = ClaimUtils.SerializeToJson(claims);

            // Assert
            var claimsCheck = ClaimUtils.DeserializeFromJson(result);
            claimsCheck.Should().BeEquivalentTo(claims);
        }

        [Fact]
        public void SerializePropertiesToJson_ShouldReturnValidJson_WhenPropertiesAreProvided()
        {
            // Arrange
            var properties = new Dictionary<string, string>
            {
                { "currency", "USD" },
                { "description", "Account balance in USD" }
            };

            // Act
            var result = ClaimUtils.SerializePropertiesToJson(properties);

            // Assert
            result.Should().NotBeNullOrEmpty();

            var deserializedProperties = ClaimUtils.DeserializePropertiesFromJson(result);
            deserializedProperties.Should().BeEquivalentTo(properties);
        }

        [Fact]
        public void SerializePropertiesToJson_ShouldReturnEmptyJson_WhenNoProperties()
        {
            // Arrange
            var properties = new Dictionary<string, string>();

            // Act
            var result = ClaimUtils.SerializePropertiesToJson(properties);

            // Assert
            "{}".Should().Be(result);

            var deserializedProperties = ClaimUtils.DeserializePropertiesFromJson(result);
            deserializedProperties.Should().BeNull();
        }

        [Fact]
        public void SerializePropertiesToJson_ShouldReturnEmptyJson_WhenNull()
        {
            // Arrange
            Dictionary<string, string>? properties = null;

            // Act
            var result = ClaimUtils.SerializePropertiesToJson(properties);

            // Assert
            "{}".Should().Be(result);

            var deserializedProperties = ClaimUtils.DeserializePropertiesFromJson(result);
            deserializedProperties.Should().BeNull();
        }

        [Fact]
        public void SerializePropertiesToJson_ShouldHandleSpecialCharactersInKeysAndValues()
        {
            // Arrange
            var properties = new Dictionary<string, string>
            {
                { "key with spaces", "value with spaces" },
                { "special!@#$%^&*()", "chars!@#$%^&*()" }
            };

            // Act
            var result = ClaimUtils.SerializePropertiesToJson(properties);

            // Assert
            result.Should().NotBeNullOrEmpty();

            var deserializedProperties = ClaimUtils.DeserializePropertiesFromJson(result);
            deserializedProperties.Should().BeEquivalentTo(properties);
        }
    
    }
}
