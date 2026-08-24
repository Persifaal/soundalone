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
                <form action="/auth/confirm" method="POST">
                    <div className='form-wrapper form-header'>
                        <h1>Подтверждение почты</h1>
                        <h4>Мы отправили на вашу почту код для подтверждения аккаунта</h4>
                    </div>
                    <div className='form-wrapper form-inputs'>
                        <div className="email-inputs">
                            <div>
                                <label htmlFor="code">Код из сообщения</label>
                            </div>
                            <div>
                                <input
                                    type="code"
                                    placeholder="Введите код"
                                    name="code"
                                    className="form-input"
                                    id="code"
                                    required>

                                </input>
                            </div>
                            <div>
                                <p className="error-text email-error-text">Ошибка</p>
                            </div>
                        </div>
                    </div>
                    <div className="form-wrapper confirm-form">
                        <button type="submit">Подтвердить вход</button>
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