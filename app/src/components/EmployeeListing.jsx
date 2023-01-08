import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Employee from './Employee/Employee';
import { useGetEmployees } from '../hooks/getEmployees';
import UpsertEmployeeModal from './Employee/UpsertEmployeeModal';
import DeleteEmployeeModal from './Employee/DeleteEmployeeModal';
import ProcessPayrollModal from './Payroll/ProcessPayrollModal';

const initialEmployeeState = {
    id: 0,
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    salary: 0
};

const EmployeeListing = () => {
    const [employees, setEmployees] = useState([]);
    const [selectedEmployee, setSelectedEmployee] = useState(initialEmployeeState);
    const [isUpsertModalOpen, setIsUpsertModalOpen] = useState(false);
    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
    const [isPayrollModalOpen, setIsPayrollModalOpen] = useState(false);
    const [dep, setDep] = useState(0);
    const [error, setError] = useState(null);

    const withResp = (data) => {
        console.log(data);
        setEmployees(data);
        setError(null);
    };

    const withError = (message) => {
        setEmployees([]);
        setError(message);
    };

    const onClick = (setModalOpen) => (employee) => {
        setSelectedEmployee(employee);
        setModalOpen(true);
    };

    const onSubmit = (setModalOpen) => () => {
        setDep(dep + 1);
        setModalOpen(false);
    };

    const onClose = (setModalOpen) => () => {
        setModalOpen(false);
        setSelectedEmployee(initialEmployeeState);
    };

    const onEditEmployeeClick = onClick(setIsUpsertModalOpen);
    const onEditEmployeeSubmit = onSubmit(setIsUpsertModalOpen);
    const onEditEmployeeClose = onClose(setIsUpsertModalOpen);

    const onDeleteEmployeeClick = onClick(setIsDeleteModalOpen);
    const onDeleteEmployeeSubmit = onSubmit(setIsDeleteModalOpen);
    const onDeleteEmployeeClose = onClose(setIsDeleteModalOpen);

    const onProcessPayrollClick = onClick(setIsPayrollModalOpen);
    const onProcessPayrollSubmit = onSubmit(setIsPayrollModalOpen);
    const onProcessPayrollClose = onClose(setIsPayrollModalOpen);

    useGetEmployees(withResp, withError, [dep]);

    const upsertEmployeeModalId = "upsert-employee-modal";
    const deleteEmployeeModalId = "delete-employee-modal";
    const processEmployeeModalId = "process-employee-modal";

    return (
        <div className="employee-listing">
            <table className="table caption-top">
                <caption>Employees</caption>
                <thead className="table-dark">
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">LastName</th>
                        <th scope="col">FirstName</th>
                        <th scope="col">DOB</th>
                        <th scope="col">Salary</th>
                        <th scope="col">Dependents</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map(({ id, firstName, lastName, dateOfBirth, salary, dependents }) => (
                        <Employee
                            key={id}
                            id={id}
                            employee={{
                                id, firstName, lastName, dateOfBirth, salary, dependents
                            }}
                            onEditClick={onEditEmployeeClick}
                            onDeleteClick={onDeleteEmployeeClick}
                            onProcessPayrollClick={onProcessPayrollClick}
                        />
                    ))}
                </tbody>
            </table>
            <Button onClick={() => setIsUpsertModalOpen(true)} variant="primary">Add Employee</Button>
            <UpsertEmployeeModal
                isOpen={isUpsertModalOpen}
                onClose={onEditEmployeeClose}
                onSubmit={onEditEmployeeSubmit}
                employee={selectedEmployee}
                setSelectedEmployee={setSelectedEmployee}
            />
            <DeleteEmployeeModal
                isOpen={isDeleteModalOpen}
                onClose={onDeleteEmployeeClose}
                onSubmit={onDeleteEmployeeSubmit}
                employee={selectedEmployee}
                setSelectedEmployee={setSelectedEmployee}
            />
            <ProcessPayrollModal
                isOpen={isPayrollModalOpen}
                onClose={onProcessPayrollClose}
                onSubmit={onProcessPayrollSubmit}
                employee={selectedEmployee}
                setSelectedEmployee={setSelectedEmployee}
            />
        </div>
    );
};

export default EmployeeListing;