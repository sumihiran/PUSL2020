using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PUSL2020.Application.DataAnnotations;
using PUSL2020.Domain.ValueObjects;
using Xunit;

namespace PUSL2020.Application.Tests;

public class NicAttributeTests
{
    private class Model
    {
        [Nic] public Nic Nic { get; set; }
    }

    [Fact]
    public void Nic_ValidModelWithNic_ValidationPass()
    {
        var model = new Model()
        {
            Nic = new Nic("981360566V")
        };

        var errors = ValidateModel(model);
        Assert.True(errors.Count == 0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("981360988")]
    [InlineData("2000159320290")]
    public void Nic_ModelWithInvalidLengthNic_ReturnError(string nic)
    {
        var model = new Model()
        {
            Nic = new Nic(nic)
        };

        var errors = ValidateModel(model);
        Assert.Contains(errors,
            e => e.ErrorMessage != null && e.ErrorMessage.Contains("Nic must contain either 10 or 12 characters"));
    }

    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults;
    }
}