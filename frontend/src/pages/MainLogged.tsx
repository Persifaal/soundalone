import React from 'react';

import '../styles/index.css';
import '../styles/App.css';
import '../styles/RotatingCards.css';

import Footer from "../Components/Footer";
import HeaderLogged from "../Components/HeaderLogged";


import { getDominantColors } from "../utils/getDominantColors";
import Player from "../Components/Player";


function MainLogged() {

    // Тестовый список треков

    return (
        <div
            className="App">

            <HeaderLogged/>

            <main>

                <div className="playlists">
                </div>

                <div className="page">
                </div>

            </main>

            <Footer/>

            <Player />
        </div>
    );
}


export default MainLogged;