import React from 'react';

import '../styles/index.css';
import '../styles/App.css';
import '../styles/RotatingCards.css';

import Header from "../Components/Header";
import Footer from "../Components/Footer";

import { useAuth } from "../hooks/useAuth";

// Для незарегистрированной страницы

// Для зарегистрированной страницы
import Player from "../Components/Player";
import Plus from "../Images/icons/plus.png";
import Search from "../Images/icons/free-icon-magnifying-glass-25313.png";
import Favorite from "../Images/icons/free-icon-heart-1077035.png";
import Cover from "../Images/cover2.jpg";
import GuestContent from "../Components/GuestContent";
import UserProfileComponent from "../Components/UserProfileComponent";

import testAvatar from "../Images/avatar.png";
import testBanner from "../Images/banner.gif";

enum CoverType {
    Colored,
    Image,
    Mixed
}

class PlaylistCard {
    name: string;
    playlistId: string;
    type: string;
    author: string;
    authorTag: string;
    coverType: CoverType;
    image?: {};
    colors?: string[];

    constructor(
        name: string,
        type: string,
        playlistId: string,
        author: string,
        authorTag: string,
        coverType: CoverType,
        image?: string,
        colors?: string[]
    ) {
        this.name = name;
        this.type = type;
        this.playlistId = playlistId;
        this.author = author;
        this.authorTag = authorTag;
        this.coverType = coverType;
        this.image = image;
        this.colors = colors;
    }
}

function PlaylistCardComponent(card: PlaylistCard) {
    let cardCover;

    if (card.coverType === CoverType.Image) {
        cardCover = (
            <div className="playlist-cover">
                <img src={card.image as string} alt="playlist cover" />
            </div>
        );
    }

    else if (
        card.coverType === CoverType.Colored &&
        card.colors !== undefined
    ) {
        const styleColors =
            `linear-gradient(45deg, ${card.colors[0]}, ${card.colors[1]}, ${card.colors[2]})`;

        cardCover = (
            <div
                className="playlist-cover"
                style={{ background: styleColors }}
            />
        );
    }

    else {
        let styleColors = "";

        if (card.colors !== undefined) {
            styleColors =
                `linear-gradient(45deg, ${card.colors[0]}, ${card.colors[1]}, ${card.colors[2]})`;
        }

        let image;

        if (card.image !== undefined) {
            image = (
                <img
                    src={card.image as string}
                    alt="playlist cover"
                />
            );

            if (card.name === "Любимые") {
                image = (
                    <img
                        src={card.image as string}
                        className="inverted"
                        alt="playlist cover"
                        style={{
                            padding: "20px",
                            boxSizing: "border-box"
                        }}
                    />
                );
            }
        }

        cardCover = (
            <div
                className="playlist-cover"
                style={{ background: styleColors }}
            >
                {image}
            </div>
        );
    }

    return (
        <a
            href={`/playlists/${card.playlistId}`}
            className="playlist-link"
        >
            <div className="playlists-list-card">
                {cardCover}

                <div className="playlist-card-info">
                    <div className="playlist-card-text-block">
                        <p>{card.name}</p>
                    </div>

                    <div className="playlist-card-text-block-bottom">
                        <p>{card.type}</p>
                        <p>|</p>
                        <a href={card.authorTag}>
                            {card.author}
                        </a>
                    </div>
                </div>
            </div>
        </a>
    );
}


function Main() {
    const isLoggedIn = true; //useAuth();

    return (
        <div className="App">

            <Header />

            <main>
                {isLoggedIn
                    ? <LoggedContent />
                    : <GuestContent />
                }
            </main>

            <Footer />

            {isLoggedIn && <Player />}

        </div>
    );
}

function LoggedContent() {

    const playlists = [
        new PlaylistCard(
            "Любимые",
            "Плейлист",
            "my/favorite",
            "Persifal",
            "persifal_",
            CoverType.Mixed,
            Favorite,
            ["#5A6FC3", "#8249AA", "#8B6570"]
        ),

        new PlaylistCard(
            "Плейлист 1",
            "Плейлист",
            "PlaylistId",
            "Persifal",
            "persifal_",
            CoverType.Colored,
            undefined,
            ["#81F423", "#6FD3CC", "#735EFF"]
        ),

        new PlaylistCard(
            "Специально для вас",
            "Плейлист",
            "PlaylistId",
            "SoundAlone",
            "soundalone",
            CoverType.Colored,
            undefined,
            ["#FF7EA3", "#FFB89D", "#FFF897"]
        ),

        new PlaylistCard(
            "Perfect Pair",
            "Альбом",
            "perfect-pair",
            "R. L. Beats",
            "R. L. Beats",
            CoverType.Image,
            Cover,
            undefined
        ),

        new PlaylistCard(
            "Плейлист 2",
            "Плейлист",
            "playlist2",
            "Someone",
            "someone",
            CoverType.Colored,
            undefined,
            ["#FFFF7E", "#F6C7C3", "#EE97FF"]
        ),
    ];

    return (
        <div className="main-page-wrapper">

            <div className="my-playlists-wrapper">

                <div className="my-playlists-header">

                    <div className="my-playlists-search-wrapper">
                        <img
                            src={Search}
                            alt="search"
                            className="inverted"
                        />

                        <input
                            type="text"
                            placeholder="Что слушаем сегодня?"
                        />
                    </div>

                    <a
                        href="/playlists/my/create-new-playlist"
                        className="my-playlists-create-new"
                    >
                        <img
                            src={Plus}
                            alt="add"
                            className="inverted"
                        />
                    </a>

                </div>

                <div className="my-playlists-body">
                    {playlists.map((card) => (
                        <PlaylistCardComponent
                            key={card.playlistId + card.name}
                            {...card}
                        />
                    ))}
                </div>

            </div>

            <div className="page">
                <UserProfileComponent
                    displayName={"Display Name"}
                    userTag={"User Tag"}
                    avatarUrl={testAvatar}
                    bannerUrl={testBanner}
                    isMusician={false}
                    bio={"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "}
                ></UserProfileComponent>
            </div>

        </div>
    );
}

export default Main;