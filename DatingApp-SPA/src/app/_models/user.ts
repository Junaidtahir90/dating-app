import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    nickname: string;
    age: number;
    gender: string;
    createdDate: Date;
    lastActive: Date;
    city: string;
    country: string;
    photoUrl: string;
    intersets ?: string;
    introduction ?: string;
    lookingFir ?: string;
    photos ?: Photo [];
}
