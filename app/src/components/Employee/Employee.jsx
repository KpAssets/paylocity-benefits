import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';
import { currencyFormat } from "../../Constants";

/**
 * @param {{id: number, employee: {id: number, firstName: string, lastName: string, salary: number, dateOfBirth: string dependents: number }, onEditClick: (employee) => {}, onDeleteClick: (employee) => {}, onProcessPayrollClick: (employee) => {}}} props
 * @returns
 */
const Employee = (props) => {
    const firstName = props.employee?.firstName || '';
    const lastName = props.employee?.lastName || '';
    const salary = props.employee?.salary || 0;

    const onClick = (parentClick) => {
        parentClick(props.employee);
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
                <ButtonGroup>
                    <Button onClick={() => onClick(props.onEditClick)} variant="secondary">Edit</Button>
                    <Button onClick={() => onClick(props.onDeleteClick)} variant="danger">Delete</Button>
                    <Button onClick={() => onClick(props.onProcessPayrollClick)} variant="success">Process Payroll</Button>
                </ButtonGroup>
            </td>
        </tr >
    );
};

export default Employee;