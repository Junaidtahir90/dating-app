import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    nickName: string;
    age: number;
    gender: string;
    createdDate: Date;
    lastActive: Date;
    city: string;
    country: string;
    photoUrl: string;
    interests ?: string;
    introduction ?: string;
    lookingFor ?: string;
    photos ?: Photo [];
}
