import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {HeaderInsightComponent} from "./views/header-insight/header-insight.component";
import {FooterInsightComponent} from "./views/footer-insight/footer-insight.component";
import {RouterModule, Routes} from "@angular/router";
import {SurveysListComponent} from "./views/surveys/surveys-list.component";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {SurveyDetailComponent} from "./views/surveys/survey-detail.component";
import {LoginComponent} from './views/login/login.component';
import {RegisterComponent} from './views/register/register.component';
import {ClientComponent} from './views/client/client.component';
import {AuthService} from "./services/auth.service";
import {AuthActivatorService} from "./services/activators/auth-activator.service";
import {JwtTokenService} from "./services/jwt-token.service";
import {LocalStorageService} from "./services/local-storage.service";
import {ClientSurveysListComponent} from "./views/client/surveys/client-surveys-list/client-surveys-list.component";
import {SurveysService} from "./services/surveys.service";
import {ClientSurveyDetailComponent} from './views/client/surveys/client-survey-detail/client-survey-detail.component';
import {AdminActivatorService} from "./services/activators/admin-activator.service";
import {ClientActivatorService} from "./services/activators/client-activator.service";
import {CookieService} from 'ngx-cookie-service';
import {AdminComponent} from './views/admin/admin.component';
import {AdminUsersListComponent} from './views/admin/users/admin-users-list/admin-users-list.component';
import {UsersService} from "./services/users.service";
import {AdminUserDetailComponent} from './views/admin/users/admin-user-detail/admin-user-detail.component';
import {MapToArray} from "./utils/map-to-array-pipe";
import {AdminAssignRequestsListComponent} from './views/admin/assign-requests/assign-requests-list/admin-assign-requests-list.component';
import {AssignRequestService} from "./services/assign-request.service";
import {AssignRequestsListComponent} from './views/assign-requests-list/assign-requests-list.component';
import {UserDetailComponent} from './views/user-detail/user-detail.component';
import {ProfileComponent} from './views/profile/profile.component';
import {ChangePasswordComponent} from './views/change-password/change-password.component';
import {TokenInterceptor} from "./services/interceptors/token-interceptor.service";
import { SpinnerComponent } from './views/spinner/spinner.component';

const routes: Routes = [
    {path: 'statistic-surveys', component: SurveysListComponent, data: {type: "ForStatistics"}},
    {path: 'quiz-surveys', component: SurveysListComponent, data: {type: "ForQuiz"}},
    {path: 'statistic-surveys/:id', component: SurveyDetailComponent},
    {path: 'quiz-surveys/:id', component: SurveyDetailComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {
        path: 'client', component: ClientComponent,
        canActivate: [ClientActivatorService, AuthActivatorService]
    },
    {
        path: 'client/statistic-surveys', component: ClientSurveysListComponent,
        canActivate: [ClientActivatorService, AuthActivatorService],
        data: {type: "ForStatistics"}
    },
    {
        path: 'client/statistic-surveys/add-survey', component: ClientSurveyDetailComponent,
        canActivate: [ClientActivatorService, AuthActivatorService],
        data: {type: "ForStatistics"}
    },
    {
        path: 'client/quiz-surveys', component: ClientSurveysListComponent,
        canActivate: [ClientActivatorService, AuthActivatorService],
        data: {type: "ForQuiz"}
    },
    {
        path: 'client/quiz-surveys/add-survey', component: ClientSurveyDetailComponent,
        canActivate: [ClientActivatorService, AuthActivatorService],
        data: {type: "ForQuiz"}
    },
    {
        path: 'client/statistic-surveys/:id',
        canActivate: [ClientActivatorService, AuthActivatorService],
        component: ClientSurveyDetailComponent,
        data: {type: "ForStatistics"}
    },
    {
        path: 'client/quiz-surveys/:id',
        canActivate: [ClientActivatorService, AuthActivatorService],
        component: ClientSurveyDetailComponent,
        data: {type: "ForQuiz"}
    },
    {
        path: 'admin', component: AdminComponent,
        canActivate: [AdminActivatorService, AuthActivatorService]
    },
    {
        path: 'admin/users', component: AdminUsersListComponent,
        canActivate: [AdminActivatorService, AuthActivatorService],
    },
    {
        path: 'admin/users/:id', component: AdminUserDetailComponent,
        canActivate: [AdminActivatorService, AuthActivatorService],
    },
    {
        path: 'admin/assign-requests', component: AdminAssignRequestsListComponent,
        canActivate: [AdminActivatorService, AuthActivatorService],
    },
    {
        path: 'profile', component: ProfileComponent,
        canActivate: [AuthActivatorService]
    },
    {
        path: 'profile/change-password', component: ChangePasswordComponent,
        canActivate: [AuthActivatorService]
    },
    {path: '**', redirectTo: "/"}
];


@NgModule({
    declarations: [
        HeaderInsightComponent,
        FooterInsightComponent,
        AppComponent,
        SurveysListComponent,
        SurveyDetailComponent,
        LoginComponent,
        RegisterComponent,
        ClientComponent,
        ClientSurveysListComponent,
        ClientSurveyDetailComponent,
        AdminComponent,
        AdminUsersListComponent,
        AdminUserDetailComponent,
        MapToArray,
        AdminAssignRequestsListComponent,
        AssignRequestsListComponent,
        UserDetailComponent,
        ProfileComponent,
        ChangePasswordComponent,
        SpinnerComponent
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),
        HttpClientModule,
        MatProgressSpinnerModule,
        BrowserAnimationsModule,
        FormsModule,
    ],
    providers: [
        CookieService,
        BrowserModule,
        FormsModule,
        HttpClientModule,
        SurveysService,
        JwtTokenService,
        AuthService,
        AuthActivatorService,
        AdminActivatorService,
        ClientActivatorService,
        LocalStorageService,
        UsersService,
        AssignRequestService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: TokenInterceptor,
            multi: true
        }],
    bootstrap: [
        AppComponent,
        HeaderInsightComponent,
        FooterInsightComponent]
})
export class AppModule {

    constructor(private jwtTokenStorage: JwtTokenService) {
        jwtTokenStorage.setJwtTokenFromStorage()
    }

}
