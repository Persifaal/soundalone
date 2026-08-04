import React from 'react';
import '../styles/header.css'
function Header() {
  return (
      <header className="app-header">
          <div className="app-header-wrapper">
              <div className="header-logo-wrap">
                  <div className="header-logo-section-wrap">
                      <a href="/" aria-label="Перейти на главную" className="logo-link">
                          SoundAlone
                      </a>
                  </div>
                  <div className="header-logo-section-wrap">
                      <p>
                          Your only social for music
                      </p>
                  </div>
              </div>
              <div className="header-sections-wrap">
                  <div className="header-buttons">
                      <a href="/search" className="header-button">Искать музыку</a>
                      <a href="/community" className="header-button">Сообщество</a>
                      <a href="/register" className="header-button">Зарегистрироваться</a>
                  </div>
              </div>
          </div>
      </header>
  );
}

export default Header;