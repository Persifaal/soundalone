import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

import "../styles/index.css";
import "../styles/register-page.css";

import Header from "./Header";
import Footer from "./Footer";

import { useAuth } from "../Context/AuthContext";

interface AuthFormProps {
    mode: "login" | "register";
}

interface FormData {
    tag: string;
    email: string;
    password: string;
    confirmPassword: string;
}

interface FieldErrors {
    tag?: string;
    email?: string;
    password?: string;
    confirmPassword?: string;
    form?: string;
}

function AuthForm({ mode }: AuthFormProps) {

    const navigate = useNavigate();

    const {
        login,
        register
    } = useAuth();

    const isLogin = mode === "login";

    const [formData, setFormData] = useState<FormData>({
        tag: "",
        email: "",
        password: "",
        confirmPassword: ""
    });

    const [errors, setErrors] = useState<FieldErrors>({});

    const [isSubmitting, setIsSubmitting] = useState(false);


    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement>
    ) => {

        const {
            name,
            value
        } = e.target;

        setFormData(prev => ({
            ...prev,
            [name]: value
        }));

        setErrors(prev => ({
            ...prev,
            [name]: undefined,
            form: undefined
        }));
    };


    const validate = (): boolean => {

        const newErrors: FieldErrors = {};


        /*
         * LOGIN
         */
        if (isLogin) {

            if (!formData.email.trim()) {
                newErrors.email =
                    "Введите почту, номер или тег";
            }

        }


        /*
         * REGISTER
         */
        if (!isLogin) {

            if (!formData.tag.trim()) {

                newErrors.tag =
                    "Введите тег";

            } else if (formData.tag.length < 3) {

                newErrors.tag =
                    "Тег должен содержать минимум 3 символа";
            }


            if (!formData.email.trim()) {

                newErrors.email =
                    "Введите email";

            } else if (
                !/^[^\s@]+@[^\s@]+\.[^\s@]+$/
                    .test(formData.email)
            ) {

                newErrors.email =
                    "Введите корректный email";
            }


            if (!formData.confirmPassword) {

                newErrors.confirmPassword =
                    "Подтвердите пароль";

            } else if (
                formData.password !==
                formData.confirmPassword
            ) {

                newErrors.confirmPassword =
                    "Пароли не совпадают";
            }
        }


        /*
         * PASSWORD
         */
        if (!formData.password) {

            newErrors.password =
                "Введите пароль";

        } else if (
            formData.password.length < 6
        ) {

            newErrors.password =
                "Пароль должен содержать минимум 6 символов";
        }


        setErrors(newErrors);

        return Object.keys(newErrors).length === 0;
    };


    const handleSubmit = async (
        e: React.FormEvent<HTMLFormElement>
    ) => {

        e.preventDefault();

        if (!validate()) {
            return;
        }

        setIsSubmitting(true);
        setErrors({});


        try {

            /*
             * LOGIN
             */
            if (isLogin) {

                await login({
                    email: formData.email,
                    password: formData.password
                });

                navigate("/");

            }


            /*
             * REGISTER
             */
            else {

                await register({
                    tag: formData.tag,
                    email: formData.email,
                    password: formData.password,
                    confirmPassword:
                    formData.confirmPassword
                });

                navigate("/auth/confirm");
            }

        } catch (error) {

            console.error(error);

            setErrors({
                form:
                    error instanceof Error
                        ? error.message
                        : "Произошла ошибка"
            });

        } finally {

            setIsSubmitting(false);
        }
    };


    return (
        <div className="App">

            <Header />

            <main>

                <section className="register-section">

                    <form onSubmit={handleSubmit}>

                        {/* HEADER */}

                        <div className="form-wrapper form-header">

                            <h1>
                                {isLogin
                                    ? "Вход"
                                    : "Регистрация"}
                            </h1>

                        </div>


                        {/* INPUTS */}

                        <div className="form-wrapper form-inputs">


                            {/* LOGIN */}

                            {isLogin && (

                                <div className="email-inputs">

                                    <div>
                                        <label htmlFor="login">
                                            Почта, номер или тег
                                        </label>
                                    </div>

                                    <div>

                                        <input
                                            type="text"
                                            placeholder="Введите вашу почту, номер или тег"
                                            name="email"
                                            id="email"
                                            className="form-input"
                                            value={formData.email}
                                            onChange={handleChange}
                                            disabled={isSubmitting}
                                            required
                                        />

                                    </div>

                                    {errors.email && (
                                        <p className="error-text">
                                            {errors.email}
                                        </p>
                                    )}

                                </div>

                            )}


                            {/* REGISTER: TAG */}

                            {!isLogin && (

                                <div className="tag-inputs">

                                    <div>
                                        <label htmlFor="tag">
                                            Ваш тег
                                        </label>
                                    </div>

                                    <div>

                                        <input
                                            type="text"
                                            placeholder="Введите ваш тег"
                                            name="tag"
                                            id="tag"
                                            className="form-input"
                                            value={formData.tag}
                                            onChange={handleChange}
                                            disabled={isSubmitting}
                                            required
                                        />

                                    </div>

                                    {errors.tag && (
                                        <p className="error-text">
                                            {errors.tag}
                                        </p>
                                    )}

                                </div>

                            )}


                            {/* REGISTER: EMAIL */}

                            {!isLogin && (

                                <div className="email-inputs">

                                    <div>
                                        <label htmlFor="email">
                                            Ваша почта
                                        </label>
                                    </div>

                                    <div>

                                        <input
                                            type="email"
                                            placeholder="Введите вашу почту"
                                            name="email"
                                            id="email"
                                            className="form-input"
                                            value={formData.email}
                                            onChange={handleChange}
                                            disabled={isSubmitting}
                                            required
                                        />

                                    </div>

                                    {errors.email && (
                                        <p className="error-text">
                                            {errors.email}
                                        </p>
                                    )}

                                </div>

                            )}


                            {/* PASSWORD */}

                            <div className="password-inputs">

                                <div>
                                    <label htmlFor="password">
                                        Ваш пароль
                                    </label>
                                </div>

                                <div>

                                    <input
                                        type="password"
                                        placeholder="Введите ваш пароль"
                                        name="password"
                                        id="password"
                                        className="form-input"
                                        value={formData.password}
                                        onChange={handleChange}
                                        disabled={isSubmitting}
                                        required
                                    />

                                </div>

                                {errors.password && (
                                    <p className="error-text">
                                        {errors.password}
                                    </p>
                                )}

                            </div>


                            {/* CONFIRM PASSWORD */}

                            {!isLogin && (

                                <div className="password-confrimation-inputs">

                                    <div>
                                        <label htmlFor="confirmPassword">
                                            Подтверждение пароля
                                        </label>
                                    </div>

                                    <div>

                                        <input
                                            type="password"
                                            placeholder="Повторите ваш пароль"
                                            name="confirmPassword"
                                            id="confirmPassword"
                                            className="form-input"
                                            value={
                                                formData.confirmPassword
                                            }
                                            onChange={handleChange}
                                            disabled={isSubmitting}
                                            required
                                        />

                                    </div>

                                    {errors.confirmPassword && (
                                        <p className="error-text">
                                            {errors.confirmPassword}
                                        </p>
                                    )}

                                </div>

                            )}

                        </div>


                        {/* SUBMIT */}

                        <div className="form-wrapper confirm-form">

                            <button
                                type="submit"
                                disabled={isSubmitting}
                            >
                                {isSubmitting
                                    ? "Загрузка..."
                                    : isLogin
                                        ? "Войти"
                                        : "Зарегистрироваться"}
                            </button>

                            {errors.form && (
                                <p className="error-text register-error-text">
                                    {errors.form}
                                </p>
                            )}

                        </div>


                        {/* LINKS */}

                        <div className="form-wrapper social-login">

                            <div className="sub-text">


                                {/* LOGIN LINKS */}

                                {isLogin && (

                                    <>

                                        <div>
                                            <p>
                                                Забыли пароль?

                                                {" "}

                                                <a
                                                    href="/auth/reset-password"
                                                    className="app-link"
                                                >
                                                    Восстановить
                                                </a>
                                            </p>
                                        </div>


                                        <div>
                                            <p>
                                                Еще нет аккаунта?

                                                {" "}

                                                <a
                                                    href="/auth/register"
                                                    className="app-link"
                                                >
                                                    Зарегистрироваться
                                                </a>
                                            </p>
                                        </div>

                                    </>

                                )}


                                {/* REGISTER LINKS */}

                                {!isLogin && (

                                    <div>
                                        <p>
                                            Уже есть аккаунт?

                                            {" "}

                                            <a
                                                href="/auth/login"
                                                className="app-link"
                                            >
                                                Войти
                                            </a>
                                        </p>
                                    </div>

                                )}


                                <div>
                                    <p>
                                        Или войдите используя
                                        социальные сети:
                                    </p>
                                </div>

                            </div>

                        </div>

                    </form>

                </section>

            </main>

            <Footer />

        </div>
    );
}

export default AuthForm;