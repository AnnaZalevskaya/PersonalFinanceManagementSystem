import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MainComponent } from './components/main/main.component';
import { AuthComponent } from './components/auth/auth.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AccountComponent } from './components/account/account.component';
import { AllAccountsComponent } from './components/all-accounts/all-accounts.component';
import { AddAccountComponent } from './components/add-account/add-account.component';
import { AuthGuard } from './extensions/auth.guard';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AllUsersComponent } from './components/all-users/all-users.component';
import { ProfileStructureComponent } from './components/profile-structure/profile-structure.component';
import { UpdateAccountComponent } from './components/update-account/update-account.component';
import { DeleteAccountComponent } from './components/delete-account/delete-account.component';
import { AddOperationComponent } from './components/add-operation/add-operation.component';
import { AdminComponent } from './components/admin/admin.component';
import { AuthInterceptor } from './extensions/auth.interceptor';
import { AccountStructureComponent } from './components/account-structure/account-structure.component';

export const routes: Routes = [
    { 
        path: '', 
        redirectTo: '/main', 
        pathMatch: 'full' 
    },
    { 
        path: 'main', 
        component: MainComponent 
    },
    { 
        path: 'auth', 
        component: AuthComponent 
    },
    { 
        path: 'profile/:userId', 
        component: ProfileStructureComponent, 
        canActivate: [AuthGuard],
        children: [
            { 
                path: '', 
                component: ProfileComponent 
            },
            { 
                path: 'account/:id', 
                component: AccountStructureComponent, 
                children:[
                    {
                        path: '', 
                        component: AccountComponent,
                    },
                    { 
                        path: 'update-account', 
                        component: UpdateAccountComponent
                    },
                    { 
                        path: 'delete-account',
                        component: DeleteAccountComponent
                    },
                    { 
                        path: 'add-operation',
                        component: AddOperationComponent
                    }
                ] 
            },
            { 
                path: 'add-account', 
                component: AddAccountComponent, 
            },
        ] 
    },  
    {
        path: 'admin',
        component: AdminComponent,
        canActivate: [AuthGuard],
        children: [
            { 
                path: 'all-accounts', 
                component: AllAccountsComponent 
            },
            { 
                path: 'all-users', 
                component: AllUsersComponent 
            },
        ]
    },
    { 
        path: '**', 
        component: NotFoundComponent 
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes), 
        HttpClientModule,
        BrowserModule,
        FormsModule
    ],
    exports: [RouterModule],
    providers: [
        {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true
      },
      AuthGuard
    ]
  })
  export class AppRoutingModule { }