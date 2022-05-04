import { Component } from '@angular/core';
import { Quran } from './models/quran/quran';
import { Repository } from './models/repository';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'QuranAnalysis';
  constructor(private repo: Repository) { }

  get quran(): Quran[] {
    return this.repo.quran;
  }
}
