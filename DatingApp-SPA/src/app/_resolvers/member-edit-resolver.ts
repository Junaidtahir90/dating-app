import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import {  Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_service/user.service';
import { AlertifyService } from '../_service/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_service/auth.service';


@Injectable()

export class MemberEditResolver implements Resolve<User> {
    constructor(private userSevice: UserService, private authService: AuthService,
                private router: Router, private alertify: AlertifyService) {}

            resolve(route: ActivatedRouteSnapshot): Observable<User> {
                return this.userSevice.getUser(this.authService.decodedToken.nameid[0]).pipe
                         (catchError(error => {
                             //console.log(this.authService.decodedToken.nameId[0]);
                            this.alertify.error('Failed to Load Data');
                            this.router.navigate(['/members']);
                            return of(null);
                        }
                    )
                );
            }
}
