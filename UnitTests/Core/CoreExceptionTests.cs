using System;
using AutoFixture.Xunit2;
using CleanApi.Core.Exceptions;
using FluentAssertions;
using Xunit;

namespace UnitTests.Core
{
    public class CoreExceptionTests
    {
        [Fact]
        public void Ctor_Empty()
        {
            var coreException = new CoreException();
            coreException.Should().BeOfType<CoreException>();
        }

        [Theory]
        [AutoData]
        public void Ctor_Message(string message)
        {
            var coreException = new CoreException(message);
            coreException.Message.Should().Be(message);
        }

        [Theory]
        [AutoData]
        public void Ctor_Message_InnerException(string message, Exception innerException)
        {
            var coreException = new CoreException(message,innerException);
            coreException.Message.Should().Be(message);
            coreException.InnerException.Should().Be(innerException);
        }
    }
}