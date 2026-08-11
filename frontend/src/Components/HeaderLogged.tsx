import React from 'react';
import '../styles/header.css'
// @ts-ignore
import bellImage from '../Images/icons/bell.png';
// @ts-ignore
import settingsImage from '../Images/icons/free-icon-settings-126472.png';
import searchImage from '../Images/icons/free-icon-magnifying-glass-25313.png';

function HeaderLogged() {
    let loggedUser = {
        displayname: "Persifal",
        tag: "persifal_",
        avatar_url: require("../Images/avatar.jpg"),
    }

    return (
      <header className="app-header">
          <div className="app-header-wrapper">
              <div className="header-logo-wrap">
                  <div className="header-logo-section-wrap">
                      <a href="/" aria-label="Перейти на главную" className="logo-link">
                          SoundAlone
                      </a>
                  </div>
              </div>
              <div className="header-sections-wrap-first">
                  <div className="header-buttons">
                      <a href="/search" className="header-button"><img className="header-icon inverted" src={searchImage} alt="search"/></a>
                      <a href="/community" className="header-button">Сообщество</a>
                      <a href="/premium" className="header-button">Премиум</a>
                  </div>
              </div>
              <div className="header-sections-wrap">
                  <div className="header-buttons">
                      <a href="/notifications" className="header-button">
                          <img src={bellImage} alt="notifications" className="header-icon inverted">
                          </img>
                      </a>
                      <a href="/user" className="header-button">
                          <img src={loggedUser.avatar_url} alt="profile" className="header-icon image-round avatar"></img>
                          <p>{loggedUser.displayname}</p>
                      </a>
                      <a href="/settings" className="header-button">
                          <img src={settingsImage} alt="settings" className="header-icon inverted"></img>
                      </a>
                  </div>
              </div>
          </div>
      </header>
  );
}

export default HeaderLogged;