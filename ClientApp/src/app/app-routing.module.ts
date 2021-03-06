import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './main/home/home.component';
import { ReadComponent } from "./main/read/read.component";
import { SearchComponent } from './main/search/search.component';
import { StatisticsComponent } from './main/statistics/statistics.component';
import { NoteComponent } from './main/note/note.component';
import { AnalysisComponent } from './main/analysis/analysis.component';
import { MindMapComponent } from './main/mindMap/mindMap.component';
import { AuthenticationComponent } from './auth/authentication.component';
import { AuthenticationGuard } from './auth/authentication.guard';

const routes: Routes = [
  {path : "" , component : HomeComponent},
  { path: "login" , component : AuthenticationComponent},
  { path: "read", component: ReadComponent },
  { path: "search", component: SearchComponent },
  { path: "statistics", component: StatisticsComponent },
  { path: "notes", component: NoteComponent },
  { path: "mindMaps", component: MindMapComponent },
  { path: "analysis", component: AnalysisComponent, canActivate: [AuthenticationGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
