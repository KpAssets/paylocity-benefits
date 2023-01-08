import { Sdk } from "../lib/sdk"

/**
 * @param {Sdk} sdk
 * @returns
 */
const makeProcessPayrollForEmployee = (sdk) =>
    /**
     * @param {(data: any) => {}} withResp 
     * @param {(error: string) => {}} withError 
     * @returns
     */
    (withResp, withError) =>
        /**
         * @param {number} employeeId
         */
        (employeeId) => {
            sdk.processPayrollForEmployee(employeeId).then(resp => {
                if (resp.success)
                    withResp(resp.data);
                else
                    withError(`${resp.message}:${resp.error}`);
            })
        };

export const useProcessPayrollForEmployee = makeProcessPayrollForEmployee(Sdk);