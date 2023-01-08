import { baseUrl } from '../../Constants';
import { makeAddEmployee, makeDeleteEmployee, makeGetEmployees, makeUpdateEmployee } from './employees';
import { makeProcessPayrollForEmployee } from './payroll';

/**
 * @param {string} baseUrl 
 * @param {fetch} fetchImpl 
 * @returns 
 */
const makeSetupSdk = (baseUrl, fetchImpl) => {
    return {
        getEmployees: () => makeGetEmployees(baseUrl, fetchImpl),
        deleteEmployee: (employeeId) => makeDeleteEmployee(baseUrl, fetchImpl)(employeeId),
        addEmployee: (employee) => makeAddEmployee(baseUrl, fetchImpl)(employee),
        updateEmployee: (employeeId, employee) => makeUpdateEmployee(baseUrl, fetchImpl)(employeeId, employee),
        processPayrollForEmployee: (employeeId) => makeProcessPayrollForEmployee(baseUrl, fetchImpl)(employeeId)
    };
};

export const Sdk = makeSetupSdk(baseUrl, window.fetch);