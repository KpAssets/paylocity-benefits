using Business.Employees;
using Business.Payroll;
using Business.Payroll.Earnings;
using Business.Payroll.Deductions;

namespace Tests.Business.Payroll;

public class PayrollServiceTests
{
    [
        Theory,
        ClassData(typeof(CanCalculatePaycheckForEmployeeTheoryData))
    ]
    public async Task CanCalculatePaycheckForEmployee(
        Employee employee,
        IEnumerable<Earning> earnings,
        IEnumerable<Deduction> deductions,
        decimal expectedNetPay)
    {
        var employeeService = new Mock<IEmployeeService>();
        employeeService
            .Setup(x => x.GetAsync(It.IsAny<uint>()))
            .ReturnsAsync(employee)
            .Verifiable();

        var earningsService = new Mock<ICalculationStepService<IEarningCalculationStep>>();
        earningsService
            .Setup(x => x.Process(It.IsAny<Paycheck>()))
            .Callback<Paycheck>(p => p.Earnings = earnings.ToList());

        var deductionsService = new Mock<ICalculationStepService<IDeductionCalculationStep>>();
        deductionsService
            .Setup(x => x.Process(It.IsAny<Paycheck>()))
            .Callback<Paycheck>(p => p.Deductions = deductions.ToList());

        var target = BuildTestTarget(employeeService, earningsService, deductionsService);

        var actual = await target.CalculatePaycheckForEmployee(1);

        Assert.NotNull(actual);
        Assert.Equal(expectedNetPay, actual.NetPay);

        employeeService.Verify(x => x.GetAsync(It.Is<uint>(x => x == 1)), Times.Once);
        earningsService.Verify(x => x.Process(It.IsAny<Paycheck>()), Times.Once);
        deductionsService.Verify(x => x.Process(It.IsAny<Paycheck>()), Times.Once);
    }

    private IPayrollService BuildTestTarget(
        Mock<IEmployeeService> employeeService = null,
        Mock<ICalculationStepService<IEarningCalculationStep>> earningsService = null,
        Mock<ICalculationStepService<IDeductionCalculationStep>> deductionsService = null)
    {
        employeeService ??= new Mock<IEmployeeService>();
        earningsService ??= new Mock<ICalculationStepService<IEarningCalculationStep>>();
        deductionsService ??= new Mock<ICalculationStepService<IDeductionCalculationStep>>();

        return new PayrollService(
            employeeService.Object,
            earningsService.Object,
            deductionsService.Object);
    }

    #region Theories
    private class CanCalculatePaycheckForEmployeeTheoryData : TheoryData<Employee, IEnumerable<Earning>, IEnumerable<Deduction>, decimal>
    {
        public CanCalculatePaycheckForEmployeeTheoryData()
        {
            Add(
                new Employee(),
                new List<Earning>
                {
                    new Earning
                    {
                        Amount = 100
                    }
                },
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 50
                    }
                },
                50);
            Add(
                new Employee { Salary = 92365.22m },
                new List<Earning>
                {
                    new Earning
                    {
                        Amount = 3552.51m
                    }
                },
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 461.54m
                    },
                    new Deduction
                    {
                        Amount = 276.92m
                    },
                    new Deduction
                    {
                        Amount = 276.92m
                    },
                    new Deduction
                    {
                        Amount = 276.92m
                    },
                    new Deduction
                    {
                        Amount = 71.05m
                    }
                },
                2189.16m);
            Add(
                new Employee { Salary = 75420.99m },
                new List<Earning>
                {
                    new Earning
                    {
                        Amount = 2900.81m
                    }
                },
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 461.54m
                    }
                },
                2439.27m);
            Add(
                new Employee { Salary = 143211.12m },
                new List<Earning>
                {
                    new Earning
                    {
                        Amount = 5508.12m
                    }
                },
                new List<Deduction>
                {
                    new Deduction
                    {
                        Amount = 461.54m
                    },
                    new Deduction
                    {
                        Amount = 276.92m
                    },
                    new Deduction
                    {
                        Amount = 110.16m
                    }
                },
                4659.50m);
        }

    }
    #endregion Theories
}