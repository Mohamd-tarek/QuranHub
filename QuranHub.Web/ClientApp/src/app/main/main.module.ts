import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { AuthenticationGuard } from "../services/authentication.guard";
import { CommonModule } from '@angular/common';
import { ReadComponent } from "./read/read.component";
import { QuranComponent } from "./read/quran/quran.component";
import { RecitalComponent } from "./recital/recital.component";
import { TafseerAndTransComponent } from "./read/tafseer-and-trans/tafseer-and-trans.component";
import { SearchComponent } from "./search/search.component";
import { TafseerComponent } from "./tafseer/tafseer.component";
import { StatisticsComponent } from "./statistics/statistics.component";
import { NoteComponent } from "./note/note.component";
import { NoteTextComponent } from "./note/note-text/note-text.component";
import { AnalysisComponent } from "./analysis/analysis.component";
import { SimilarComponent } from "./analysis/similar.component";
import { LexicalAnalysisComponent } from "./analysis/lexical-analysis.component";
import { SyntaxAnalysisComponent } from "./analysis/syntax-analysis.component";
import { UniquesComponent } from "./analysis/uniques.component";
import { TopicsComponent } from "./analysis/topics.component";
import { CompareComponent } from "./analysis/compare.component";
import { MindMapComponent } from "./mind-map/mind-map.component";
import { DragulaModule } from 'ng2-dragula'
import { TemplateModule } from "../templateComponents/template.module";
import { DocumentaryComponent } from "./documentary/documentary.component";
import { DocumentaryPlayListsComponent } from "./documentary/documentary-play-lists.component";
import { DocumentaryPlayListComponent } from "./documentary/documentary-play-list.component";
import { DocumentaryVideoViewerComponent } from "./documentary/documentary-video-viewer.component"
import { TranslateModule } from '@ngx-translate/core';


const routes: Routes = [
  { path: "read/:ayaIndex", component: ReadComponent },
  { path: "read", component: ReadComponent } ,
  { path: "recital", component: RecitalComponent } ,
  { path: "search", component: SearchComponent },
  { path: "tafseer", component :TafseerComponent},
  { path: "statistics", component: StatisticsComponent },
  { path: "notes", component: NoteComponent, canActivate: [AuthenticationGuard] },
  { path: "mindMaps", component: MindMapComponent },
  { path: "analysis", component: AnalysisComponent,
      children: [
        {path: "", redirectTo: "similar", pathMatch: "full"},
        {path: "similar", component: SimilarComponent},
        {path: "lexical", component: LexicalAnalysisComponent},
        {path: "syntax", component: SyntaxAnalysisComponent},
        {path: "uniques", component: UniquesComponent},
        {path: "topics", component: TopicsComponent},
        {path: "compare", component: CompareComponent}
      ],
    canActivate: [AuthenticationGuard]},
  {
    path: "documentary", component: DocumentaryComponent,
    children: [
      { path: "", redirectTo: "playLists", pathMatch: "full" },
      { path: "playLists", component: DocumentaryPlayListsComponent },
      { path: "playLists/playList/:name", component: DocumentaryPlayListComponent },
      { path: "playLists/playList/videoViewer/:name", component: DocumentaryVideoViewerComponent },
      
    ],
  },
   ];

    
@NgModule({   
  imports: [
    RouterModule.forChild(routes),
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    DragulaModule,
    TemplateModule,
    TranslateModule
  ],

  declarations: [
    ReadComponent,
    QuranComponent,
    RecitalComponent,
    TafseerAndTransComponent,
    SearchComponent,
    TafseerComponent,
    StatisticsComponent,
    AnalysisComponent, 
    SimilarComponent,
    LexicalAnalysisComponent,
    SyntaxAnalysisComponent,
    UniquesComponent,
    TopicsComponent,
    CompareComponent,
    MindMapComponent,
    NoteComponent,
    NoteTextComponent,
    DocumentaryComponent,
    DocumentaryPlayListsComponent,
    DocumentaryPlayListComponent,
    DocumentaryVideoViewerComponent
  ],

  providers: [],
  exports: []
})

export class MainModule {}
