export interface Message {
    id: number;
    senderId: number;
    senderNickName: string;
    senderPhotoUrl: string;
    recipientId: number;
    recipientNickName: string;
    recipientPhotoUrl: string;
    content: string;
    isRead: boolean;
    dateRead: Date;
    messageSent: Date;

}
