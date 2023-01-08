/**
 * @param {Promise<Response>} func
 * @returns {Promise<{success: boolean, data: any, message: string, error: string}>}
 */
export const handleApiResponse = async (func) => {
    try {
        const response = await func;
        const data = await response.json();

        if (isApiResponseShape(data))
            return data;
        else {
            console.error(data);
            throw new Error("Unexpected response shape received");
        }
    }
    catch (error) {
        console.error(error);
        return {
            success: false,
            data: null,
            message: error.message,
            error: error.message
        };
    }
};

const isApiResponseShape = (resp) => {
    if (resp === null || resp === undefined)
        return false;
    if (typeof resp.success === 'boolean'
        && typeof resp.data === 'object'
        && typeof resp.message === 'string'
        && typeof resp.error === 'string')
        return true;
    return false;
};