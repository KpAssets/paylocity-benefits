import { baseUrl } from '../../Constants';
import { makeAddEmployee, makeDeleteEmployee, makeGetEmployees, makeUpdateEmployee } from './employees';

/**
 * 
 * @param {string} baseUrl 
 * @param {fetch} fetchImpl 
 * @returns 
 */
const makeSetupSdk = (baseUrl, fetchImpl) => {
    return {
        getEmployees: () => makeGetEmployees(baseUrl, fetchImpl),
        deleteEmployee: (employeeId) => makeDeleteEmployee(baseUrl, fetchImpl)(employeeId),
        addEmployee: (employee) => makeAddEmployee(baseUrl, fetchImpl)(employee),
        updateEmployee: (employeeId, employee) => makeUpdateEmployee(baseUrl, fetchImpl)(employeeId, employee)
    };
};

export const Sdk = makeSetupSdk(baseUrl, window.fetch);