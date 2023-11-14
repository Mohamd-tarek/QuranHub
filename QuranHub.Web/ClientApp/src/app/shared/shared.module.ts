import { NgModule } from '@angular/core';
import { TemplateModule } from '../templateComponents/template.module';
import { FindComponent } from './find.component';
import { SearchResultsComponent } from './search-results.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AllResultsComponent } from './all-results.component';
import { PostsResultsComponent } from './posts-results.component';
import { PeopleResultsComponent } from './people-results.component';
import { NavbarComponent } from "./navbar.component";
import { AuthenticationInfoComponent } from './authentication-info.component';
import { MiniUserPanelComponent } from './mini-user-panel/mini-user-panel.component';
import { NotificationsContainerComponent } from './notificationComponents/notifications-container/notifications-container.component';
import { NotificationComponent } from './notificationComponents/notification/notification.component';
import { ViewMoreNotificationsComponent } from './notificationComponents/view-more-notifications/view-more-notifications.component';
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
