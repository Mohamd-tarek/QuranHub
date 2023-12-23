import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from '@angular/common';
import { HomeComponent } from "./home.component";
import { AddPostComponent } from "./add-post/add-post.component";
import { TemplateModule } from "../templateComponents/template.module";
import { PostViewerComponent } from "./post-viewer/post-viewer.component";
import { AuthenticationGuard } from "../services/authentication.guard";
import { TranslateModule } from '@ngx-translate/core';


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
    TranslateModule
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
