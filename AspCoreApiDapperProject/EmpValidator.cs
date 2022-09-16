using AspCoreApiDapperProject.Models;
using FluentValidation;
using Microsoft.Azure.Management.Batch.Fluent.Models;

namespace AspCoreApiDapperProject
{
    public class EmpValidator : AbstractValidator<Employee>
    {
        public EmpValidator()
        {
            RuleFor(u => u.EmpName).NotNull().NotEmpty();
            RuleFor(u => u.EmpDepartment).NotNull().NotEmpty();
            RuleFor(u => u.EmpCode).NotNull().NotEmpty();
            RuleFor(u => u.Age).NotEmpty().InclusiveBetween(1, 100);
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.TelephoneNo).Cascade(CascadeMode.Stop).Length(10).Matches(@"\d{10}");
            //RuleFor(u => u.Gender).Cascade(CascadeMode.Stop).NotEmpty().IsInEnum().WithMessage(string.Format(ResourceFile.Read("InvalidValueMessage"), "male","female"));
            RuleFor(u => u.Gender).NotEmpty().NotNull();
        }

    }
}
