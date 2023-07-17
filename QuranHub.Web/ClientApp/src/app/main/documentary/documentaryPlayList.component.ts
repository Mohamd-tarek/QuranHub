import { Component } from '@angular/core';
import { DocumentaryRepository } from 'src/app/abstractions/repositories/documentaryRepository';


@Component({
  selector: "documentaryPlayList",
  templateUrl: "documentaryPlayList.component.html"
})

export class DocumentaryPlayListComponent  {

  constructor(public documentaryRepository: DocumentaryRepository) { }

}
