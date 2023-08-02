import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from '@angular/common';
import { HomeComponent } from "./home.component";
import { AddPostComponent } from "./addPostComponent/addPost.component";
import { TemplateModule } from "../templateComponents/template.module";
import { PostViewerComponent } from "./PostViewerComponent/PostViewer.component";
import { AuthenticationGuard } from "../services/authentication.guard";

const routes: Routes = [
  { path: "", component: HomeComponent, canActivate: [AuthenticationGuard] },
  { path: "postViewer/:notificationId", component: PostViewerComponent, canActivate: [AuthenticationGuard] }]


@NgModule({   
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    TemplateModule,
  ],
  declarations: [
    HomeComponent,
    AddPostComponent,
    PostViewerComponent,
  ],

  providers: [],

  exports: []
})

export class HomeModule {}
