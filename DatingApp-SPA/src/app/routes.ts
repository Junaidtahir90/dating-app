import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail-resolver';
import { MemberListResolver } from './_resolvers/member-list-resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit-resolver';
import { PreventUnsavedChnages } from './_guards/prevent-unsaved-changes-guards';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';


export const appRoute: Routes = [

    // Ordering is important in angular routing,if we use last routing as wild in first position,
    // then it can not check belows routes,Be Carefull using routing
    { path : '', component: HomeComponent },

    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate : [AuthGuard],
        children:
                [
                    { path : 'members', component: MemberListComponent,
                        resolve: { 'users' : MemberListResolver }
                    },
                    { path : 'members/:id', component: MemberDetailComponent,
                            resolve: { 'user' : MemberDetailResolver }
                    },
                    { path : 'member/edit', component: MemberEditComponent,
                            resolve : { 'user' : MemberEditResolver },
                            canDeactivate : [PreventUnsavedChnages]
                    },
                    { path : 'messages', component: MessagesComponent , 
                     resolve : { 'messages' : MessagesResolver},
                    },
                    {   path : 'lists', component: ListsComponent,
                        resolve : {users : ListsResolver}
                    },
                ]

    },
    // AuthGuard to one route
    // { path : 'members', component: MemberListComponent , canActivate : [AuthGuard] },
    { path : '**', redirectTo: '' , pathMatch : 'full' },
];

