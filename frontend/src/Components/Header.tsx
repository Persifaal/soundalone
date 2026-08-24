import React, { useEffect, useState } from 'react';
import '../styles/header.css';

import searchImage from "../Images/icons/free-icon-magnifying-glass-25313.png";
import bellImage from "../Images/icons/bell.png";
import settingsImage from "../Images/icons/free-icon-settings-126472.png";
import {useAuth} from "../Context/AuthContext";

function Header() {
    const {
        user,
        isAuthenticated,
        isLoading
    } = useAuth();

    if (isLoading) {
        return null;
    }

    if (!isAuthenticated) {
        return (
            <header className="app-header">
                <header className="app-header">
                    <div className="app-header-wrapper">

                        <div className="header-logo-wrap">
                            <div className="header-logo-section-wrap">
                                <a
                                    href="/"
                                    aria-label="Перейти на главную"
                                    className="logo-link"
                                >
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

                                <a href="/search" className="header-button">
                                    Искать музыку
                                </a>

                                <a href="/community" className="header-button">
                                    Сообщество
                                </a>

                                <a
                                    href="/auth/register"
                                    className="header-button"
                                >
                                    Зарегистрироваться
                                </a>

                            </div>
                        </div>

                    </div>
                </header>
            </header>
        );
    }

    return (
        <header className="app-header">
            <div className="app-header-wrapper">

                <div className="header-logo-wrap">
                    <div className="header-logo-section-wrap">
                        <a
                            href="/"
                            aria-label="Перейти на главную"
                            className="logo-link"
                        >
                            SoundAlone
                        </a>
                    </div>
                </div>

                <div className="header-sections-wrap-first">
                    <div className="header-buttons">

                        <a href="/search" className="header-button">
                            <img
                                className="header-icon inverted"
                                src={searchImage}
                                alt="search"
                            />
                        </a>

                        <a href="/community" className="header-button">
                            Сообщество
                        </a>

                        <a href="/premium" className="header-button">
                            Премиум
                        </a>

                    </div>
                </div>

                <div className="header-sections-wrap">
                    <div className="header-buttons">

                        <a
                            href="/notifications"
                            className="header-button"
                        >
                            <img
                                src={bellImage}
                                alt="notifications"
                                className="header-icon inverted"
                            />
                        </a>

                        <a
                            href="/user"
                            className="header-button"
                        >
                            <img
                                src={user?.avatarUrl}
                                alt="profile"
                                className="header-icon image-round avatar"
                            />

                            <p>{user?.displayName}</p>
                        </a>

                        <a
                            href="/settings"
                            className="header-button"
                        >
                            <img
                                src={settingsImage}
                                alt="settings"
                                className="header-icon inverted"
                            />
                        </a>

                    </div>
                </div>

            </div>
        </header>
    );
}


export default Header;