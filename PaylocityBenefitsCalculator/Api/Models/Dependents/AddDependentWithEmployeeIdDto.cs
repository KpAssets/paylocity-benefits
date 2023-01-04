using Api.Models.Validations;

namespace Api.Models.Dependents
{
    public class AddDependentWithEmployeeIdDto : AddDependentDto
    {
        public uint EmployeeId { get; set; }

        protected override void GatherInvalidReasons()
        {
            EmployeeId.GreaterThan(InvalidReasons, 0);
            base.GatherInvalidReasons();
        }
    }
}
