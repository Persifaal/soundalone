import React, {useState} from 'react';
import '../styles/index.css';
import '../styles/register-page.css';
import Header from "../Components/Header";
import Footer from "../Components/Footer";

function RegisterPage() {
    const [agree, setAgree] = useState(false);
    const [newsletter, setNewsletter] = useState(false);

  return (
    <div className="App">
        <Header/>
        <main>
            <section className="register-section">
                <form action="/register" method="POST">
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
                                    id="tag-input">

                                </input>
                            </div>
                            <div>
                                <p className="error-text tag-error-text">Ошибка</p>
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
                        <div className="password-confrimation-inputs">
                            <div>
                                <label htmlFor="password-confirmation">Подтверждение пароля</label>
                            </div>
                            <div>
                                <input
                                    type="password"
                                    placeholder="Введите ваш пароль"
                                    name="password-confirmation"
                                    className="form-input"
                                    id="password-confirmation">
                                </input>
                            </div>
                            <div>
                                <p className="error-text passord-confirmation-error-text">Ошибка</p>
                            </div>
                        </div>
                    </div>
                    <div className="form-wrapper confirm-form">
                        <button type="submit">Зарегистрироваться</button>
                        <p className="error-text register-error-text">Ошибка</p>
                        <div className="agreements">
                            <div>
                                <div>
                                    <label htmlFor="agree">Я согласен с <a href="/policy" className="app-link">условиями использования</a> и согласен на обработку персональных данных</label>
                                </div>
                                <div>
                                    <input type="checkbox"
                                           name="agree"
                                           checked={agree}
                                           id="agree"/>
                                    <span className={`custom-checkbox ${agree ? "checked" : ""}`}
                                          onClick={() => setAgree(!agree)}></span>
                                </div>
                            </div>
                            <div>
                                <div>
                                    <label htmlFor="newsletter" >Я хочу получать рассылку на почту</label>
                                </div>
                                <div>
                                    <input type="checkbox"
                                           name="newsletter"
                                           checked={newsletter}
                                           id="newsletter"/>
                                    <span className={`custom-checkbox ${newsletter ? "checked" : ""}`}
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