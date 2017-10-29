using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VersioningManagement.Versions;

namespace VersioningManagement.Tests.Versions
{
    [TestClass]
    public class VersionChangerTests
    {
        [TestMethod]
        public void ParseFromString_WhenVersionsAbove99Used_ShouldIdentifyAllPartsCorrectly()
        {
            VersionChanger.ParseFromString("100.200.300.400", out int major, out int minor, out int revision, out int build);
            major.Should().Be(100);
            minor.Should().Be(200);
            revision.Should().Be(300);
            build.Should().Be(400);
        }

        [TestMethod]
        public void ParseFromString_WhenVersionsAbove9Used_ShouldIdentifyAllPartsCorrectly()
        {
            VersionChanger.ParseFromString("10.20.30.40", out int major, out int minor, out int revision, out int build);
            major.Should().Be(10);
            minor.Should().Be(20);
            revision.Should().Be(30);
            build.Should().Be(40);
        }

        [TestMethod]
        public void ParseFromString_WhenAllPartsUsed_ShouldIdentifyAllPartsCorrectly()
        {
            VersionChanger.ParseFromString("1.2.3.4", out int major, out int minor, out int revision, out int build);
            major.Should().Be(1);
            minor.Should().Be(2);
            revision.Should().Be(3);
            build.Should().Be(4);
        }

        [TestMethod]
        public void ParseFromString_When3PartsUsed_ShouldIdentify3PartsCorrectly()
        {
            VersionChanger.ParseFromString("1.2.3", out int major, out int minor, out int revision, out int build);
            major.Should().Be(1);
            minor.Should().Be(2);
            revision.Should().Be(3);
            build.Should().Be(-1);
        }

        [TestMethod]
        public void ParseFromString_When2PartsUsed_ShouldIdentify2PartsCorrectly()
        {
            VersionChanger.ParseFromString("1.2", out int major, out int minor, out int revision, out int build);
            major.Should().Be(1);
            minor.Should().Be(2);
            revision.Should().Be(-1);
            build.Should().Be(-1);
        }

        [TestMethod]
        public void ParseFromString_When1PartUsed_ShouldIdentify1PartCorrectly()
        {
            VersionChanger.ParseFromString("1", out int major, out int minor, out int revision, out int build);
            major.Should().Be(1);
            minor.Should().Be(-1);
            revision.Should().Be(-1);
            build.Should().Be(-1);
        }

        [TestMethod]
        public void ParseFromString_WhenBuildHasAsteriskUsed_ShouldIdentifyAllPartsCorrectly()
        {
            VersionChanger.ParseFromString("1.2.3.*", out int major, out int minor, out int revision, out int build);
            major.Should().Be(1);
            minor.Should().Be(2);
            revision.Should().Be(3);
            build.Should().Be(int.MaxValue);
        }

        [TestMethod]
        public void ParseFromString_When3PartsUsedAndRevisionHasAsteriskUsed_ShouldIdentifyAllPartsCorrectly()
        {
            VersionChanger.ParseFromString("1.2.*", out int major, out int minor, out int revision, out int build);
            major.Should().Be(1);
            minor.Should().Be(2);
            revision.Should().Be(int.MaxValue);
            build.Should().Be(-1);
        }

