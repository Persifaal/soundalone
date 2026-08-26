import React, {
    createContext,
    useContext,
    useEffect,
    useState
} from "react";

import Cookies from "universal-cookie";

import { UserProfile } from "../types/UserProfile";
import {
    LoginRequest,
    RegisterRequest
} from "../types/Auth";

interface AuthContextType {
    user: UserProfile | null;

    isAuthenticated: boolean;
    isLoading: boolean;

    login: (data: LoginRequest) => Promise<void>;
    register: (data: RegisterRequest) => Promise<void>;

    logout: () => void;
    refreshUser: () => Promise<void>;
}

const AuthContext =
    createContext<AuthContextType | undefined>(undefined);

const API_URL = "http://localhost:8081/api/auth";

export function AuthProvider({
                                 children
                             }: {
    children: React.ReactNode;
}) {

    const [user, setUser] = useState<UserProfile | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    const cookies = new Cookies();


    /**
     * Получить текущего пользователя
     */
    const refreshUser = async () => {

        try {

            const token = cookies.get("auth-token");

            if (!token) {
                setUser(null);
                return;
            }

            const response = await fetch(
                `${API_URL}/me`,
                {
                    method: "GET",

                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );

            if (!response.ok) {
                throw new Error(
                    "Не удалось получить пользователя"
                );
            }

            const userData: UserProfile =
                await response.json();
            setUser(userData);

        } catch (error) {

            console.error(
                "Ошибка получения пользователя:",
                error
            );

            cookies.remove("auth-token", {
                path: "/"
            });

            setUser(null);

        } finally {

            setIsLoading(false);
        }
    };


    /**
     * Вход
     */
    const login = async (data: LoginRequest) => {

        const response = await fetch(
            `${API_URL}/login`,
            {
                method: "POST",

                headers: {
                    "Content-Type": "application/json"
                },

                body: JSON.stringify(data)
            }
        );

        if (!response.ok) {

            const errorData =
                await response.json().catch(() => ({}));

            throw new Error(
                errorData.message ||
                `Ошибка входа: ${response.status}`
            );
        }

        const authData =
            await response.json();

        cookies.set(
            "auth-token",
            authData.accessToken,
            {
                path: "/"
            }
        );

        cookies.set(
            "refresh-token",
            authData.refreshToken,
            {
                path: "/"
            }
        );

        await refreshUser();
    };


    /**
     * Регистрация
     */
    const register = async (
        data: RegisterRequest
    ) => {

        const response = await fetch(
            `${API_URL}/register`,
            {
                method: "POST",

                headers: {
                    "Content-Type": "application/json"
                },

                body: JSON.stringify(data)
            }
        );

        if (!response.ok) {

            const errorData =
                await response.json().catch(() => ({}));

            throw new Error(
                errorData.message ||
                `Ошибка регистрации: ${response.status}`
            );
        }

        /*
         * Здесь зависит от твоей backend-логики.
         *
         * Если регистрация возвращает accessToken:
         */

        const authData =
            await response.json();

        if (authData.accessToken) {

            cookies.set(
                "auth-token",
                authData.accessToken,
                {
                    path: "/"
                }
            );

            await refreshUser();
        }
    };


    /**
     * Выход
     */
    const logout = () => {

        cookies.remove(
            "auth-token",
            {
                path: "/"
            }
        );

        setUser(null);
    };


    /**
     * Проверяем авторизацию
     * при запуске приложения
     */
    useEffect(() => {

        refreshUser();

    }, []);


    return (
        <AuthContext.Provider
            value={{
                user,

                isAuthenticated:
                    user !== null,

                isLoading,

                login,
                register,

                logout,
                refreshUser
            }}
        >
            {children}
        </AuthContext.Provider>
    );
}


export function useAuth() {

    const context =
        useContext(AuthContext);

    if (!context) {

        throw new Error(
            "useAuth должен использоваться внутри AuthProvider"
        );
    }

    return context;
}