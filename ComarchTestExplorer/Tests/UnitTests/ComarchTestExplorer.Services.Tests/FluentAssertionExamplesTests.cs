using FluentAssertions;
using FluentAssertions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchTestExplorer.Services.Tests;

public class FluentAssertionExamplesTests
{
    [Test]
    public void FluentAssertionStringExample1()
    {
        // Arrange
        string actual = "Hello, World!";

        // Assert
        actual.Should().NotBeEmpty().And.StartWith("Hello").And.EndWith("!").And.Contain("Wor").And.HaveLength(13);
    }

    [Test]
    public async Task FluentAssertionDateTimeTest()
    {
        DateTime actual = 26.September(2024).At(9, 49, 55).AsLocal();

        actual.Should().BeAfter(25.September(2024).At(9, 0)).And.BeIn(DateTimeKind.Local)
            .And.BeCloseTo(27.September(2024), TimeSpan.FromDays(1));

    }
}
