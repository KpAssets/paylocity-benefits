import { handleApiResponse } from "./utils";

const payrollUrl = 'api/v1/payroll';

/**
 * @param {string} baseUrl
 * @param {fetch} fetchImpl
 * @returns
 */
export const makeProcessPayrollForEmployee = (baseUrl, fetchImpl) =>
    /**
     * @param {number} employeeId 
     * @returns 
     */
    (employeeId) =>
        handleApiResponse(fetchImpl(`${baseUrl}/${payrollUrl}/${employeeId}`, { mode: 'cors', method: 'POST' }));