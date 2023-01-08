import { useState } from "react";
import Modal from "../Modal";
import { useAddEmployee } from '../../hooks/addEmployee';
import { useUpdateEmployee } from '../../hooks/updateEmployee';
import EmployeeForm from "../EmployeeForm";

/**
 * @param {{isOpen: boolean, onClose: () => {}, submitButtonName: string, onSubmit: () => {}, employee: object}} props
 * @returns
 */
const UpsertEmployeeModal = (props) => {
    const [canSubmit, setCanSubmit] = useState(true);

    const withResp = (data) => {
        console.log(data);
        props.onSubmit();
        setCanSubmit(true);
        props.onClose();
    };

    const withError = (message) => {
        console.error(message);
        setCanSubmit(true);
        props.onClose();
    };

    const getTitle = () => {
        if (!props.employee.id)
            return 'Add Employee'
        return 'Edit Employee';
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
            isOpen={props.isOpen}
            onClose={props.onClose}
            title={getTitle()}
            submitButtonName={props?.submitButtonName || "Submit"}
            onSubmit={onSubmit}
            canSubmit={canSubmit}
        >
            <EmployeeForm
                employee={props.employee}
                setSelectedEmployee={props.setSelectedEmployee}
            />
        </Modal>
    );
};

export default UpsertEmployeeModal;