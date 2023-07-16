import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClientXsrfModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { DragulaModule } from 'ng2-dragula';
import { ModelModule } from './models/model.module';
import { MainModule } from "./main/main.module";
import { TemplateModule } from './templateComponents/template.module';
import { HttpXsrfInterceptor } from './HttpXsrfInterceptor';
import { SharedModule } from './shared/shared.module';
import { AuthModule } from './auth/auth.module';
import { HomeModule } from './home/home.module';
import { ProfileModule } from './profile/profile.module';
import { StateService } from './abstractions/services/stateService';
import { ProfileService } from "./abstractions/services/profileService";
import { ProfileDataService } from "./services/profileDataService.service";
import { StateDataService } from "./services/stateDataService.service";
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { UserModule } from './user/user.module';
import { HomeService } from "./abstractions/services/homeService";
import { HomeDataService } from "./services/homeDataService.service";
import { UserService } from "./abstractions/services/userService";
import { UserDataService } from "./services/userDataService.service";
import { AuthenticationService } from "./abstractions/services/authenticationService";
import { BasicAuthenticationService } from "./services/authentication.service";
import { AuthenticationGuard } from "./services/authentication.guard";
import { WithCredentialsInterceptor } from './WithCredentialsInterceptor';
import { AuthenticationTokenInterceptor } from './AuthenticationTokenInterceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const routes: Routes = []  

@NgModule({
  
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    HttpClientModule,
    HttpClientXsrfModule,
    FormsModule,
    ModelModule,
    MainModule,
    SharedModule, 
    TemplateModule,
    AuthModule,
    UserModule,
    HomeModule,
    ProfileModule,
    DragulaModule.forRoot(),

  ],
  declarations: [
    AppComponent
  ],
  providers: [
    { provide: StateService, useClass: StateDataService },
    { provide: ProfileService, useClass: ProfileDataService },
    { provide: HomeService, useClass: HomeDataService },
    { provide: UserService, useClass: UserDataService },
    { provide: AuthenticationService, useClass: BasicAuthenticationService },
    { provide: HTTP_INTERCEPTORS, useClass: HttpXsrfInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: WithCredentialsInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationTokenInterceptor, multi: true },
     AuthenticationGuard,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
