import { currencyFormat } from "../Constants";

/**
 * @param {{id: number, employee: {id: number, firstName: string, lastName: string, salary: number, dateOfBirth: string dependents: number }, onClick: (employee) => {}}} props
 * @returns
 */
const Employee = (props) => {
    const firstName = props.employee?.firstName || '';
    const lastName = props.employee?.lastName || '';
    const salary = props.employee?.salary || 0;

    const onClick = () => {
        console.log(props.employee);
        props.onClick(props.employee);
    };

    return (
        <tr>
            <th scope="row">{props.id}</th>
            <td>{lastName}</td>
            <td>{firstName}</td>
            <td>{props.employee?.dateOfBirth || ''}</td>
            <td>{currencyFormat(salary)}</td>
            <td>{props.employee?.dependents?.length || 0}</td>
            <td>
                <div className="btn-group" role="group">
                    <button onClick={onClick} type="button" className="btn btn-secondary" data-bs-toggle="modal" data-bs-target={`#${props.editModalId}`}>Edit</button>
                    <button onClick={onClick} type="button" className="btn btn btn-danger" data-bs-toggle="modal" data-bs-target={`#${props.deleteModalId}`}>Delete</button>
                    <button onClick={onClick} type="button" className="btn btn-success" data-bs-toggle="modal" data-bs-target={`#${props.payrollModalId}`}> Process Payroll</button>
                </div>
            </td>
        </tr >
    );
};

export default Employee;