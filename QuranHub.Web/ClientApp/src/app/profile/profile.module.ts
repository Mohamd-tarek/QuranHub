import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from '@angular/common';
import { TemplateModule } from "../templateComponents/template.module";
import { ProfileComponent } from "./profile.component";
import { CoverComponent } from "./cover.component";
import { MainInfoComponent } from "./mainInfo.component"
import { OtherInfoComponent } from "./otherInfo.component";
import { ProfileDetailsComponent } from "./profileDetails.component";
import { AboutComponent } from "./about.component";
import { AllFollowersComponent } from "./allFollowers.component";
import { AllFollowingsComponent } from "./allFollowings.component";
import { FollowInfoComponent } from "./followInfo.component";
import { FollowersComponent } from "./followers.component";
import { FollowingsComponent } from "./followings.component";
import { PostsComponent } from "./posts.component";
import { FollowOrUnfollowButtonComponent } from "./followOrUnfollowButton.component";
import { AuthenticationGuard } from "../services/authentication.guard";

const routes: Routes = [
    {
    path: "profile/:userId", component: ProfileComponent,
    canActivate: [AuthenticationGuard],
      children: [
              { path: "", redirectTo: "profileDetails", pathMatch: "full"  },
              {path: "profileDetails", component: ProfileDetailsComponent},
              {path: "about", component: AboutComponent},
              {path: "followers", component: AllFollowersComponent},
              {path: "followings", component: AllFollowingsComponent},
              ]
              
     }
  ]

@NgModule({   
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    TemplateModule
  ],

  declarations: [
    ProfileComponent,
    CoverComponent,
    MainInfoComponent,
    OtherInfoComponent,
    ProfileDetailsComponent,
    FollowInfoComponent,
    FollowersComponent,
    FollowingsComponent,
    PostsComponent,
    FollowOrUnfollowButtonComponent,
    AboutComponent,
    AllFollowersComponent,
    AllFollowingsComponent
  ],

  providers: [],
  exports: []
})

export class ProfileModule {}
