import React, { useEffect, useState } from 'react';
import '../styles/Player.css';

// icons
import playIcon from '../Images/icons/play.png';
import nextIcon from '../Images/icons/next-icon.png';
import shuffleIcon from '../Images/icons/shuffle.png';
import repeatIcon from '../Images/icons/loop.png';

// Тестовые обложки
import cover1 from "../Images/cover.png";
import cover2 from "../Images/cover2.jpg";
import cover3 from "../Images/cover3.jpg";
import cover4 from "../Images/cover4.jpg";
import cover5 from "../Images/cover5.jpg";
import cover6 from "../Images/cover6.webp";
import {getDominantColors} from "../utils/getDominantColors";


function Player(props: any) {
    const tracks = [
        {
            name: "Paparazzi (Dubstep)",
            author: "Alximo",
            cover: cover1
        },
        {
            name: "Perfect Pair",
            author: "R. L. Beats",
            cover: cover2
        },
        {
            name: "To dream to Never Dream",
            author: "strvshine",
            cover: cover3
        },
        {
            name: "Dejitaru to resu",
            author: "DJ UNIVXRSEL",
            cover: cover4
        },
        {
            name: "LEGACY",
            author: "PIXY",
            cover: cover5
        },
        {
            name: "A Platonic Thing",
            author: "stvrshine",
            cover: cover6
        }
    ];

// Текущий трек
    const [currentTrackIndex, setCurrentTrackIndex] = useState(0);

// Цвета текущей обложки
    const [coverColors, setCoverColors] = useState([]);


    const currentTrack = tracks[currentTrackIndex];


// При смене обложки получаем новые цвета
    useEffect(() => {

        getDominantColors(currentTrack.cover, 3)
            .then(colors => {
                console.log("New cover colors:", colors);

                setCoverColors(colors);
            })
            .catch(error => {
                console.error(
                    "Failed to extract cover colors:",
                    error
                );
            });

    }, [currentTrack.cover]);


// Следующий трек
    const nextTrack = () => {

        setCurrentTrackIndex(prevIndex =>
            (prevIndex + 1) % tracks.length
        );

    };


// Предыдущий трек
    const previousTrack = () => {

        setCurrentTrackIndex(prevIndex =>
            (prevIndex - 1 + tracks.length) % tracks.length
        );

    };

    const cssVariables = {
        "--cover-color-1": coverColors[0] || "#292D3B",
        "--cover-color-2": coverColors[1] || "#1b2030",
        "--cover-color-3": coverColors[2] || "#151a24"
    } as React.CSSProperties;

    const [progress, setProgress] = useState(35);

    const handleProgressChange = (
        event: React.ChangeEvent<HTMLInputElement>
    ) => {
        setProgress(Number(event.target.value));
    };

    return (
        <div className="player-block" style= {cssVariables}>

            <div className="current-playing-info">

                <div className="player-current-cover-container">

                    <img
                        src={currentTrack.cover}
                        alt="player cover"
                        className="player-cover"
                    />

                </div>


                <div className="current-playing-text-block">

                    <div className="current-playing-info-container current-playing-name-container">

                        <a
                            href="/albums?=paparazzi"
                            className="current-playing-name"
                        >
                            {currentTrack.name}
                        </a>

                    </div>


                    <div className="current-playing-info-container current-playing-text-break-container">

                        <hr/>

                    </div>


                    <div className="current-playing-info-container current-playing-authors-container">

                        <a
                            href="/profiles?=Alximo"
                            className="current-playing-authors"
                        >
                            {currentTrack.author}
                        </a>

                    </div>

                </div>

            </div>


            <div className="playing-controls">
                <div className="controls-buttons-wrapper">
                    <button className="player-control-button player-loop-button">
                        <img src={repeatIcon} alt="loop"/>
                    </button>
                    <button onClick={previousTrack} className="player-control-button player-previous-button">
                        <img src={nextIcon} alt="previous"/>
                    </button>
                    <button className="player-control-button player-play-loop-button">
                        <img src={playIcon} alt="play"/>
                    </button>
                    <button onClick={nextTrack} className="player-control-button player-next-button">
                        <img src={nextIcon} alt="next"/>
                    </button>
                    <button className="player-control-button player-shuffle-button">
                        <img src={shuffleIcon} alt="shuffle"/>
                    </button>
                </div>
                <div
                    className="controls-progress-bar-wrapper"
                    id="track-current-time-text"
                >
                    <p className="progress-bar-text">
                        0:23
                    </p>

                    <div
                        className="progress-container"
                        style={{
                            "--progress": `${progress}%`
                        } as React.CSSProperties}
                    >
                        <input
                            type="range"
                            className="progress-bar"
                            min="0"
                            max="100"
                            value={progress}
                            onChange={handleProgressChange}
                        />
                    </div>

                    <p
                        className="progress-bar-text"
                        id="track-full-length-text"
                    >
                        1:30
                    </p>
                </div>

            </div>


            <div className="player-options">

            </div>

        </div>
    );
}

export default Player;