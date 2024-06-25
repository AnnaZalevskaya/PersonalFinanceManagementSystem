import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MainComponent } from './components/main-pages/main/main.component';
import { AuthComponent } from './components/auth-logic/auth/auth.component';
import { ProfileComponent } from './components/main-pages/profile/profile.component';
import { AccountComponent } from './components/account-functionality/account/account.component';
import { AllAccountsComponent } from './components/admin-functionality/all-accounts/all-accounts.component';
import { AddAccountComponent } from './components/account-functionality/add-account/add-account.component';
import { AuthGuard } from './extensions/auth.guard';
import { NotFoundComponent } from './components/additional-pages/not-found/not-found.component';
import { AllUsersComponent } from './components/admin-functionality/all-users/all-users.component';
import { ProfileStructureComponent } from './components/main-pages/profile-structure/profile-structure.component';
import { UpdateAccountComponent } from './components/account-functionality/update-account/update-account.component';
import { AddOperationComponent } from './components/account-functionality/add-operation/add-operation.component';
import { AdminComponent } from './components/admin-functionality/admin/admin.component';
import { AccountStructureComponent } from './components/account-functionality/account-structure/account-structure.component';
import { AdminStructureComponent } from './components/admin-functionality/admin-structure/admin-structure.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegularPaymentsComponent } from './components/regular-payment-functionality/regular-payments/regular-payments.component';
import { AccountGoalsComponent } from './components/goal-functionality/account-goals/account-goals.component';
import { ServerErrorComponent } from './components/additional-pages/server-error/server-error.component';
import { AddGoalComponent } from './components/goal-functionality/add-goal/add-goal.component';
import { UpdateGoalComponent } from './components/goal-functionality/update-goal/update-goal.component';
import { UpdateRegularPaymentComponent } from './components/regular-payment-functionality/update-regular-payment/update-regular-payment.component';
import { CreateRegularPaymentComponent } from './components/regular-payment-functionality/create-regular-payment/create-regular-payment.component';
import { AllOperationsComponent } from './components/admin-functionality/all-operations/all-operations.component';

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
                        path: 'add-operation',
                        component: AddOperationComponent
                    },
                    {
                        path: 'account-goals',
                        component: AccountGoalsComponent
                    },
                    {
                        path: 'add-goal',
                        component: AddGoalComponent
                    },
                    {
                        path: 'update-goal/:goalId',
                        component: UpdateGoalComponent
                    }
                ] 
            },
            { 
                path: 'add-account', 
                component: AddAccountComponent
            },
            {
                path: 'regular-payments',
                component: RegularPaymentsComponent
            },
            {
                path: 'create-regular-payment',
                component: CreateRegularPaymentComponent
            },
            {
                path: 'update-regular-payment/:regPayId',
                component: UpdateRegularPaymentComponent
            }
        ] 
    },  
    {
        path: 'admin',
        component: AdminStructureComponent,
        canActivate: [AuthGuard],
        children: [
            {
                path: '', 
                component: AdminComponent,
            },
            { 
                path: 'all-accounts', 
                component: AllAccountsComponent 
            },
            { 
                path: 'all-users', 
                component: AllUsersComponent 
            },
            { 
                path: 'all-operations', 
                component: AllOperationsComponent 
            },
        ]
    },
    { 
        path: 'internal-server-error', 
        component: ServerErrorComponent 
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
        BrowserAnimationsModule,
        FormsModule
    ],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }