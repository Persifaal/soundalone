export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    tag: string;
    email: string;
    password: string;
    confirmPassword: string;
}

export interface AuthResponse {
    accessToken: string;
}