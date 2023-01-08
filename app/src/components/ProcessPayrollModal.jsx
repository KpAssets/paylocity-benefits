import { useState } from "react";
import Modal from "./Modal";
import { useProcessPayrollForEmployee } from "../hooks/processPayroll";

/**
 * @param {{id: string, title: string, submitButtonName: string, onSubmit: () => {}, employee: object}} props
 * @returns
 */
const ProcessPayrollModal = (props) => {
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

    const processPayrollForEmployee = useProcessPayrollForEmployee(withResp, withError);

    const onSubmit = () => {
        setCanSubmit(false);
        processPayrollForEmployee(props.employee.id);
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
                </div>}
        />
    );
};

export default ProcessPayrollModal;