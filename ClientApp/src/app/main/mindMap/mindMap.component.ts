import { Component } from '@angular/core';
import { Repository } from "../../models/repository";
import { StateService } from '../../stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "mindMap",
  templateUrl: "mindMap.component.html"
})
export class MindMapComponent {
  
  dataLoaded : boolean = false;
  currentMindMap : number = 0;
  currentMindMapSura: number = 0;
  
  get curSura(): number { return this.currentMindMapSura; }

  set curSura(value: number) {
    this.currentMindMapSura = value;
    this.updatecurrentMindMap(value);

    let state: any  = {"currentMindMapSura": this.currentMindMapSura };
    this.stateService.next(state);    
  }


  constructor(private repo: Repository, private stateService: StateService) { 

    this.stateService.pipe(skipWhile(newState => newState["currentMindMapSura"] == this.curSura))
               .subscribe(newState =>{
                 this.currentMindMapSura = newState["currentMindMapSura"];
                 this.updatecurrentMindMap(this.currentMindMapSura);
                 this.updateSura();
                });
    
    this.repo.suras.subscribe(d => this.dataLoaded = d.length > 1);
  }
 
  updateSura(){
    this.repo.getMindMap(this.currentMindMap).subscribe(data => {
      document.getElementById("mindmap")?.setAttribute("src","data:image/png;base64," + data ) ;

    })
  }

updatecurrentMindMap(value: number): void{
    if(value < 57){
      this.currentMindMap =  value - 1;
    }
    else if(value < 67){
      this.currentMindMap = 57;
    }
    else if(value < 78){
      this.currentMindMap = 58;
    }
    else if(value < 93){
      this.currentMindMap = 59;
    }else{
      this.currentMindMap = 60;
    }
}

  get suras(){
    return this.repo.suras.getValue();
  }
  
  next(){
    if(this.curSura < 115){
          this.curSura++;
    }
  }

  prev(){
    if(this.curSura > 1 ){
          this.curSura--;
    }
  }

}
