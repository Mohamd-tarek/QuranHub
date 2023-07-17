import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { AuthenticationGuard } from "../services/authentication.guard";
import { CommonModule } from '@angular/common';
import { ReadComponent } from "./read/read.component";
import { QuranComponent } from "./read/quran/quran.component";
import { TafseerAndTransComponent } from "./read/tafseerAndTrans/tafseerAndTrans.component";
import { SearchComponent } from "./search/search.component";
import { TafseerComponent } from "./tafseer/tafseer.component";
import { StatisticsComponent } from "./statistics/statistics.component";
import { NoteComponent } from "./note/note.component";
import { NoteTextComponent } from "./note/noteText/noteText.component";
import { AnalysisComponent } from "./analysis/analysis.component";
import { SimilarComponent } from "./analysis/similar.component";
import { UniquesComponent } from "./analysis/uniques.component";
import { TopicsComponent } from "./analysis/topics.component";
import { CompareComponent } from "./analysis/compare.component";
import { MindMapComponent } from "./mindMap/mindMap.component";
import { DragulaModule } from 'ng2-dragula'
import { TemplateModule } from "../templateComponents/template.module";
import { DocumentaryComponent } from "./documentary/documentary.component";
import { DocumentaryPlayListsComponent } from "./documentary/documentaryPlayLists.component";
import { DocumentaryPlayListComponent } from "./documentary/documentaryPlayList.component";
import { VideoViewerComponent } from "../templateComponents/videoViewer/videoViwer.component";

const routes: Routes = [
  { path: "read/:ayaIndex", component: ReadComponent },
  { path: "read", component: ReadComponent } ,
  { path: "search", component: SearchComponent },
  { path: "tafseer", component :TafseerComponent},
  { path: "statistics", component: StatisticsComponent },
  { path: "notes", component: NoteComponent, canActivate: [AuthenticationGuard] },
  { path: "mindMaps", component: MindMapComponent },
  { path: "analysis", component: AnalysisComponent,
      children: [
        {path: "", redirectTo: "similar", pathMatch: "full"},
        {path: "similar", component: SimilarComponent},
        {path: "uniques", component: UniquesComponent},
        {path: "topics", component: TopicsComponent},
        {path: "compare", component: CompareComponent}
      ],
    canActivate: [AuthenticationGuard]},
  {
    path: "documentary", component: DocumentaryComponent,
    children: [
      { path: "", redirectTo: "similar", pathMatch: "full" },
      { path: "playLists", component: DocumentaryPlayListsComponent },
      { path: "playList/:name", component: DocumentaryPlayListComponent },
    ],
  },
  { path: "videoViewer/:path", component: VideoViewerComponent },
   ];

    
@NgModule({   
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    DragulaModule,
    TemplateModule
  ],

  declarations: [
    ReadComponent,
    QuranComponent,
    TafseerAndTransComponent,
    SearchComponent,
    TafseerComponent,
    StatisticsComponent,
    AnalysisComponent, 
    SimilarComponent,
    UniquesComponent,
    TopicsComponent,
    CompareComponent,
    MindMapComponent,
    NoteComponent,
    NoteTextComponent,
    DocumentaryComponent,
    DocumentaryPlayListsComponent,
    DocumentaryPlayListComponent
  ],

  providers: [],
  exports: []
})

export class MainModule {}
