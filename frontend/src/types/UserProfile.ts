export interface UserProfile {
    displayName: string;
    userTag: string;
    avatarUrl: string;
    bannerUrl?: string | null;
    isMusician: boolean;
    bio: string;
}