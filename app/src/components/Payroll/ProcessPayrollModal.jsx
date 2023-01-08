import { useState } from "react";
import Modal from "../Modal";
import { useProcessPayrollForEmployee } from "../../hooks/processPayroll";
import PayrollDetail from "./PayrollDetail";

const initialPayrollState = {
    id: '',
    netPay: 0.00,
    employee: {},
    earnings: [],
    deductions: []
};

/**
 * @param {{isOpen: boolean, onClose: () => {}, submitButtonName: string, onSubmit: () => {}, employee: object}} props
 * @returns
 */
const ProcessPayrollModal = (props) => {
    const [payroll, setPayroll] = useState(initialPayrollState);
    const [canSubmit, setCanSubmit] = useState(true);

    const withResp = (data) => {
        setPayroll(data);
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

    const onClose = () => {
        props.onClose();
        setPayroll(initialPayrollState);
    }

    return (
        <Modal
            isOpen={props.isOpen}
            onClose={onClose}
            title={props?.title || `Process ${props.employee.firstName}'s payroll?`}
            submitButtonName={props?.submitButtonName || "Process Payroll"}
            onSubmit={onSubmit}
            canSubmit={canSubmit}
        >
            <PayrollDetail payroll={payroll} />
        </Modal>
    );
};

export default ProcessPayrollModal;