        [TestMethod]
        public void TryParse_WhenValidMajorVersionGiven_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseMajorVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1");
            version.IncreaseVersion(VersionPart.Major);
            version.Version.Should().Be("2");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldDecreaseMajorVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1");
            version.DecreaseVersion(VersionPart.Major);
            version.Version.Should().Be("0");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldNotDecreaseMajorVersion_WhenVersionWouldBeLowerThanZero()
        {
            var version = new VersionChanger("0");
            version.DecreaseVersion(VersionPart.Major);
            version.Version.Should().Be("0");
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseMajorVersion_WhenVersionContainsAllParts()
        {
            var version = new VersionChanger("1.0.0.0");
            version.IncreaseVersion(VersionPart.Major);
            version.Version.Should().Be("2.0.0.0");
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseMajorVersion_WhenVersionContainsAllPartsWithAsterisk()
        {
            var version = new VersionChanger("1.0.0.*");
            version.IncreaseVersion(VersionPart.Major);
            version.Version.Should().Be("2.0.0.*");
        }

        [TestMethod]
        public void Ctor_WhenValidMajorVersionGiven_ShouldBeTheSameAsVersionProperty()
        {
            new VersionChanger("1").Version.Should().Be("1");
        }

        [TestMethod]
        public void TryParse_WhenValidMinorVersionGiven_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.0", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseMinorVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1.0");
            version.IncreaseVersion(VersionPart.Minor);
            version.Version.Should().Be("1.1");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldDecreaseMinorVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1.1");
            version.DecreaseVersion(VersionPart.Minor);
            version.Version.Should().Be("1.0");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldNotDecreaseMinorVersion_WhenVersionWouldBeLowerThanZero()
        {
            var version = new VersionChanger("1.0");
            version.DecreaseVersion(VersionPart.Minor);
            version.Version.Should().Be("1.0");
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseMinorVersion_WhenVersionContainsAllParts()
        {
            var version = new VersionChanger("1.0.0.0");
            version.IncreaseVersion(VersionPart.Minor);
            version.Version.Should().Be("1.1.0.0");
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseMinorVersion_WhenVersionContainsAllPartsWithAsterisk()
        {
            var version = new VersionChanger("1.0.0.*");
            version.IncreaseVersion(VersionPart.Minor);
            version.Version.Should().Be("1.1.0.*");
        }

        [TestMethod]
        public void Ctor_WhenValidMinorVersionGiven_ShouldBeTheSameAsVersionProperty()
        {
            new VersionChanger("1.0").Version.Should().Be("1.0");
        }

        [TestMethod]
        public void TryParse_WhenValidMinorWithAsteriskVersionGiven_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.*", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void Ctor_WhenValidMinorWithAsteriskVersionGiven_ShouldBeTheSameAsVersionProperty()
        {
            new VersionChanger("1.*").Version.Should().Be("1.*");
        }

        [TestMethod]
        public void TryParse_WhenValidRevisionVersionGiven_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.0.0", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseRevisionVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1.0.0");
            version.IncreaseVersion(VersionPart.Revision);
            version.Version.Should().Be("1.0.1");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldDecreaseRevisionVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1.0.1");
            version.DecreaseVersion(VersionPart.Revision);
            version.Version.Should().Be("1.0.0");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldNotDecreaseRevisionVersion_WhenVersionWouldBeLowerThanZero()
        {
            var version = new VersionChanger("1.0.0");
            version.DecreaseVersion(VersionPart.Revision);
            version.Version.Should().Be("1.0.0");
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseRevisionVersion_WhenVersionContainsAllParts()
        {
            var version = new VersionChanger("1.0.0.0");
            version.IncreaseVersion(VersionPart.Revision);
            version.Version.Should().Be("1.0.1.0");
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseRevisionVersion_WhenVersionContainsAllPartsWithAsterisk()
        {
            var version = new VersionChanger("1.0.0.*");
            version.IncreaseVersion(VersionPart.Revision);
            version.Version.Should().Be("1.0.1.*");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldNotDecreaseRevisionVersion_WhenRevisionIsAsterisk()
        {
            var version = new VersionChanger("1.0.*");
            version.DecreaseVersion(VersionPart.Revision);
            version.Version.Should().Be("1.0.*");
        }

        [TestMethod]
        public void Ctor_WhenValidRevisionVersionGiven_ShouldBeTheSameAsVersionProperty()
        {
            new VersionChanger("1.0.0").Version.Should().Be("1.0.0");
        }

        [TestMethod]
        public void TryParse_WhenValidRevisionWithAsteriskVersionGiven_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.0.*", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void Ctor_WhenValidRevisionWithAsteriskVersionGiven_ShouldBeTheSameAsVersionProperty()
        {
            new VersionChanger("1.0.*").Version.Should().Be("1.0.*");
        }

        [TestMethod]
        public void TryParse_WhenValidBuildVersionGiven_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.0.0.0", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void IncreaseVersion_ShouldIncreaseBuildVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1.0.0.0");
            version.IncreaseVersion(VersionPart.Build);
            version.Version.Should().Be("1.0.0.1");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldDecreaseBuildVersion_WhenVersionIsValid()
        {
            var version = new VersionChanger("1.0.0.1");
            version.DecreaseVersion(VersionPart.Build);
            version.Version.Should().Be("1.0.0.0");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldNotDecreaseBuildVersion_WhenVersionWouldBeLowerThanZero()
        {
            var version = new VersionChanger("1.0.0.0");
            version.DecreaseVersion(VersionPart.Build);
            version.Version.Should().Be("1.0.0.0");
        }

        [TestMethod]
        public void IncreaseVersion_ShouldNotIncreaseBuildVersion_WhenBuildContainsAsterisk()
        {
            var version = new VersionChanger("1.0.0.*");
            version.IncreaseVersion(VersionPart.Build);
            version.Version.Should().Be("1.0.0.*");
        }

        [TestMethod]
        public void DecreaseVersion_ShouldNotDecreaseBuildVersion_WhenBuildIsAsterisk()
        {
            var version = new VersionChanger("1.0.0.*");
            version.IncreaseVersion(VersionPart.Build);
            version.Version.Should().Be("1.0.0.*");
        }

        [TestMethod]
        public void Ctor_WhenValidBuildVersionGiven_ShouldBeTheSameAsVersionProperty()
        {
            new VersionChanger("1.0.0.0").Version.Should().Be("1.0.0.0");
        }

        [TestMethod]
        public void TryParse_WhenValidBuildWithAsteriskVersionGiven_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.0.0.*", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void Ctor_WhenValidBuildWithAsteriskVersionGiven_ShouldBeTheSameAsVersionProperty()
        {
            new VersionChanger("1.0.0.*").Version.Should().Be("1.0.0.*");
        }

        [TestMethod]
        public void TryParse_WhenMajorContainsAsterisk_ShouldReturnFalse()
        {
            VersionChanger.TryParse("*", out VersionChanger version).Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_WhenMinorAndRevisionContainsAsterisk_2ndAsteriskShouldBeIgnored()
        {
            VersionChanger.TryParse("1.*.*.0", out VersionChanger version);
            version.Version.Should().Be("1.*");
        }

        [TestMethod]
        public void TryParse_WhenMinorAndBuildContainsAsterisk_2ndAsteriskShouldBeIgnored()
        {
            VersionChanger.TryParse("1.*.0.*", out VersionChanger version);
            version.Version.Should().Be("1.*");
        }

        [TestMethod]
        public void TryParse_WhenRevisionAndBuildContainsAsterisk_2ndAsteriskShouldBeIgnored()
        {
            VersionChanger.TryParse("1.0.*.*", out VersionChanger version);
            version.Version.Should().Be("1.0.*");
        }

        [TestMethod]
        public void TryParse_WhenMajorIsInValidButMinorIsNot_ShouldReturnFalse()
        {
            VersionChanger.TryParse("X.0", out VersionChanger version).Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_WhenMajorIsValidButMinorIsNot_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.X", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void TryParse_WhenMinorIsValidButRevisionIsNot_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.0.X", out VersionChanger version).Should().BeTrue();
        }

        [TestMethod]
        public void TryParse_WhenRevisionIsValidButBuildIsNot_ShouldReturnTrue()
        {
            VersionChanger.TryParse("1.0.X", out VersionChanger version).Should().BeTrue();
        }
    }
}
