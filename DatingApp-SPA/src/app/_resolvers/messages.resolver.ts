import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_service/user.service';
import { AlertifyService } from '../_service/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from '../_models/message';
import { AuthService } from '../_service/auth.service';


@Injectable()

export class MessagesResolver implements Resolve<Message[]> {
    pageNumber = 1;
    pageSize = 10;
    messageContainer = 'Unread';

    constructor(private userSevice: UserService, private authService: AuthService,
                private router: Router, private alertify: AlertifyService) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
        // tslint:disable-next-line: max-line-length
        return this.userSevice.getMessages(this.authService.decodedToken.nameid[0], this.pageNumber, this.pageSize, this.messageContainer).pipe
            (catchError(error => {
                this.alertify.error('Failed to Load Users Data');
                this.router.navigate(['/home']);
                return of(null);
            }
            )
            );
    }
}
