import { Sdk } from "../lib/sdk"

/**
 * @param {Sdk} sdk
 * @returns
 */
const makeAddEmployee = (sdk) =>
    /**
     * @param {(data: any) => {}} withResp 
     * @param {(error: string) => {}} withError 
     * @returns
     */
    (withResp, withError) =>
        /**
         * @param {{firstName: string, lastName: string, dateOfBirth: string, salary: number}} employee 
         */
        (employee) => {
            sdk.addEmployee(employee).then(resp => {
                if (resp.success)
                    withResp(resp.data);
                else
                    withError(`${resp.message}:${resp.error}`);
            })
        };

export const useAddEmployee = makeAddEmployee(Sdk);