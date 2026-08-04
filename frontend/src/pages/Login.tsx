import React, {useState} from 'react';
import '../styles/index.css';
import '../styles/register-page.css';
import Header from "../Components/Header";
import Footer from "../Components/Footer";

function LoginPage() {

  return (
    <div className="App">
        <Header/>
        <main>
            <section className="register-section">
                <form action="/login" method="POST">
                    <div className='form-wrapper form-header'>
                        <h1>Вход</h1>
                    </div>
                    <div className='form-wrapper form-inputs'>
                        <div className="email-inputs">
                            <div>
                                <label htmlFor="login">Почта, номер или тег</label>
                            </div>
                            <div>
                                <input
                                    type="login"
                                    placeholder="Введите вашу почту"
                                    name="login"
                                    className="form-input"
                                    id="login"
                                    required>

                                </input>
                            </div>
                            <div>
                                <p className="error-text email-error-text">Ошибка</p>
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
                                    id="password">
                                </input>
                            </div>
                            <div>
                                <p className="error-text password-error-text">Ошибка</p>
                            </div>
                        </div>
                    </div>
                    <div className="form-wrapper confirm-form">
                        <button type="submit">Зарегистрироваться</button>
                        <p className="error-text register-error-text">Ошибка</p>
                    </div>
                    <div className="form-wrapper social-login">
                        <div className="sub-text">
                            <div>
                                <p>Забыли пароль? <a href="/reset-password" className="app-link">Восстановить</a></p>
                            </div>
                            <div>
                                <p>Еще нет аккаунта? <a href="/register" className="app-link">Зарегистрироваться</a></p>
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

export default LoginPage;