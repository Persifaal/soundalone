import React from 'react';
import '../styles/index.css';
import '../styles/App.css';
import '../styles/RotatingCards.css';
import '../styles/Player.css';
import Footer from "../Components/Footer";
import HeaderLogged from "../Components/HeaderLogged";

function MainLogged() {

    return (
        <div className="App">
            <HeaderLogged/>
            <main>
                <div className="playlists">

                </div>
                <div className="page">

                </div>
            </main>
            <Footer/>

            <div className="player-block">
                <div className="current-playing-info">
                    <div className="player-current-cover">
                        <img src="../Images/cover.png" alt="player cover"/>
                    </div>
                    <div className="current-playing-info">
                        <a href="/albums?=paparazzi" className="current-playing-name">Paparazzi (Dubstep)</a>
                        <hr/>
                        <div className="current-playing-authors">
                            <a href="/profiles?=Alximo" className="current-playing-authors">Alximo</a>
                        </div>
                    </div>
                </div>
                <div className="playing-controls">

                </div>
                <div className="player-options">

                </div>
            </div>
        </div>
);
}

export default MainLogged;