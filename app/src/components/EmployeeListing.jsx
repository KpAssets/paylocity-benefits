import { useState } from 'react';
import Employee from './Employee';
import { useGetEmployees } from '../hooks/getEmployees';
import UpsertEmployeeModal from './UpsertEmployeeModal';
import DeleteEmployeeModal from './DeleteEmployeeModal';

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

    useGetEmployees(withResp, withError, [dep]);

    const upsertEmployeeModalId = "upsert-employee-modal";
    const deleteEmployeeModalId = "delete-employee-modal";

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
                            onClick={setSelectedEmployee}
                            editModalId={upsertEmployeeModalId}
                            deleteModalId={deleteEmployeeModalId}
                        />
                    ))}
                </tbody>
            </table>
            <button type="button" onClick={() => setSelectedEmployee(initialEmployeeState)} className="btn btn-primary" data-bs-toggle="modal" data-bs-target={`#${upsertEmployeeModalId}`}>Add Employee</button>
            <UpsertEmployeeModal
                id={upsertEmployeeModalId}
                title='Add Employee'
                employee={selectedEmployee}
                setSelectedEmployee={setSelectedEmployee}
                onSubmit={() => { setDep(dep + 1) }}
            />
            <DeleteEmployeeModal
                id={deleteEmployeeModalId}
                title='Delete Employee?'
                employee={selectedEmployee}
                setSelectedEmployee={setSelectedEmployee}
                onSubmit={() => { setDep(dep + 1) }}
            />
        </div>
    );
};

export default EmployeeListing;