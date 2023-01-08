import { useState } from "react";
import Modal from "./Modal";
import { useAddEmployee } from '../hooks/addEmployee';
import { useUpdateEmployee } from '../hooks/updateEmployee';

/**
 * @param {{id: string, title: string, submitButtonName: string, onSubmit: () => {}, employee: object}} props
 * @returns
 */
const UpsertEmployeeModal = (props) => {
    const [canSubmit, setCanSubmit] = useState(true);

    const withResp = (data) => {
        console.log(data);
        props.onSubmit();
    };

    const withError = (message) => {
        console.error(message);
    };

    const addEmployee = useAddEmployee(withResp, withError);
    const updateEmployee = useUpdateEmployee(withResp, withError);

    const onSubmit = () => {
        if (!props.employee.id)
            addEmployee(props.employee);
        else
            updateEmployee(props.employee);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;

        props.setSelectedEmployee({
            ...props.employee,
            [name]: value
        });
    };

    return (
        <Modal
            id={props.id}
            title={props.title}
            submitButtonName={props?.submitButtonName || "Submit"}
            onSubmit={onSubmit}
            canSubmit={canSubmit}
            children={
                <form>
                    <div className="mb-3">
                        <label htmlFor="first-name" className="col-form-label">First Name:</label>
                        <input type="text" className="form-control" id="first-name" name="firstName" value={props.employee.firstName} onChange={handleChange} />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="last-name" className="col-form-label">Last Name:</label>
                        <input type="text" className="form-control" id="last-name" name="lastName" value={props.employee.lastName} onChange={handleChange} />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="date-of-birth" className="col-form-label">Date Of Birth:</label>
                        <input type="datetime-local" className="form-control" id="date-of-birth" name="dateOfBirth" value={props.employee.dateOfBirth} onChange={handleChange} />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="salary" className="col-form-label">Salary:</label>
                        <input type="number" className="form-control" id="salary" name="salary" value={props.employee.salary} onChange={handleChange} />
                    </div>
                </form>}
        />
    );
};

export default UpsertEmployeeModal;