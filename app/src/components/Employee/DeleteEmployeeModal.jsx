import { useState } from "react";
import Modal from "../Modal";
import { useDeleteEmployee } from '../../hooks/deleteEmployee';

/**
 * @param {{isOpen: boolean, onClose: () => {}, submitButtonName: string, onSubmit: () => {}, employee: object}} props
 * @returns
 */
const DeleteEmployeeModal = (props) => {
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

    const deleteEmployee = useDeleteEmployee(withResp, withError);

    const onSubmit = () => {
        setCanSubmit(false);
        deleteEmployee(props.employee.id);
    };

    return (
        <Modal
            isOpen={props.isOpen}
            onClose={props.onClose}
            title={props?.title || "Delete Employee?"}
            submitButtonName={props?.submitButtonName || "Delete"}
            onSubmit={onSubmit}
            canSubmit={canSubmit}
        >
            <div>
                <p>
                    Are you sure you want to delete {props.employee.firstName} {props.employee.lastName}?
                </p>
            </div>
        </Modal>
    );
};

export default DeleteEmployeeModal;