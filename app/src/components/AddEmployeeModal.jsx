import UpsertEmployeeModal from "./UpsertEmployeeModal";
import { useAddEmployee } from '../hooks/addEmployee';

const AddEmployeeModal = (props) => {

    const withResp = (data) => {
        console.log(data);
    };

    const withError = (message) => {
        console.error(message);
    };

    const addEmployee = useAddEmployee(withResp, withError);

    const onSubmit = () => {
        console.log(props.employee);
        props.onSubmit();
    }
    return (
        <UpsertEmployeeModal
            id={props.id}
            title='Add Employee'
            submitButtonName='Submit'
            employee={props.employee}
            onSubmit={onSubmit} />
    );
};

export default AddEmployeeModal;