import { Component, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: "uploadFile",
  templateUrl: "uploadFile.component.html"
})

export class UploadFileComponent { 

  @Input()
  buttonName: string = "";

  @Input()
  fileName!: string;

  @Output()
  fileSelectedEvent = new EventEmitter<FormData>();

  onFileSelected(event:any) {

      const file:File = event.target.files[0];
      const formData=new FormData();
      formData.append(this.fileName, file);
      this.fileSelectedEvent.emit(formData);
    }

}
