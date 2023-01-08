import { useState } from "react";
import Modal from "./Modal";
import { useAddEmployee } from '../hooks/addEmployee';
import { useUpdateEmployee } from '../hooks/updateEmployee';
import EmployeeForm from "./EmployeeForm";

/**
 * @param {{id: string, title: string, submitButtonName: string, onSubmit: () => {}, employee: object}} props
 * @returns
 */
const UpsertEmployeeModal = (props) => {
    const [canSubmit, setCanSubmit] = useState(true);

    const withResp = (data) => {
        console.log(data);
        props.onSubmit();
        setCanSubmit(true);
    };

    const withError = (message) => {
        console.error(message);
        setCanSubmit(true);
    };

    const addEmployee = useAddEmployee(withResp, withError);
    const updateEmployee = useUpdateEmployee(withResp, withError);

    const onSubmit = () => {
        setCanSubmit(false);

        if (!props.employee.id)
            addEmployee(props.employee);
        else
            updateEmployee(props.employee);
    };

    return (
        <Modal
            id={props.id}
            title={props.title}
            submitButtonName={props?.submitButtonName || "Submit"}
            onSubmit={onSubmit}
            canSubmit={canSubmit}
            children={
                <EmployeeForm
                    employee={props.employee}
                    setSelectedEmployee={props.setSelectedEmployee}
                />}
        />
    );
};

export default UpsertEmployeeModal;