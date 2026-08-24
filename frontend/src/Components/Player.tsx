import React, { useEffect, useState } from "react";
import "../styles/Player.css";

// icons
import playIcon from "../Images/icons/play.png";
import nextIcon from "../Images/icons/next-icon.png";
import shuffleIcon from "../Images/icons/shuffle.png";
import repeatIcon from "../Images/icons/loop.png";

import { getDominantColors } from "../utils/getDominantColors";
import { usePlayer } from "../Context/PlayerContext";

function Player() {
    const {
        currentTrack,
        isPlaying,
        currentTime,
        duration,

        togglePlay,

        next,
        previous,

        seek,

        toggleShuffle,
        toggleRepeat,

        shuffle,
        repeat
    } = usePlayer();

    const [coverColors, setCoverColors] = useState<string[]>([]);

    /*
     * Получаем цвета текущей обложки
     */
    useEffect(() => {
        if (!currentTrack?.cover) {
            setCoverColors([]);
            return;
        }

        getDominantColors(currentTrack.cover, 3)
            .then(colors => {
                setCoverColors(colors);
            })
            .catch(error => {
                console.error(
                    "Failed to extract cover colors:",
                    error
                );

                setCoverColors([]);
            });
    }, [currentTrack?.cover]);

    /*
     * Процент проигрывания трека.
     *
     * currentTime = секунды
     * duration = общая длительность в секундах
     */
    const progress =
        duration > 0
            ? (currentTime / duration) * 100
            : 0;

    /*
     * CSS-переменные для градиента плеера
     */
    const cssVariables = {
        "--cover-color-1": coverColors[0] || "#292D3B",
        "--cover-color-2": coverColors[1] || "#1b2030",
        "--cover-color-3": coverColors[2] || "#151a24",
        "--progress": `${progress}%`
    } as React.CSSProperties;

    /*
     * Перемещение по треку
     */
    const handleProgressChange = (
        event: React.ChangeEvent<HTMLInputElement>
    ) => {
        seek(Number(event.target.value));
    };

    /*
     * Форматирование времени:
     *
     * 83 -> 1:23
     * 240 -> 4:00
     */
    const formatTime = (time: number) => {
        if (!Number.isFinite(time) || time < 0) {
            return "0:00";
        }

        const minutes = Math.floor(time / 60);
        const seconds = Math.floor(time % 60);

        return `${minutes}:${seconds
            .toString()
            .padStart(2, "0")}`;
    };

    return (
        <div
            className="player-block"
            style={cssVariables}
        >

            {/* =========================
                ТЕКУЩИЙ ТРЕК
            ========================== */}

            <div className="current-playing-info">

                <div className="player-current-cover-container">

                    {currentTrack ? (
                        <img
                            src={currentTrack.cover}
                            alt="player cover"
                            className="player-cover"
                        />
                    ) : (
                        <div className="player-cover-placeholder">
                            Ничего не играет
                        </div>
                    )}

                </div>


                <div className="current-playing-text-block">

                    <div
                        className="
                            current-playing-info-container
                            current-playing-name-container
                        "
                    >
                        {currentTrack ? (
                            <a
                                href={`/albums/${currentTrack.albumId ?? ""}`}
                                className="current-playing-name"
                            >
                                {currentTrack.name}
                            </a>
                        ) : (
                            <span className="current-playing-name">
                                Ничего не играет
                            </span>
                        )}
                    </div>


                    <div
                        className="
                            current-playing-info-container
                            current-playing-text-break-container
                        "
                    >
                        <hr />
                    </div>


                    <div
                        className="
                            current-playing-info-container
                            current-playing-authors-container
                        "
                    >
                        {currentTrack && (
                            <a
                                href={`/profiles/${currentTrack.author}`}
                                className="current-playing-authors"
                            >
                                {currentTrack.author}
                            </a>
                        )}
                    </div>

                </div>

            </div>


            {/* =========================
                УПРАВЛЕНИЕ
            ========================== */}

            <div className="playing-controls">

                <div className="controls-buttons-wrapper">

                    {/* Repeat */}

                    <button
                        onClick={toggleRepeat}
                        className={`
                            player-control-button
                            player-loop-button
                            ${repeat ? "active" : ""}
                        `}
                        type="button"
                    >
                        <img
                            src={repeatIcon}
                            alt="repeat"
                        />
                    </button>


                    {/* Previous */}

                    <button
                        onClick={previous}
                        className="
                            player-control-button
                            player-previous-button
                        "
                        type="button"
                    >
                        <img
                            src={nextIcon}
                            alt="previous"
                        />
                    </button>


                    {/* Play / Pause */}

                    <button
                        onClick={togglePlay}
                        className="
                            player-control-button
                            player-play-loop-button
                        "
                        type="button"
                        disabled={!currentTrack}
                    >
                        <img
                            src={playIcon}
                            alt={isPlaying ? "pause" : "play"}
                        />
                    </button>


                    {/* Next */}

                    <button
                        onClick={next}
                        className="
                            player-control-button
                            player-next-button
                        "
                        type="button"
                    >
                        <img
                            src={nextIcon}
                            alt="next"
                        />
                    </button>


                    {/* Shuffle */}

                    <button
                        onClick={toggleShuffle}
                        className={`
                            player-control-button
                            player-shuffle-button
                            ${shuffle ? "active" : ""}
                        `}
                        type="button"
                    >
                        <img
                            src={shuffleIcon}
                            alt="shuffle"
                        />
                    </button>

                </div>


                {/* =========================
                    PROGRESS BAR
                ========================== */}

                <div
                    className="controls-progress-bar-wrapper"
                    id="track-current-time-text"
                >

                    <p className="progress-bar-text">
                        {formatTime(currentTime)}
                    </p>


                    <div
                        className="progress-container"
                        style={
                            {
                                "--progress": `${progress}%`
                            } as React.CSSProperties
                        }
                    >

                        <input
                            type="range"
                            className="progress-bar"
                            min={0}
                            max={duration || 0}
                            step={0.1}
                            value={currentTime}
                            onChange={handleProgressChange}
                            disabled={!currentTrack}
                        />

                    </div>


                    <p
                        className="progress-bar-text"
                        id="track-full-length-text"
                    >
                        {formatTime(duration)}
                    </p>

                </div>

            </div>


            {/* =========================
                OPTIONS
            ========================== */}

            <div className="player-options">
                options
            </div>

        </div>
    );
}

export default Player;