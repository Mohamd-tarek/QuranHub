import { NgModule } from "@angular/core";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { CommonModule } from '@angular/common';
import { ReadComponent } from "./read/read.component";
import { QuranComponent } from "./read/quran/quran.component";
import { TafseerAndTransComponent } from "./read/tafseerAndTrans/tafseerAndTrans.component";
import { TafseerComponent } from "./read/tafseerAndTrans/tafseer/tafseer.component";
import { TranslationComponent } from "./read/tafseerAndTrans/translation/translation.component";
import { AyaComponent } from "./read/tafseerAndTrans/aya/aya.component";
import { SearchComponent } from "./search/search.component";
import { StatisticsComponent } from "./statistics/statistics.component";
import { ContainerComponent } from "./statistics/container/container.component";
import { PaginationComponent } from "./statistics/pagination/pagination.component";
import { AnalysisComponent } from "./analysis/analysis.component";
import { StateSevice } from "./stateService.service";



@NgModule({
  imports: [RouterModule, FormsModule, CommonModule, ReactiveFormsModule],
  declarations: [ReadComponent, QuranComponent,TafseerAndTransComponent, TafseerComponent, 
                TranslationComponent,AyaComponent, SearchComponent, StatisticsComponent,
                 ContainerComponent, PaginationComponent, AnalysisComponent],
  providers: [StateSevice],
  exports: [ReadComponent]
})

export class MainModule {}
