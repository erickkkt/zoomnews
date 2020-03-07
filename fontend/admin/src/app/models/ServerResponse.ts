export interface ServerResponse<T> {
    requestWasSuccessful: boolean;
    responseData?: T;
}
