using System.ComponentModel.DataAnnotations;
using PUSL2020.Domain.Validators;
using Xunit;

namespace PUSL2020.Domain.Tests;

public class NicValidatorTests
{
    [Theory]
    [InlineData("981360566V")]
    public void IsValid_OldNicFormat_ReturnTrue(string nic)
    {
        Assert.True(NicValidator.IsValid(nic));
    }
    
    [Theory]
    [InlineData("199813600988")]
    [InlineData("200015932029")]
    public void IsValid_NewNicFormat_ReturnTrue(string nic)
    {
        Assert.True(NicValidator.IsValid(nic));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("981360988")]
    [InlineData("2000159320290")]
    public void IsValid_WhenLengthIsInvalid_ThrowsValidationException(string nic)
    {
        var exception = Assert.Throws<ValidationException>(() =>
        {
            NicValidator.IsValid(nic);
        });

        Assert.Contains("Nic must contain either 10 or 12 characters", exception.Message);
    }
    
    [Theory]
    [InlineData("ABCDEFGHIJ")]
    [InlineData("2000/08/28")]
    public void IsValid_WhenLengthIsValidFormatInvalid_ReturnFalse(string nic)
    {
        Assert.False(NicValidator.IsValid(nic));
    }
}