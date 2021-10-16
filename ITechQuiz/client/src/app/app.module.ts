import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {HeaderInsightComponent} from "./views/header-insight/header-insight.component";
import {FooterInsightComponent} from "./views/footer-insight/footer-insight.component";
import {RouterModule, Routes} from "@angular/router";
import {SurveysListComponent} from "./views/surveys/surveys-list.component";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {SurveyDetailComponent} from "./views/surveys/survey-detail.component";
import {LoginComponent} from './views/login/login.component';
import {RegisterComponent} from './views/register/register.component';
import {AdminComponent} from './views/admin/admin.component';
import {AuthService} from "./services/auth.service";
import {AuthActivatorService} from "./services/activators/auth-activator.service";
import {JwtTokenService} from "./services/jwt-token.service";
import {LocalStorageService} from "./services/local-storage.service";
import {AdminSurveysListComponent} from "./views/admin/surveys/admin-surveys-list/admin-surveys-list.component";
import {SurveysService} from "./services/surveys.service";
import {AdminSurveyDetailComponent} from './views/admin/surveys/admin-survey-detail/admin-survey-detail.component';
import {AdminActivatorService} from "./services/activators/admin-activator.service";
import {ClientActivatorService} from "./services/activators/client-activator.service";

const routes: Routes = [
    {path: 'statistic-surveys', component: SurveysListComponent, data: {type: "ForStatistics"}},
    {path: 'quiz-surveys', component: SurveysListComponent, data: {type: "ForQuiz"}},
    {path: 'statistic-surveys/:id', component: SurveyDetailComponent},
    {path: 'quiz-surveys/:id', component: SurveyDetailComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {
        path: 'admin', component: AdminComponent,
        canActivate: 
            [ClientActivatorService, AdminActivatorService, AuthActivatorService]
    },
    {
        path: 'admin/statistic-surveys', component: AdminSurveysListComponent,
        canActivate:
            [ClientActivatorService, AdminActivatorService, AuthActivatorService],
        data: {type: "ForStatistics"}
    },
    {
        path: 'admin/quiz-surveys', component: AdminSurveysListComponent,
        canActivate:
            [ClientActivatorService, AdminActivatorService, AuthActivatorService],
        data: {type: "ForQuiz"}
    },
    {
        path: 'admin/statistic-surveys/:id',
        canActivate:
            [ClientActivatorService, AdminActivatorService, AuthActivatorService],
        component: AdminSurveyDetailComponent
    },
    {
        path: 'admin/quiz-surveys/:id',
        canActivate:
            [ClientActivatorService, AdminActivatorService, AuthActivatorService],
        component: AdminSurveyDetailComponent
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
        AdminComponent,
        AdminSurveysListComponent,
        AdminSurveyDetailComponent
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),
        HttpClientModule,
        MatProgressSpinnerModule,
        BrowserAnimationsModule,
        FormsModule
    ],
    providers: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        SurveysService,
        JwtTokenService,
        AuthService,
        AuthActivatorService,
        AdminActivatorService,
        ClientActivatorService,
        LocalStorageService],
    bootstrap: [
        AppComponent,
        HeaderInsightComponent,
        FooterInsightComponent]
})
export class AppModule {

    constructor(private jwtTokenStorage: JwtTokenService,
                private localStorageService: LocalStorageService) {
        const token = localStorageService.get("token")
        jwtTokenStorage.setJwtToken(token)
    }
}
