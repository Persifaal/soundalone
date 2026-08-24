export interface Track {
    id: string;

    name: string;
    author: string;

    cover: string;
    url: string;

    duration?: number;
    albumId: string;
}