import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from '@angular/common';
import { UserComponent } from "./user.component";
import { ChangePasswordComponent } from "./change-password.component";
import { AboutInfoComponent } from "./about-Info.component";
import { DeleteAccountComponent } from "./deleteAccount.component";
import { LoginInfoComponent } from "./login-info.component";
import { EditLoginInfoComponent } from "./edit-login-info.component";
import { EditPrivacySettingComponent } from "./edit-privacy-setting.component";
import { TemplateModule } from "../templateComponents/template.module";
import { TranslateModule } from '@ngx-translate/core';
import { LanguageSettingComponent } from "./languageSetting.component";


const routes: Routes = [ { path: "user" ,
                        component : UserComponent,
                            children: [
                                {path: "loginInfo", component: LoginInfoComponent},
                                {path: "editLoginInfo", component: EditLoginInfoComponent},
                                {path: "changePassword", component: ChangePasswordComponent},
                                {path: "aboutInfo", component: AboutInfoComponent },
                                {path: "editPrivacy", component: EditPrivacySettingComponent },
                                {path: "deleteAccount" , component: DeleteAccountComponent},
                                {path: "languageSetting" , component: LanguageSettingComponent},
                            ]
                        } ]

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    TemplateModule,
    TranslateModule
  ],

  declarations: [
    UserComponent,
    LoginInfoComponent,
    EditLoginInfoComponent,
    ChangePasswordComponent,
    AboutInfoComponent,
    EditPrivacySettingComponent,
    DeleteAccountComponent,
    LanguageSettingComponent
  ],

  providers: [],
    exports: []
})

export class UserModule { }
