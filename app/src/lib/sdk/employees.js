import { handleApiResponse } from './utils';

const employeesUrl = 'api/v1/employees'
/**
 * @param {string} baseUrl
 * @param {fetch} fetchImpl
 * @returns 
 */
export const makeGetEmployees = (baseUrl, fetchImpl) =>
    handleApiResponse(fetchImpl(`${baseUrl}/${employeesUrl}`));

/**
 * @param {string} baseUrl
 * @param {fetch} fetchImpl
 * @returns
 */
export const makeDeleteEmployee = (baseUrl, fetchImpl) =>
    /**
     * 
     * @param {number} employeeId 
     * @returns
     */
    (employeeId) =>
        handleApiResponse(fetchImpl(`${baseUrl}/${employeesUrl}/${employeeId}`, { mode: 'cors', method: 'DELETE' }));

/**
 * @param {string} baseUrl
 * @param {fetch} fetchImpl
 * @returns
 */
export const makeAddEmployee = (baseUrl, fetchImpl) =>
    /**
     * @param {{firstName: string, lastName: string, dateOfBirth: string, salary: number}} employee 
     * @returns 
     */
    (employee) =>
        handleApiResponse(
            fetchImpl(
                `${baseUrl}/${employeesUrl}`,
                {
                    mode: 'cors',
                    method: 'POST',
                    body: JSON.stringify(employee),
                    headers: {
                        'Content-Type': 'application/json'
                    },
                }));

/**
 * @param {string} baseUrl
 * @param {fetch} fetchImpl
 * @returns
 */
export const makeUpdateEmployee = (baseUrl, fetchImpl) =>
    /**
     * @param {{id: number, firstName: string, lastName: string, dateOfBirth: string, salary: number}} employee 
     * @returns 
     */
    (employee) =>
        handleApiResponse(
            fetchImpl(
                `${baseUrl}/${employeesUrl}/${employee.id}`,
                {
                    mode: 'cors',
                    method: 'PUT',
                    body: JSON.stringify(employee),
                    headers: {
                        'Content-Type': 'application/json'
                    },
                }));