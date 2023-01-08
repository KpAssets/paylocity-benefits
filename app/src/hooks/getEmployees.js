import { useEffect, DependencyList } from "react";
import { Sdk } from "../lib/sdk";

/**
 * 
 * @param {Sdk} sdk
 * @param {useEffect} useEffectImpl
 * @returns
 */
const makeUseGetEmployees = (sdk, useEffectImpl) =>
    /**
     * @param {(data: any) => {}} withResp
     * @param {(error: string) => {}} withError
     * @param {DependencyList} deps
     */
    (withResp, withError, deps = []) => {
        useEffectImpl(() => {
            sdk.getEmployees().then(resp => {
                if (resp.success)
                    withResp(resp.data)
                else
                    withError(`${resp.message}:${resp.error}}`);
            })
        }, deps);
    };

export const useGetEmployees = makeUseGetEmployees(Sdk, useEffect);