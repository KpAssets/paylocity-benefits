import { Sdk } from "../lib/sdk"

/**
 * @param {Sdk} sdk
 * @returns
 */
const makeUpdateEmployee = (sdk) =>
    /**
     * @param {(data: any) => {}} withResp 
     * @param {(error: string) => {}} withError 
     * @returns
     */
    (withResp, withError) =>
        /**
         * @param {{id: number, firstName: string, lastName: string, dateOfBirth: string, salary: number}} employee 
         */
        (employee) => {
            sdk.updateEmployee(employee).then(resp => {
                if (resp.success)
                    withResp(resp.data);
                else
                    withError(`${resp.message}:${resp.error}`);
            })
        };

export const useUpdateEmployee = makeUpdateEmployee(Sdk);