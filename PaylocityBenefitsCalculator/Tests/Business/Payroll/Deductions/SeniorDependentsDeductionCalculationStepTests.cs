using Business.Dependents;
using Business.Employees;
using Business.Payroll;
using Business.Payroll.Deductions;

namespace Tests.Business.Payroll.Deductions;

public class SeniorDependentsDeductionCalculationStepTests
{
    [
        Theory,
        ClassData(typeof(CanCalculateSeniorDependentDeductionTheoryData))
    ]
    public async Task CanCalculateSeniorDependentDeductionTests(Paycheck check, IEnumerable<Deduction> expectedDeductions)
    {
        var target = BuildTestTarget();

        var actual = await target.Process(check);

        DeductionTestHelpers.AssertEqual(expectedDeductions, actual.Deductions);
    }

    private IDeductionCalculationStep BuildTestTarget() =>
        new SeniorDependentsDeductionCalculationStep();

    #region Theories
    private class CanCalculateSeniorDependentDeductionTheoryData : TheoryData<Paycheck, IEnumerable<Deduction>>
    {
        public CanCalculateSeniorDependentDeductionTheoryData()
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
                            DateOfBirth = DateTime.Today
                        }
                    }
                }),
                new List<Deduction>());
            Add(
                new Paycheck(new Employee
                {
                    Dependents = new List<Dependent>
                    {
                        new Dependent
                        {
                            DateOfBirth = DateTime.Today - (DateTime.Today - DateTime.Today.AddYears(-30))
                        }
                    }
                }),
                new List<Deduction>());
            Add(
                new Paycheck(new Employee
                {
                    Dependents = new List<Dependent>
                    {
                        new Dependent
                        {
                            DateOfBirth = DateTime.Today - (DateTime.Today - DateTime.Today.AddYears(-49))
                        }
                    }
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
                            LastName = "One",
                            DateOfBirth = DateTime.Today - (DateTime.Today - DateTime.Today.AddYears(-50))
                        }
                    }
                }),
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 92.31m,
                        Description= "Senior dependent deduction for Dependent One"
                    }
                });
            Add(
                new Paycheck(new Employee
                {
                    Dependents = new List<Dependent>
                    {
                        new Dependent
                        {
                            DateOfBirth = DateTime.Today
                        },
                        new Dependent
                        {
                            FirstName = "Dependent",
                            LastName = "One",
                            DateOfBirth = DateTime.Today - (DateTime.Today - DateTime.Today.AddYears(-50))
                        }
                    }
                }),
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 92.31m,
                        Description= "Senior dependent deduction for Dependent One"
                    }
                });
            Add(
                new Paycheck(new Employee
                {
                    Dependents = new List<Dependent>
                    {
                        new Dependent
                        {
                            DateOfBirth = DateTime.Today
                        },
                        new Dependent
                        {
                            FirstName = "Dependent",
                            LastName = "One",
                            DateOfBirth = DateTime.Today - (DateTime.Today - DateTime.Today.AddYears(-50))
                        },
                        new Dependent
                        {
                            FirstName = "Dependent",
                            LastName = "Two",
                            DateOfBirth = DateTime.Today - (DateTime.Today - DateTime.Today.AddYears(-50))
                        }
                    }
                }),
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 92.31m,
                        Description= "Senior dependent deduction for Dependent One"
                    },
                    new Deduction
                    {
                        Amount = 92.31m,
                        Description= "Senior dependent deduction for Dependent Two"
                    }
                });
        }
    }
    #endregion
}