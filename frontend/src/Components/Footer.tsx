import React from 'react';
function Footer() {
  return (
      <footer>
          <div className="footer-block">
              <a href="/about">О нас</a>
              <a href="/tos">Условия использования</a>
              <a href="/privacy">Политика конфиденциальности</a>
          </div>
          <div className="footer-block">
              <p>© 2026 SoundAlone</p>
              <p>Made with love by Another Angle Team</p>
          </div>
          <div className="footer-block">
              <a href="/t.me">Telegram</a>
          </div>
      </footer>
  );
}

export default Footer;