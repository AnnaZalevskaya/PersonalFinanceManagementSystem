export interface RegisterRequest
{
    email: string;
    username: string;
    phoneNumber: string;
    password: string;
    passwordConfirm: string;
}

export interface RegisterResponse
{
    email: string;
    username: string;
    phoneNumber: string;
}