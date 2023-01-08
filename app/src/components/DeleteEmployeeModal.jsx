import { useState } from "react";
import Modal from "./Modal";
import { useDeleteEmployee } from '../hooks/deleteEmployee';

/**
 * @param {{id: string, title: string, submitButtonName: string, onSubmit: () => {}, employee: object}} props
 * @returns
 */
const DeleteEmployeeModal = (props) => {
    const [canSubmit, setCanSubmit] = useState(true);

    const withResp = (data) => {
        console.log(data);
        props.onSubmit();
    };

    const withError = (message) => {
        console.error(message);
    };

    const deleteEmployee = useDeleteEmployee(withResp, withError);

    const onSubmit = () => {
        deleteEmployee(props.employee.id);
    };

    return (
        <Modal
            id={props.id}
            title={props.title}
            submitButtonName={props?.submitButtonName || "Delete"}
            onSubmit={onSubmit}
            canSubmit={canSubmit}
            children={
                <div>
                    Are you sure you want to delete {props.employee.firstName} {props.employee.lastName}?
                </div>}
        />
    );
};

export default DeleteEmployeeModal;