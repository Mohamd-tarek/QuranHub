import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from '@angular/common';
import { AuthenticationComponent } from "./authentication.component";
import { LoginComponent } from "./login.component";
import { ExternalButtonComponent } from "./externalButton.component";
import { ExternalLoginsComponent } from "./externalLogins.component";
import { ExternalSignUpsComponent } from "./externalSignUps.component";
import { SignUpComponent } from "./signUpComponents/signUp.component";
import { SignUpConfirmComponent } from "./signUpComponents/signUpConfirm.component";
import { RecoverPasswordComponent } from "./recoverPasswordComponents/recoverPassword.component";
import { RecoverPasswordConfirmComponent } from "./recoverPasswordComponents/recoverPasswordConfirm.component";
import { TemplateModule } from "../templateComponents/template.module";

const routes: Routes = [ { path: "auth" ,
                        component : AuthenticationComponent,
                            children: [
                                {path: "login", component: LoginComponent},
                                { path: "login/:LoginExternalCallback", component: LoginComponent },
                                { path: "signup", component: SignUpComponent },
                                { path: "signup/:SignUpExternalCallback", component: SignUpComponent},
                                {path: "signupConfirm", component: SignUpConfirmComponent},
                                {path: "recoverPassword", component: RecoverPasswordComponent},
                                {path: "recoverPasswordConfirm", component: RecoverPasswordConfirmComponent}
                            ]
                        } ]

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    TemplateModule
  ],

  declarations: [
    AuthenticationComponent,
    LoginComponent,
    ExternalButtonComponent,
    ExternalLoginsComponent,
    ExternalSignUpsComponent,
    SignUpComponent, 
    SignUpConfirmComponent,
    RecoverPasswordComponent,
    RecoverPasswordConfirmComponent,
  ],

    providers: [],
    exports: []
})

export class AuthModule { }
