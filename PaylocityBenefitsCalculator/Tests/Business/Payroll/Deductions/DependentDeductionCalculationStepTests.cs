using Business.Dependents;
using Business.Employees;
using Business.Payroll;
using Business.Payroll.Deductions;

namespace Tests.Business.Payroll.Deductions;

public class DependentDeductionCalculationStepTests
{
    [
        Theory,
        ClassData(typeof(CanCalculateDeductionsForDependentsTheoryData))
    ]
    public async Task CanCalculateDeductionsForDependents(Paycheck check, IEnumerable<Deduction> expectedDeductions)
    {
        var target = BuildTestTarget();

        var actual = await target.Process(check);

        DeductionTestHelpers.AssertEqual(expectedDeductions, actual.Deductions);
    }

    private IDeductionCalculationStep BuildTestTarget() =>
        new DependentDeductionCalculationStep();

    #region Theories
    private class CanCalculateDeductionsForDependentsTheoryData : TheoryData<Paycheck, IEnumerable<Deduction>>
    {
        public CanCalculateDeductionsForDependentsTheoryData()
        {
            Add(
                new Paycheck(new Employee()),
                new List<Deduction>());
            Add(
                new Paycheck(new Employee
                {
                    Dependents = null
                }),
                new List<Deduction>());
            Add(
                new Paycheck(new Employee
                {
                    Dependents = new List<Dependent>
                    {
                        new Dependent
                        {
                            FirstName = "Dependent",
                            LastName = "One"
                        }
                    }
                }),
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 276.92m,
                        Description = "Dependent deduction for Dependent One"
                    }
                });
            Add(
                new Paycheck(new Employee
                {
                    Dependents = new List<Dependent>
                    {
                        new Dependent
                        {
                            FirstName = "Dependent",
                            LastName = "One"
                        },
                        new Dependent
                        {
                            FirstName = "Dependent",
                            LastName = "Two"
                        }
                    }
                }),
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 276.92m,
                        Description = "Dependent deduction for Dependent One"
                    },
                    new Deduction
                    {
                        Amount = 276.92m,
                        Description = "Dependent deduction for Dependent Two"
                    }
                });
        }
    }
    #endregion Theories
}