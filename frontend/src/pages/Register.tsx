import React, { useState } from 'react';
import '../styles/index.css';
import '../styles/register-page.css';
import Header from "../Components/Header";
import Footer from "../Components/Footer";

interface FormData {
    tag: string;
    email: string;
    password: string;
    confirmPassword: string;
}

interface FormErrors {
    tag: string;
    email: string;
    password: string;
    confirmPassword: string;
    form: string;
}

function RegisterPage() {
    const [agree, setAgree] = useState(false);
    const [newsletter, setNewsletter] = useState(false);

    const [errors, setErrors] = useState<FormErrors>({
        tag: '',
        email: '',
        password: '',
        confirmPassword: '',
        form: '',
    });

    const validateForm = (data: FormData): FormErrors => {
        const newErrors: Partial<FormErrors> = {};

        if (!data.tag || data.tag.length < 3) {
            newErrors.tag = 'Тег должен содержать минимум 3 символа';
        }

        if (!data.email) {
            newErrors.email = 'Введите email';
        } else if (!/\S+@\S+\.\S+/.test(data.email)) {
            newErrors.email = 'Некорректный email';
        }

        if (!data.password || data.password.length < 6) {
            newErrors.password = 'Пароль должен содержать минимум 6 символов';
        }

        if (data.password !== data.confirmPassword) {
            newErrors.confirmPassword = 'Пароли не совпадают';
        }

        if (!agree) {
            newErrors.form = 'Необходимо принять условия использования';
        }

        return newErrors as FormErrors;
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const form = e.currentTarget;

        // 4. Убрали чтение 'agree' из DOM, оно нам не нужно
        const formData: FormData = {
            tag: (form.elements.namedItem('tag') as HTMLInputElement).value,
            email: (form.elements.namedItem('email') as HTMLInputElement).value,
            password: (form.elements.namedItem('password') as HTMLInputElement).value,
            confirmPassword: (form.elements.namedItem('password-confirmation') as HTMLInputElement).value,
        };

        const validationErrors = validateForm(formData);

        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
        } else {
            setErrors({
                tag: '',
                email: '',
                password: '',
                confirmPassword: '',
                form: '',
            });

            // Нативная отправка формы на сервер
            try {
                // 1. Проверьте, что здесь именно http://, а не https://
                const response = await fetch('http://localhost:8081/api/auth/register', {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(formData),
                });

                // 2. Проверяем, что ответ успешный (статус 2xx)
                if (!response.ok) {
                    // Пытаемся прочитать ошибку, которую мог вернуть сервер (например, "Email уже занят")
                    const errorData = await response.json().catch(() => ({}));
                    throw new Error(errorData.message || `Ошибка сервера: ${response.status}`);
                }

                const data = await response.json();
                console.log("Успешная регистрация:", data);

                // TODO: Здесь можно сделать редирект на страницу логина или показать сообщение об успехе
                // navigate('/login');

            } catch (err: any) {
                console.error("Error on sending form: ", err);

                // 3. Показываем понятную ошибку пользователю прямо в форме
                setErrors(prev => ({
                    ...prev,
                    form: err.message.includes("Failed to fetch")
                        ? "Не удалось подключиться к серверу. Проверьте, запущен ли он, и правильность адреса (http vs https)."
                        : (err.message || "Произошла неизвестная ошибка при регистрации")
                }));
            }
        }
    };

    return (
        <div className="App">
            <Header/>
            <main>
                <section className="register-section">
                    <form onSubmit={handleSubmit}>
                        <div className='form-wrapper form-header'>
                            <h1>Регистрация</h1>
                        </div>
                        <div className='form-wrapper form-inputs'>
                            <div className="tag-inputs">
                                <div>
                                    <label htmlFor="tag-input">Ваш тег (никнейм)</label>
                                </div>
                                <div>
                                    <input
                                        type="text"
                                        placeholder="Введите ваш тег"
                                        name="tag"
                                        className="form-input"
                                        id="tag-input"
                                    />
                                </div>
                                <div>
                                    {errors.tag && (
                                        <p className="error-text tag-error-text">{errors.tag}</p>
                                    )}
                                </div>
                            </div>
                            <div className="email-inputs">
                                <div>
                                    <label htmlFor="email">Ваша почта</label>
                                </div>
                                <div>
                                    <input
                                        type="email"
                                        placeholder="Введите вашу почту"
                                        name="email"
                                        className="form-input"
                                        id="email"
                                    />
                                </div>
                                <div>
                                    {errors.email && (
                                        <p className="error-text email-error-text">{errors.email}</p>
                                    )}
                                </div>
                            </div>

                            <div className="password-inputs">
                                <div>
                                    <label htmlFor="password">Ваш пароль</label>
                                </div>
                                <div>
                                    <input
                                        type="password"
                                        placeholder="Введите ваш пароль"
                                        name="password"
                                        className="form-input"
                                        id="password"
                                    />
                                </div>
                                <div>
                                    {errors.password && (
                                        <p className="error-text password-error-text">{errors.password}</p>
                                    )}
                                </div>
                            </div>
                            <div className="password-confrimation-inputs">
                                <div>
                                    <label htmlFor="password-confirmation">Подтверждение пароля</label>
                                </div>
                                <div>
                                    <input
                                        type="password"
                                        placeholder="Повторите ваш пароль"
                                        name="password-confirmation"
                                        className="form-input"
                                        id="password-confirmation"
                                    />
                                </div>
                                <div>
                                    {errors.confirmPassword && (
                                        <p className="error-text passord-confirmation-error-text">
                                            {errors.confirmPassword}
                                        </p>
                                    )}
                                </div>
                            </div>
                        </div>
                        <div className="form-wrapper confirm-form">
                            <button type="submit">Зарегистрироваться</button>

                            {/* Ошибка формы (включая чекбокс согласия) */}
                            {errors.form && (
                                <p className="error-text register-error-text">{errors.form}</p>
                            )}

                            <div className="agreements">
                                <div>
                                    <div>
                                        <label htmlFor="agree">
                                            Я согласен с <a href="/policy" className="app-link">условиями использования</a> и согласен на обработку персональных данных
                                        </label>
                                    </div>
                                    <div className="checkbox-wrapper">
                                        <input
                                            type="checkbox"
                                            name="agree"
                                            checked={agree}
                                            id="agree"
                                            onChange={() => {
                                                setAgree(!agree);
                                                if (errors.form) {
                                                    setErrors(prev => ({ ...prev, form: '' }));
                                                }
                                            }}
                                        />
                                        <span
                                            className={`custom-checkbox ${agree ? "checked" : ""}`}
                                            onClick={() => {
                                                setAgree(!agree);
                                                if (errors.form) {
                                                    setErrors(prev => ({ ...prev, form: '' }));
                                                }
                                            }}
                                        ></span>
                                    </div>
                                </div>
                                <div>
                                    <div>
                                        <label htmlFor="newsletter">Я хочу получать рассылку на почту</label>
                                    </div>
                                    <div className="checkbox-wrapper">
                                        <input
                                            type="checkbox"
                                            name="newsletter"
                                            checked={newsletter}
                                            id="newsletter"
                                            onChange={() => setNewsletter(!newsletter)}
                                        />
                                        <span
                                            className={`custom-checkbox ${newsletter ? "checked" : ""}`}
                                            onClick={() => setNewsletter(!newsletter)}
                                        ></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="form-wrapper social-login">
                            <div className="sub-text">
                                <div>
                                    <p>Уже есть аккаунт? <a href="/login" className="app-link">Войти в аккаунт</a></p>
                                </div>
                                <div>
                                    <p>Или войдите используя социальные сети:</p>
                                </div>
                            </div>
                        </div>
                    </form>
                </section>
            </main>
            <Footer/>
        </div>
    );
}

export default RegisterPage;