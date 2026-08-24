import React, {
    createContext,
    useContext,
    useEffect,
    useRef,
    useState
} from "react";

import { Track } from "../types/Track";

interface PlayerContextType {
    currentTrack: Track | null;
    queue: Track[];

    isPlaying: boolean;

    currentTime: number;
    duration: number;

    volume: number;

    shuffle: boolean;
    repeat: boolean;

    play: (track?: Track) => Promise<void>;
    pause: () => void;
    togglePlay: () => Promise<void>;

    next: () => Promise<void>;
    previous: () => Promise<void>;

    seek: (time: number) => void;

    setVolume: (volume: number) => void;

    setQueue: (tracks: Track[]) => void;

    toggleShuffle: () => void;
    toggleRepeat: () => void;
}

const PlayerContext =
    createContext<PlayerContextType | undefined>(undefined);


export function PlayerProvider({
                                   children
                               }: {
    children: React.ReactNode;
}) {

    const audioRef = useRef<HTMLAudioElement | null>(null);

    const [currentTrack, setCurrentTrack] =
        useState<Track | null>(null);

    const [queue, setQueue] =
        useState<Track[]>([]);

    const [isPlaying, setIsPlaying] =
        useState(false);

    const [currentTime, setCurrentTime] =
        useState(0);

    const [duration, setDuration] =
        useState(0);

    const [volume, setVolumeState] =
        useState(1);

    const [shuffle, setShuffle] =
        useState(false);

    const [repeat, setRepeat] =
        useState(false);


    /*
     * Создаём audio один раз.
     */
    useEffect(() => {

        const audio = new Audio();

        audioRef.current = audio;

        audio.volume = volume;

        const handleTimeUpdate = () => {
            setCurrentTime(audio.currentTime);
        };

        const handleLoadedMetadata = () => {
            setDuration(audio.duration);
        };

        const handlePlay = () => {
            setIsPlaying(true);
        };

        const handlePause = () => {
            setIsPlaying(false);
        };

        const handleEnded = () => {
            next();
        };

        audio.addEventListener(
            "timeupdate",
            handleTimeUpdate
        );

        audio.addEventListener(
            "loadedmetadata",
            handleLoadedMetadata
        );

        audio.addEventListener(
            "play",
            handlePlay
        );

        audio.addEventListener(
            "pause",
            handlePause
        );

        audio.addEventListener(
            "ended",
            handleEnded
        );

        return () => {

            audio.pause();

            audio.removeEventListener(
                "timeupdate",
                handleTimeUpdate
            );

            audio.removeEventListener(
                "loadedmetadata",
                handleLoadedMetadata
            );

            audio.removeEventListener(
                "play",
                handlePlay
            );

            audio.removeEventListener(
                "pause",
                handlePause
            );

            audio.removeEventListener(
                "ended",
                handleEnded
            );
        };

    }, []);


    /*
     * Воспроизведение.
     */
    const play = async (track?: Track) => {

        const audio = audioRef.current;

        if (!audio) {
            return;
        }

        if (track) {

            setCurrentTrack(track);

            audio.src = track.url;

            audio.currentTime = 0;

            setCurrentTime(0);

            await audio.play();

            return;
        }

        await audio.play();
    };


    /*
     * Пауза.
     */
    const pause = () => {

        const audio = audioRef.current;

        if (!audio) {
            return;
        }

        audio.pause();
    };


    /*
     * Play / Pause.
     */
    const togglePlay = async () => {

        if (isPlaying) {
            pause();
        } else {
            await play();
        }
    };


    /*
     * Следующий трек.
     */
    const next = async () => {

        if (!currentTrack || queue.length === 0) {
            return;
        }

        if (repeat) {
            await play(currentTrack);
            return;
        }

        const currentIndex =
            queue.findIndex(
                track => track.id === currentTrack.id
            );

        if (currentIndex === -1) {
            return;
        }

        let nextIndex: number;

        if (shuffle) {

            nextIndex =
                Math.floor(
                    Math.random() * queue.length
                );

        } else {

            nextIndex =
                (currentIndex + 1) % queue.length;
        }

        await play(queue[nextIndex]);
    };


    /*
     * Предыдущий трек.
     */
    const previous = async () => {

        if (!currentTrack || queue.length === 0) {
            return;
        }

        const currentIndex =
            queue.findIndex(
                track => track.id === currentTrack.id
            );

        if (currentIndex === -1) {
            return;
        }

        const previousIndex =
            currentIndex === 0
                ? queue.length - 1
                : currentIndex - 1;

        await play(queue[previousIndex]);
    };


    /*
     * Перемотка.
     */
    const seek = (time: number) => {

        const audio = audioRef.current;

        if (!audio) {
            return;
        }

        audio.currentTime = time;

        setCurrentTime(time);
    };


    /*
     * Громкость.
     */
    const setVolume = (value: number) => {

        const audio = audioRef.current;

        if (!audio) {
            return;
        }

        audio.volume = value;

        setVolumeState(value);
    };


    /*
     * Shuffle.
     */
    const toggleShuffle = () => {

        setShuffle(prev => !prev);
    };


    /*
     * Repeat.
     */
    const toggleRepeat = () => {

        setRepeat(prev => !prev);
    };


    return (
        <PlayerContext.Provider
            value={{
                currentTrack,
                queue,

                isPlaying,

                currentTime,
                duration,

                volume,

                shuffle,
                repeat,

                play,
                pause,
                togglePlay,

                next,
                previous,

                seek,

                setVolume,

                setQueue,

                toggleShuffle,
                toggleRepeat
            }}
        >
            {children}
        </PlayerContext.Provider>
    );
}


export function usePlayer() {

    const context =
        useContext(PlayerContext);

    if (!context) {
        throw new Error(
            "usePlayer должен использоваться внутри PlayerProvider"
        );
    }

    return context;
}