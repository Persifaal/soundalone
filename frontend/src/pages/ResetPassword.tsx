import React from 'react';
import '../styles/index.css';
import '../styles/register-page.css';
import Header from "../Components/Header";
import Footer from "../Components/Footer";

function ResetPasswordPage() {

  return (
    <div className="App">
        <Header/>
        <main>
            <section className="register-section">
                <form action="/password-reset" method="POST">
                    <div className='form-wrapper form-header'>
                        <h1>Восстановление пароля</h1>
                    </div>
                    <div className='form-wrapper form-inputs'>
                        <div className="email-inputs">
                            <div>
                                <label htmlFor="login">Почта или номер</label>
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
                    </div>
                    <div className="form-wrapper confirm-form">
                        <button type="submit">Восстановить пароль</button>
                        <p className="error-text register-error-text">Ошибка</p>
                    </div>
                </form>
            </section>
        </main>
        <Footer/>
    </div>
  );
}

export default ResetPasswordPage;