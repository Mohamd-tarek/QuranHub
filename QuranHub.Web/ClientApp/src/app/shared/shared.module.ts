import { NgModule } from '@angular/core';
import { TemplateModule } from '../templateComponents/template.module';
import { FindComponent } from './find.component';
import { SearchResultsComponent } from './searchResults.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AllResultsComponent } from './allResults.component';
import { PostsResultsComponent } from './postsResults.component';
import { PeopleResultsComponent } from './peopleResults.component';
import { NavbarComponent } from "./navbar.component";
import { AuthenticationInfoComponent } from './authenticationInfo.component';
import { MiniUserPanelComponent } from './miniUserPanel/miniUserPanel.component';
import { NotificationsContainerComponent } from './notificationComponents/notificationsContainer/notificationsContainer.component';
import { NotificationComponent } from './notificationComponents/notification/notification.component';
import { ViewMoreNotificationsComponent } from './notificationComponents/viewMoreNotifications/viewMoreNotifications.component';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  {
    path: "searchResults", component: SearchResultsComponent,
    children: [
      { path: "all/:q", component: AllResultsComponent },
      { path: "posts/:q", component: PostsResultsComponent },
      { path: "people/:q", component: PeopleResultsComponent },
    ]
  }
]


@NgModule({
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    TemplateModule,
  ],

  declarations: [
    FindComponent,
    SearchResultsComponent,
    AllResultsComponent, 
    PostsResultsComponent,
    PeopleResultsComponent,
    NavbarComponent,
    AuthenticationInfoComponent,
    MiniUserPanelComponent,
    NotificationsContainerComponent,
    NotificationComponent,
    ViewMoreNotificationsComponent
  ],

  providers: [],
  exports: [
   NavbarComponent,
  ]
})
   
export class SharedModule { }
